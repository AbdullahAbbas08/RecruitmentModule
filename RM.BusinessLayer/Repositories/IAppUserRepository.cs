namespace RM.BusinessLayer.IRepositories
{
    #region Definitions
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {

    }
    #endregion

    #region Implementation Section
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {

        public AppUserRepository(DatabaseContext db, IUnitOfWork uow) : base(db,uow)
        {
            
        }
    }
    #endregion
}
 