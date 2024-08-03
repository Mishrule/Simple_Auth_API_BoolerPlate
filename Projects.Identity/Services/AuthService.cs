using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Projects.Application.Contracts.Identity;
using Projects.Application.Model.Identity;
using Projects.Application.Reponses;
using Projects.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projects.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var response = new AuthResponse();
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = $"Invalid Username.";
                //throw new NotFoundException("User with {request.Email} not found.", request.Username);
                return response;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded == false)
            {
                response.IsSuccess = false;
                response.Message = $"Invalid Password.";
                //throw new BadRequestException($"Credentials for '{request.Username} aren't valid'.");
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName,
                Email = user.Email,
                IsSuccess = true

            };
            return response;
        }

        public async Task<BaseResponse<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var response = new BaseResponse<RegistrationResponse>();
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
                ImageUrl = "",
                LockoutEnabled = false,
                PhoneNumber = request.PhoneNumber,
                Security = request.Password

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                
                response.IsSuccess = true;
                response.Message = "User Created Successfully";
                // response.Result = new RegistrationResponse() {UserId = user.Id};
                //return response;
            }
            else
            {
                var res = result.Errors.FirstOrDefault().ToString();
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                    response.Errors = new List<string>(); 
                    response.Errors.Add(err.Description);
                }
                response.IsSuccess = false;
                response.Message = str.ToString();

                //throw new BadRequestException($"{str}");
            }

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials

            );

            return jwtSecurityToken;
        }






    }
}
