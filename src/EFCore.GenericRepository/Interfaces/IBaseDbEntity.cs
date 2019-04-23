using System;

namespace EFCore.GenericRepository.Interfaces
{
    public interface IBaseDbEntity
    {
        int ID { get; set; }
        DateTime CreationTime { get; set; }
        DateTime? LastUpdateTime { get; set; }
    }
}
