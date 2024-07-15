using Projects.Application.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Application.Contracts.Identity
{
    public interface IRoleService
    {
        Task<List<AppUserRole>> GetUsersWithRoles();
        Task<string> UpdateUserRoles(UpdateRole updateRole);
    }
}
