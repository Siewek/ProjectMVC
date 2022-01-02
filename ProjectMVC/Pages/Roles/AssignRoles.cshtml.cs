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

            while (roles.ElementAt(index).Id != id )
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
            while (allusers.ElementAt(index).Id != id && index <= allusers.Count() - 1) index++;
            return allusers.ElementAt(index);
        }
        public LibraryTwoUser GetUser2(string id)
        {
            int index = 0;
            while (roleusers.ElementAt(index).Id != id && index <= roleusers.Count() - 1) index++;
            return roleusers.ElementAt(index);
        }

        public IdentityRole GetRole(string id)
        {
            int index = 0;
            while (roles.ElementAt(index).Id.ToString() != id && index <= roles.Count()-1)
            {
                index++;
            }
            return roles.ElementAt(index);
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(RoleID == null)
            RoleID = id;
            else
            {
                var RoleIDaddress = HttpContext.Session.GetString("RoleAddress");
                RoleID = JsonConvert.DeserializeObject<string>(RoleIDaddress);
            }
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
            string name = GetRole(RoleID).Name;
            if (action == "add")
            {
                await _userManager.AddToRoleAsync(GetUser(id), name);
                HttpContext.Session.SetString("RoleAddress",
         JsonConvert.SerializeObject(RoleID));
                //return Page();
                  return RedirectToPage("./ViewRoles");
                //return RedirectToPage();
            }
            
            if (action =="remove")
            {
                await _userManager.RemoveFromRoleAsync(GetUser2(id),name);
                HttpContext.Session.SetString("RoleAddress",
         JsonConvert.SerializeObject(RoleID));
                //return RedirectToPage("AssignRoles");
                  return RedirectToPage("./ViewRoles");

            }
            return Page();
        }
    }
}
