using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectMVC.Data.LibraryTwoDBContext _context;

        public DetailsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public Book book { get; set; }

        public IList<AuthorBook> AuthorBooks { get; set; }

        public IList<Author> Authors { get; set; }

        public IList<BookTag> BookTags { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Category> Categories { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id == null) { return NotFound(); }

            book = _context.Books.FirstOrDefault(m => m.BookID == id);
            if (book == null) { return NotFound(); }
            Authors = (from a in _context.Authors join ab in _context.AuthorBooks on a.AuthorID equals ab.AuthorID where ab.BookID == id select a).ToList();
            Tags = (from t in _context.Tags join BT in _context.BookTags on t.TagID equals BT.TagID where BT.BookID == id select t).ToList();
            Categories = (from c in _context.Categories join b in _context.Books on c.CategoryID equals b.CategoryFK where b.BookID == id select c).ToList();
            return Page();
        }
    }
}

