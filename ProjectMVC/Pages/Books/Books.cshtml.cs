using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public IList<Book> selectedBook { get; set; }
        public List<Order> Orders { get; set; }
        
        public List<Order> returnedOrder { get; set; }
        public List<Order> selectedOrder { get; set; }
        public List<Book> myOrder;
        
        public async Task OnGetAsync()
        {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var OrderAddress = HttpContext.Session.GetString("OrderAddress");
                myOrder = JsonConvert.DeserializeObject<List<Book>>(OrderAddress);
            }
            catch { }
            if(myOrder == null)
            {
                myOrder = new List<Book>();
            }
            Books = await _context.Books.ToListAsync();
            if (userId != null)
            {
                Orders = await (from o in _context.Orders where o.UserID == userId.ToString() select o).ToListAsync();
                returnedOrder =  (from o in Orders where o.returned select o).ToList();
                selectedOrder = await (from o in _context.Orders where o.UserID == userId.ToString() select o).ToListAsync();
                foreach (Order order in Orders)
                {
                    foreach(Order order2 in returnedOrder)
                    {
                        if (order == order2)
                            selectedOrder.Remove(order);
                    }
                }
            }


        }
        public async Task<IActionResult> OnPostAsync(int? book, string action)
        {
            await OnGetAsync();
            selectedBook = (from b in _context.Books where b.BookID == book select b).ToList();
            if(action == "Add To Cart")
            {
                myOrder.Add(selectedBook.ElementAt(0));
                selectedBook.ElementAt(0).Ammount--;
                HttpContext.Session.SetString("OrderAddress",JsonConvert.SerializeObject(myOrder));
              await  _context.SaveChangesAsync();
                return RedirectToPage("/Books/Cart");
            }
            return RedirectToPage("/Index");
        }
    }
}
