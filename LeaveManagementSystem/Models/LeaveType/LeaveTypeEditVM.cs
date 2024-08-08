using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveType
{
    public class LeaveTypeEditVM : BaseLeaveTypeVM, ILeaveType
    {
        //Heredando ID desde BaseLeaveTypeVM

        [Required]
        [Length(3, 150, ErrorMessage = "You have violated the lenght requirements.")]
        [Display(Name = "Tipo de abstencion")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 90)]
        [Display(Name = "Numero de Dias")]
        public int NumberOfDays { get; set; }
    }
}
