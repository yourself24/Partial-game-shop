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
    public class BillController : Controller
    {
        private readonly VshopContext _context;

        public BillController(VshopContext context)
        {
            _context = context;
        }

        // GET: Bill
        public async Task<IActionResult> Index()
        {
            var vshopContext = _context.Bills.Include(b => b.CartNavigation).Include(b => b.PaymentMethodNavigation).Include(b => b.ShipmentNavigation);
            return View(await vshopContext.ToListAsync());
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bills == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.CartNavigation)
                .Include(b => b.PaymentMethodNavigation)
                .Include(b => b.ShipmentNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bill/Create
        public IActionResult Create()
        {
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id");
            ViewData["PaymentMethod"] = new SelectList(_context.UserPayments, "Id", "Id");
            ViewData["Shipment"] = new SelectList(_context.Shippings, "Id", "Name");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cart,Shipment,PaymentMethod,Price")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id", bill.Cart);
            ViewData["PaymentMethod"] = new SelectList(_context.UserPayments, "Id", "Id", bill.PaymentMethod);
            ViewData["Shipment"] = new SelectList(_context.Shippings, "Id", "Name", bill.Shipment);
            return View(bill);
        }

        // GET: Bill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bills == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id", bill.Cart);
            ViewData["PaymentMethod"] = new SelectList(_context.UserPayments, "Id", "Id", bill.PaymentMethod);
            ViewData["Shipment"] = new SelectList(_context.Shippings, "Id", "Name", bill.Shipment);
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cart,Shipment,PaymentMethod,Price")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id", bill.Cart);
            ViewData["PaymentMethod"] = new SelectList(_context.UserPayments, "Id", "Id", bill.PaymentMethod);
            ViewData["Shipment"] = new SelectList(_context.Shippings, "Id", "Name", bill.Shipment);
            return View(bill);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bills == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.CartNavigation)
                .Include(b => b.PaymentMethodNavigation)
                .Include(b => b.ShipmentNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bills == null)
            {
                return Problem("Entity set 'VshopContext.Bills'  is null.");
            }
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
          return (_context.Bills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
