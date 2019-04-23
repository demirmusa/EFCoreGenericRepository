
namespace EFCore.GenericRepository.Interfaces
{
    public interface ISoftUpdatableEntity : ISoftDeletableEntity
    {
        int? FKPreviousVersionID { get; set; }
    }
}
