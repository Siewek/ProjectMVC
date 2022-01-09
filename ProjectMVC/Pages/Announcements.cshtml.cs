using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages
{
    public class AnnouncementsModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public AnnouncementsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<AdminMessage> Messages { get; set; }
        public void OnGet()
        {
            Messages = _context.AdminMessages.ToList();
        }
    }
}
