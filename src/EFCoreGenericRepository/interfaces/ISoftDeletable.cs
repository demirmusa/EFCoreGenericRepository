

namespace EFCoreGenericRepository.interfaces
{
    internal interface ISoftDeletable: IBaseDbEntity
    {
        bool Deleted { get; set; }
    }
}
