

namespace EFCore.GenericRepository.interfaces
{
    internal interface ISoftDeletableEntity: IBaseDbEntity
    {
        bool Deleted { get; set; }
    }
}
