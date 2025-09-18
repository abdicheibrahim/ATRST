namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
       
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId);
       
        
    }
}

