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
    public class ManageAmountModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public ManageAmountModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; }
        public Book _Book { get; set; }
        public void OnGet(int? id)
        {
            Books = (from c in _context.Books where c.BookID == id select c).ToList();
        }
        public async Task<IActionResult> OnPostAsync(string action, int amount, int id)
        {
            Books = (from c in _context.Books where c.BookID == id select c).ToList();
            if (action == "Add")
            {
                Books.ElementAt(0).Ammount += amount;
                if(ModelState.IsValid)
                await _context.SaveChangesAsync();
            }
            if (action == "Substract")
            {
                Books.ElementAt(0).Ammount -= amount;
                if (Books.ElementAt(0).Ammount < 0)
                    Books.ElementAt(0).Ammount = 0;
                if (ModelState.IsValid)
                    await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Books/Books");
        }
    }
}
