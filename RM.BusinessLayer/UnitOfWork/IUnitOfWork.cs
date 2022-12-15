

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace RM.BusinessLayer.IUnitOfWorkNameSpace
{
    public interface IUnitOfWork
    {
        DatabaseContext DbContext { get; }
        IMapper Mapper { get; }
        AppUser CurrentUser { get; }
        UserManager<AppUser> UserManager { get; }
        RoleManager<AppRole> RoleManager { get; }
        AppUserRepository Users { get; }
        IRoleRepository Roles { get; }
    }

    #region Definitions
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext db;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;
        private readonly HttpContext httpContext;

        public UnitOfWork(DatabaseContext db,
                         UserManager<AppUser> userManager,
                         RoleManager<AppRole> roleManager,
                         SignInManager<AppUser> signInManager,
                         IMapper mapper,
                         HttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.httpContext = httpContextAccessor?.HttpContext;
        }

        private DatabaseContext dbContext;
        public DatabaseContext DbContext
        {
            get
            {
                if (this.dbContext == null) this.dbContext = db;
                return dbContext;
            }
        }

        public IMapper Mapper => this.mapper;

        public AppUser CurrentUser => Users.FirstOrDefault(u => u.UserName == httpContext?.User?.Identity?.Name);


        public UserManager<AppUser> UserManager => userManager;
        public RoleManager<AppRole> RoleManager => roleManager;

        private AppUserRepository applicationUserRepository;
        public AppUserRepository Users
        {
            get
            {
                if (this.applicationUserRepository == null)
                {
                    this.applicationUserRepository = new AppUserRepository(db, this);
                }
                return applicationUserRepository;
            }
        }


        private IRoleRepository roleRepository;
        public IRoleRepository Roles
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new RoleRepository(db, this, roleManager);
                }
                return roleRepository;
            }
        }
    }
    #endregion
}
