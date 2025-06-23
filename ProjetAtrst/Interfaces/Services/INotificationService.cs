using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Services
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(string userId, string title, string message, NotificationType type = NotificationType.General, int? relatedId = null);
        Task<List<Notification>> GetUnreadNotificationsAsync(string userId);
        Task<List<Notification>> GetAllNotificationsAsync(string userId);
        Task MarkAsReadAsync(int notificationId);
    }
}
