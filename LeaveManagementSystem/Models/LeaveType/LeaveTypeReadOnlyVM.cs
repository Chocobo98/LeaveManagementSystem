using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveType
{
    public class LeaveTypeReadOnlyVM : BaseLeaveTypeVM, ILeaveType
    {
        //Heredando ID desde BaseLeaveTypeVM

        [Display(Name = "Tipo de abstencion")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Numero de Dias")]
        public int NumberOfDays { get; set; }
    }
}
