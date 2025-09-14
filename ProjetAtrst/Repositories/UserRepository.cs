using ProjetAtrst.DTO;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Account;
namespace ProjetAtrst.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ApplicationUser?> GetUserWithResearcherAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Researcher)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<ApplicationUser?> GetUserWithDetailsAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Researcher)
                .Include(u => u.Partner)
               .Include(u => u.Associate)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<RoleType> GetRoleAsync(string userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.RoleType) 
                .FirstOrDefaultAsync();
        }


        // Method جديدة للحصول على جميع الباحثين المتاحين
        public async Task<List<ApplicationUser>> GetAllAvailableUsersAsync(List<string> excludedIds)
        {
            return await _context.Users
                .Where(r => !excludedIds.Contains(r.Id))
                .ToListAsync();
        }
        public async Task<List<ApplicationUser>> GetAvailableUsersAsync(List<string> excludedIds, int start, int pageSize, string searchValue = null, string sortColumn = null, string sortDirection = null)
        {
            var query = _context.Users.Where(u => !excludedIds.Contains(u.Id));

            // تطبيق البحث
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(u => u.FullName.Contains(searchValue) ||
                                        u.UserName.Contains(searchValue) ||
                                        u.Email.Contains(searchValue));
            }

            // تطبيق الترتيب
            if (!string.IsNullOrEmpty(sortColumn))
            {
                switch (sortColumn.ToLower())
                {
                    case "fullname":
                        query = sortDirection == "asc" ? query.OrderBy(u => u.FullName) : query.OrderByDescending(u => u.FullName);
                        break;
                    case "gender":
                        query = sortDirection == "asc" ? query.OrderBy(u => u.Gender) : query.OrderByDescending(u => u.Gender);
                        break;
                    default:
                        query = query.OrderByDescending(u => u.FullName);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(u => u.FullName);
            }

            return await query.Skip(start).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetAvailableUsersCountAsync(List<string> excludedIds, string searchValue = null)
        {
            var query = _context.Users.Where(u => !excludedIds.Contains(u.Id));

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(u => u.FullName.Contains(searchValue) ||
                                        u.UserName.Contains(searchValue) ||
                                        u.Email.Contains(searchValue));
            }

            return await query.CountAsync();
        }
    }


}
