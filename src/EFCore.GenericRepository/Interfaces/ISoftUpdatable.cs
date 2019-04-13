


namespace EFCore.GenericRepository.interfaces
{
    internal interface ISoftUpdatable : ISoftDeletable
    {
        int? FKPreviousVersionID { get; set; }
    }
}
