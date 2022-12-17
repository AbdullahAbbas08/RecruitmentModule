using RM.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Shared.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter FirstName")]
        [MaxLength(20, ErrorMessage = "Maximum 20 Characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter LastName")]
        [MaxLength(20, ErrorMessage = "Maximum 20 Characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Please Enter Title")]
        //[Display(Name = "Title")]
        //public int TitleId { get; set; }


        [Required(ErrorMessage = "Please Enter Valid Mobile Number")]
        [Phone(ErrorMessage = "Please Enter Valid Mobile Number")]
        [Display(Name = "Mobile Number")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please Enter Valid Mobile Number")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessage = "Please Enter Valid Username")]
        [Display(Name = "Username")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Valid Password")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "minimum 8 characters , At least one uppercase , At least one lowercase , At least one digit , At least one special character")]
        public string Password { get; set; }
        //public Gender Gender { get; set; }
    }
}
