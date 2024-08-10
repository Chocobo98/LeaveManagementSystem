using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models.LeaveType;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services
{
    public class LeaveTypeServices(ApplicationDbContext context, IMapper mapper) : ILeaveTypeServices
    {
        //private readonly ApplicationDbContext context = context;
        //private readonly IMapper mapper = mapper;

        public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
        {
            //Data Model
            var dataModel = await context.LeaveTypes.ToListAsync();
            //AutoMapper
            var viewModelData = mapper.Map<List<LeaveTypeReadOnlyVM>>(dataModel);
            return viewModelData;
        }

        public async Task<T?> Find<T>(int id) where T : class
        {
            var data = await context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return null;
            }

            var viewModel = mapper.Map<T>(data);

            return viewModel;
        }

        public async Task Create(LeaveTypeCreateVM model)
        {
            var data = mapper.Map<LeaveType>(model);
            context.Add(data);
            await context.SaveChangesAsync();
        }

        public async Task Edit(LeaveTypeEditVM model)
        {
            var data = mapper.Map<LeaveType>(model);
            context.Update(data);
            await context.SaveChangesAsync();
        }

        public async Task Remove(LeaveTypeReadOnlyVM model)
        {
            var data = await context.LeaveTypes.FindAsync(model.Id);

            if (data != null)
            {
                context.LeaveTypes.Remove(data);
                await context.SaveChangesAsync();
            }

        }

        public bool LeaveTypeExists(int id)
        {
            return context.LeaveTypes.Any(e => e.Id == id);
        }

        //Metodo asincrono para buscar si el tipo de ausencia existe
        public async Task<bool> CheckIfLeaveTypeNameExistsAsync(string name)
        {
            var lowerCaseName = name.ToLower();
            return await context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(lowerCaseName));
        }

        public async Task<bool> CheckIfLeaveTypeNameExistsAsyncForEdit(LeaveTypeEditVM leaveType)
        {
            var lowerCaseName = leaveType.Name.ToLower();
            return await context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(lowerCaseName) && x.Id != leaveType.Id);
        }

    }
}
