namespace LearningSystem.Web
{
    using Areas.Blog.Controllers;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Web.Controllers;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<LearningSystemDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<LearningSystemDbContext>()
                .AddDefaultTokenProviders();

            // External Authentication with Facebook & Google
            services
                .AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = this.Configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = this.Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(options =>
                {
                    options.ClientId = this.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = this.Configuration["Authentication:Google:ClientSecret"];
                });

            // Add application services.
            services.AddDomainServices();
            services.AddRouting(routing => routing.LowercaseUrls = true);
            //services.AddSession();

            // Add Auto Mapper
            services.AddAutoMapper();

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Database migrations
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                // Users Profile Route
                routes.MapRoute(
                    name: "profile",
                    template: "users/{username}",
                    defaults: new
                    {
                        controller = "Users",
                        action = nameof(UsersController.Profile)
                    });

                //Friendly URL blog article routing
                routes.MapRoute(
                    name: "blog",
                    template: "blog/articles/{id}/{title}",
                    defaults: new
                    {
                        area = WebConstants.BlogArea,
                        controller = "Articles",
                        action = nameof(ArticlesController.Details)
                    });

                // Areas Routing
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
