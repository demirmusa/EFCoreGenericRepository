


namespace EFCoreGenericRepository.interfaces
{
    internal interface ISoftUpdatable : ISoftDeletable
    {
        int? FKPreviousVersionID { get; set; }
    }
}
