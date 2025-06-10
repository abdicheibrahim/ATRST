using Microsoft.AspNetCore.Identity;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Identity;
using ProjetAtrst.ViewModels.Researcher;
using System.Security.Claims;
using ProjetAtrst.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
namespace ProjetAtrst.Services
{
    public class ResearcherService : IResearcherService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IResearcherRepository _researcherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResearcherService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_researcherRepository = researcherRepository;
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

            //await _researcherRepository.CreateAsync(researcher);
            _unitOfWork.Researchers.AddAsync(researcher);
            await _unitOfWork.SaveAsync();
            await _signInManager.SignInAsync(user, isPersistent: false);

            return IdentityResult.Success;
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

        public async Task<CompleteProfileViewModel?> GetDashboardAsync(ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user);
            var researcher = await _unitOfWork.Researchers.GetByIdAsync(userId);

            if (researcher == null)
                return null;

            return new CompleteProfileViewModel
            {

                IsLeader = researcher.ProjectLeader != null,
                IsMember = researcher.ProjectMember != null

            };
        }
      

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}