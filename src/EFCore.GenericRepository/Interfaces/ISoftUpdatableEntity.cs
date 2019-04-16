


namespace EFCore.GenericRepository.interfaces
{
    public interface ISoftUpdatableEntity : ISoftDeletableEntity
    {
        int? FKPreviousVersionID { get; set; }
    }
}
