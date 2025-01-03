
using LeaveManagementSystem.Models.LeaveAllocations;

namespace LeaveManagementSystem.Services.LeaveAllocations
{
    public interface ILeaveAllocationsService
    {
        Task AllocateLeave(string employeeId);
        Task<EmployeeAllocationVM> GetEmployeeAllocation(string? userid);

        Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id);
        Task EditAllocation(LeaveAllocationEditVM allocationEditVM);
        Task<List<EmployeeListVM>> GetEmployees();
    }
}
