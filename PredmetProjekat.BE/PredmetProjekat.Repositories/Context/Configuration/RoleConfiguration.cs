using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PredmetProjekat.Common.Constants;

namespace PredmetProjekat.Repositories.Context.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole
                    {
                        Name = Constants.AdminRole,
                        NormalizedName = Constants.AdminRole.Normalize()
                    },
                    new IdentityRole
                    {
                        Name = Constants.EmployeeRole,
                        NormalizedName = Constants.EmployeeRole.Normalize()
                    }
                );
        }
    }
}
