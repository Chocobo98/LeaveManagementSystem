using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Application.Services.LeaveRequests
{
    public class LeaveRequestsService(
        IMapper _mapper,
        IUserService _userService,
        ApplicationDbContext _context,
        ILeaveAllocationsService _leaveAllocationService) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;

            //Restore allocation days based on the request
            //var currentDate = DateTime.Now;
            //var period = await _context.Periods.SingleAsync(x => x.EndDate.Year == currentDate.Year);
            //var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StarDate.DayNumber;
            //var allocationToRestore = await _context.LeaveAllocations
            //    .FirstAsync(x => x.LeaveTypeID == leaveRequest.LeaveTypeID
            //                && x.EmployeeId == leaveRequest.EmployeeId
            //                && x.PeriodId == period.Id);

            //allocationToRestore.Days += numberOfDays;

            //Refactor by creating the UpdateAllocationDays
            await UpdateAllocationDays(leaveRequest, false);
            await _context.SaveChangesAsync();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            //Mapper vm to model
            var leaveRequest = _mapper.Map<LeaveRequest>(model);

            //Get user info
            var user = await _userService.GetLoggedInUser();
            leaveRequest.EmployeeId = user.Id;

            //Assing status to pending
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;

            //Save leave request (For now)
            _context.Add(leaveRequest);

            //Deduct allocation days based on the request
            //var currentDate = DateTime.Now;
            //var period = await _context.Periods.SingleAsync(x => x.EndDate.Year == currentDate.Year);
            //var numberOfDays = model.EndDate.DayNumber - model.StarDate.DayNumber;
            //var allocationToDeduct = await _context.LeaveAllocations
            //    .FirstAsync(x => x.LeaveTypeID == model.LeaveTypeId
            //                && x.EmployeeId == user.Id
            //                && x.PeriodId == period.Id);

            //allocationToDeduct.Days -= numberOfDays;

            //Refactor by creating the UpdateAllocationDays
            await UpdateAllocationDays(leaveRequest, true);

            //Save data into the databse
            await _context.SaveChangesAsync();

        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests()
        {
            var user = await _userService.GetLoggedInUser();

            var leaveRequest = await _context.LeaveRequests
                .Include(x => x.LeaveType)
                .Where(x => x.EmployeeId == user.Id)
                .ToListAsync();

            var model = leaveRequest.Select(x => new LeaveRequestReadOnlyVM
            {
                StartDate = x.StarDate,
                EndDate = x.EndDate,
                Id = x.Id,
                LeaveType = x.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequestStatusId,
                NumberOfDays = x.EndDate.DayNumber - x.StarDate.DayNumber
            }).ToList();


            return model;

        }

        public async Task<EmployeeLeaveRequestListVM> AdminGetEmployeeLeaveRequests()
        {
            var leaveRequest = await _context.LeaveRequests
                 .Include(x => x.LeaveType)
                 .ToListAsync();

            var leaveRequestsModels = leaveRequest.Select(x => new LeaveRequestReadOnlyVM
            {
                StartDate = x.StarDate,
                EndDate = x.EndDate,
                Id = x.Id,
                LeaveType = x.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequestStatusId,
                NumberOfDays = x.EndDate.DayNumber - x.StarDate.DayNumber
            }).ToList();

            var model = new EmployeeLeaveRequestListVM
            {
                ApprovedRequests = leaveRequest.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved),
                PendingRequests = leaveRequest.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending),
                RejectedRequests = leaveRequest.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Canceled),
                TotalRequests = leaveRequest.Count,
                LeaveRequests = leaveRequestsModels
            };

            return model;
        }

        public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
        {

            var user = await _userService.GetLoggedInUser();
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(x => x.EndDate.Year == currentDate.Year);
            var numberOfDays = model.EndDate.DayNumber - model.StarDate.DayNumber;
            var allocation = await _context.LeaveAllocations
                .FirstAsync(x => x.LeaveTypeID == model.LeaveTypeId
                            && x.EmployeeId == user.Id
                            && x.PeriodId == period.Id);


            return allocation.Days < numberOfDays;
        }

        public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
        {
            var user = await _userService.GetLoggedInUser();
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = approved
                ? (int)LeaveRequestStatusEnum.Approved
                : (int)LeaveRequestStatusEnum.Declined;


            leaveRequest.ReviewerId = user.Id;

            if (!approved)
            {
                //Refactor by creating the UpdateAllocationDays
                await UpdateAllocationDays(leaveRequest, false);

                //var allocationToRestore = await _context.LeaveAllocations
                //.FirstAsync(x => x.LeaveTypeID == leaveRequest.LeaveTypeID
                //            && x.EmployeeId == leaveRequest.EmployeeId);
                //var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StarDate.DayNumber;
                //allocationToRestore.Days += numberOfDays;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id)
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstAsync(x => x.Id == id);

            var user = await _userService.GetUserById(leaveRequests.EmployeeId);

            var model = new ReviewLeaveRequestVM
            {
                Id = leaveRequests.Id,
                StartDate = leaveRequests.StarDate,
                EndDate = leaveRequests.EndDate,
                NumberOfDays = leaveRequests.EndDate.DayNumber - leaveRequests.StarDate.DayNumber,
                LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequests.LeaveRequestStatusId,
                LeaveType = leaveRequests.LeaveType.Name,
                RequestComments = leaveRequests.Comments,
                Employee = new EmployeeListVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                }
            };

            return model;
        }

        public async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
        {
            var allocation = await _leaveAllocationService.GetCurrentAllocation(leaveRequest.LeaveTypeID, leaveRequest.EmployeeId);
            var numberOfDays = CalculateDays(leaveRequest.StarDate, leaveRequest.EndDate);

            if (deductDays)
            {
                allocation.Days -= numberOfDays;
            }
            else
            {
                allocation.Days += numberOfDays;
            }

            _context.Entry(allocation).State = EntityState.Modified;
        }

        private int CalculateDays(DateOnly start, DateOnly end)
        {
            return end.DayNumber - start.DayNumber;
        }
    }
}
