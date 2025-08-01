using Microsoft.AspNetCore.Identity;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Models;
using System.Security.Claims;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Account;
using ProjetAtrst.ViewModels.Researcher;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ProjetAtrst.Helpers;
namespace ProjetAtrst.Services
{
    public class ResearcherService : IResearcherService
    {
        private readonly StaticDataLoader _staticDataLoader;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public ResearcherService(
            StaticDataLoader staticDataLoader,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork
            )
        {
            _staticDataLoader = staticDataLoader;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> RegisterNewResearcherAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return result;

            var researcher = new Researcher
            {
                Id = user.Id,
                User = user

            };

          
            await _unitOfWork.Researchers.AddAsync(researcher);
            await _unitOfWork.SaveAsync();
            await _signInManager.SignInAsync(user, isPersistent: false);

            return IdentityResult.Success;
        }

        //test

        public async Task EditProfileResearcherViewModelAsync(string userId, EditResearcherProfileViewModel model)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return;

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
                    Title = "Votre profil est complété",
                    Message = "Vos informations de compte sont complétées avec succès et sont en cours de vérification par l'administration."
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
                    Title = "Votre profil a été modifié.",
                    Message = "Les informations de votre compte ont été modifiées avec succès et sont actuellement en cours de vérification par l'administration."
                };
                _unitOfWork.Notifications.Create(notification);
                await _unitOfWork.SaveAsync();
            }


        }
        public async Task<EditResearcherProfileViewModel?> GetEditProfileResearcherViewModelAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return null;

          
            return new EditResearcherProfileViewModel
            {
                EstablishmentsList= _staticDataLoader.LoadEstablishments(),
                Establishment = user.Researcher.Establishment,

                GradesList = _staticDataLoader.LoadGrades(),
                Grade = user.Researcher.Grade,

                StatutList = _staticDataLoader.LoadStatuts(),
                Statut = user.Researcher.Statut,

                Speciality = user.Researcher.Speciality,
                Mobile = user.Researcher.Mobile,
                Diploma = user.Researcher.Diploma,
                DipInstitution = user.Researcher.DipInstitution,
                DipDate = user.Researcher.DipDate,
                IsCompleted = user.Researcher.IsCompleted
            };
        }

        public async Task<bool> IsProfileCompleteAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);

            if (user == null) return false;

            // تحقق حسب منطقك مثل الاسم أو رقم الهوية أو أي شيء
            return !string.IsNullOrEmpty(user.FullName) &&
                   !string.IsNullOrEmpty(user.FirstName)&&
                   !string.IsNullOrEmpty(user.LastName) &&
                   !string.IsNullOrEmpty(user.FirstNameAr) &&
                   !string.IsNullOrEmpty(user.LastNameAr) ;
        }


        //--------------New Code----------------


        public async Task<(List<ResearcherViewModel> Researchers, int TotalCount)> GetAvailableResearchersForInvitationAsync(int projectId, int page, int pageSize)
        {
            var excludedIds = await _unitOfWork.Researchers.GetInvitedOrMembersIdsAsync(projectId);

            var totalCount = await _unitOfWork.Researchers.GetAvailableResearchersCountAsync(excludedIds);
            var researchers = await _unitOfWork.Researchers.GetAvailableResearchersAsync(excludedIds, page, pageSize);

            var mapped = researchers.Select(r => new ResearcherViewModel
            {
                Id = r.Id,
                FullName = r.User.FullName,
                Gender = r.User.Gender,
                ProfilePicturePath = r.User.ProfilePicturePath
            }).ToList();

            return (mapped, totalCount);
        }
    }
}
