using Chaturgate.Dtos.Request.LiveStreamDtos;
using Chaturgate.Dtos.Response.LiveStreamDtos;

namespace Chaturgate.Services.Interfaces
{
    public interface ILiveStreamService
    {
        Task<string> CreateLiveStreamAsync(CreateLiveStreamRequestDto liveStreamDto);

        Task EndLiveStreamAsync(string streamKey);

        Task<StreamStatusResponseDto> GetStreamStatus(string streamKey);
    }
}
