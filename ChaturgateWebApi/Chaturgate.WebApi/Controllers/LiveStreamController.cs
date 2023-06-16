using Chaturgate.Dtos.Request.LiveStreamDtos;
using Chaturgate.Services;
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

        [HttpGet("create")]
        public async Task<IActionResult> CreateLiveStreamAsync([FromQuery] CreateLiveStreamRequestDto startStreamDto)
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

        [HttpPut("end/{streamKey}")]
        public async Task<IActionResult> EndLiveStreamAsync(string streamKey)
        {
            try
            {
                await liveStreamService.EndLiveStreamAsync(streamKey);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
