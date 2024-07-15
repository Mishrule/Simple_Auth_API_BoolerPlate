using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projects.Application.Contracts.Identity;
using Projects.Application.Model.Identity;
using Projects.Identity.Models;
using System.Security.Claims;

namespace Projects.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<AppUser>> GetUsers(string role)
        {
            //var users = await _userManager.GetUsersInRoleAsync("User");
            var users = await _userManager.GetUsersInRoleAsync(role);
            return users.Select(user => new AppUser
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Username = user.UserName,
                Contact = user.Contact,
                ImageUrl = user.ImageUrl,
                DepartmentId = user.DepartmentId,
                Security = user.Security,
                Othername = user.Othername,
                Role = user.Role

            }).ToList();
        }

        public async Task<bool> GetUserWithUsername(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null ? true : false;
            //return new AppUser()
            //{
            //  Id = user.Id,
            //  Email = user.Email,
            //  Firstname = user.FirstName,
            //  Lastname = user.LastName,
            //  Username = user.UserName,
            //  Contact = user.Contact,
            //  ImageUrl = user.ImageUrl,
            //  DepartmentId = user.DepartmentId,
            //  Security = user.Security,
            //  Othername = user.Othername,
            //  CategoryId = (Application.Responses.Category)user.CategoryId,
            //  Role = user.Role
            //};
        }

        //public Task<bool> UpdateUser(Application.Model.ApplicationUser user)
        //{
        //  throw new NotImplementedException();
        //}

        //public async Task<bool> UpdateUser(ApplicationUser user)
        //{
        //  ApplicationUser ap = new ApplicationUser()
        //  {
        //    UserName = user.UserName,
        //    FirstName = user.FirstName,
        //    LastName = user.LastName,
        //    CategoryId = (Models.Category) user.CategoryId,
        //    //Role = user.Role,
        //    Contact = user.Contact,
        //    ImageUrl = user.ImageUrl,
        //    Email = user.Email,
        //    Id = user.Id,
        //    Othername = user.Othername,
        //    PhoneNumber = user.Contact,
        //    //Security = user.Security,

        //  };

        //  var identityUser = await _userManager.UpdateAsync(ap);
        //    return identityUser.Succeeded;
        //}

        //public async Task<bool> UpdateUser(ApplicationUser user)
        //{
        //  var identityUser = await _userManager.UpdateAsync(user);
        //  return identityUser.Succeeded;
        //}


        public async Task<List<AppUser>> GetAllUsers()
        {
            var data = new List<AppUser>();
            var users = await _userManager.Users.ToListAsync(); // Assuming _userManager is of type UserManager<AppUser>
            foreach (var user in users)
            {
                data.Add(
                    new AppUser()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Firstname = user.FirstName,
                        Lastname = user.LastName,
                        Username = user.UserName,
                        Contact = user.Contact,
                        ImageUrl = user.ImageUrl,
                        DepartmentId = user.DepartmentId,
                        Security = user.Security,
                        Othername = user.Othername,
                        Role = user.Role
                    });
            }
            return data;
        }

        public async Task<AppUser> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return new AppUser()
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Username = user.UserName,
                Contact = user.Contact,
                ImageUrl = user.ImageUrl,
                DepartmentId = user.DepartmentId,
                Security = user.Security,
                Othername = user.Othername,
                Role = user.Role
            };
        }

        public async Task<string> ForgotPassword(ForgotPasswordViewModel model)
        {
            var resultErrors = string.Empty;

            if (model != null)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return "Invalid User";
                }
                else if (user.Email != model.Email)
                {
                    return "Invalid Email";
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);

                if (result.Succeeded)
                {
                    // Password reset successful
                    return "Password Reset Succesfully";
                }

                foreach (var r in result.Errors)
                {
                    resultErrors += r.Description + " ";
                }

            }

            return resultErrors;
        }

        public async Task<string> ResetPassword(ResetPasswordViewModel model)
        {

            if (model != null)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return "Invalid User";
                }
                model.Code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return "Password Reset Successfully";
                }

            }

            return "Password Reset Failed";
        }

        public async Task<string> LockUserAccount(LockAccount lockAccount)
        {
            var user = await _userManager.FindByIdAsync(lockAccount.UserId);

            if (user == null)
            {
                return $"User with ID {lockAccount.UserId} not found.";
            }

            // Set the lock status property to true
            user.LockoutEnabled = lockAccount.Lock;

            // You can set additional properties like lockout end date if needed
            // user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(lockoutDuration);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return $"User with ID {lockAccount.UserId} locked successfully.";
            }
            else
            {
                return $"Failed to lock user with ID {lockAccount.UserId}. Error: {string.Join(", ", result.Errors)}";
            }
        }


        public string UserId
        {
            get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }
    }
}
