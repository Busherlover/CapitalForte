using Finances.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace Finances.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> ListUsers()
        {
            var users = _userManager.Users.ToList();
            var userinRole = new List<UserInRole>();
            foreach (var user in users)
            {
                var userInRole = new UserInRole
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
                userinRole.Add(userInRole);
            }
            return View(userinRole);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == string.Empty)
            {
                return NotFound();
            }
            var currentUser = await _userManager.FindByIdAsync(Id);
            if ( currentUser == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var allRoles = _roleManager.Roles.ToList();

            ViewBag.AllRoles = allRoles;
            ViewBag.UserRoles = userRoles;

            return View(currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string Id, List<string> roles)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = roles.Except(userRoles).ToList();
            var rolesToRemove = userRoles.Except(roles).ToList();

            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add roles.");
                return View(user);
            }

            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove roles.");
                return View(user);
            }

            return RedirectToAction("ListUsers");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("ListUsers");
        }   


    }
}
