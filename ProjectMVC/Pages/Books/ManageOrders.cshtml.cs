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
    public class ManageOrdersModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        public ManageOrdersModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public List<Order> orders { get; set; }
        public List<Order> selectedOrder { get; set; }
        public List <Book> returnedBook { get; set; }
        public void OnGet()
        {
            orders = _context.Orders.ToList();
            
        }

        public async Task<IActionResult> OnPostAsync(int? id, string action)
        {
            OnGet();
            selectedOrder = (from o in _context.Orders where o.OrderID == id select o).ToList();
            returnedBook = (from b in _context.Books where b.BookID == Convert.ToInt32(selectedOrder[0].BookID) 
                            select b).ToList();

            if (action == "Fulfill")
            {
                if(DateTime.Now < selectedOrder.ElementAt(0).EndDate)
                {
                    selectedOrder[0].fulfilled = true;
                }
                else
                {
                    //nothing, go home 
                }
                await _context.SaveChangesAsync();
                OnGet();
                return Page();
            }
            if(action == "Return")
            {
                if(DateTime.Now < selectedOrder[0].EndDate)
                {
                    selectedOrder[0].returned = true;
                    selectedOrder[0].returnedInTime = true;
                    selectedOrder[0].DateOfReturn = DateTime.Now;
                }
                else
                {
                    selectedOrder[0].returned = true;
                    selectedOrder[0].penalty = Convert.ToInt32((DateTime.Now - selectedOrder[0].EndDate).TotalDays*3);
                    selectedOrder[0].DateOfReturn = DateTime.Now;
                }
                returnedBook[0].Ammount++;
                await _context.SaveChangesAsync();
                OnGet();
                return Page();
            }
            return Page();
        }
    }
}
