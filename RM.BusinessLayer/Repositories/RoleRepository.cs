using RM.Shared.Enums;

namespace RM.BusinessLayer.IRepositories
{

    #region Definitions
    public interface IRoleRepository : IGenericRepository<AppRole>
    {

    }
    #endregion


    #region Implementation Section
    public class RoleRepository : GenericRepository<AppRole>, IRoleRepository
    {
        private readonly DatabaseContext db;
        private readonly RoleManager<AppRole> roleManager;

        public RoleRepository(DatabaseContext db, IUnitOfWork uow, RoleManager<AppRole> roleManager) : base(db, uow)
        {
            this.db = db;
            this.roleManager = roleManager;

            InitialData();
        }

        private void InitialData()
        {
            if (Count() == 0)
            {
                AppRole[] roles = new AppRole[]
                {
                    new AppRole(){Name = Roles.SuperAdmin.ToString()},
                    new AppRole(){Name = Roles.Admin.ToString()},
                    new AppRole(){Name = Roles.Applicant.ToString()},
                };
                AddRange(roles);
            }
        }

        public override AppRole Add(AppRole role)
        {
            Task<IdentityResult> result = roleManager.CreateAsync(role);
            result.Wait();
            if (result.Result.Succeeded)
            {
                return FirstOrDefault(r => r.Name == role.Name);
            }
            else
            {
                return null;
            }
        }

        public override IEnumerable<AppRole> AddRange(IEnumerable<AppRole> roles)
        {
            foreach (AppRole role in roles)
            {
                Add(role);
            }
            db.SaveChanges();
            return roles;
        }
    }

    #endregion


}
