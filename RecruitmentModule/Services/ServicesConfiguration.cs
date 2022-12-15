using Microsoft.EntityFrameworkCore;
using RM.BusinessLayer.IRepositories;
using RM.DataAccessLayer.Data;
using RM.Shared;

namespace RecruitmentModule.Services
{
    public static class ServicesConfiguration
    {
        public static void ServicesRegisteration(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString));

            
            builder.Services.AddIdentity<AppUser, AppRole>(Options =>
            {
                Options.User.RequireUniqueEmail = false;
                Options.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<DatabaseContext>();

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

            builder.Services.AddScoped(typeof(GenericRepository<>),typeof(GenericRepository<>));

        }
    }
}
