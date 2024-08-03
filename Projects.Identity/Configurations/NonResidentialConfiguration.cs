using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projects.Identity.Models;

namespace Projects.Identity.Configurations
{
    public class NonResidentialConfiguration : IEntityTypeConfiguration<NonResidentialBooking>
    {
        public void Configure(EntityTypeBuilder<NonResidentialBooking> builder)
        {
            builder.ToTable(nameof(NonResidentialBooking));
           
        }
    }
}
