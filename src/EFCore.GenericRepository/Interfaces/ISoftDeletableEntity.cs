namespace EFCore.GenericRepository.Interfaces
{
    public interface ISoftDeletableEntity: IBaseDbEntity
    {
        bool Deleted { get; set; }
    }
}
