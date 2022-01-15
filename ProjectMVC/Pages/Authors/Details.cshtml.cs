using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        public DetailsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Author> Authors { get; set; }
        public IList<AuthorBook> AuthorsBooks { get; set; }
        public IList<Book> Books { get; set; }
        public IList<Category> Categories { get; set; }
        public void OnGet(int? id)
        {
            Authors = (from a in _context.Authors where a.AuthorID == id select a).ToList();
            Books = (from b in _context.Books join ab in _context.AuthorBooks
                     on b.BookID equals ab.BookID where ab.AuthorID == id select b).ToList();
        }
    }
}
