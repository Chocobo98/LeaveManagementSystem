﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //IdentityDbContext - Tiene ya definido y creado las tablas y roles
        //DBContext - Crear los modelos, tablas y demas desde Scratch sin algun helper. Representa esta clase como la Base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Permite que EF tome esta clase(DbSet<ClaseModelo>) como tabla, siempre enlistar los modelos que creamos
        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
