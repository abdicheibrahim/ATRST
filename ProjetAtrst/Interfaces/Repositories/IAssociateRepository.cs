using ProjetAtrst.ViewModels.Associate;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IAssociateRepository : IGenericRepository<Associate>
    {
        Task<AssociateDetailsViewModel?> GetAssociateDetailsAsync(string AssociateId);

    }
}
