namespace RM.Shared
{
    public class JobCategory: _Model
    {
        [Required(ErrorMessage = "PleaseEnterTitle")]
        [MaxLength(100, ErrorMessage = "Maximum100Characters")]
        [Display(Name = "Title")]
        public string Title { get; set; } 
    }
} 