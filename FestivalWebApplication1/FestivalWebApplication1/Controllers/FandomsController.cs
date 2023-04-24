using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FestivalWebApplication1.Models;

namespace FestivalWebApplication1.Controllers
{
    public class FandomsController : Controller
    {
        private readonly Lab1Context _context;

        public FandomsController(Lab1Context context)
        {
            _context = context;
        }

        // GET: Fandoms
        public async Task<IActionResult> Index()
        {
            var lab1Context = _context.Fandoms.Include(f => f.FandomNavigation);
            return View(await lab1Context.ToListAsync());
        }

        // GET: Fandoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fandoms == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandoms
                .Include(f => f.FandomNavigation)
                .FirstOrDefaultAsync(m => m.FandomId == id);
            if (fandom == null)
            {
                return NotFound();
            }

            return View(fandom);
        }

        // GET: Fandoms/Create
        public IActionResult Create()
        {
            ViewData["FandomId"] = new SelectList(_context.Pictures, "PictureId", "PictureId");
            return View();
        }

        // POST: Fandoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FandomId,FandomName")] Fandom fandom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fandom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FandomId"] = new SelectList(_context.Pictures, "PictureId", "PictureId", fandom.FandomId);
            return View(fandom);
        }

        // GET: Fandoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fandoms == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandoms.FindAsync(id);
            if (fandom == null)
            {
                return NotFound();
            }
            ViewData["FandomId"] = new SelectList(_context.Pictures, "PictureId", "PictureId", fandom.FandomId);
            return View(fandom);
        }

        // POST: Fandoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FandomId,FandomName")] Fandom fandom)
        {
            if (id != fandom.FandomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fandom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FandomExists(fandom.FandomId))
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
            ViewData["FandomId"] = new SelectList(_context.Pictures, "PictureId", "PictureId", fandom.FandomId);
            return View(fandom);
        }

        // GET: Fandoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fandoms == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandoms
                .Include(f => f.FandomNavigation)
                .FirstOrDefaultAsync(m => m.FandomId == id);
            if (fandom == null)
            {
                return NotFound();
            }

            return View(fandom);
        }

        // POST: Fandoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fandoms == null)
            {
                return Problem("Entity set 'Lab1Context.Fandoms'  is null.");
            }
            var fandom = await _context.Fandoms.FindAsync(id);
            if (fandom != null)
            {
                _context.Fandoms.Remove(fandom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FandomExists(int id)
        {
          return (_context.Fandoms?.Any(e => e.FandomId == id)).GetValueOrDefault();
        }
    }
}
