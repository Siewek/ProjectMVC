using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectMVC.Data;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public IndexModel(ILogger<IndexModel> logger, LibraryTwoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<AdminMessage> Messages { get; set; }
        public void OnGet()
        {
            Messages = _context.AdminMessages.ToList();
        }
    }
}
