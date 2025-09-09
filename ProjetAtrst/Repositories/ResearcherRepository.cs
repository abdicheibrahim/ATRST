using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;
using System.Linq;

namespace ProjetAtrst.Repositories
{
    public class ResearcherRepository : GenericRepository<Researcher>, IResearcherRepository
    {
        public ResearcherRepository(ApplicationDbContext context) : base(context) { }


        public async Task<List<string>> GetInvitedOrMembersIdsAsync(int projectId)
        {
            var invited = await _context.ProjectRequests
                .Where(pr => pr.ProjectId == projectId && pr.Type == RequestType.Invitation)
                .Select(pr => pr.ReceiverId)
                .ToListAsync();

            var members = await _context.ProjectMemberships
                .Where(pm => pm.ProjectId == projectId)
                .Select(pm => pm.UserId)
                .ToListAsync();

            return invited.Union(members).ToList();
        }

        public async Task<List<Researcher>> GetAvailableResearchersAsync(List<string> excludedIds, int page, int pageSize)
        {
            return await _context.Researchers
                .Where(r => !excludedIds.Contains(r.Id))
                .Include(r => r.User)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetAvailableResearchersCountAsync(List<string> excludedIds)
        {
            return await _context.Researchers
                .CountAsync(r => !excludedIds.Contains(r.Id));
        }


        public async Task<ResearcherDetailsViewModel?> GetPartnerDetailsAsync(string ResearcherId)
        {
            return await _context.Researchers
                .Where(p => p.Id == ResearcherId)
                .Select(p => new ResearcherDetailsViewModel
                {

                    FullName = p.User.FullName,
                    FullNameAr = p.User.FirstNameAr + " " + p.User.LastNameAr,
                    Gender = p.User.Gender,
                    Birthday = p.User.Birthday,
                    Mobile = p.User.Mobile,
                    Diploma=p.Diploma,
                    Grade=p.Grade,
                    Speciality=p.Speciality,
                    Establishment=p.Establishment,
                    ParticipationPrograms = p.ParticipationPrograms

                })
                .FirstOrDefaultAsync();
        }

    }

}

