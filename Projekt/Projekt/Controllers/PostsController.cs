using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;

namespace Projekt.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BlogUser> _userManager;

        public PostsController(ApplicationDbContext context, UserManager<BlogUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Post
                .Include(a => a.author)
                .Include(c => c.comments);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Post model = _context.Post.Where(x => x.postId == id).Select(x =>

                new Post()
                {
                    postId = x.postId,
                    title = x.title,
                    author = x.author,
                    content = x.content,
                    date = x.date,
                    comments = x.comments

                }).SingleOrDefault();

            return View(model);
   
        }

        // GET: Posts/Create

        [Authorize(Policy = "editpostpolicy")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("postId,title,content")] Post post)
        {
            if (ModelState.IsValid)
            {
                var currentUser = GetCurrentUserAsync().Result;
                post.author = currentUser;
                post.date = DateTime.UtcNow;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Policy = "editpostpolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.postId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(post.authorFK == GetCurrentUserId().Result)
                {
                    try
                    {
                        _context.Update(post);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostExists(post.postId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Policy = "editpostpolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.postId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post.authorFK == GetCurrentUserId().Result)
            {
                _context.Post.Remove(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Policy = "editpostpolicy")]
        [Route("/Posts/Details/{postId}")]
        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, Comment comment)
        {
            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.postId == postId);

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            if (post is null)
            {
                return NotFound();
            }

            if (comment is null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            post.comments.Add(comment);
            _context.Update(comment);
            comment.author = GetCurrentUserAsync().Result;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new{id = postId});
        }

        [Authorize(Policy = "editpostpolicy")]
        [Route("/Posts/Details/DeleteComment/{postId}")]
        public async Task<IActionResult> DeleteComment(int postId)
        {
            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.postId == postId);


            if (post is null)
            {
                return NotFound();
            }

            var comment =  _context.Comment.FirstOrDefault(c => c.post.postId == postId);

            if (comment is null)
            {
                return NotFound();
            }

     
            post.comments.Remove(comment);
            _context.Comment.Remove(comment);
            _context.Update(post);
            _context.Update(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = postId });
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.postId == id);
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            BlogUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        private Task<BlogUser> GetCurrentUserAsync() =>
            _userManager.GetUserAsync(HttpContext.User);
    }
}
