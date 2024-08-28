using Microsoft.AspNetCore.Identity;

namespace LeaveManagementSystem.Data
{
    //ApplicationUser indica que la tabla relacionada por defecto de usuarios, tendra tambien estas propiedades adicionales de este modelo
    //EDIT: La aplicacion usara este modelo como referenrecia para enfocarse solamente en esas columnas que usara en la tabla de usuarios.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
    }
}
