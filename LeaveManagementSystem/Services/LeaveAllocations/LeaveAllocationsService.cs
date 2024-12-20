
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string employeeId)
        {
            //get all the leaveTypes
            var leavetypes = await _context.LeaveTypes.ToListAsync();

            //get the current period based on the year
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(x => x.EndDate.Year == currentDate.Year);
            int monthsRemaing = period.EndDate.Month - currentDate.Month;

            //foreach leave type, create an allocaiton entry
            foreach (var leaveType in leavetypes)
            {
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId, //<-- FK
                    LeaveTypeID = leaveType.Id, //<-- FK 
                    PeriodId = period.Id, //<-- FK
                    Days = leaveType.NumberOfDays / monthsRemaing
                };

                _context.Add(leaveAllocation);
            }

            await _context.SaveChangesAsync();

        }
    }
}
