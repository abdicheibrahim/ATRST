using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Researcher;
using System.Security.Claims;

namespace ProjetAtrst.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int draw = 1, int start = 0, int length = 10)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var searchValue = Request.Query["search[value]"].FirstOrDefault();
                var orderColumnIndex = Request.Query["order[0][column]"].FirstOrDefault();
                var orderDirection = Request.Query["order[0][dir]"].FirstOrDefault();

                var columnNames = new[] { "profilePicturePath", "fullName", "email", "isComplete", "isApproved", "roleType" };
                var columnName = orderColumnIndex != null && int.Parse(orderColumnIndex) < columnNames.Length
                    ? columnNames[int.Parse(orderColumnIndex)]
                    : "fullName";

                var totalCount = await _userService.GetUsersCountAsync();
                var filteredCount = await _userService.GetUsersCountAsync(searchValue);

                var users = await _userService.GetUsersAsync(start, length, searchValue, columnName, orderDirection);

                var data = users.Select(u => new
                {
                    profilePicturePath = string.IsNullOrEmpty(u.ProfilePicturePath) ? "/images/default-project.png" : u.ProfilePicturePath,
                    fullName = u.FullName,
                    email = u.Email,
                    isComplete = u.IsCompleted ? "✔️" : "❌",
                    isApproved = u.UserApprovalStatus.ToString(),
                    roleType = u.RoleType.ToString(),
                    id = u.Id 
                }).ToList();

                return Json(new
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = filteredCount,
                    data = data
                });
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Approve(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
                return BadRequest();
            var AdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _userService.ApproveUserAsync(UserId, AdminId);

            if (!success)
                return NotFound();

            return Json(new { success = true });
        }

    }
}

  
