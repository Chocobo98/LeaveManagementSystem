using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            //Llenado de Roles
            builder.HasData(
                new IdentityRole
                {
                    Id = "5d2f419b-1e2e-425c-b0c0-eeedbed738f5",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "084d4bda-bbec-4140-ae2c-4e59f6ff0b44",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole
                {
                    Id = "4ac2cb24-f0cb-424d-8c97-76337e0d9404",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );

        }
    }
}
