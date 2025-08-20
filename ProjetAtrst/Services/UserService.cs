using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using ProjetAtrst.Helpers;

namespace ProjetAtrst.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly StaticDataLoader _staticDataLoader;
        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, StaticDataLoader staticDataLoader)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _staticDataLoader = staticDataLoader;
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
            user.Researcher.Statut = model.Statut;
            user.Researcher.Speciality = model.Speciality;
            user.Mobile = model.Mobile;
            user.Researcher.Diploma = model.Diploma;
            user.Researcher.DipInstitution = model.DipInstitution;
            user.Researcher.DipDate = model.DipDate;
            user.Researcher.WantsToContributeAsPartner = model.WantsToContributeAsPartner;
            user.Researcher.SocioEconomicContributions = model.SocioEconomicContributions;
            user.Researcher.IsCompleted = true;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

            #region commented out logic for notifications
            //this is commented out because the logic for notifications is not implemented in the original code.
            //if (!user.Researcher.IsCompleted)
            //{
            //    user.Researcher.IsCompleted = true;
            //     _unitOfWork.Users.Update(user);
            //    var notification = new Notification
            //    {
            //        UserId = userId,
            //        Title = "Votre profil est complété",
            //        Message = "Vos informations de compte sont complétées avec succès et sont en cours de vérification par l'administration."
            //    };
            //    _unitOfWork.Notifications.Create(notification);
            //    await _unitOfWork.SaveAsync();
            //}
            //else
            //{
            //     _unitOfWork.Users.Update(user);
            //    var notification = new Notification
            //    {
            //        UserId = userId,
            //        Title = "Votre profil a été modifié.",
            //        Message = "Les informations de votre compte ont été modifiées avec succès et sont actuellement en cours de vérification par l'administration."
            //    };
            //    _unitOfWork.Notifications.Create(notification);
            //    await _unitOfWork.SaveAsync();
            //}
            #endregion

        }
        public async Task<CompleteProfileViewModel?> GetCompleteProfileViewModelAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null) return null;

            return new CompleteProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FirstNameAr = user.FirstNameAr,
                LastNameAr = user.LastNameAr,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Establishment = user.Researcher.Establishment,
                Grade = user.Researcher.Grade,
                Statut = user.Researcher.Statut,
                Speciality = user.Researcher.Speciality,
                Mobile = user.Mobile,
                Diploma = user.Researcher.Diploma,
                DipInstitution = user.Researcher.DipInstitution,
                DipDate = user.Researcher.DipDate,
                WantsToContributeAsPartner = user.Researcher.WantsToContributeAsPartner,
                SocioEconomicContributions = user.Researcher.SocioEconomicContributions,
                IsCompleted = user.Researcher.IsCompleted
            };
        }

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

            #region commented out logic for notifications
            //var notification = new Notification
            //{
            //    UserId = userId,
            //    Title = "Votre profil a été modifié.",
            //    Message = "Les informations de votre compte ont été modifiées avec succès et sont actuellement en cours de vérification par l'administration."
            //};
            //_unitOfWork.Notifications.Create(notification);
            #endregion

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
