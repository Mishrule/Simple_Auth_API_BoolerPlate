using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projects.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Identity.DbContext
{
    public class ProjectIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ProjectIdentityDbContext(DbContextOptions<ProjectIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ProjectIdentityDbContext).Assembly);
        }
    }
}
