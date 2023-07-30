using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PredmetProjekat.Common.Enums;

namespace PredmetProjekat.Repositories.Context.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole 
                    { 
                        Name = UserRole.Admin.ToString(),
                        NormalizedName = UserRole.Admin.ToString().Normalize()
                    },
                    new IdentityRole
                    {
                        Name = UserRole.Employee.ToString(),
                        NormalizedName = UserRole.Employee.ToString().Normalize()
                    }
                );
        }
    }
}
