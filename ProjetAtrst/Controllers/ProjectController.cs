using Microsoft.AspNetCore.Mvc;

namespace ProjetAtrst.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult EditProject()
        {
            return View();
        }
        public IActionResult Documents()
        {
            return View();
        }
        public IActionResult Partners()
        {
            return View();
        }
        public IActionResult WorkPackages()
        {
            return View();
        }
        public IActionResult Deliverables()
        {
            return View();
        }
    }
}