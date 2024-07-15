using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projects.Application.Contracts.Identity;
using Projects.Application.Model.Identity;
using Projects.Identity.Models;

namespace Projects.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<AppUserRole>> GetUsersWithRoles()
        {
            var data = new List<AppUserRole>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                data.Add(new AppUserRole()
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Roles = roles.ToList()
                });
            }

            return data;
        }

        public async Task<string> UpdateUserRoles(UpdateRole updateRole)
        {
           

            var user = await _userManager.FindByIdAsync(updateRole.UserId);
            if (user == null)
            {
                return "User not found";
            }

            var role = await _roleManager.FindByIdAsync(updateRole.RoleId);
            if (role == null)
            {
                return "Role not found";
            }

            var existingRoles = await _userManager.GetRolesAsync(user);

            // If the user already has the desired role, no need to update
            if (existingRoles.Contains(role.Name))
            {
                return "User already has the specified role";
            }

            // Remove existing roles
            await _userManager.RemoveFromRolesAsync(user, existingRoles.ToArray());

            // Add the new role by ID
            await _userManager.AddToRoleAsync(user, role.Name);

            return "User role updated successfully";


        }



    }
}
