namespace Prestissimo.Web.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection; // NB!
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        private const string AdminUsername = "admin";
        private const string AdminEmail = "admin@mysite.com";
        private const string AdminPassword = "@@@admin###";

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PrestissimoDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task
                    .Run(async () =>
                    {
                        // Seed Roles TODO
                        var roles = new[]
                        {
                            WebConstants.AdministratorRole,
                            WebConstants.BlogAuthorRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        // Seed Admin User
                        var adminUser = await userManager.FindByEmailAsync(AdminEmail);

                        if (adminUser == null)
                        {
                            // Create Admin User
                            adminUser = new User
                            {
                                UserName = AdminUsername, // change Register action as well
                                Email = AdminEmail,
                                // custom properties:
                                Name = WebConstants.AdministratorRole // Add Name to User
                            };

                            var result = await userManager.CreateAsync(adminUser, AdminPassword);

                            // Add User to Role
                            if (result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                            }
                        }
                        else
                        {
                            // Add User to Role
                            await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                        }
                    })
                    .GetAwaiter()
                    .GetResult();
            }

            return app;
        }
    }
}
