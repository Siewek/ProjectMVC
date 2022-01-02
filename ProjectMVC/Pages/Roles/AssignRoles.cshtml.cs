using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectMVC.Data;

namespace ProjectMVC.Pages.Roles
{
    public class AssignRolesModel : PageModel
    {
        private readonly UserManager<LibraryTwoUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AssignRolesModel(UserManager<LibraryTwoUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public string GetRoleName(string id)
        {
            int index = 0;

            while (roles.ElementAt(index).Id != id)
                index++;
            return roles.ElementAt(index).Name;
        }

        public string RoleID; 
        public IList<LibraryTwoUser> allusers { get; set; }
        public IList<LibraryTwoUser> roleusers { get; set; }
        public IList<IdentityRole> roles { get; set; }
        public LibraryTwoUser GetUser(string id)
        {
            int index = 0;
            while (allusers.ElementAt(index).Id != id) index++;
            return allusers.ElementAt(index);
        }

        public IdentityRole GetRole(string id)
        {
            int index = 0;
            while (roles.ElementAt(index).Id != id && index <= roles.Count()-1) index++;
            return roles.ElementAt(index);
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            RoleID = id;
            roles = await _roleManager.Roles.ToListAsync();
            allusers = await _userManager.Users.ToListAsync();
            roleusers = await _userManager.GetUsersInRoleAsync(GetRole(RoleID).Name);
            foreach(LibraryTwoUser user in allusers.ToList())
            {
                foreach(LibraryTwoUser user2 in roleusers.ToList())
                {
                    if (user == user2)
                        allusers.Remove(user);
                }
            }
          
            HttpContext.Session.SetString("RoleAddress",
            JsonConvert.SerializeObject(RoleID));
             return Page();
           
        }
        public async Task<IActionResult> OnPostAsync(string action, string id)
        {
           var RoleIDaddress = HttpContext.Session.GetString("RoleAddress");
           RoleID = JsonConvert.DeserializeObject<string>(RoleIDaddress);
            await OnGetAsync(RoleID);
            
            if (action == "add")
            {
                await _userManager.AddToRoleAsync(GetUser(id), GetRole(RoleID).Name);
              //   HttpContext.Session.SetString("RoleAddress",
            //JsonConvert.SerializeObject(RoleID));
                // HttpContext.Session.Clear();
                //return Page();
                  return RedirectToPage("./ViewRoles");
                
            }
            if(action =="remove")
            {
                return Page();
            }
            return Page();
        }
    }
}
