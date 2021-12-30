using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Filters
{
    public class FiltersModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public FiltersModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Category> Categories { get; set; }
        public IList<Tag> Tags { get; set; }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            Tags = _context.Tags.ToList();
        }
    }
}
