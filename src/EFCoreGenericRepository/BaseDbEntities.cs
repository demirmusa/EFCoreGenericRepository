using EFCoreGenericRepository.interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreGenericRepository
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
    public class SoftDeletableDbEntity : BaseDbEntity, ISoftDeletable
    {
        public bool Deleted { get; set; }
    }

    /// <summary>
    /// when entity updated. ERepository soft deletes it and create brand new copy of it with FKPreviousVersionID.
    /// </summary>
    [Serializable]
    public class SoftUpdatableDbEntity : SoftDeletableDbEntity, ISoftUpdatable
    {
        public int? FKPreviousVersionID { get; set; }
    }
}
