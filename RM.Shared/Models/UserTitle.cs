namespace RM.Shared
{
    public class UserTitle : _Model
    {
        [Required(ErrorMessage = "PleaseEnterTitle")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public virtual List<AppUser> Users { get; set; }
    }

} 