namespace RM.Shared
{
    public class EmployeeVacancy
    {
        public EmployeeVacancy()
        {
            CreatedAt = DateTime.Now;
        }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public int VacancyId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set;}
        

        [ForeignKey("VacancyId")]
        public Vacancy Vacancy { get; set;}
    }
} 