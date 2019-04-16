

namespace EFCore.GenericRepository.interfaces
{
    public interface ISoftDeletableEntity: IBaseDbEntity
    {
        bool Deleted { get; set; }
    }
}
