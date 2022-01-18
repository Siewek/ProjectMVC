using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Pages.Authors
{
    [Authorize(Roles = "Admin , Mod")]
    public class EditModel : PageModel
    {
        
        public ProjectMVC.Data.LibraryTwoDBContext _context;
        public EditModel(LibraryTwoDBContext context)
        {
            _context = context;
        }
        public IList<Author> author { get; set; }
        public int ID;
        public void OnGet(int? id)
        {
            author = (from a in _context.Authors where a.AuthorID == id select a).ToList();
            HttpContext.Session.SetString("IDAddress",
        JsonConvert.SerializeObject(id));
        }
        public async Task<IActionResult> OnPostAsync(string action, string name, string last_name)
        {
            var IDaddress = HttpContext.Session.GetString("IDAddress");
            ID = JsonConvert.DeserializeObject<int>(IDaddress);
            OnGet(ID);
             author = (from a in _context.Authors where a.AuthorID == ID select a).ToList();
            if (action == "Confirm")
            {
                author[0].AuthorName = name;
                author[0].AuthorLastName = last_name;
                await _context.SaveChangesAsync();
                HttpContext.Session.Clear();
            }
            return RedirectToPage("/Authors/Authors");
        }
    }
}
