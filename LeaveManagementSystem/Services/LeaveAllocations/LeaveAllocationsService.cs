
using AutoMapper;
using LeaveManagementSystem.Models.LeaveAllocations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context,
        IHttpContextAccessor _httpContext,
        UserManager<ApplicationUser> _userManager,
        IMapper _mapper) : ILeaveAllocationsService
    {

        public async Task AllocateLeave(string employeeId)
        {


            //get the current period based on the year
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(x => x.EndDate.Year == currentDate.Year);
            int monthsRemaing = period.EndDate.Month - currentDate.Month;


            //get all the leaveTypes
            //var leavetypes = await _context.LeaveTypes.ToListAsync();

            //Get all the LeaveTypes by EmployeeId (Using the EF Core Relationaships) [Best practice]
            //Esta expresion lambda se representa de la siguiente manera
            /*
                SELECT L.* 
                FROM LeaveTypes AS L 
                WHERE NOT EXISTS 
                    (SELECT 1 
                    FROM LeaveAllocations AS LA 
                    WHERE L.Id= LA.LeaveTypeID AND LA.EmployeeId = [id])
            */


            var leaveTypes = await _context.LeaveTypes
                .Where(x => !x.LeaveAllocations.Any(y => y.EmployeeId == employeeId && y.PeriodId == period.Id))
                .ToListAsync();


            //foreach leave type, create an allocaiton entry
            foreach (var leaveType in leaveTypes)
            {
                //Works, but not best practice
                //You should decide it depending the number of types you will request to the db
                //var allocationExists = await AllocationExists(employeeId, period.Id, leaveType.Id);
                //if(allocationExists)
                //{
                //    continue;
                //}

                var actualRate = decimal.Divide(leaveType.NumberOfDays, 12);

                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId, //<-- FK
                    LeaveTypeID = leaveType.Id, //<-- FK 
                    PeriodId = period.Id, //<-- FK
                    Days = (int)Math.Ceiling(actualRate * monthsRemaing)
                };

                _context.Add(leaveAllocation);
            }

            await _context.SaveChangesAsync();

        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
        {
            //You can use this option when you are not sure
            //What data will be change
            //var allocation = await GetEmployeeAllocation(allocationEditVM.Id);
            //if (allocation == null)
            //{
            //    throw new Exception("Leave Allocation record does not exists");
            //}

            //allocation.Days = allocationEditVM.Days;
            // Option 1 _context.Update(allocation);
            // Option 2 _context.Entry(allocation).State = EntityState.Modified;
            // await _context.SaveChangesAsync();


            //Option 3, just require a single call to DB. 
            //Just if you know what will be change
            //You need to be careful on filter the rows with where, just like with the DELETE command
            await _context.LeaveAllocations
                .Where(x => x.Id == allocationEditVM.Id)
                .ExecuteUpdateAsync(x => x.SetProperty(y => y.Days, allocationEditVM.Days));

            await _context.SaveChangesAsync();


        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocation(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await _userManager.GetUserAsync(_httpContext.HttpContext?.User)
                : await _userManager.FindByIdAsync(userId);


            var allocations = await GetAllocations(user.Id);
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);

            var leaveTypesCount = await _context.LeaveTypes.CountAsync();

            var employeeVM = new EmployeeAllocationVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                LeaveAllocations = allocationVmList,
                IsAllocationCompleted = leaveTypesCount == allocations.Count

            };

            return employeeVM;

        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == allocationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);
            return model;

        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }

        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            //Maneras de obtener informacion del usuario (Usuario con la sesion abierta)
            //var username = _httpContext.HttpContext?.User?.Identity?.Name;
            //var user = await _userManager.GetUserAsync(_httpContext.HttpContext?.User);

            var currentDate = DateTime.Now;

            var leaveAllocations = await _context.LeaveAllocations
                .Include(x => x.LeaveType) //<-- Inner Join
                .Include(x => x.Period)
                .Where(x => x.EmployeeId == userId && x.Period.EndDate.Year == currentDate.Year) //Condiciona que busque la info del empleado y que estos registros sean del año actual al periodo
                .ToListAsync();

            //if (leaveAllocations.Count <= 0)
            //{
            //    await AllocateLeave(userId);
            //}

            return leaveAllocations;
        }


        //private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        //{
        //    var exists = await _context.LeaveAllocations.AnyAsync(x => x.EmployeeId == userId && x.LeaveTypeID == leaveTypeId && x.PeriodId == periodId);
        //    return exists;
        //}


    }

}
