using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectMVC.Pages.Roles
{
    public class ViewRolesModel : PageModel
    {
        RoleManager<IdentityRole> roleManager;

        public ViewRolesModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public List<IdentityRole> roles { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            roles = await roleManager.Roles.ToListAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            
            // roleManager.Roles.FirstOrDefault().
            var role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            await OnGetAsync();
            return Page();
        }
    }
}
