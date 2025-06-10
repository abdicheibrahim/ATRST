using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Researcher;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Models;

namespace ProjetAtrst.Services
{
    public class UserService : IUserService
    {
        // private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly INotificationService _notificationService;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CompleteUserProfileAsync(string userId, CompleteProfileViewModel model)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FirstNameAr = model.FirstNameAr;
            user.LastNameAr = model.LastNameAr;
            user.Gender = model.Gender;
            user.Birthday = model.Birthday;
            user.RegisterDate = DateTime.UtcNow;
            user.Researcher.Establishment = model.Establishment;
            user.Researcher.Status = model.Status;
            user.Researcher.Grade = model.Grade;
            user.Researcher.Speciality = model.Speciality;
            user.Researcher.Mobile = model.Mobile;
            user.Researcher.Diploma = model.Diploma;
            user.Researcher.DipInstitution = model.DipInstitution;
            user.Researcher.DipDate = model.DipDate;




            if (!user.Researcher.IsCompleted)
            {
                user.Researcher.IsCompleted = true;
                 _unitOfWork.Users.Update(user);
                var notification = new Notification
                {
                    UserId = userId,
                    Title = "تم تعديل ملفك الشخصي",
                    Message = "تم تعديل معلومات حسابك بنجاح، وجاري التحقق منها من طرف الإدارة."
                };
                _unitOfWork.Notifications.Create(notification);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                 _unitOfWork.Users.Update(user);
                var notification = new Notification
                {
                    UserId = userId,
                    Title = "تم تعديل ملفك الشخصي",
                    Message = "تم تعديل معلومات حسابك بنجاح، وجاري التحقق منها من طرف الإدارة."
                };
                _unitOfWork.Notifications.Create(notification);
                await _unitOfWork.SaveAsync();
            }
            

        }
        public async Task<CompleteProfileViewModel?> GetCompleteProfileViewModelAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return null;

            return new CompleteProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FirstNameAr = user.FirstNameAr,
                LastNameAr = user.LastNameAr,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Establishment = user.Researcher.Establishment,
                Status = user.Researcher.Status,
                Grade = user.Researcher.Grade,
                Speciality = user.Researcher.Speciality,
                Mobile = user.Researcher.Mobile,
                Diploma = user.Researcher.Diploma,
                DipInstitution = user.Researcher.DipInstitution,
                DipDate = user.Researcher.DipDate,
                IsCompleted = user.Researcher.IsCompleted
            };
        }

    }

}
