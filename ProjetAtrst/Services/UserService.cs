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
        public async Task<IdentityResult> RegisterNewAccountAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return result;
            await _unitOfWork.SaveAsync();
            await _signInManager.SignInAsync(user, isPersistent: false);

            return IdentityResult.Success;
        }
        public async Task CompleteProfileAsync(string userId, CompleteProfileViewModel model)
        {
            var user = await _unitOfWork.Users.GetUserWithDetailsAsync(userId);
            if (user == null )
                return;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FirstNameAr = model.FirstNameAr;
            user.LastNameAr = model.LastNameAr;
            user.FullName = model.LastName + " " + model.FirstName;
            user.Gender = model.Gender;
            user.Birthday = model.Birthday;
            user.Mobile = model.Mobile;
            user.RegisterDate = DateTime.UtcNow;
            user.RoleType = (RoleType)model.RoleType;
            if (user.RoleType == RoleType.Researcher)
            {
                user.Researcher = user.Researcher ?? new Researcher { Id = user.Id, User = user };
                user.Researcher.Diploma = model.Diploma;
                user.Researcher.Grade = model.Grade;
                user.Researcher.Speciality = model.Speciality;
                user.Researcher.Establishment = model.Establishment;
                user.Researcher.ParticipationPrograms= model.ParticipationPrograms;
                user.IsCompleted = true;
            }
            else
            if (user.RoleType == RoleType.Partner)
            {
                //user.Partner = user.Partner ?? new Partner { Id = user.Id, User = user };
                //user.Partner.Diploma = model.Diploma;
                //user.Partner.Baccalaureat = model.Baccalaureat;
                //user.Partner.Profession = model.Profession;
                //user.Partner.Speciality = model.Speciality;
                //user.Partner.Establishment = model.Establishment;
                //user.Partner.ParticipationPrograms = model.ParticipationPrograms;
                //user.Partner.SocioEconomicContributions = model.SocioEconomicContributions;
                //user.Partner.IsCompleted = true;
            }
            else if (user.RoleType == RoleType.Associate)
            {
                //user.Associate = user.Associate ?? new Associate { Id = user.Id, User = user };
                //user.Associate.Diploma = model.Diploma;
                //user.Associate.Speciality = model.Speciality;
                //user.Associate.Establishment = model.Establishment;
                //user.Associate.MemberParticipation = model.MemberParticipation;
                //user.Associate.IsCompleted = true;

            }

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

        }
        public async Task<CompleteProfileViewModel?> GetCompleteProfileViewModelAsync(string userId)
        {
            // نجلب المستخدم مع الـ navigation properties
            var user = await _unitOfWork.Users.GetUserWithDetailsAsync(userId);
            if (user == null) return null;

            var model = new CompleteProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FirstNameAr = user.FirstNameAr,
                LastNameAr = user.LastNameAr,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Mobile = user.Mobile,
                RoleType = user.RoleType,
                IsCompleted = user.IsCompleted
            };

            switch (user.RoleType)
            {
                case RoleType.Researcher:
                    if (user.Researcher != null)
                    {
                        model.Establishment = user.Researcher.Establishment;
                        model.Grade = user.Researcher.Grade;
                        model.Speciality = user.Researcher.Speciality;
                        model.Diploma = user.Researcher.Diploma;
                        model.ParticipationPrograms = user.Researcher.ParticipationPrograms;
                    }
                    break;

                //case RoleType.Partner:
                //    if (user.Partner != null)
                //    {
                //        model.Baccalaureat = user.Partner.Baccalaureat;
                //        model.Profession = user.Partner.Profession;
                //        model.SocioEconomicContributions = user.Partner.SocioEconomicContributions;
                //    }
                //    break;

                //case RoleType.Associate:
                //    if (user.Associate != null)
                //    {
                //        model.MemberParticipation = user.Associate.MemberParticipation;
                //        model.OtherProjects = user.Associate.OtherProjects;
                //    }
                //    break;
            }

            return model;
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
            user.Mobile = model.Mobile;

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
                Mobile = user.Mobile

            };
        }
        public async Task<bool> IsProfileCompleteAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return false;
            return user.IsCompleted;
        }
    }

}
