namespace Chaturgate.Dtos.Response.LiveStreamDtos
{
    public class StreamStatusResponseDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public string Status { get; set; }

        public DateTime StartTime { get; set; }

        public string UserId { get; set; }

        public string StreamKey { get; set; }
    }
}
