using LeaveManagementSystem.Models.LeaveType;
using LeaveManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Controllers
{

    [Authorize(Roles = Roles.Administrator)]

    //CONTROLADOR GENERADO POR SCAFFOLDING (Excepto las conexiones a la base de datos [_context])
    public class LeaveTypesController(ILeaveTypeServices _leaveTypeServices) : Controller
    {
        //private readonly ApplicationDbContext _context;
        //private readonly IMapper _mapper;
        private const string NameExistsValidationMessage = "Esta opcion ya esta registrada";
        //private readonly ILeaveTypeServices _leaveTypeServices = leaveTypeServices;

        //Dependecy Injection - You always will need a DB connection everytime when you call the controller (And the Mapper)
        //public LeaveTypesController(ApplicationDbContext context, IMapper mapper)
        //{
        //    _context = context;
        //    this._mapper = mapper;
        //}

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            //Two options to biding data model into View Model
            //1.- Manually convert it line by line
            //Data Model
            //List<LeaveType> data = await _context.LeaveTypes.ToListAsync();

            ////Data Model INTO ViewModel
            //IEnumerable<IndexVM> viewModelData = data.Select(x => new IndexVM
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    NumberOfDays = x.NumberOfDays
            //});

            //2.- Using AutoMapper (You need to install it from NuGet Package and add into the Program.Cs. Then create the AutoMapper class and injecte it into the controller)
            //Data Model
            //var dataModel = await _context.LeaveTypes.ToListAsync();
            ////AutoMapper
            //var viewModelData = _mapper.Map<IEnumerable<LeaveTypeReadOnlyVM>>(dataModel);

            //Invoking the Data Business from the Service Layer
            var viewModelData = await _leaveTypeServices.GetAll();

            //Returning ViewModel
            return View(viewModelData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModelData = await _leaveTypeServices.Find<LeaveTypeReadOnlyVM>(id.Value);

            if (viewModelData == null)
            {
                return NotFound();
            }

            return View(viewModelData);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,NumberOfDays")] LeaveType leaveType) //Possible security risk by the Id Field (OverPosting)
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            //Manually adding custom Model Validation
            //if (leaveTypeCreate.Name.Contains("HomeOffice"))
            //{
            //    ModelState.AddModelError(nameof(leaveTypeCreate.Name), "Esta opcion no esta considerado como dia libre/inhabil");
            //}

            if (await _leaveTypeServices.CheckIfLeaveTypeNameExistsAsync(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _leaveTypeServices.Create(leaveTypeCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreate);
        }



        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataModel = await _leaveTypeServices.Find<LeaveTypeEditVM>(id.Value);
            if (dataModel == null)
            {
                return NotFound();
            }

            return View(dataModel);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Edit/5 - [Id=1,Name="",NumberOfDays=0]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberOfDays")] LeaveType leaveType)
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await _leaveTypeServices.CheckIfLeaveTypeNameExistsAsyncForEdit(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypeServices.Edit(leaveTypeEdit);
                }
                //E.X If two persons try to change an register, one of them will submit it first and the other
                //Will get a Not Found page because the register isn't exists anymore.
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypeServices.LeaveTypeExists(id))
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
            return View(leaveTypeEdit);
        }



        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataModel = await _leaveTypeServices.Find<LeaveTypeReadOnlyVM>(id.Value);

            if (dataModel == null)
            {
                return NotFound();
            }

            return View(dataModel);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(LeaveTypeReadOnlyVM leaveTypeReadOnly)
        {
            await _leaveTypeServices.Remove(leaveTypeReadOnly);
            return RedirectToAction(nameof(Index));
        }


    }
}
