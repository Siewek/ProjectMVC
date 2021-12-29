using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Authoors
{
    public class AuthorsModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        public AuthorsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Author> Authors { get; set; }
        public async Task OnGetAsync()
        {
            var _authors = from a in _context.Authors select a;
            Authors = await _context.Authors.ToListAsync();
        }
    }
}
