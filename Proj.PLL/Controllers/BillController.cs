using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj.BLL.Services;
using Proj.BLL.Services.Contracts;
using Proj.BLL.Services.Proj.BLL.Services;
using Proj.DAL.DataContext;
using Proj.DAL.Models;

namespace Proj.PLL.Controllers
{
    public class BillController : Controller
    {
        private readonly VshopContext _context;
        private readonly ICartItemService _itemService;
        private readonly IGameService _gameService;
        private readonly IShippingService _shippingService;
        private readonly IUserService _userService;

        public BillController(VshopContext context,ICartItemService itemService, IGameService gameService, IShippingService shippingService, IUserService userService)
        {
            _context = context;
            _itemService = itemService;
            _gameService = gameService;
            _shippingService = shippingService;
            _userService = userService;
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
                

                ViewData["Cart"] = new SelectList(_context.Carts, "Id", "Id", bill.Cart);
                ViewData["PaymentMethod"] = new SelectList(_context.UserPayments, "Id", "Id", bill.PaymentMethod);
                ViewData["Shipment"] = new SelectList(_context.Shippings, "Id", "Name", bill.Shipment);
                var items = await _itemService.ReadItems2();
                while (await items.HasNextAsync())
                {
                    var item = await items.NextAsync();
                    //var game = await _gameService.ReadOneGame(item.Game.Value);
                    if (bill.Cart == item.Cart)
                    {
                        bill.Price = bill.Price + item.Price;
                    }
                }
                var shipment = await _shippingService.ReadOne(bill.Shipment.Value);
                bill.Price += (decimal.ToInt32(shipment.Price.Value));
                var cart = await _context.Carts.FindAsync(bill.Cart);
                var user = await _userService.ReadOneUser(cart.UserId.Value);
                var id =HttpContext.Session.GetInt32("UserId");
                Console.WriteLine(user.Id);
                Console.WriteLine(id);
                if(user.Id == id)
                {
                    bill.Price = bill.Price - (20 * (user.Purchases % 5));
                    user.Purchases++;
                    _context.Update(user);
                    Console.WriteLine("Bill Price is: " + bill.Price);
                    _context.Add(bill);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new ArgumentException("You can only create bills for your own order!");
                }
               
            }
        
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
