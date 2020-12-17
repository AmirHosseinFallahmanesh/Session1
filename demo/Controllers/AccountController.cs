using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public  IActionResult Login(string returnurl)
        {

            LoginViewModel viewModel = new LoginViewModel()
            {
                RetrunUrl = returnurl
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginViewModel loginViewModel)
        {
           var user= await this.userManager.FindByNameAsync(loginViewModel.Username);
            if (user!=null)
            {

                Microsoft.AspNetCore.Identity.SignInResult result = new Microsoft.AspNetCore.Identity.SignInResult();
              var passwordCheck=  await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (passwordCheck.Succeeded)
                {
                    return Redirect(loginViewModel.RetrunUrl??"/");
                }

            }
            ModelState.AddModelError("", "Invalid Username or Password");

            return View(loginViewModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
