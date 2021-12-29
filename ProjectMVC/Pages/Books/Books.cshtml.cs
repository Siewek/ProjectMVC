using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
{
    public class BooksModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public BooksModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; }
        public async Task OnGetAsync()
        {
            
            Books = await _context.Books.ToListAsync();
        }
    }
}
