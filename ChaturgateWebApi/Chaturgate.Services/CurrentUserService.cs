using Chaturgate.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Chaturgate.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
        }

        public string GetCurrentUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserEmail");
        }

        public string GetCurrentUserUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserName");
        }
    }
}
