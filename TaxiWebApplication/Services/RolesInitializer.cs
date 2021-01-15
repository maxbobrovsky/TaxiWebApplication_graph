using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiWebApplication.Models;

namespace TaxiWebApplication
{
    public class RolesInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
           // string adminEmail = "admin@gmail.com";
           // string password = "_Aa123456";
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("driver") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("driver"));
            }
        }
    }
}
