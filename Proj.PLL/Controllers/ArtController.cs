using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj.DAL.DataContext;
using Proj.DAL.Models;

namespace Proj.PLL.Controllers
{
    public class ArtController : Controller
    {
        private readonly VshopContext _context;

        public ArtController(VshopContext context)
        {
            _context = context;
        }

        // GET: Art
        public async Task<IActionResult> Index()
        {
            var vshopContext = _context.GameArts.Include(g => g.Game);
            return View(await vshopContext.ToListAsync());
        }

        // GET: Art/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GameArts == null)
            {
                return NotFound();
            }

            var gameArt = await _context.GameArts
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameArt == null)
            {
                return NotFound();
            }

            return View(gameArt);
        }

        // GET: Art/Create
        public IActionResult Create()
        {
            ViewData["Gameid"] = new SelectList(_context.Games, "Id", "Name");
            return View();
        }

        // POST: Art/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gameid,Url,Description")] GameArt gameArt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameArt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gameid"] = new SelectList(_context.Games, "Id", "Name", gameArt.Gameid);
            return View(gameArt);
        }

        // GET: Art/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GameArts == null)
            {
                return NotFound();
            }

            var gameArt = await _context.GameArts.FindAsync(id);
            if (gameArt == null)
            {
                return NotFound();
            }
            ViewData["Gameid"] = new SelectList(_context.Games, "Id", "Name", gameArt.Gameid);
            return View(gameArt);
        }

        // POST: Art/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Gameid,Url,Description")] GameArt gameArt)
        {
            if (id != gameArt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameArt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameArtExists(gameArt.Id))
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
            ViewData["Gameid"] = new SelectList(_context.Games, "Id", "Name", gameArt.Gameid);
            return View(gameArt);
        }

        // GET: Art/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GameArts == null)
            {
                return NotFound();
            }

            var gameArt = await _context.GameArts
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameArt == null)
            {
                return NotFound();
            }

            return View(gameArt);
        }

        // POST: Art/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GameArts == null)
            {
                return Problem("Entity set 'VshopContext.GameArts'  is null.");
            }
            var gameArt = await _context.GameArts.FindAsync(id);
            if (gameArt != null)
            {
                _context.GameArts.Remove(gameArt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameArtExists(int id)
        {
          return (_context.GameArts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
