using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //<-- Identifica el modelo donde Identity va tomar de referencia
    {
        //IdentityDbContext - Tiene ya definido y creado las tablas y roles
        //Al hacer override al IdentityDBContext, sera relativo con las tabla que tomara en cuenta a la nueva tabla
        //Solo lo permite aquellos modelos que tiene hereado el Identity User
        //DBContext - Crear los modelos, tablas y demas desde Scratch sin algun helper. Representa esta clase como la Base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Data Seeding
        //Nos permite llenar con valores las tablas involucradas cuando el modelo se crea (Solo cambia el tipo de dato dentro del buider.Entity y especifica las columnas a llenar)
        //NOTE: Para los Id, utiliza formato GUID para mayro seguridad. Para contraseñas, usar PasswordHasher
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Llenado de Roles
            builder.Entity<IdentityRole>().HasData(
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

            //Llenado del admin user
            //EDIT: Se cambio IdentityUser por ApplicationUser
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
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

            //Relacion del usuario Admin con el Rol Admin
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4ac2cb24-f0cb-424d-8c97-76337e0d9404",
                    UserId = "74f30c00-f192-4a39-8188-33be260d31c2"
                });
        }

        //Permite que EF tome esta clase(DbSet<ClaseModelo>) como tabla, siempre enlistar los modelos que creamos
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<Period> Periods { get; set; }
    }
}
