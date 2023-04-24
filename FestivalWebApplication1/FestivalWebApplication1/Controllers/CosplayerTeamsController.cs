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
    public class CosplayerTeamsController : Controller
    {
        private readonly Lab1Context _context;

        public CosplayerTeamsController(Lab1Context context)
        {
            _context = context;
        }

        // GET: CosplayerTeams
        public async Task<IActionResult> Index()
        {
              return _context.CosplayerTeams != null ? 
                          View(await _context.CosplayerTeams.ToListAsync()) :
                          Problem("Entity set 'Lab1Context.CosplayerTeams'  is null.");
        }

        // GET: CosplayerTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CosplayerTeams == null)
            {
                return NotFound();
            }

            var cosplayerTeam = await _context.CosplayerTeams
                .FirstOrDefaultAsync(m => m.CosplayerTeamId == id);
            if (cosplayerTeam == null)
            {
                return NotFound();
            }

            return View(cosplayerTeam);
        }

        // GET: CosplayerTeams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CosplayerTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CosplayerTeamId,CosplayerTeamName")] CosplayerTeam cosplayerTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cosplayerTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cosplayerTeam);
        }

        // GET: CosplayerTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CosplayerTeams == null)
            {
                return NotFound();
            }

            var cosplayerTeam = await _context.CosplayerTeams.FindAsync(id);
            if (cosplayerTeam == null)
            {
                return NotFound();
            }
            return View(cosplayerTeam);
        }

        // POST: CosplayerTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CosplayerTeamId,CosplayerTeamName")] CosplayerTeam cosplayerTeam)
        {
            if (id != cosplayerTeam.CosplayerTeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cosplayerTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosplayerTeamExists(cosplayerTeam.CosplayerTeamId))
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
            return View(cosplayerTeam);
        }

        // GET: CosplayerTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CosplayerTeams == null)
            {
                return NotFound();
            }

            var cosplayerTeam = await _context.CosplayerTeams
                .FirstOrDefaultAsync(m => m.CosplayerTeamId == id);
            if (cosplayerTeam == null)
            {
                return NotFound();
            }

            return View(cosplayerTeam);
        }

        // POST: CosplayerTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CosplayerTeams == null)
            {
                return Problem("Entity set 'Lab1Context.CosplayerTeams'  is null.");
            }
            var cosplayerTeam = await _context.CosplayerTeams.FindAsync(id);
            if (cosplayerTeam != null)
            {
                _context.CosplayerTeams.Remove(cosplayerTeam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosplayerTeamExists(int id)
        {
          return (_context.CosplayerTeams?.Any(e => e.CosplayerTeamId == id)).GetValueOrDefault();
        }
    }
}
