using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;

namespace ProjetAtrst.Controllers
{
    public class InvitationController : Controller
    {
        private readonly IInvitationRequestService _invitationService;
        private readonly IUserAccessService _userAccess;

        public InvitationController(IInvitationRequestService invitationService, IUserAccessService userAccess)
        {
            _invitationService = invitationService;
            _userAccess = userAccess;
        }

        public async Task<IActionResult> Received()
        {
            var userId = _userAccess.GetUserId();
            if (userId == null) return Unauthorized();

            var invitations = await _invitationService.GetReceivedInvitationsAsync(userId);
            return View(invitations);
        }

        public async Task<IActionResult> Sent()
        {
            var userId = _userAccess.GetUserId();
            if (userId == null) return Unauthorized();

            var invitations = await _invitationService.GetSentInvitationsAsync(userId);
            return View(invitations);
        }

        public async Task<IActionResult> Accept(int id)
        {
            var result = await _invitationService.AcceptAsync(id);
            return RedirectToAction(nameof(Received));
        }

        public async Task<IActionResult> Reject(int id)
        {
            var result = await _invitationService.RejectAsync(id);
            return RedirectToAction(nameof(Received));
        }

        [HttpPost]
        public async Task<IActionResult> Send(string receiverId, int projectId)
        {
            var senderId = _userAccess.GetUserId();
            if (senderId == null) return Unauthorized();

            await _invitationService.SendInvitationAsync(senderId, receiverId, projectId);
            return RedirectToAction(nameof(Sent));
        }
    }
}
