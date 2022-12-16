using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RM.BusinessLayer.Helpers;
using RM.BusinessLayer.IRepositories;
using RM.DataAccessLayer.Data;
using RM.Shared;
using System.Text;

namespace RecruitmentModule.Services
{
    public static class ServicesConfiguration
    {

        public static void ServicesRegisteration(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Map Classes From Appsettings
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            #endregion

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //.UseLazyLoadingProxies()
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString));


            builder.Services.AddIdentity<AppUser, AppRole>(Options =>
            {
                Options.User.RequireUniqueEmail = false;
                Options.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<DatabaseContext>();


            #region  JWT Configurations
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
            });
            #endregion

            builder.Services.AddAutoMapper(typeof(Program));


            builder.Services.AddAuthorizationCore();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(builder.Configuration["MyAllowSpecificOrigins"], builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            builder.Services.AddScoped<HttpContextAccessor>();

            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
            builder.Services.AddTransient<IRoleRepository, RoleRepository>();
            builder.Services.AddTransient<IVacancyRepository, VacancyRepository>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
