using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Associate;
using ProjetAtrst.ViewModels.Partner;

namespace ProjetAtrst.Services
{
    public class AssociateService: IAssociateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssociateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task EditProfileAssociateAsync(string userId, EditAssociateProfileViewModel model)
        {
            var Associate = await _unitOfWork.Associates.GetByIdAsync(userId);
            if (Associate == null)
                return;

            Associate.Diploma = model.Diploma;
            Associate.Speciality = model.Speciality;
            Associate.MemberParticipation = model.MemberParticipation;

            _unitOfWork.Associates.Update(Associate);
            await _unitOfWork.SaveAsync();


        }
        public async Task<EditAssociateProfileViewModel?> GetEditProfileAssociateAsync(string userId)
        {
            var Associate = await _unitOfWork.Associates.GetByIdAsync(userId);
            if (Associate == null)
                return null;

            return new EditAssociateProfileViewModel
            {

                Diploma = Associate.Diploma,
                Speciality = Associate.Speciality,
                MemberParticipation = Associate.MemberParticipation

            };
        }
        public Task<AssociateDetailsViewModel?> GetAssociateDetailsAsync(string AssociateId)
        {
            return _unitOfWork.Associates.GetAssociateDetailsAsync(AssociateId);
        }
    }
}
