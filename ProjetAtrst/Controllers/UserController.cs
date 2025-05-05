using Microsoft.AspNetCore.Mvc;

namespace ProjetAtrst.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult Education()
        {
            return View();
        }
        public IActionResult Employments()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout() { 
         return View();
        }
    }
}
