using Chaturgate.Data.Models.BaseModels;

namespace Chaturgate.Data.Models
{
    public class ChatMessage : BaseModel<int>
    {
        public int StreamId { get; set; }

        public string UserId { get; set; }

        public string MessageText { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual LiveStream Stream { get; set; }
    }
}
