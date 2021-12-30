using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Filters
{
    public class FilterCategoryDisplayModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        public FilterCategoryDisplayModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; }
        public Category Categories { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Books = (from b in _context.Books where b.CategoryFK == id select b).ToList();
            return Page();
        }
    }
}
