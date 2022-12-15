namespace RM.Shared
{
    public class Skills
    {
        [Key] 
        public int Id { get; set; }

        [Required(ErrorMessage = "PleaseEnterSkillItem")]
        [MaxLength(100, ErrorMessage = "Maximum100Characters")]
        [Display(Name = "SkillItem")]
        public string SkillItem { get; set; }
    }

}