using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaxiWebApplication.Models;

namespace TaxiWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
                var host = CreateHostBuilder(args).Build();
                
                try
                {
                    var scope = host.Services.CreateScope();
               
                
                    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    ctx.Database.EnsureCreated();

                    var adminRole = new IdentityRole("admin");

                    if (!ctx.Roles.Any())
                    {
                        rolesManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                    }

                    if(!ctx.Users.Any(u => u.UserName == "admin"))
                    {
                    var adminUser = new User
                    {
                        UserName = "admin",
                        Email = "admin@test.ua"
                    };

                    var res = userManager.CreateAsync(adminUser, "Polshaua1 ").GetAwaiter().GetResult();

                    userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

                }
                    //await RolesInitializer.InitializeAsync(userManager, rolesManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " );
                    //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(ex, "An error occurred while seeding the database.");
                }
            

                host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
