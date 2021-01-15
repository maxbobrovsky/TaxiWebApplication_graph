using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxiWebApplication.Models;
using TaxiWebApplication.ViewModels;

namespace TaxiWebApplication.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult RegisterPage()
        {
            // var roles = new List<string> { "user", "driver" };
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "user", Value = "user", Selected = false });
            items.Add(new SelectListItem() { Text = "driver", Value = "driver", Selected = true });

            SelectList sl = new SelectList(items, "Value", "Text");
            ViewData["Roles"] = sl;
            var io = (List<SelectListItem>)sl.Items;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPage(RegisterViewModel model)
            {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, NativeCity = model.NativeCity };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                var creator = await _userManager.AddToRoleAsync(user, model.Role);
                
                if (result.Succeeded && creator.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LoginPage(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPage(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login and (or) password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
