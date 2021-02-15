using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Projekt.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly ILogger _logger;
        UserManager<BlogUser> _userManager;
        private ApplicationDbContext _db;
        //UserDetails mymodel;

        public UserDetailsController(UserManager<BlogUser> userManager, ApplicationDbContext db, ILogger<BlogUser> logger)
        {
            _userManager = userManager;
            _db = db;
            _logger = logger;
            //mymodel = new UserDetails();
        }

        public IActionResult Index()
        {
            var user = GetCurrentUserAsync().Result;
            return View(user);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(BlogUser model)
        {
            if (ModelState.IsValid)
            {
                var search = _db.Users.Where(i => i.UserName == model.UserName);
                if (!search.Any())
                {
                    var user = GetCurrentUserAsync().Result;
                    user.UserName = model.UserName;
                    user.dateOfBirth = model.dateOfBirth;
                    user.pesel = model.pesel;

                    _db.Users.Update(user);
                }
                else
                {
                    _db.Users.Update(model);
                }

                await _db.SaveChangesAsync();
            }
            else
            {
                return View("Index", model);
            }

            return RedirectToAction("Index");
        }

        private Task<BlogUser> GetCurrentUserAsync() =>
            _userManager.GetUserAsync(HttpContext.User);
    }
}
