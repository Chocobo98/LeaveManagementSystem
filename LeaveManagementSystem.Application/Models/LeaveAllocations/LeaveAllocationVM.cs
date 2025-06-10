using LeaveManagementSystem.Application.Models.LeaveType;
using LeaveManagementSystem.Application.Models.Periods;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models.LeaveAllocations
{
    public class LeaveAllocationVM
    {

        //Este viewModel tiene el proposito de MAPPEAR los datos desde el DataModel
        //Mientras que en EmployeeAllocationVM, tiene el proposito de mostrar los datos del usuario
        //Y relacionar los datos con este viewModel.
        public int Id { get; set; }

        [Display(Name = "Number of Days")]
        public int Days { get; set; }

        [Display(Name = "Allocation Period")]
        public PeriodsVM Period { get; set; } = new PeriodsVM();

        public LeaveTypeReadOnlyVM LeaveType { get; set; } = new LeaveTypeReadOnlyVM();

    }
}