using ProjetAtrst.Models;
using ProjetAtrst.Repository;
using ProjetAtrst.Services.Interfaces;
using ProjetAtrst.ViewModel.Researcher;

namespace ProjetAtrst.Services.Implementation
{
    public class ResearcherService : IResearcherService
    {
        private readonly IGenericRepository<Researcher> _repository;

        public ResearcherService(IGenericRepository<Researcher> repository)
        {
            _repository = repository;
        }

        public async Task<ResearcherProfileViewModel> GetProfileAsync(string userId)
        {
            var researcher = await _repository.GetByIdAsync(userId);
            if (researcher == null) return null;

            return new ResearcherProfileViewModel
            {
                FirstName = researcher.FirstName,
                LastName = researcher.LastName,
                FirstNameAr=researcher.FirstNameAr,
                LastNameAr=researcher.LastNameAr,
                Gender = researcher.Gender,
                Birthday = researcher.Birthday,
                Establishment=researcher.Establishment,
                Status = researcher.Status,
                Grade = researcher.Grade,
                Speciality = researcher.Speciality,
                Mobile = researcher.Mobile,
                Diploma = researcher.Diploma,
                DipInstitution=researcher.DipInstitution,
                DipDate = researcher.DipDate,
            };
        }

        public async Task UpdateProfileAsync(string userId, ResearcherProfileViewModel model)
        {
            var researcher = await _repository.GetByIdAsync(userId);
            bool isNewResearche = false;
            if (researcher == null)
            {
                isNewResearche = true;
                researcher = new Researcher();
                researcher.Id = userId; // Ensure the Id is set for new researcher

            }

            researcher.FirstName = model.FirstName;
            researcher.LastName = model.LastName;
            researcher.FirstNameAr = model.FirstNameAr;
            researcher.LastNameAr = model.LastNameAr;
            researcher.Gender = model.Gender;
            researcher.Birthday = model.Birthday;
            researcher.Establishment = model.Establishment;
            researcher.Status = model.Status;
            researcher.Grade=model.Grade;
            researcher.Speciality = model.Speciality;
            researcher.Mobile = model.Mobile;
            researcher.Diploma = model.Diploma;
            researcher.DipInstitution = model.DipInstitution;
            researcher.DipDate = model.DipDate;
            if (isNewResearche)
            {
                await _repository.Attach(researcher);
            }
            else
            {
                 _repository.Update(researcher);
            }
            
            await _repository.SaveAsync();
        }
    }

}
