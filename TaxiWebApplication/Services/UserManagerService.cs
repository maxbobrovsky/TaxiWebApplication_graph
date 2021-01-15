using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaxiWebApplication.Models;

namespace TaxiWebApplication.Services
{
    public interface IUserManagerService
    {
        public string GetIdUser();

        public Task<User> GetUser();
    }

    public class UserManagerService : IUserManagerService
    {
        private UserManager<User> _userManager;
        //private RoleManager<User> _roleManager;
        private readonly IHttpContextAccessor _accessor;
        

        public UserManagerService(UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
           // _roleManager = roleManager;
            _accessor = accessor;
        }

        public string GetIdUser() => _userManager.GetUserId(_accessor.HttpContext.User);

        public async Task<User> GetUser() => await _userManager.GetUserAsync(_accessor.HttpContext.User);




    }
}
