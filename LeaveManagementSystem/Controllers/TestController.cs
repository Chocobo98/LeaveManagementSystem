using LeaveManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var model = new TestViewModel
            {
                Name = "Saul",
                DateOfBirth = new DateTime(1998, 03, 01)
            };

            return View(model);
        }
    }
}
