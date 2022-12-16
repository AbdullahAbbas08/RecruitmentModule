namespace RM.Shared.ViewModels
{
    public class VacancyViewModel 
    {
        public VacancyViewModel()
        {
            Responsibilities = new();
            Skills = new();    

        }

        [Required(ErrorMessage = "PleaseEnterName")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PleaseDescription")]
        [MinLength(100, ErrorMessage = "Minimum100Characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public  List<ResponsibilitiesViewModel> Responsibilities { get; set; }
        public  List<SkillsViewModel> Skills { get; set; }


        [Required]
        public DateTime ValidateFrom { get; set; }

        [Required]
        public DateTime ValidateTo { get; set; }

        public int? MaximumApplications { get; set; }


        public int JobCategoryId { get; set; }
    }
}
