using LeaveManagementSystem.Models.LeaveRequests;

namespace LeaveManagementSystem.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestListVM> AdminGetEmployeeLeaveRequests();
        Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests();
        Task CancelLeaveRequest(int leaveRequestId);
        Task ReviewLeaveRequest(int leaveRequestId, bool approved);
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
        Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id);
        Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays);

    }
}