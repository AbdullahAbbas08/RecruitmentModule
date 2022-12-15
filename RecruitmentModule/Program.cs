using RecruitmentModule.Extentions;
using RecruitmentModule.Services;

var builder = WebApplication.CreateBuilder(args);
builder.ServicesRegisteration();


var app = builder.Build();
app.AppPipeline();
app.Run();