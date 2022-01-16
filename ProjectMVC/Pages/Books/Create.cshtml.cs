using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
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
        public Book _Books { get; set; }

        public IList<Book> Books { get; set; }
        public IList<Category> Categories { get; set; }
        public void OnGet()
        {
            Books= _context.Books.ToList();
            Categories = _context.Categories.ToList();
        }
        public async Task<IActionResult> OnPostAsync(string cat, int am)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Categories = (from c in _context.Categories
                          where c.CategoryName == cat
                          select c).ToList();
            _Books.IsReady = false;
            _Books.CategoryFK = Categories.ElementAt(0).CategoryID;
            _Books.Ammount = am;
            _context.Books.Add(_Books);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Books");
        }
    }
}
