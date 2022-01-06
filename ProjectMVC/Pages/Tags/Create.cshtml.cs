using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Tags
{
    [Authorize(Roles = "Admin , Mod")]
    public class CreateModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public CreateModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Tag _Tags { get; set; }
        public IList<Tag> Tags { get; set; }
        public void OnGet()
        {
            Tags = _context.Tags.ToList();
        }

        public async Task<IActionResult> OnPostAsync(string action, string TagName)
        {
            if (!ModelState.IsValid)
                return Page();
            if(action == "Add")
            {
                _Tags.TagName = TagName;
                _context.Tags.Add(_Tags);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Tags");
        }
    }
}
