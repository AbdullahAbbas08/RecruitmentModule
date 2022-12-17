namespace RM.BusinessLayer.IRepositories
{

    #region Definitions
    public interface IJobCategoryRepository : IGenericRepository<JobCategory>
    {

    }
    #endregion

    #region Implementation Section
    public class JobCategoryRepository : GenericRepository<JobCategory>, IJobCategoryRepository
    {
        public JobCategoryRepository(DatabaseContext db, IUnitOfWork uow) : base(db, uow) { }

    }
    #endregion

}
