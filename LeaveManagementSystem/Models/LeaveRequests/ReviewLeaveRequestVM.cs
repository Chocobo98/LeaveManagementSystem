using LeaveManagementSystem.Models.LeaveAllocations;

namespace LeaveManagementSystem.Models.LeaveRequests
{
    public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
    {
        public EmployeeListVM Employee = new EmployeeListVM();
        public string? RequestComments { get; set; }

    }
}