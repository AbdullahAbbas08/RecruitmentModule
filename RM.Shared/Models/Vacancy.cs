namespace RM.Shared
{
    public class Vacancy : _Model
    {
        [Required(ErrorMessage = "PleaseEnterName")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PleaseDescription")]
        [MinLength(20, ErrorMessage = "Minimum20Characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public DateTime ValidateFrom { get; set; }

        [Required]
        public DateTime ValidateTo { get; set; }

        public int MaximumApplications { get; set; }


        public int JobCategoryId { get; set; }

        [ForeignKey("JobCategoryId")]
        public  JobCategory JobCategory { get; set; }

        public ICollection<Applicant> Applicants { get; set; } ;
        public  ICollection<Responsibilities> Responsibilities { get; set; }
        public  ICollection<Skills> Skills { get; set; }

        public bool IsDelete { get; set; } = false;

        [NotMapped]
        public bool IsClosed
        {
            get
            {
                return MaximumApplications > Applicants.Count() && ValidateTo > DateTime.Now ?  false :  true;
            }
        }

    }
}