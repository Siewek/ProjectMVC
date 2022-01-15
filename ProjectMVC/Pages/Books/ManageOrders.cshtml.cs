using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Books
{
    [Authorize(Roles = "Admin , Mod")]
    public class ManageOrdersModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        private readonly UserManager<LibraryTwoUser> _userManager;
        public ManageOrdersModel(LibraryTwoDBContext context, UserManager<LibraryTwoUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<Order> orders { get; set; }
        public List<Order> selectedOrder { get; set; }
        public List <Book> returnedBook { get; set; }
        public IList<LibraryTwoUser> allusers { get; set; }
        public LibraryTwoUser GetUser(string id)
        {
            int index = 0;
            while (allusers.ElementAt(index).Id != id && index <= allusers.Count() - 1) index++;
            return allusers.ElementAt(index);
        }
        public IList<AdminMessage> Announcements { get; set; }
        [BindProperty]
        public AdminMessage _Announcement { get; set; }
        public void OnGet()
        {
            orders = _context.Orders.ToList();
            allusers =  _userManager.Users.ToList();

        }

        public async Task<IActionResult> OnPostAsync(int? id, string action)
        {
           
            OnGet();
            selectedOrder = (from o in _context.Orders where o.OrderID == id select o).ToList();
            

           // LibraryTwoUser user = _userManager.FindByIdAsync(selectedOrder[0].UserID);
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

            if (action == "Return")
            {
                returnedBook = (from b in _context.Books
                                where b.BookID == Convert.ToInt32(selectedOrder[0].BookID)
                                select b).ToList();
                if (DateTime.Now < selectedOrder[0].EndDate)
                {
                    selectedOrder[0].returned = true;
                    selectedOrder[0].returnedInTime = true;
                    selectedOrder[0].DateOfReturn = DateTime.Now;
                }
                else
                {
                    selectedOrder[0].returned = true;
                    selectedOrder[0].penalty = Convert.ToInt32((DateTime.Now - selectedOrder[0].EndDate).TotalDays * 3);
                    selectedOrder[0].DateOfReturn = DateTime.Now;
                }
                returnedBook[0].Ammount++;
                if (returnedBook[0].Ammount == 1)
                {
                    _Announcement.mesage = returnedBook[0].Title + " Book now back in stock";
                    _Announcement.timestamp = DateTime.Now;
                    _context.AdminMessages.Add(_Announcement);
                }
                await _context.SaveChangesAsync();
                OnGet();
                return Page();
            }

                if (action == "Send Reminder")
                {
               // string userid = selectedOrder[0].UserID;
                     var messsage = new MimeMessage();
                messsage.From.Add(MailboxAddress.Parse("PrzepisyKulinarne.com"));
                messsage.To.Add(MailboxAddress.Parse(GetUser(selectedOrder[0].UserID).Email));
                messsage.Subject = "Your time is almost up";
                messsage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "your order of "  + selectedOrder[0].Title + " Book is almost up, please return it to the Emvicee library"
                };
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.Connect("smtp.gmail.com", 587, false);
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Authenticate("przepisykulinarneps6mo@gmail.com", "YX5ibhzvFPrvp#n");
                    client.Send(messsage);
                    client.Disconnect(true);
                }
                OnGet();
                return Page();
            }
                //await _context.SaveChangesAsync();
                OnGet();
                return Page();
            }
        }
    }
