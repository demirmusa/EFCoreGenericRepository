# EFCoreGenericRepository
Generic entity framework sql server repository which manages your entities. It allows you sign your entities as a ISoftDeletable , ISoftUpdatable and manages them automatically

## Usage ##
Create entity and context
```csharp
public class UserPhone :BaseDbEntity
{
    public string PhoneNumber{get;set;}
}

public class Person : SoftDeletableDbEntity //when you delete entity repo will sign it as a deleted. But do not delete 
{
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class User : SoftUpdatableDbEntity //when you update entity repo will create current copy of it for auditing (with FKPreviousVersionID)
{
    public string UserName { get; set; }
}
...

public class ExampleDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPhone> UserPhones { get; set; }
    ...
}
````
#### Implementation Option 1 ####

Add generic repository to your dependency injection service with one of these extensions
```csharp
...
services.AddGenericRepositoryScoped();
services.AddGenericRepositoryTransient();
services.AddGenericRepositorySingleton();
...
```

Then just call generic repo 

```csharp 
...
IGenericRepository<ExampleDbContext, Person> _personRepo;
public PeopleService(IGenericRepository<ExampleDbContext, Person> personRepo)
{
    _personRepo = personRepo;
}


public async Task<Person> AddPerson(Person person)
{         
    var newPerson = await _personRepo.InsertAsync(person);
    ...
    return newPerson;        
}
public Person UpdatePerson(Person person)
{         
    var updated =  _personRepo.Update(person);
    ...
    return updated;        
}
...
        
```
#### Implementation Option 2 ####
 Create generic repos by context
 
 ```csharp
public interface IExampleDbContextGenericRepository<TEntity> : IGenericRepository<ExampleDbContext, TEntity>
    where TEntity : IBaseDbEntity
{

}
public class ExampleDbContextGenericRepository<TEntity> : GenericRepository<ExampleDbContext, TEntity>, IExampleDbContextGenericRepository<TEntity>
    where TEntity : IBaseDbEntity
{
    public ExampleDbContextGenericRepository(ExampleDbContext context) : base(context)
    {
    }
}
 ```
 
Inject your custom generic repos to dependency injection service collection

```csharp
...
services.AddScoped(typeof(IExampleDbContextGenericRepository<>), typeof(ExampleDbContextGenericRepository<>));  
...
```

Then just call your custom generic repo 
```csharp    
...
IExampleDbContextGenericRepository<Person> _personRepo;
public PeopleService(IExampleDbContextGenericRepository<Person> personRepo)
{
    _personRepo = personRepo;
}


public async Task<Person> AddPerson(Person person)
{         
    return await _personRepo.InsertAsync(person);       
}
public async Task<Person> UpdatePerson(Person person)
{         
    ...
    return await _personRepo.UpdateAsync(person);          
}
...
        
```



