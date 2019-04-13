


namespace EFCore.GenericRepository.interfaces
{
    internal interface ISoftUpdatableEntity : ISoftDeletableEntity
    {
        int? FKPreviousVersionID { get; set; }
    }
}
