using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace ProjetAtrst.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task CompleteProfileAsync(string userId, CompleteProfileViewModel model)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FirstNameAr = model.FirstNameAr;
            user.LastNameAr = model.LastNameAr;
            user.FullName = model.LastName + " " + model.FirstName;
            user.Gender = model.Gender;
            user.Birthday = model.Birthday;
            user.RegisterDate = DateTime.UtcNow;
            user.Researcher.Establishment = model.Establishment;
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
                    Title = "تم اكمال ملفك الشخصي",
                    Message = "تم اكمال معلومات حسابك بنجاح، وجاري التحقق منها من طرف الإدارة."
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
                //ResearcherApprovalStatus = user.Researcher.ResearcherApprovalStatus,
                Grade = user.Researcher.Grade,
                Speciality = user.Researcher.Speciality,
                Mobile = user.Researcher.Mobile,
                Diploma = user.Researcher.Diploma,
                DipInstitution = user.Researcher.DipInstitution,
                DipDate = user.Researcher.DipDate,
                IsCompleted = user.Researcher.IsCompleted
            };
        }
        //edit
        public async Task EditProfileAsync(string userId, EditProfileViewModel model)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FirstNameAr = model.FirstNameAr;
            user.LastNameAr = model.LastNameAr;
            user.FullName = model.LastName + " " + model.FirstName;
            user.Gender = model.Gender;
            user.Birthday = model.Birthday;
           




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
        public async Task<EditProfileViewModel?> GetEditProfileViewModelAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return null;

            return new EditProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FirstNameAr = user.FirstNameAr,
                LastNameAr = user.LastNameAr,
                Gender = user.Gender,
                Birthday = user.Birthday,
               
            };
        }

    }

}
