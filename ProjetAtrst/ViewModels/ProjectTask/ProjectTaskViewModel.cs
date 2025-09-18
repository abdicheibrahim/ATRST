namespace ProjetAtrst.ViewModels.ProjectTask
{
    public class ProjectTaskViewModel
    {
        [Required(ErrorMessage = "L'identifiant du projet est requis")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Le nom de la tâche est requis")]
        [StringLength(200, ErrorMessage = "Le nom de la tâche ne peut pas dépasser 200 caractères")]
        [Display(Name = "Nom de la tâche")]
        public string TaskName { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
        public string Description { get; set; }

        [Display(Name = "Statut")]
        public string Status { get; set; } = "Pending";

        [Range(1, 3, ErrorMessage = "La priorité doit être comprise entre 1 et 3")]
        [Display(Name = "Priorité")]
        public int Priority { get; set; } = 2;

        [DataType(DataType.Date)]
        [Display(Name = "Date de début")]
        public DateOnly? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de fin")]
        public DateOnly? EndDate { get; set; }

        [Range(0, 100, ErrorMessage = "La progression doit être comprise entre 0 et 100")]
        [Display(Name = "Progression")]
        public int Progress { get; set; } = 0;

        // Custom validation method for date logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.HasValue && EndDate.HasValue && StartDate.Value > EndDate.Value)
            {
                yield return new ValidationResult(
                    "La date de début ne peut pas être postérieure à la date de fin",
                    new[] { nameof(StartDate), nameof(EndDate) });
            }

            // Additional business logic validation
            if (StartDate.HasValue && StartDate.Value < DateOnly.FromDateTime(DateTime.Today.AddDays(-30)))
            {
                yield return new ValidationResult(
                    "La date de début ne peut pas être antérieure à 30 jours",
                    new[] { nameof(StartDate) });
            }
        }
    }
}
