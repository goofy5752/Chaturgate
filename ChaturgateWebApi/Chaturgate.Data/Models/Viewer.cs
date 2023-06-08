using Chaturgate.Data.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace Chaturgate.Data.Models
{
    public class Viewer : BaseModel<int>
    {
        public string UserId { get; set; }

        public int StreamId { get; set; }

        public DateTime JoinTime { get; set; }

        public DateTime LeaveTime { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual LiveStream Stream { get; set; }
    }
}
