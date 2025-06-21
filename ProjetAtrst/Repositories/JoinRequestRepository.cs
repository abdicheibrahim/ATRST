//using ProjetAtrst.Interfaces.Repositories;
//using ProjetAtrst.Models;

//namespace ProjetAtrst.Repositories
//{
//    public class JoinRequestRepository : IJoinRequestRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public JoinRequestRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<JoinRequest>> GetJoinRequestsToMyProjectsAsync(string leaderId)
//        {
//            // اجلب معرفات المشاريع التي الباحث هو قائد فيها
//            var myProjectIds = await _context.ProjectMemberships
//                .Where(pm => pm.ResearcherId == leaderId && pm.Role == Role.Leader)
//                .Select(pm => pm.ProjectId)
//                .ToListAsync();

//            // ثم اجلب طلبات الانضمام إلى تلك المشاريع
//            return await _context.JoinRequests
//                .Include(j => j.Project)
//                .Include(j => j.Requester)
//                    .ThenInclude(r => r.User)
//                .Where(j => myProjectIds.Contains(j.ProjectId))
//                .ToListAsync();
//        }
//        public async Task<List<JoinRequest>> GetJoinRequestsISentAsync(string researcherId)
//        {
//            return await _context.JoinRequests
//                .Include(j => j.Project)
//                .Where(j => j.RequesterId == researcherId)
//                .ToListAsync();
//        }
//        public async Task<List<IGrouping<int, JoinRequest>>> GetGroupedJoinRequestsToMyProjectsAsync(string leaderId)
//        {
//            var myProjectIds = await _context.ProjectMemberships
//                .Where(pm => pm.ResearcherId == leaderId && pm.Role == Role.Leader)
//                .Select(pm => pm.ProjectId)
//                .ToListAsync();

//            return (await _context.JoinRequests
//                .Include(j => j.Project)
//                .Include(j => j.Requester)
//                    .ThenInclude(r => r.User)
//                .Where(j => myProjectIds.Contains(j.ProjectId))
//                .ToListAsync())
//                .GroupBy(j => j.ProjectId)
//                .ToList();
//        }

//        //New
//        public async Task<JoinRequest?> GetByProjectAndResearcherAsync(int projectId, string researcherId)
//        {
//            return await _context.JoinRequests
//                .FirstOrDefaultAsync(j => j.ProjectId == projectId && j.RequesterId == researcherId);
//        }

//        public async Task AddAsync(JoinRequest joinRequest)
//        {
//            _context.JoinRequests.Add(joinRequest);
//            await _context.SaveChangesAsync();
//        }

//    }


//}
