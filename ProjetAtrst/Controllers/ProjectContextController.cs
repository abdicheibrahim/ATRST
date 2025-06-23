using Microsoft.AspNetCore.Mvc;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [Route("MyProject/{projectId}/[action]")]
    public class ProjectContextController : Controller
    {
        [AuthorizeProjectLeader]

        public IActionResult Index(int projectId)
        {
            return View();
        }
    }
}
