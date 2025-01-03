using AutoMapper;
using LeaveManagementSystem.Models.Periods;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services.Periods
{
    public class PeriodsService(ApplicationDbContext _context, IMapper _mapper) : IPeriodsService
    {

        public async Task<List<PeriodsVM>> GetAll()
        {
            var data = await _context.Periods.ToListAsync();
            var viewModel = _mapper.Map<List<PeriodsVM>>(data);

            return viewModel;
        }

        public async Task<T?> Find<T>(int id) where T : class
        {

            var period = await _context.Periods.FirstOrDefaultAsync(x => x.Id == id);

            if (period == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<T>(period);
            return viewModel;

        }

        public async Task Create(PeriodsCreateVM model)
        {
            var data = _mapper.Map<Period>(model);
            _context.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(PeriodsVM model)
        {
            var data = _mapper.Map<Period>(model);
            _context.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(PeriodsVM model)
        {
            var data = await _context.Periods.FindAsync(model.Id);
            if (data != null)
            {
                _context.Periods.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckIfPeriodExistsAsync(string name)
        {
            var periodName = name.ToLower();
            return await _context.Periods.AnyAsync(x => x.Name.ToLower().Equals(periodName));
        }

        public async Task<bool> CheckIfPeriodExistsAsyncForEdit(PeriodsVM viewModel)
        {
            var periodName = viewModel.Name.ToLower();
            return await _context.Periods.AnyAsync(x => x.Name.ToLower().Equals(periodName) && x.Id != viewModel.Id);
        }

        public bool PeriodExists(int? id)
        { return _context.Periods.Any(x => x.Id == id); }

    }
}
