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
    public class ShippingController : Controller
    {
        private readonly VshopContext _context;

        public ShippingController(VshopContext context)
        {
            _context = context;
        }

        // GET: Shipping
        public async Task<IActionResult> Index()
        {
              return _context.Shippings != null ? 
                          View(await _context.Shippings.ToListAsync()) :
                          Problem("Entity set 'VshopContext.Shippings'  is null.");
        }

        // GET: Shipping/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shippings == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shippings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // GET: Shipping/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,DeliveryTime")] Shipping shipping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipping);
        }

        // GET: Shipping/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shippings == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping == null)
            {
                return NotFound();
            }
            return View(shipping);
        }

        // POST: Shipping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,DeliveryTime")] Shipping shipping)
        {
            if (id != shipping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingExists(shipping.Id))
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
            return View(shipping);
        }

        // GET: Shipping/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shippings == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shippings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // POST: Shipping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shippings == null)
            {
                return Problem("Entity set 'VshopContext.Shippings'  is null.");
            }
            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping != null)
            {
                _context.Shippings.Remove(shipping);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingExists(int id)
        {
          return (_context.Shippings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
