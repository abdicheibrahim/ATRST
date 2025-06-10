using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        void Create(Notification notification);
        Task<List<Notification>> GetUnreadNotificationsAsync(string userId);
        Task<List<Notification>> GetAllNotificationsAsync(string userId);
        Task MarkAsReadAsync(int notificationId);
    }
}
