using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem.Controllers
{
    public class LeaveRequestsController(ILeaveTypeServices _leaveTypeServices, ILeaveRequestsService _leaveRequestsService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestsService.GetEmployeeLeaveRequests();
            return View(model);
        }

        //GET: Employee Create Request
        public async Task<IActionResult> Create(int? leaveTypeId)
        {
            var leaveTypes = await _leaveTypeServices.GetAll();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);

            var model = new LeaveRequestCreateVM
            {
                StarDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList
            };

            return View(model);
        }

        //POST: Employee Create Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            //Validate that the days don't exceed the allocation
            if (await _leaveRequestsService.RequestDatesExceedAllocation(model))
            {
                ModelState.AddModelError(string.Empty, "You have exceeded your allocation");
                ModelState.AddModelError(nameof(model.EndDate), "The number of days requested is invalid");
            }


            if (ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Index));
            }

            var leaveTypes = await _leaveTypeServices.GetAll();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int leaveRequestId)
        {
            await _leaveRequestsService.CancelLeaveRequest(leaveRequestId);
            return RedirectToAction(nameof(Index));
        }

        //Admin/Sup review request
        [Authorize(Policy = "AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestsService.AdminGetEmployeeLeaveRequests();

            return View(model);
        }

        //Admin/Sup review request
        public async Task<IActionResult> Review(int id)
        {
            var model = await _leaveRequestsService.GetLeaveRequestForReview(id);
            return View(model);
        }

        //Admin/Sup review request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await _leaveRequestsService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }



    }
}
