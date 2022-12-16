namespace RM.Shared.ViewModels
{
    public class SkillsViewModel
    {
        [Required(ErrorMessage = "PleaseEnterSkillItem")]
        [MaxLength(100, ErrorMessage = "Maximum100Characters")]
        [Display(Name = "SkillItem")]
        public string SkillItem { get; set; }
    }



}
