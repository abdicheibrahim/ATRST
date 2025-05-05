using Microsoft.AspNetCore.Mvc;

namespace ProjetAtrst.Controllers
{
    public class teamController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }
        public IActionResult JoinRequests()
        {
            return View();
        }
    }
}
