using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Categories
{
    public class CreateModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public CreateModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Category _Categories { get; set; }
        public IList<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Categories.Add(_Categories);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Categories");
        }
    }
}
