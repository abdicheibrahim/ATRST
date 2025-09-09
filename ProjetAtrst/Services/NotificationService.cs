using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
namespace ProjetAtrst.Services
{
    public class NotificationService : INotificationService
    {

        //private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNotificationAsync(string userId, string title, string message, NotificationType type = NotificationType.General, int? relatedId = null)
        {
            var notification = new Notification
            { 
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                RelatedEntityId = relatedId
            };

            _unitOfWork.Notifications.Create(notification);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Notification>> GetUnreadNotificationsAsync(string userId)
        {
            return await _unitOfWork.Notifications.GetUnreadNotificationsAsync(userId);
        }

        public async Task<List<Notification>> GetAllNotificationsAsync(string userId)
        {
            return await _unitOfWork.Notifications.GetAllNotificationsAsync(userId);
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            await _unitOfWork.Notifications.MarkAsReadAsync(notificationId);
            await _unitOfWork.SaveAsync(); // حفظ التغييرات بعد تحديث الحالة
        }
        
    }
}
