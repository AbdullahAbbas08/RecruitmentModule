namespace RM.Shared
{
    public class Applicant:AppUser
    {
        public  ICollection<Vacancy> Vacancies { get; set; }
    }
} 