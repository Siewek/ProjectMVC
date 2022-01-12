using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
{
    [Authorize(Roles = "Admin,Mod,User")]
    public class OrdersModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public OrdersModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public List<Order> orders { get; set; }
        public List<Book> book { get; set; }
        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            orders = (from o in _context.Orders where o.UserID == userId select o).ToList();
            if (orders != null)
            {
                book = (from b in _context.Books
                        join o in _context.Orders on b.BookID equals o.OrderID
                        where o.UserID == userId
                        select b).ToList();
            }

        }

    }
}
