using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Roles
{
    public class CreateModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public CreateModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IdentityRole _roles { get; set; }
        public IList<IdentityRole> Roles { get; set; }

       // public List<IdentityRole> roles { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Roles.Add(_roles);
            await _context.SaveChangesAsync();
            return RedirectToPage("./ViewRoles");
        }
    }
}
