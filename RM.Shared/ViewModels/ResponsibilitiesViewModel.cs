namespace RM.Shared.ViewModels
{
    public class ResponsibilitiesViewModel
    {

        [Required(ErrorMessage = "PleaseEnterResponsibilityItem")]
        [MaxLength(200, ErrorMessage = "Maximum200Characters")]
        [Display(Name = "ResponsibilityItem")]
        public string ResponsibilityItem { get; set; }

    }



}
