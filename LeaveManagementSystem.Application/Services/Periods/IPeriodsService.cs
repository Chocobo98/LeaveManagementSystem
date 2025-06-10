using LeaveManagementSystem.Application.Models.Periods;

namespace LeaveManagementSystem.Application.Services.Periods
{
    public interface IPeriodsService
    {
        Task<bool> CheckIfPeriodExistsAsync(string name);
        Task<bool> CheckIfPeriodExistsAsyncForEdit(PeriodsVM viewModel);

        Task<Period> GetCurrentPeriod();
        Task Create(PeriodsCreateVM model);
        Task Edit(PeriodsVM model);
        Task<T?> Find<T>(int id) where T : class;
        Task<List<PeriodsVM>> GetAll();
        bool PeriodExists(int? id);
        Task Remove(PeriodsVM model);
    }
}
