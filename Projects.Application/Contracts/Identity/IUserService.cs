using Projects.Application.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<AppUser>> GetUsers(string role);
        Task<AppUser> GetUser(string userId);
        Task<bool> GetUserWithUsername(string userName);
        Task<List<AppUser>> GetAllUsers();
        Task<string> ForgotPassword(ForgotPasswordViewModel model);
        Task<string> ResetPassword(ResetPasswordViewModel model);
        Task<string> LockUserAccount(LockAccount lockAccount);
        public string UserId { get; }
    }
}
