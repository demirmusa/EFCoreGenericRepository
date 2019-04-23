using EFCore.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericRepositoryUnitTests.Data.DbEntities
{
    public class Person_SoftUpdatableDbEntity : SoftUpdatableDbEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
