using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Othername { get; set; }
        public string? Email { get; set; }
        public string? Security { get; set; }
        public string? Role { get; set; }
        public string? Contact { get; set; }
        public int DepartmentId { get; set; }
        public string? ImageUrl { get; set; }

    }


}
