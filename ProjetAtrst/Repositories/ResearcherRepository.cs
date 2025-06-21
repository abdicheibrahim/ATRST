using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class ResearcherRepository : GenericRepository<Researcher>, IResearcherRepository
    {
        public ResearcherRepository(ApplicationDbContext context) : base(context) { }

    }

}

