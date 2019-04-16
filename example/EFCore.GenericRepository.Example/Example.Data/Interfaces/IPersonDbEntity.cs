using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data.Interfaces
{
    public interface IPersonDbEntity
    {
        string Name { get; set; }
        string Surname { get; set; }
    }
}
