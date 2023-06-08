namespace Chaturgate.Data.Models.BaseModels
{
    public abstract class BaseDeletableModel<TKey> : BaseAuditModel<TKey>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
