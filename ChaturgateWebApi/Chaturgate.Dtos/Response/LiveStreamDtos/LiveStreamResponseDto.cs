namespace Chaturgate.Dtos.Response.LiveStreamDtos
{
    public class LiveStreamResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public string Status { get; set; }

        public DateTime StartTime { get; set; }
    }
}
