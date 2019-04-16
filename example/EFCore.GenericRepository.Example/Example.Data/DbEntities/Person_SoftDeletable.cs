using EFCore.GenericRepository;
using Example.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Data.DbEntities
{
    //when you delete this entity it will sing it as a deleted.
    public class Person_SoftDeletable : SoftDeletableDbEntity, IPersonDbEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
