using LeaveManagementSystem.Models.Periods;
using LeaveManagementSystem.Services.Periods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class PeriodsController(IPeriodsService _periodService) : Controller
    {
        //private readonly ApplicationDbContext _context;
        private const string NameExistsValidationMessage = "Esta opcion ya esta registrada";

        //public PeriodsController()
        //{
        //    _context = context;
        //}

        // GET: Periods
        public async Task<IActionResult> Index()
        {
            var model = await _periodService.GetAll();
            return View(model);
        }

        // GET: Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _periodService.Find<PeriodsVM>(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Periods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeriodsCreateVM periodsCreateVM)
        {

            if (await _periodService.CheckIfPeriodExistsAsync(periodsCreateVM.Name))
            {
                ModelState.AddModelError(nameof(periodsCreateVM.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _periodService.Create(periodsCreateVM);
                return RedirectToAction(nameof(Index));
            }
            return View(periodsCreateVM);
        }

        // GET: Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _periodService.Find<PeriodsVM>(id.Value);

            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeriodsVM periods)
        {
            if (id != periods.Id)
            {
                return NotFound();
            }

            if (await _periodService.CheckIfPeriodExistsAsyncForEdit(periods))
            {
                ModelState.AddModelError(nameof(periods.Name), NameExistsValidationMessage);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    await _periodService.Edit(periods);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_periodService.PeriodExists(periods.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            //If you get until this points, something failed
            return View(periods);
        }

        // GET: Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _periodService.Find<PeriodsVM>(id.Value);

            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(PeriodsVM periods)
        {
            await _periodService.Remove(periods);
            return RedirectToAction(nameof(Index));
        }

        //private bool PeriodExists(int id)
        //{
        //    return _context.Periods.Any(e => e.Id == id);
        //}
    }
}
