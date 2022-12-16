namespace RM.Shared
{
    public class Applicant:AppUser
    {
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
} 