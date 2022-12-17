using Microsoft.AspNetCore.Identity;
using RecruitmentModule.Extentions;
using RecruitmentModule.Services;
using RM.BusinessLayer.Seeds;

var builder = WebApplication.CreateBuilder(args);
builder.ServicesRegisteration();
var app = builder.Build();
app.AppPipeline();

#region Seed Users And Roles
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerProvider>();
var logger = loggerFactory.CreateLogger("app");

try
{
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await DefaultRoles.SeedRole(roleManager);
    await DefaultUsers.SeedAdminUser(userManager, roleManager);

}
catch (System.Exception ex)
{
}

#endregion




app.Run();