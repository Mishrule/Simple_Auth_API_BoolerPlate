using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Contracts.Identity;
using Projects.Application.Model.Identity;
using Projects.Application.Reponses;
using System.Net;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse<RegistrationResponse>>> Register(RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("getwithuserrole")]
        [ProducesResponseType(typeof(IEnumerable<AppUser>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<AppUser>> GetUsers(string role)
        {
            return await _userService.GetUsers(role);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("getalluser")]
        [ProducesResponseType(typeof(IEnumerable<AppUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResponseList<AppUser>>> GetAllUsers()
        {
            var responses = new BaseResponseList<AppUser>();
            var data = new List<AppUser>();
            var users = await _userService.GetAllUsers();
            if (users.Any())
            {
                foreach (var user in users)
                {
                    data.Add(new AppUser()
                    {
                        ImageUrl = user.ImageUrl,
                        Firstname = user.Firstname,
                        Othername = user.Othername,
                        Lastname = user.Lastname,
                        Username = user.Username,
                        Security = user.Security
                    });
                }
                responses.Results = data;
                responses.IsSuccess = true;
            }
            else
            {
                responses.IsSuccess = false;
                responses.Message = "No user found";
                responses.Results = null;
            }



            return Ok(responses);

        }
        [AllowAnonymous]
        [HttpPost("forgotpassword")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<string> ForgotPassword(ForgotPasswordViewModel model)
        {
            return await _userService.ForgotPassword(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("resetpassword")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<string> ResetPassword(ResetPasswordViewModel model)
        {
            return await _userService.ResetPassword(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("lock/{userId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<string> LockUserAccount(LockAccount lockAccount)
        {
            return await _userService.LockUserAccount(lockAccount);
        }
    }
}

