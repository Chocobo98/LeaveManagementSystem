using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //Relacion del usuario Admin con el Rol Admin
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4ac2cb24-f0cb-424d-8c97-76337e0d9404",
                    UserId = "74f30c00-f192-4a39-8188-33be260d31c2"
                });

        }
    }
}
