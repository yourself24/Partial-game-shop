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
    public class UserPayController : Controller
    {
        private readonly VshopContext _context;
    

        public UserPayController(VshopContext context)
        {
            _context = context;
        }

        // GET: UserPay
        public async Task<IActionResult> Index()
        {
            var vshopContext = _context.UserPayments.Include(u => u.Method).Include(u => u.User);
            return View(await vshopContext.ToListAsync());
        }

        // GET: UserPay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserPayments == null)
            {
                return NotFound();
            }

            var userPayment = await _context.UserPayments
                .Include(u => u.Method)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPayment == null)
            {
                return NotFound();
            }

            return View(userPayment);
        }

        // GET: UserPay/Create
        public IActionResult Create()
        {
            ViewData["MethodId"] = new SelectList(_context.PayMethods, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: UserPay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,MethodId,CardNo,SecurityCode")] UserPayment userPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MethodId"] = new SelectList(_context.PayMethods, "Id", "Name", userPayment.MethodId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userPayment.UserId);
            return View(userPayment);
        }

        // GET: UserPay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserPayments == null)
            {
                return NotFound();
            }

            var userPayment = await _context.UserPayments.FindAsync(id);
            if (userPayment == null)
            {
                return NotFound();
            }
            ViewData["MethodId"] = new SelectList(_context.PayMethods, "Id", "Name", userPayment.MethodId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userPayment.UserId);
            return View(userPayment);
        }

        // POST: UserPay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,MethodId,CardNo,SecurityCode")] UserPayment userPayment)
        {
            if (id != userPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPaymentExists(userPayment.Id))
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
            ViewData["MethodId"] = new SelectList(_context.PayMethods, "Id", "Name", userPayment.MethodId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userPayment.UserId);
            return View(userPayment);
        }

        // GET: UserPay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserPayments == null)
            {
                return NotFound();
            }

            var userPayment = await _context.UserPayments
                .Include(u => u.Method)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPayment == null)
            {
                return NotFound();
            }

            return View(userPayment);
        }

        // POST: UserPay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserPayments == null)
            {
                return Problem("Entity set 'VshopContext.UserPayments'  is null.");
            }
            var userPayment = await _context.UserPayments.FindAsync(id);
            if (userPayment != null)
            {
                _context.UserPayments.Remove(userPayment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPaymentExists(int id)
        {
          return (_context.UserPayments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
