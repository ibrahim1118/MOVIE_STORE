using DAL.Entitys.IdentityEntitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models;
using System.Data;

namespace MovieStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.Users.Select(u => new UserVM
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Roels = userManager.GetRolesAsync(u).Result.ToList(),

            }).ToListAsync(); 
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var UserRole = new UserRoelVM
            {
               id= user.Id,
               Name = user.Name,
               Email  = user.Email,
               Roles = roleManager.Roles.Select(r => new RoleVM
               {
                   id= r.Id,
                   Name = r.Name,
                   isSelected= userManager.IsInRoleAsync(user , r.Name).Result
               }).ToList()
            };
            return View(UserRole);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRoelVM mode)
        {
            var user = await userManager.FindByIdAsync(mode.id);
            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in mode.Roles)
            {
                if (!role.isSelected &&userRole.Any(r=>r==role.Name))
                    await userManager.RemoveFromRoleAsync(user , role.Name);
                else if (role.isSelected&&!userRole.Any(r=>r ==role.Name))
                    await userManager.AddToRoleAsync(user , role.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
