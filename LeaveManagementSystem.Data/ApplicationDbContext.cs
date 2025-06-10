using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
        //Edit-Refactor: Los llenados se movieron para la carpeta de Configuration

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Manera para separar tus Data Seedings en diferentes archivos para evitar aumentar de codigo en OnModelCreating()
            //[Crea una carpeta llamada Configuration y dentro de ella agrega las clases que deseas separar sus llenados,
            //heredando cada una con IEntityTypeConfiguration<modelo>]
            //Opcion 1
            //builder.ApplyConfiguration(new ApplicationUserConfiguration());
            //builder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            //builder.ApplyConfiguration(new LeaveRequestStatusConfiguration());

            //Opcion 2
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //Permite que EF tome esta clase(DbSet<ClaseModelo>) como tabla, siempre enlistar los modelos que creamos
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

    }
}
