using ProjetAtrst.DTO;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Project;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
      
        Task<Project?> GetByIdAsync(int id);
        //delete
       // Task<(List<Project> Projects, int TotalCount)> GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize);
        //

        Task<(string ProjectTitle, string LeaderFullName)> GetProjectInfoAsync(int projectId);

        //new

        Task<(List<AvailableProjectDto> Projects, int TotalCount)>GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize);

        // (اختياري) لاستخدام DataTables: Query قابل للتوسعة (بحث/فرز) في الـ Service
        IQueryable<AvailableProjectDto> GetAvailableProjectsQuery(string researcherId);

        //

        Task<ProjectDetailsDto?> GetProjectDetailsAsync(int projectId);
    }
}
