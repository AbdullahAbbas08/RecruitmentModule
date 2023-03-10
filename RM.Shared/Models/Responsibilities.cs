namespace RM.Shared
{
    public class Responsibilities
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "PleaseEnterResponsibilityItem")]
        [MaxLength(200, ErrorMessage = "Maximum200Characters")]
        [Display(Name = "ResponsibilityItem")]
        public string ResponsibilityItem { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}