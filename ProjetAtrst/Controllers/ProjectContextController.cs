using Microsoft.AspNetCore.Mvc;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProfileCompletionFilter))]
    public class ProjectContextController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
