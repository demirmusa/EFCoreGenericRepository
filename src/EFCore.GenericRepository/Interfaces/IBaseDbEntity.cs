using System;

namespace EFCore.GenericRepository.interfaces
{
    internal interface IBaseDbEntity
    {
        int ID { get; set; }
        DateTime CreationTime { get; set; }
        DateTime? LastUpdateTime { get; set; }
    }
}
