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
    public class FilterTagDisplayModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        public FilterTagDisplayModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; }

        public Tag Tags { get; set; }

        public BookTag BookTag { get; set; }
        public IActionResult OnGet(int? id)
        {
            Books = (from b in _context.Books join bt in _context.BookTags on b.BookID equals bt.BookID where bt.TagID== id select b ).ToList();
            return Page();
        }
    }
}
