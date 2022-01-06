using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
{
    [Authorize(Roles = "Admin , Mod")]
    public class ManageAuthorsModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        public ManageAuthorsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; }
        public IList<Author> Authors { get; set; }
        public IList<Author> Authors2 { get; set; }
        public IList<AuthorBook> AuthorBooks { get; set; }
        [BindProperty]
        public AuthorBook _AuthorBook { get; set; }
        public int Id;
        public void OnGet(int? id)
        {
            Books = (from b in _context.Books where b.BookID == id select b).ToList();
            Authors = (from a in _context.Authors
                    join ba in _context.AuthorBooks on a.AuthorID equals ba.AuthorID
                    where ba.BookID == id
                    select a).ToList();
            Authors2 = _context.Authors.ToList();
            foreach (Author au in Authors2.ToList())
            {
                foreach (Author au2 in Authors.ToList())
                {
                    if (au == au2)
                        Authors2.Remove(au);
                }
            }
            HttpContext.Session.SetString("AuthAddress",
        JsonConvert.SerializeObject(id));
        }
        public async Task<IActionResult> OnPostAsync(string action, string authid)
        {
            var IDaddress = HttpContext.Session.GetString("AuthAddress");
            Id = JsonConvert.DeserializeObject<int>(IDaddress);
            OnGet(Id);
            Id = Books.ElementAt(0).BookID;
            
            if(action == "Add Author")
            {
                _AuthorBook.BookID = Id;
                _AuthorBook.AuthorID = Convert.ToInt32(authid);
                await _context.AuthorBooks.AddAsync(_AuthorBook);
                await _context.SaveChangesAsync();
                OnGet(Id);
                return Page();
            }
            if(action == "Remove Author")
            {
                _AuthorBook.BookID = Id;
                _AuthorBook.AuthorID = Convert.ToInt32(authid);
                 _context.AuthorBooks.Remove(_AuthorBook);
                await _context.SaveChangesAsync();
                OnGet(Id);
                return Page();
            }
            return RedirectToPage("/Books/Books");
        }
    }
}
