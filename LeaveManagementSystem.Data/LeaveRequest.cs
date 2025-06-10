namespace LeaveManagementSystem.Data
{
    public class LeaveRequest : BaseEntity
    {
        public DateOnly StarDate { get; set; }

        public DateOnly EndDate { get; set; }

        //FK_Leavetype
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeID { get; set; }

        //FK_LeaveRequest
        public LeaveRequestStatus? LeaveRequestStatus { get; set; }
        public int LeaveRequestStatusId { get; set; }

        //FK_Employee
        public ApplicationUser? Employee { get; set; }
        public string EmployeeId { get; set; } = default!;
        public ApplicationUser? Reviewer { get; set; }
        public string? ReviewerId { get; set; }

        public string? Comments { get; set; }


    }
}