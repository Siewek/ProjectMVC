using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Data;

namespace ProjectMVC.Pages.Roles
{
    [Authorize(Roles ="Admin")]
    public class ViewRolesModel : PageModel
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<LibraryTwoUser> userManager;
        
        public ViewRolesModel(RoleManager<IdentityRole> roleManager, UserManager<LibraryTwoUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public List<IdentityRole> roles { get; set; }
        public string GetRole( string id)
        {
            int index = 0;
            while (roles.ElementAt(index).Id != id && index <= roles.Count()) index++;
            return roles.ElementAt(index).Name;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            roles = await roleManager.Roles.ToListAsync();
            HttpContext.Session.Clear();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {           
            
            // roleManager.Roles.FirstOrDefault().
           var role = await roleManager.FindByIdAsync(id);
            // var usersbyrole = userManager.GetUsersInRoleAsync(GetRole(id));
            try
            {
                foreach (LibraryTwoUser user in await userManager.GetUsersInRoleAsync(GetRole(id)))
                {
                    await userManager.RemoveFromRoleAsync(user, GetRole(id));
                }
            }
            catch { }

            await roleManager.DeleteAsync(role);
            await OnGetAsync();
            return Page();
        }
    }
}
