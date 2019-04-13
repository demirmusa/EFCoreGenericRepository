

namespace EFCore.GenericRepository.interfaces
{
    internal interface ISoftDeletable: IBaseDbEntity
    {
        bool Deleted { get; set; }
    }
}
