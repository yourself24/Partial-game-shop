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
    public class ItemController : Controller
    {
        private readonly VshopContext _context;

        public ItemController(VshopContext context)
        {
            _context = context;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var vshopContext = _context.CartItems.Include(c => c.CartNavigation).Include(c => c.GameNavigation);
            return View(await vshopContext.ToListAsync());
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.CartNavigation)
                .Include(c => c.GameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id");
            ViewData["Game"] = new SelectList(_context.Games, "Id", "Name");
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Game,Cart,Quantity,Price")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id", cartItem.Cart);
            ViewData["Game"] = new SelectList(_context.Games, "Id", "Name", cartItem.Game);
            return View(cartItem);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id", cartItem.Cart);
            ViewData["Game"] = new SelectList(_context.Games, "Id", "Name", cartItem.Game);
            return View(cartItem);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Game,Cart,Quantity,Price")] CartItem cartItem)
        {
            if (id != cartItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.Id))
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
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id", cartItem.Cart);
            ViewData["Game"] = new SelectList(_context.Games, "Id", "Name", cartItem.Game);
            return View(cartItem);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.CartNavigation)
                .Include(c => c.GameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartItems == null)
            {
                return Problem("Entity set 'VshopContext.CartItems'  is null.");
            }
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
          return (_context.CartItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
