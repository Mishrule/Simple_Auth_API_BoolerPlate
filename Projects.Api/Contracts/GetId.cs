using Microsoft.AspNetCore.Identity;
using Projects.Application.Constants;
using Projects.Identity.Models;

namespace Projects.Api.Contracts
{
    public class GetId
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public GetId(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUser()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaims.uid)?.Value;
            var user = await _userManager.FindByIdAsync(username);
            return user;
        }

        public async Task<string> GetUsername()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaims.uid)?.Value;
            var user = await _userManager.FindByIdAsync(username);
            return user.UserName;
        }
    }
}
