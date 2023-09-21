using DAL.Entitys.IdentityEntitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Win32;
using MovieStore.Models;
using System.Text.RegularExpressions;

namespace MovieStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager 
            ,RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM modle)
        {
            if (ModelState.IsValid) {
                var user = await userManager.FindByEmailAsync(modle.Email); 
                if (user is not null)
                {
                    ModelState.AddModelError("Email", "This Email is Exisist"); 
                }
                var appuser = new  AppUser
                { 
                  Email = modle.Email,
                  Name= modle.Name,
                  UserName = modle.Email.Split("@")[0], 
                };
                var res = await userManager.CreateAsync(appuser , modle.Password);
                if (res.Succeeded)
                {
                    return RedirectToAction("Login", "Account");   
                }
                ModelState.AddModelError(string.Empty, "The register is filed"); 
            }
            return View(modle);

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogInVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email); 
                if (user is null)
                {
                    ModelState.AddModelError("Email", "This Emile is not fount"); 
                    return View(model);
                }
                var res = await userManager.CheckPasswordAsync(user, model.Password);
                
                if (!res)
                {
                    ModelState.AddModelError("Password", "Invalid Password"); 
                    return View(model);
                }
                var res2 = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false); 
                if (res2.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); 
                }

            }
            return View(model); 
        }

        public async Task<IActionResult> SinOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account"); 
        }
    }
}
