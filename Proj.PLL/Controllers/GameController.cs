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
    public class GameController : Controller
    {
        private readonly VshopContext _context;

        public GameController(VshopContext context)
        {
            _context = context;
        }

        // GET: Game
        public async Task<IActionResult> Index()
        {
            var vshopContext = _context.Games.Include(g => g.DeveloperNavigation).Include(g => g.GenreNavigation).Include(g => g.PlatformNavigation);
            return View(await vshopContext.ToListAsync());
        }

        // GET: Game/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.DeveloperNavigation)
                .Include(g => g.GenreNavigation)
                .Include(g => g.PlatformNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            ViewData["Developer"] = new SelectList(_context.Developers, "Id", "Name");
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["Platform"] = new SelectList(_context.Platforms, "Id", "Name");
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,Developer,Platform,Price,ReleaseDate,Stock")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Developer"] = new SelectList(_context.Developers, "Id", "Name", game.Developer);
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Name", game.Genre);
            ViewData["Platform"] = new SelectList(_context.Platforms, "Id", "Name", game.Platform);
            return View(game);
        }

        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["Developer"] = new SelectList(_context.Developers, "Id", "Name", game.Developer);
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Name", game.Genre);
            ViewData["Platform"] = new SelectList(_context.Platforms, "Id", "Name", game.Platform);
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,Developer,Platform,Price,ReleaseDate,Stock")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["Developer"] = new SelectList(_context.Developers, "Id", "Name", game.Developer);
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Name", game.Genre);
            ViewData["Platform"] = new SelectList(_context.Platforms, "Id", "Name", game.Platform);
            return View(game);
        }

        // GET: Game/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.DeveloperNavigation)
                .Include(g => g.GenreNavigation)
                .Include(g => g.PlatformNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'VshopContext.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
          return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
