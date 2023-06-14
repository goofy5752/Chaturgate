using Chaturgate.Dtos.Request.LiveStreamDtos;
using Chaturgate.Dtos.Response.LiveStreamDtos;
using Chaturgate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chaturgate.WebApi.Controllers
{
    public class LiveStreamController : ApiController
    {
        private readonly ILiveStreamService liveStreamService;

        public LiveStreamController(ILiveStreamService liveStreamService)
        {
            this.liveStreamService = liveStreamService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLiveStreamAsync(CreateLiveStreamRequestDto startStreamDto)
        {
            var streamKey = await liveStreamService.CreateLiveStreamAsync(startStreamDto);
            return Ok(streamKey);
        }

        [HttpGet("{streamKey}")]
        public async Task<IActionResult> GetStreamStatus(string streamKey)
        {
            var streamStatus = await liveStreamService.GetStreamStatus(streamKey);
            return Ok(streamStatus);
        }
    }
}
