using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Tags
{
    public class TagsModel : PageModel
    {
        public ProjectMVC.Data.LibraryTwoDBContext _context;

        public TagsModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Tag> Tags { get; set; }
        public void OnGet()
        {
            Tags = _context.Tags.ToList();
        }
    }
}
