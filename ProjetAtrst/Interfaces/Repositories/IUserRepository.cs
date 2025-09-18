using ProjetAtrst.Models;
using ProjetAtrst.Repositories;

namespace ProjetAtrst.Interfaces.Repositories
{ 
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetUserWithResearcherAsync(string userId);

        Task<ApplicationUser?> GetUserWithDetailsAsync(string userId);
        Task<RoleType> GetRoleAsync(string userId);
       
        Task<List<ApplicationUser>> GetAllAvailableUsersAsync(List<string> excludedIds);
        Task<List<ApplicationUser>> GetAvailableUsersAsync(List<string> excludedIds, int start, int pageSize, string searchValue = null, string sortColumn = null, string sortDirection = null);
        Task<int> GetAvailableUsersCountAsync(List<string> excludedIds, string searchValue = null);
    }

}
