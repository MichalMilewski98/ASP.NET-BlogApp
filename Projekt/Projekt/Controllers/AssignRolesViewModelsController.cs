using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class AssignRolesViewModelsController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<BlogUser> _userManager;
        private ApplicationDbContext _db;
        private readonly ILogger _logger;
        AssignRolesViewModel mymodel;

        public AssignRolesViewModelsController(RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager, ILogger<AssignRolesViewModelsController> logger, ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _db = db;
           mymodel = new AssignRolesViewModel(_userManager, _db);
        }

        [Authorize(Policy = "readrolepolicy")]
        public IActionResult Index()
        {
            mymodel.users = _userManager.Users.ToList();
            mymodel.roles = _roleManager.Roles.ToList();
            _logger.LogInformation(_roleManager.Roles.ToList().ElementAt(0).Name);
            return View(mymodel);
        }

        [Authorize(Policy = "editrolepolicy")]
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }


        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Assign(AssignRolesViewModel model)
        {

            var newRole = _roleManager.FindByIdAsync(model.currentRole).Result;
            var newUser = _userManager.FindByIdAsync(model.currentUser).Result;
            await _userManager.AddToRoleAsync(newUser, newRole.Name);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
