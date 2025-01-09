using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //Llenado del admin user
            //EDIT: Se cambio IdentityUser por ApplicationUser
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(new ApplicationUser
            {
                Id = "74f30c00-f192-4a39-8188-33be260d31c2",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "Admin",
                DateOfBirth = new DateOnly(1998, 03, 01)
            });
        }
    }
}
