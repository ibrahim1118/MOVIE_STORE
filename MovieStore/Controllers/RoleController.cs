using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace MovieStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var role = await roleManager.Roles.ToListAsync(); 
            return View(role);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(IdentityRole role)
        {
            
            if (ModelState.IsValid)
            {
               await roleManager.CreateAsync(role); 
               return RedirectToAction("Index");
            }
            return View(role);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
                return BadRequest(); 
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            return View(role); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
               var ro = await roleManager.FindByIdAsync(role.Id);
               ro.Name = role.Name;
               await roleManager.UpdateAsync(ro); 
               return RedirectToAction("Index");
            }
            return View(role); 
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role  =await roleManager.FindByIdAsync(id);
            await  roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
