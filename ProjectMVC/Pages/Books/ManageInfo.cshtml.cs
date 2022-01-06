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
    public class ManageInfoModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public ManageInfoModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; }
        public Book _Book { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Author> Authors { get; set; }
        public IList <Category> Categories { get; set; }
        public IList <BookTag> BookTags { get; set; }
        [BindProperty]
        public BookTag _BookTag { get; set; }
        public void OnGet(int? id)
        {
            Books = (from b in _context.Books where b.BookID == id select b).ToList();
            Tags = _context.Tags.ToList();
        }
        public async Task<IActionResult> OnPostAsync(bool rdy, int id, string tagstring)
        {
            Books = (from c in _context.Books where c.BookID == id select c).ToList();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (rdy)
            {
                Books.ElementAt(0).IsReady = true;
            }
            else
            {
                Books.ElementAt(0).IsReady = false;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/Books/Books");
        }
    }
}
