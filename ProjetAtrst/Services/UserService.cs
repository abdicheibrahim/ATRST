using Microsoft.AspNetCore.Identity;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Account;
using ProjetAtrst.ViewModels.Associate;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;

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

            user.FirstName = model.PersonalInformation.FirstName;
            user.LastName = model.PersonalInformation.LastName;
            user.FirstNameAr = model.PersonalInformation.FirstNameAr;
            user.LastNameAr = model.PersonalInformation.LastNameAr;
            user.FullName = model.PersonalInformation.LastName + " " + model.PersonalInformation.FirstName;
            user.Gender = model.PersonalInformation.Gender;
            user.Birthday = model.PersonalInformation.Birthday;
            user.Mobile = model.PersonalInformation.Mobile;
            user.RegisterDate = DateTime.UtcNow;
            user.RoleType = model.RoleType;
            if (user.RoleType == RoleType.Researcher)
            {
                user.Researcher = user.Researcher ?? new Researcher { Id = user.Id, User = user };
                user.Researcher.Diploma = model.Researcher.Diploma;
                user.Researcher.Grade = model.Researcher.Grade;
                user.Researcher.Speciality = model.Researcher.Speciality;
                user.Researcher.Establishment = model.Researcher.Establishment;
                user.Researcher.ParticipationPrograms= model.Researcher.ParticipationPrograms;
                user.IsCompleted = true;
            }
            else
            if (user.RoleType == RoleType.Partner)
            {
                user.Partner = user.Partner ?? new Partner { Id = user.Id, User = user };
                user.Partner.Baccalaureat = model.Partner.Baccalaureat;
                user.Partner.Diploma = model.Partner.Diploma;
                user.Partner.Profession = model.Partner.Profession;
                user.Partner.Speciality = model.Partner.Speciality;
                user.Partner.Establishment = model.Partner.Establishment;
                user.Partner.PartnerResearchPrograms = model.Partner.PartnerResearchPrograms;
                user.Partner.PartnerSocioEconomicWorks = model.Partner.PartnerSocioEconomicWorks;
                user.IsCompleted = true;
            }
            else if (user.RoleType == RoleType.Associate)
            {
                user.Associate = user.Associate ?? new Associate { Id = user.Id, User = user };
                user.Associate.Diploma = model.Associate.Diploma;
                user.Associate.Speciality = model.Associate.Speciality;
                user.Associate.MemberParticipation = model.Associate.MemberParticipation;
                user.IsCompleted = true;

            }

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

        }
        public async Task<CompleteProfileViewModel?> GetCompleteProfileViewModelAsync(string userId)
        {
            // نجلب المستخدم مع الـ navigation properties
            var user = await _unitOfWork.Users.GetUserWithDetailsAsync(userId);
            if (user == null) return null;
            //var _personalInformation = 
            var model = new CompleteProfileViewModel
            {
                PersonalInformation= new EditProfileViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FirstNameAr = user.FirstNameAr,
                    LastNameAr = user.LastNameAr,
                    Gender = user.Gender,
                    Birthday = user.Birthday,
                    Mobile = user.Mobile,
                },

                RoleType = user.RoleType,
                IsCompleted = user.IsCompleted
            };

            switch (user.RoleType)
            {
                case RoleType.Researcher:
                    if (user.Researcher != null)
                    {
                        model.Researcher = new ResearcherProfileViewModel
                        {
                            Establishment = user.Researcher.Establishment,
                            Grade = user.Researcher.Grade,
                            Speciality = user.Researcher.Speciality,
                            Diploma = user.Researcher.Diploma,
                            ParticipationPrograms = user.Researcher.ParticipationPrograms,
                        };
                    }
                    break;

                case RoleType.Partner:
                    if (user.Partner != null)
                    {
                        model.Partner = new PartnerProfileViewModel
                        {
                            Baccalaureat = user.Partner.Baccalaureat,
                            Diploma = user.Partner.Diploma,
                            Profession = user.Partner.Profession,
                            Speciality = user.Partner.Speciality,
                            Establishment = user.Partner.Establishment,
                            PartnerResearchPrograms = user.Partner.PartnerResearchPrograms,
                            PartnerSocioEconomicWorks = user.Partner.PartnerSocioEconomicWorks,
                        };
                    }
                    break;

                case RoleType.Associate:
                    if (user.Associate != null)
                    {
                        model.Associate = new AssociateProfileViewModel
                        {
                            Diploma = user.Associate.Diploma,
                            Speciality= user.Associate.Speciality,
                            MemberParticipation = user.Associate.MemberParticipation
                        };
                    }
                    break;
            }

            return model;
        }

        public async Task EditProfileAsync(string userId, EditProfileViewModel model)
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
            var user = await _unitOfWork.Users.GetUserWithDetailsAsync(userId);
            if (user == null )
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
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null )
                return false;
            return user.IsCompleted;
        }
    }

}
