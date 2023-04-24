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
    public class CosplayersController : Controller
    {
        private readonly Lab1Context _context;

        public CosplayersController(Lab1Context context)
        {
            _context = context;
        }

        // GET: Cosplayers
        public async Task<IActionResult> Index()
        {
            var lab1Context = _context.Cosplayers.Include(c => c.Cosplayer1).Include(c => c.CosplayerNavigation);
            return View(await lab1Context.ToListAsync());
        }

        // GET: Cosplayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cosplayers == null)
            {
                return NotFound();
            }

            var cosplayer = await _context.Cosplayers
                .Include(c => c.Cosplayer1)
                .Include(c => c.CosplayerNavigation)
                .FirstOrDefaultAsync(m => m.CosplayerId == id);
            if (cosplayer == null)
            {
                return NotFound();
            }

            return View(cosplayer);
        }

        // GET: Cosplayers/Create
        public IActionResult Create()
        {
            ViewData["CosplayerId"] = new SelectList(_context.Fandoms, "FandomId", "FandomId");
            ViewData["CosplayerId"] = new SelectList(_context.CosplayerTeams, "CosplayerTeamId", "CosplayerTeamId");
            return View();
        }

        // POST: Cosplayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CosplayerId,CosplayerName,CosplayerBirthDate,CosplayerType,FandomId,CosplayerTeamId")] Cosplayer cosplayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cosplayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CosplayerId"] = new SelectList(_context.Fandoms, "FandomId", "FandomId", cosplayer.CosplayerId);
            ViewData["CosplayerId"] = new SelectList(_context.CosplayerTeams, "CosplayerTeamId", "CosplayerTeamId", cosplayer.CosplayerId);
            return View(cosplayer);
        }

        // GET: Cosplayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cosplayers == null)
            {
                return NotFound();
            }

            var cosplayer = await _context.Cosplayers.FindAsync(id);
            if (cosplayer == null)
            {
                return NotFound();
            }
            ViewData["CosplayerId"] = new SelectList(_context.Fandoms, "FandomId", "FandomId", cosplayer.CosplayerId);
            ViewData["CosplayerId"] = new SelectList(_context.CosplayerTeams, "CosplayerTeamId", "CosplayerTeamId", cosplayer.CosplayerId);
            return View(cosplayer);
        }

        // POST: Cosplayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CosplayerId,CosplayerName,CosplayerBirthDate,CosplayerType,FandomId,CosplayerTeamId")] Cosplayer cosplayer)
        {
            if (id != cosplayer.CosplayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cosplayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosplayerExists(cosplayer.CosplayerId))
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
            ViewData["CosplayerId"] = new SelectList(_context.Fandoms, "FandomId", "FandomId", cosplayer.CosplayerId);
            ViewData["CosplayerId"] = new SelectList(_context.CosplayerTeams, "CosplayerTeamId", "CosplayerTeamId", cosplayer.CosplayerId);
            return View(cosplayer);
        }

        // GET: Cosplayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cosplayers == null)
            {
                return NotFound();
            }

            var cosplayer = await _context.Cosplayers
                .Include(c => c.Cosplayer1)
                .Include(c => c.CosplayerNavigation)
                .FirstOrDefaultAsync(m => m.CosplayerId == id);
            if (cosplayer == null)
            {
                return NotFound();
            }

            return View(cosplayer);
        }

        // POST: Cosplayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cosplayers == null)
            {
                return Problem("Entity set 'Lab1Context.Cosplayers'  is null.");
            }
            var cosplayer = await _context.Cosplayers.FindAsync(id);
            if (cosplayer != null)
            {
                _context.Cosplayers.Remove(cosplayer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosplayerExists(int id)
        {
          return (_context.Cosplayers?.Any(e => e.CosplayerId == id)).GetValueOrDefault();
        }
    }
}
