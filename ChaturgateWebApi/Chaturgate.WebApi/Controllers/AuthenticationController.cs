using Chaturgate.Common;
using Chaturgate.Common.Infrastructure;
using Chaturgate.Data.Models;
using Chaturgate.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chaturgate.WebApi.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationSettings _appSettings;

        public AuthenticationController(
            UserManager<ApplicationUser> userManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        #region Register

        [HttpPost("Register")]
        //POST : /api/Authentication/Register
        public async Task<object> Register(RegisterUserDto model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedOn = DateTime.Now,
                ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg"
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result);

                await _userManager.AddToRoleAsync(applicationUser, GlobalConstants.UserRole);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region Login

        [HttpPost]
        //POST : /api/Authentication/Login
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (user.LockoutEnd >= DateTime.UtcNow)
                {
                    user.AccessFailedCount = 0;
                    return Forbid();
                }

                user.AccessFailedCount = 0;

                //Get role assigned to the user
                var role = await _userManager.GetRolesAsync(user);
                var options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("UserID", user.Id),
                        new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }

            await _userManager.AccessFailedAsync(user); // Register failed access

            return BadRequest(new { message = "Incorrect password." });
        }

        #endregion
    }
}
