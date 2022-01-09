using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class AnnouncementsModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public AnnouncementsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }

        public IList<AdminMessage> Messages { get; set; }
        [BindProperty]
        public AdminMessage _Message { get; set; }
        public void OnGet()
        {
            Messages = _context.AdminMessages.ToList();
        }

        public async Task<IActionResult> OnPostAsync(string message, string action)
        {
          
            if(action == "add")
            {
                _Message.mesage = message;
                _Message.timestamp = DateTime.Now;
                _context.AdminMessages.Add(_Message);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
