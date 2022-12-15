namespace RecruitmentModule.Extentions
{
    public static class WebApplicationExtentions
    {
        public static void AppPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

        }

        public static T GetService<T>(this WebApplication app)
        {
            using IServiceScope serviceScope = app.Services.CreateScope();
            IServiceProvider services = serviceScope.ServiceProvider;
            return services.GetRequiredService<T>();
        }

    }
}
