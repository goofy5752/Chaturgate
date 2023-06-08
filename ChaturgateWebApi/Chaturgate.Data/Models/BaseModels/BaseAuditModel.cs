using System.ComponentModel.DataAnnotations;

namespace Chaturgate.Data.Models.BaseModels
{
    public abstract class BaseAuditModel<TKey> : BaseModel<TKey>, IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
