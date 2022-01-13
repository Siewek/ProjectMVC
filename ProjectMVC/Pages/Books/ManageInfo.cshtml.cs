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
        public IList<AdminMessage> adminMessages { get; set; }
        public IList <BookTag> BookTags { get; set; }
        [BindProperty]
        public BookTag _BookTag { get; set; }
        [BindProperty]
        public AdminMessage _AdminMessage { get; set; }
        public void OnGet(int? id)
        {
            Books = (from b in _context.Books where b.BookID == id select b).ToList();
            Tags = _context.Tags.ToList();
            adminMessages = _context.AdminMessages.ToList();
            HttpContext.Session.Clear();

        }
        public async Task<IActionResult> OnPostAsync(bool rdy, int id, string tagstring)
        {
            Books = (from c in _context.Books where c.BookID == id select c).ToList();
          /*  if (!ModelState.IsValid)
            {
                return Page();
            }*/
            if (rdy)
            {
                Books.ElementAt(0).IsReady = true;
                _AdminMessage.mesage = Books[0].Title.ToString() + " Book now available for order";
                _AdminMessage.timestamp = DateTime.Now;
                _context.AdminMessages.Add(_AdminMessage);
            }
            else
            {
                Books.ElementAt(0).IsReady = false;
                _AdminMessage.mesage = Books[0].Title.ToString() + " Book currently taken off shelves";
                _AdminMessage.timestamp = DateTime.Now;
                await _context.AdminMessages.AddAsync(_AdminMessage);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/Books/Books");
        }
    }
}
