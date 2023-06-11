using Chaturgate.Data.Models.BaseModels;

namespace Chaturgate.Data.Models
{
    public class StreamStatus : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
