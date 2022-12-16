using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RM.BusinessLayer.Helpers;
using RM.BusinessLayer.IRepositories;
using RM.DataAccessLayer.Data;
using RM.Shared;
using System.IdentityModel.Tokens.Jwt;
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
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Recruitement Module API ",
                });

                options.AddSecurityDefinition("Bearer ", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "*** Type 'Bearer ' Before Toaken ***",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
            });

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


          

            builder.Services.AddAutoMapper(typeof(Program));


            //builder.Services.AddAuthorizationCore();

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

            #region  JWT Configurations
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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
        }
    }
}
