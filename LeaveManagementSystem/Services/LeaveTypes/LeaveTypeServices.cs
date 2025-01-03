using AutoMapper;
using LeaveManagementSystem.Models.LeaveType;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services.LeaveTypes
{
    public class LeaveTypeServices(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypeServices
    {
        //private readonly ApplicationDbContext context = context;
        //private readonly IMapper mapper = mapper;

        public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
        {
            //Data Model
            var dataModel = await _context.LeaveTypes.ToListAsync();
            //AutoMapper
            var viewModelData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(dataModel);
            return viewModelData;
        }

        public async Task<T?> Find<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<T>(data);

            return viewModel;
        }

        public async Task Create(LeaveTypeCreateVM model)
        {
            var data = _mapper.Map<LeaveType>(model);
            _context.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(LeaveTypeEditVM model)
        {
            var data = _mapper.Map<LeaveType>(model);
            _context.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(LeaveTypeReadOnlyVM model)
        {
            var data = await _context.LeaveTypes.FindAsync(model.Id);

            if (data != null)
            {
                _context.LeaveTypes.Remove(data);
                await _context.SaveChangesAsync();
            }

        }

        public bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }

        //Metodo asincrono para buscar si el tipo de ausencia existe
        public async Task<bool> CheckIfLeaveTypeNameExistsAsync(string name)
        {
            var lowerCaseName = name.ToLower();
            return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(lowerCaseName));
        }

        public async Task<bool> CheckIfLeaveTypeNameExistsAsyncForEdit(LeaveTypeEditVM leaveType)
        {
            var lowerCaseName = leaveType.Name.ToLower();
            return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(lowerCaseName) && x.Id != leaveType.Id);
        }

        public async Task<bool> DaysExceedMaximum(int leaveTypeId, int days)
        {
            var leaveModel = await _context.LeaveTypes.FindAsync(leaveTypeId);
            return leaveModel.NumberOfDays < days;
        }

    }
}
