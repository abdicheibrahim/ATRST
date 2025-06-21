namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class JoinRequestCreateViewModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "الرسالة مطلوبة")]
        [StringLength(500, ErrorMessage = "يجب ألا تتجاوز الرسالة 500 حرف")]
        [Display(Name = "رسالتك لقائد المشروع")]
        public string Message { get; set; }
    }


}
