using Chaturgate.Dtos.Request.LiveStreamDtos;
using Chaturgate.Dtos.Response.LiveStreamDtos;

namespace Chaturgate.Services.Interfaces
{
    public interface ILiveStreamService
    {
        Task<string> CreateLiveStreamAsync(CreateLiveStreamRequestDto liveStreamDto);

        Task<StreamStatusResponseDto> GetStreamStatus(string streamKey);
    }
}
