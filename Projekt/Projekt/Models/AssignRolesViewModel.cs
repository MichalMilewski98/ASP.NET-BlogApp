using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Projekt.Data;

namespace Projekt.Models
{
    public class AssignRolesViewModel
    {
        private UserManager<BlogUser> _userManager;
        private UserManager<IdentityRole> _roleManager;
        private ApplicationDbContext _db;
        public List<BlogUser> users { get; set; }
        public List<IdentityRole> roles { get; set; }

        public string currentUser { get; set; }
        public string currentRole { get; set; }

        public IdentityRole usersRole { get; set; }

        public string findRole(IdentityUser user)
        {
            List<IdentityUserRole<string>> listUserRoles = _db.UserRoles.ToList();
            List<string> roleids = _db.UserRoles.Where(a => a.UserId == user.Id).Select(b => b.RoleId).Distinct().ToList();
            List<IdentityRole> listRoles = _db.Roles.Where(a => roleids.Any(c => c == a.Id)).ToList();
            String roles = "";

           foreach (var r in listRoles)
           {
               if (listRoles.Count > 1)
                   roles += r.Name + ", ";
               else
                   roles += r.Name;
           }
            return roles;
        }

        public AssignRolesViewModel()
        { }

        public AssignRolesViewModel(UserManager<BlogUser> userManager, ApplicationDbContext context)
        {
            this.users = new List<BlogUser>();
            this.roles = new List<IdentityRole>();
            _userManager = userManager;
            _db = context;
        }
    }
}
