using RM.Shared.Enums;

namespace RM.Shared
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "PleaseEnterFirstName")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "PleaseEnterLastName")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "PleaseSelectGender")]
        public Gender Gender { get; set; }


        [Required(ErrorMessage = "PleaseSelectTitleId")]
        public int TitleId { get; set; }

        public virtual UserTitle Title { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
} 