using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.UserAccess;
namespace ProjetAtrst.Services
{
    public class UserAccessService : IUserAccessService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserAccessStatusViewModel> GetAccessStatusAsync(string userId)
        {
            var researcher = await _context.Researchers
                .Include(r => r.ProjectLeader)
                .Include(r => r.ProjectMember)
                .FirstOrDefaultAsync(r => r.Id == userId);

            if (researcher == null)
            {
                return new UserAccessStatusViewModel
                {
                    IsApproved = false,
                    IsCompleted = false
                };
            }

            return new UserAccessStatusViewModel
            {
                IsApproved = researcher.IsApprovedByAdmin,
                IsCompleted = researcher.IsCompleted,
                ProjectLeaderId = researcher.ProjectLeader?.Id,
                ProjectMemberId = researcher.ProjectMember?.Id
            };
        }

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value
                ?? _httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        }
    }
}
