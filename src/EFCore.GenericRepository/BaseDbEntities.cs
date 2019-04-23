using EFCore.GenericRepository.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFCore.GenericRepository
{
    public class BaseDbEntity : IBaseDbEntity
    {
        [Key]
        public int ID { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
    }

    /// <summary>
    /// when entity deleted. ERepository sign it as a deleted and update.
    /// </summary>
    public class SoftDeletableDbEntity : BaseDbEntity, ISoftDeletableEntity
    {
        public bool Deleted { get; set; }
    }

    /// <summary>
    /// when entity updated. ERepository soft deletes it and create brand new copy of it with FKPreviousVersionID.
    /// </summary>
    public class SoftUpdatableDbEntity : SoftDeletableDbEntity, ISoftUpdatableEntity
    {
        public int? FKPreviousVersionID { get; set; }
    }
}
