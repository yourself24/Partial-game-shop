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
    public class PayMethodController : Controller
    {
        private readonly VshopContext _context;

        public PayMethodController(VshopContext context)
        {
            _context = context;
        }

        // GET: PayMethod
        public async Task<IActionResult> Index()
        {
              return _context.PayMethods != null ? 
                          View(await _context.PayMethods.ToListAsync()) :
                          Problem("Entity set 'VshopContext.PayMethods'  is null.");
        }

        // GET: PayMethod/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PayMethods == null)
            {
                return NotFound();
            }

            var payMethod = await _context.PayMethods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payMethod == null)
            {
                return NotFound();
            }

            return View(payMethod);
        }

        // GET: PayMethod/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PayMethod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PayMethod payMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payMethod);
        }

        // GET: PayMethod/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PayMethods == null)
            {
                return NotFound();
            }

            var payMethod = await _context.PayMethods.FindAsync(id);
            if (payMethod == null)
            {
                return NotFound();
            }
            return View(payMethod);
        }

        // POST: PayMethod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PayMethod payMethod)
        {
            if (id != payMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayMethodExists(payMethod.Id))
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
            return View(payMethod);
        }

        // GET: PayMethod/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PayMethods == null)
            {
                return NotFound();
            }

            var payMethod = await _context.PayMethods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payMethod == null)
            {
                return NotFound();
            }

            return View(payMethod);
        }

        // POST: PayMethod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PayMethods == null)
            {
                return Problem("Entity set 'VshopContext.PayMethods'  is null.");
            }
            var payMethod = await _context.PayMethods.FindAsync(id);
            if (payMethod != null)
            {
                _context.PayMethods.Remove(payMethod);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayMethodExists(int id)
        {
          return (_context.PayMethods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
