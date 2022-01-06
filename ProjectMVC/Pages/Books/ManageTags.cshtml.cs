using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
{
    public class ManageTagsModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public ManageTagsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Tag> Tags2 { get; set; }
        public IList<BookTag> BookTags { get; set; }
        [BindProperty]
        public BookTag _BookTag { get; set; }
        public int Id;
        public void OnGet(int? id)
        {
            Books = (from b in _context.Books where b.BookID == id select b).ToList();
            Tags = (from t in _context.Tags join bt in _context.BookTags on t.TagID equals bt.TagID 
                    where bt.BookID == id
                    select t).ToList();
            Tags2 = _context.Tags.ToList();
            foreach(Tag tag in Tags2.ToList())
            {
                foreach(Tag tag2 in Tags.ToList())
                {
                    if (tag == tag2)
                        Tags2.Remove(tag);
                }
            }
            HttpContext.Session.SetString("TagAddress",
          JsonConvert.SerializeObject(id));
        }

        public async Task<IActionResult> OnPostAsync(string action, string tagid)
        {
            var IDaddress = HttpContext.Session.GetString("TagAddress");
            Id = JsonConvert.DeserializeObject<int>(IDaddress);
            OnGet(Id);

            Id = Books.ElementAt(0).BookID;
            if (action == "Add Tag")
            {
                _BookTag.BookID = Id;
                _BookTag.TagID = Convert.ToInt32(tagid);
               await _context.BookTags.AddAsync(_BookTag);
                await _context.SaveChangesAsync();
                OnGet(Id);
                return Page();
            }
            if(action == "remove")
            {
                _BookTag.BookID = Id;
                _BookTag.TagID = Convert.ToInt32(tagid);
                 _context.BookTags.Remove(_BookTag);
                await _context.SaveChangesAsync();
                OnGet(Id);
                return Page();
            }
            if (!ModelState.IsValid)
                return Page();
            else
                return RedirectToPage("/Books/Books");
        }
    }
}
