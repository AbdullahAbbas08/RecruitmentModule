namespace RM.BusinessLayer.IRepositories
{

    #region Definitions
    public interface IVacancyRepository : IGenericRepository<Vacancy>
    {

    }
    #endregion

    #region Implementation Section
    public class VacancyRepository : GenericRepository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(DatabaseContext db, IUnitOfWork uow) : base(db, uow) { }

    }

#endregion

     
}
