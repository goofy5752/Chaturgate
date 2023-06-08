using Chaturgate.Data.Models.BaseModels;

namespace Chaturgate.Data.Models
{
    public class LiveStream : BaseModel<int>
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string StreamUrl { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Viewer> Viewers { get; set; }    

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
