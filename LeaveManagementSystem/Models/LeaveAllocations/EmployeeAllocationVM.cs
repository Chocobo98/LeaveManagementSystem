using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveAllocations
{
    public class EmployeeAllocationVM : EmployeeListVM
    {

        [Display(Name = "Date Joined")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]

        public DateOnly DateOfBirth { get; set; }

        public bool IsAllocationCompleted { get; set; }


        //SOLAMENTE LOS VIEWMODELS DEBEN RELACIONARSE CON VIEWMODELS
        //NUNCA CON DATA MODELS POR CUESTIONES DE SEGURIDAD
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
