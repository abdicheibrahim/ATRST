namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IJoinRequestRepository
    {
        Task<int> CountPendingForLeaderAsync(string userId);

    }
}
