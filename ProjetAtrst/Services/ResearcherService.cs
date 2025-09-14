using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Account;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;
using System.Collections.Generic;
using System.Security.Claims;
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


        public async Task EditProfileResearcherViewModelAsync(string userId, EditResearcherProfileViewModel model)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return;

            user.Researcher.Establishment = model.Establishment;
            user.Researcher.Grade = model.Grade;
            user.Researcher.Speciality = model.Speciality;
            user.Researcher.Diploma = model.Diploma;
            user.Researcher.ParticipationPrograms = model.ParticipationPrograms;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
            

        }
        public async Task<EditResearcherProfileViewModel?> GetEditProfileResearcherViewModelAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetUserWithResearcherAsync(userId);
            if (user == null || user.Researcher == null)
                return null;

          
            return new EditResearcherProfileViewModel
            {
              
                Establishment = user.Researcher.Establishment,
                Grade = user.Researcher.Grade,
                Speciality = user.Researcher.Speciality,
                Diploma = user.Researcher.Diploma,
                ParticipationPrograms=user.Researcher.ParticipationPrograms,
               
            };
        }
       
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

        public async Task<ResearcherDetailsViewModel?> GetResearcherDetailsAsync(string researcherId)
        {
            return await _unitOfWork.Researchers.GetPartnerDetailsAsync(researcherId);
        }
    } 
    }
