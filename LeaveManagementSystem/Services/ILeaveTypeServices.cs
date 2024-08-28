using LeaveManagementSystem.Models.LeaveType;

namespace LeaveManagementSystem.Services
{
    public interface ILeaveTypeServices
    {
        Task<bool> CheckIfLeaveTypeNameExistsAsync(string name);
        Task<bool> CheckIfLeaveTypeNameExistsAsyncForEdit(LeaveTypeEditVM leaveType);
        Task Create(LeaveTypeCreateVM model);
        Task Edit(LeaveTypeEditVM model);
        Task<T?> Find<T>(int id) where T : class;
        Task<List<LeaveTypeReadOnlyVM>> GetAll();
        bool LeaveTypeExists(int id);
        Task Remove(LeaveTypeReadOnlyVM model);
    }
}