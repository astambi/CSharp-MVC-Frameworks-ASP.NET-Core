namespace FDMC.App
{
    using Data;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options 
                => options.UseSqlServer(AppSettings.DatabaseConnectionString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseDatabaseMigration()
               .UseStaticFiles()
               .UseHtmlContentType()
               .UseRequestHandlers()
               .UseNotFoundHandler();
        }
    }
}
