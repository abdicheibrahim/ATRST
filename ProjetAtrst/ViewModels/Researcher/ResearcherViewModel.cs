namespace ProjetAtrst.ViewModels.Researcher
{
    public class ResearcherViewModel
    {
        public string MemberId { get; set; } = default!; // يمثل Id من ProjectMember
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Grade { get; set; }
        public string? Speciality { get; set; }
    }
}
