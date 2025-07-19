using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ViewComponent
{
    public class UserInfoViewModel
    {
        public string FullName { get; set; }
        public string ProfilePicturePath { get; set; }
        public List<Notification> Notifications { get; set; } = new();
    }
}
