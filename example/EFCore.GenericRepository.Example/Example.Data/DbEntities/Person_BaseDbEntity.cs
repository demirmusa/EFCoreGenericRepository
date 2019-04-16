using EFCore.GenericRepository;
using Example.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Example.Data.DbEntities
{
    // nothing different happend when you delete or update this
    public class Person_BaseDbEntity : BaseDbEntity, IPersonDbEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
