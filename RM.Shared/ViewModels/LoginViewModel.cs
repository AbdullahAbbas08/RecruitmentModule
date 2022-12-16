namespace RM.Shared.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter Valid Username")]
        [Display(Name = "Username")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Valid Password")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "minimum 8 characters , At least one uppercase , At least one lowercase , At least one digit , At least one special character")]
        public string Password { get; set; }
    }
}
