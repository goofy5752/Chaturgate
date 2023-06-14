using Chaturgate.Data.Models;
using Chaturgate.Data.Repositories.Interfaces;
using Chaturgate.Dtos.Request.LiveStreamDtos;
using Chaturgate.Dtos.Response.LiveStreamDtos;
using Chaturgate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chaturgate.Services
{
    public class LiveStreamService : ILiveStreamService
    {
        private readonly IRepository<LiveStream> liveStreamRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IRepository<StreamStatus> streamStatusRepository;

        public LiveStreamService(
            IRepository<LiveStream> liveStreamRepository,
            ICurrentUserService currentUserService,
            IRepository<StreamStatus> streamStatusRepository)
        {
            this.liveStreamRepository = liveStreamRepository;
            this.currentUserService = currentUserService;
            this.streamStatusRepository = streamStatusRepository;
        }

        public async Task<string> CreateLiveStreamAsync(CreateLiveStreamRequestDto liveStreamDto)
        {
            var currentUserId = this.currentUserService.GetCurrentUserId();

            var liveStreamKey = Guid.NewGuid().ToString();

            var liveStatus = await streamStatusRepository
                .All()
                .SingleOrDefaultAsync(ss => ss.Name == "Live");

            var liveStream = new LiveStream
            {
                Title = liveStreamDto.Title,
                Description = liveStreamDto.Description,
                Thumbnail = liveStreamDto.Thumbnail,
                Status = liveStatus,
                StartTime = DateTime.Now,
                UserId = currentUserId,
                StreamKey = liveStreamKey,
        };

            await liveStreamRepository.AddAsync(liveStream);
            await liveStreamRepository.SaveChangesAsync();

            return liveStreamKey;
        }

        public async Task<StreamStatusResponseDto> GetStreamStatus(string streamKey)
        {
            // Find the live stream with the provided stream key
            var liveStream = await liveStreamRepository
                .All()
                .SingleOrDefaultAsync(x => x.StreamKey == streamKey);

            var liveStatus = await streamStatusRepository
                .All()
                .SingleOrDefaultAsync(ss => ss.Id == liveStream.Status.Id);

            if (liveStream == null)
            {
                throw new Exception("Stream not found.");
            }

            // Return the status of the live stream
            return new StreamStatusResponseDto
            {
                Title = liveStream.Title,
                Description = liveStream.Description,
                Thumbnail = liveStream.Thumbnail,
                Status = liveStatus.Name,
                StartTime = liveStream.StartTime,
                UserId = liveStream.UserId,
                StreamKey = liveStream.StreamKey,
            };
        }
    }
}
