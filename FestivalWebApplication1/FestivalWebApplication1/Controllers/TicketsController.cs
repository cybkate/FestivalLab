﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FestivalWebApplication1.Models;

namespace FestivalWebApplication1.Controllers
{
    public class TicketsController : Controller
    {
        private readonly Lab1Context _context;

        public TicketsController(Lab1Context context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var lab1Context = _context.Tickets.Include(t => t.Guest).Include(t => t.Guest1).Include(t => t.GuestNavigation);
            return View(await lab1Context.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Guest)
                .Include(t => t.Guest1)
                .Include(t => t.GuestNavigation)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["GuestId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId");
            ViewData["GuestId"] = new SelectList(_context.Participants, "ParticipantId", "ParticipantId");
            ViewData["GuestId"] = new SelectList(_context.Cosplayers, "CosplayerId", "CosplayerId");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,TicketPrice,TicketType,TicketDate,GuestId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", ticket.GuestId);
            ViewData["GuestId"] = new SelectList(_context.Participants, "ParticipantId", "ParticipantId", ticket.GuestId);
            ViewData["GuestId"] = new SelectList(_context.Cosplayers, "CosplayerId", "CosplayerId", ticket.GuestId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", ticket.GuestId);
            ViewData["GuestId"] = new SelectList(_context.Participants, "ParticipantId", "ParticipantId", ticket.GuestId);
            ViewData["GuestId"] = new SelectList(_context.Cosplayers, "CosplayerId", "CosplayerId", ticket.GuestId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,TicketPrice,TicketType,TicketDate,GuestId")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            ViewData["GuestId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", ticket.GuestId);
            ViewData["GuestId"] = new SelectList(_context.Participants, "ParticipantId", "ParticipantId", ticket.GuestId);
            ViewData["GuestId"] = new SelectList(_context.Cosplayers, "CosplayerId", "CosplayerId", ticket.GuestId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Guest)
                .Include(t => t.Guest1)
                .Include(t => t.GuestNavigation)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'Lab1Context.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
          return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
