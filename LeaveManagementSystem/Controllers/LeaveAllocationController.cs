using LeaveManagementSystem.Models.LeaveAllocations;
using LeaveManagementSystem.Services.LeaveAllocations;
using LeaveManagementSystem.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationsService _leaveAllocationService, ILeaveTypeServices _leaveTypeServices) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employeeVM = await _leaveAllocationService.GetEmployees();
            return View(employeeVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Allocateleave(string? id)
        {
            await _leaveAllocationService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }

        public async Task<IActionResult> Details(string? userId)
        {
            var employeeVM = await _leaveAllocationService.GetEmployeeAllocation(userId);
            return View(employeeVM);
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _leaveAllocationService.GetEmployeeAllocation(id.Value);

            if (allocation == null)
            {
                return NotFound();
            }

            return View(allocation);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocation)
        {
            //Solamente los datos que envien desde la vista con asp-for, son aquellos que seran recibidos en las acciones del controlador
            if (await _leaveTypeServices.DaysExceedMaximum(allocation.LeaveType.Id, allocation.Days))
            {
                ModelState.AddModelError("Days", "The numbers of days exceed the maximun leave type value");
            }
            if (ModelState.IsValid)
            {
                await _leaveAllocationService.EditAllocation(allocation);
                return RedirectToAction(nameof(Details), new { userId = allocation.Employee.Id });

            }

            //En caso de haya un error por Modelo Invalido
            //Se trata de obtener la informacion importante para regresarse a la vista y pueda seguir manipulado}
            //[Algo similar al problema con el Sistema Idea]
            var days = allocation.Days;
            allocation = await _leaveAllocationService.GetEmployeeAllocation(allocation.Id);
            allocation.Days = days;

            return View(allocation);

        }
    }
}
