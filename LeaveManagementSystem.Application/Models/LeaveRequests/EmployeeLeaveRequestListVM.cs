using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models.LeaveRequests
{
    public class EmployeeLeaveRequestListVM
    {
        [Display(Name = "Total Number of Requests")]
        public int TotalRequests { get; set; }

        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }

        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }

        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }

        public List<LeaveRequestReadOnlyVM> LeaveRequests { get; set; } = new List<LeaveRequestReadOnlyVM>();
    }
}