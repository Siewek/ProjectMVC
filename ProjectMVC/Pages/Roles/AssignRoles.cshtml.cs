using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Data;

namespace ProjectMVC.Pages.Roles
{
    public class AssignRolesModel : PageModel
    {
        private readonly UserManager<LibraryTwoUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AssignRolesModel(UserManager<LibraryTwoUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IList<LibraryTwoUser> allusers { get; set; }
        public IList<LibraryTwoUser> roleusers { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //roles = await roleManager.Roles.ToListAsync();
            allusers = await _userManager.Users.ToListAsync();

            return Page();
        }
    }
}
