using System.ComponentModel.DataAnnotations;

namespace Chaturgate.Data.Models.BaseModels
{
    public abstract class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
