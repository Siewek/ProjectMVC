using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Authors
{
    [Authorize(Roles="Admin , Mod")]
    public class CreateModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public CreateModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
       
        public IList<Author> Authors { get; set; }
        [BindProperty]
        public Author _author { get; set; }
        public void OnGet()
        {
            Authors = _context.Authors.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _context.Authors.Add(_author);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Authors");
        }
    }
}
