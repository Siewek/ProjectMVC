using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
{
    [Authorize(Roles = "Admin,Mod,User")]
    public class CartModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        private readonly UserManager<LibraryTwoUser> _userManager;
        public CartModel(LibraryTwoDBContext context, UserManager<LibraryTwoUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IList<Book> Books { get; set; }
        public IList<Book> selectedBook { get; set; }

        public List<Book> myOrder;
        [BindProperty]
        public Order _order { get; set; }

        public IList<Order> orders { get; set; }
        public void OnGet()
        {
            try
            {
                var OrderAddress = HttpContext.Session.GetString("OrderAddress");
                myOrder = JsonConvert.DeserializeObject<List<Book>>(OrderAddress);
            }
            catch { }
            orders = _context.Orders.ToList();
            Books = _context.Books.ToList();
        }
        public async Task<IActionResult> OnPostAsync(string action, int? id)
        {
            OnGet();
            if(action == "Remove")
            { int i = 0;
                while(myOrder.ElementAt(i).BookID!= id)
                {
                    i++;
                }
                try { myOrder.Remove(myOrder.ElementAt(i)); }
                catch { }
                Books = (from b in _context.Books where b.BookID == id select b).ToList();
                Books.ElementAt(0).Ammount++;
                _context.SaveChanges();
                HttpContext.Session.SetString("OrderAddress", JsonConvert.SerializeObject(myOrder));
                OnGet();
                return Page();
            }
            if(action =="Confirm")
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // LibraryTwoUser user = await _userManager.GetUserAsync(User);
               // _context.AddRange(myOrder);
                foreach (Book item in myOrder)
                {
                    Order _order = new Order();
                    _order.BookID = item.BookID.ToString();
                    _order.UserID = userId.ToString();
                    _order.DateOfOrder = DateTime.Now;
                    _order.EndDate = _order.DateOfOrder.AddDays(14);
                    _context.Orders.Add(_order);
                    _context.SaveChanges();
                }             
                return RedirectToPage("/Books/Orders");
            }
            return Page();
        }
    }
   
}
