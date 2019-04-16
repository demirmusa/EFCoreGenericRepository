using EFCore.GenericRepository;
using Example.Data.Interfaces;

namespace Example.Data.DbEntities
{
    //when you delete this entity it will sing it as a deleted.
    //when you update this entity it will create one copy of it which previous id refer this updated one
    // You can also use ISoftUpdatableDbEntity interface and fill needed properties
    public class Person_SoftUpdatable : SoftUpdatableDbEntity, IPersonDbEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
