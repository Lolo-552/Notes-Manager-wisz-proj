using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notes_Manager.Data;
using Notes_Manager.Models;

namespace Notes_Manager.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotesController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index(string SearchString, string Category)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["CategoryName"] = new SelectList(_context.Category.Where(x => x.UserId == user.Id), "Name", "Name");
            if (!string.IsNullOrEmpty(Category) && Category!="Wszystkie")
            {
                var applicationDbContext = _context.Notes.Include(n => n.Category).Include(n => n.user).Where(x => x.UserId == user.Id && x.Category.Name == Category);
                return View(await applicationDbContext.ToListAsync());
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                var applicationDbContext = _context.Notes.Include(n => n.Category).Include(n => n.user).Where(x => x.UserId == user.Id && x.Title.Contains(SearchString) || x.Content.Contains(SearchString) || x.Category.Name.Contains(SearchString));
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Notes.Include(n => n.Category).Include(n => n.user).Where(x => x.UserId == user.Id);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.Category)
                .Include(n => n.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // GET: Notes/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["CategoryId"] = new SelectList(_context.Category.Where(x => x.UserId == user.Id), "Id", "Name");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,CategoryId")] Notes notes)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            notes.UserId = user.Id;
            //if (ModelState.IsValid)
           // {
                _context.Add(notes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           // }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", notes.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", notes.UserId);
            return View(notes);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["CategoryId"] = new SelectList(_context.Category.Where(x => x.UserId == user.Id), "Id", "Name", notes.CategoryId);
            return View(notes);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CategoryId")] Notes notes)
        {
            if (id != notes.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            var user = await _userManager.GetUserAsync(HttpContext.User);
            notes.UserId = user.Id;
            try
                {
                    _context.Update(notes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesExists(notes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", notes.CategoryId);
            return View(notes);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.Category)
                .Include(n => n.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Notes'  is null.");
            }
            var notes = await _context.Notes.FindAsync(id);
            if (notes != null)
            {
                _context.Notes.Remove(notes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesExists(int id)
        {
          return _context.Notes.Any(e => e.Id == id);
        }


    }
}
