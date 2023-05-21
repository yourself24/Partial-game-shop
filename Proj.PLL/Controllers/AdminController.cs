using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj.BLL.Services.Contracts;
using Proj.BLL.Services.Proj.BLL.Services;
using Proj.DAL.DataContext;
using Proj.DAL.Models;

namespace Proj.PLL.Controllers
{
    public class AdminController : Controller
    {
        private readonly VshopContext _context;
        private readonly IAdminService _adminService;
        private readonly IDeveloperService _devService;
        private readonly IGameService _gameService;

        public AdminController(VshopContext context,IAdminService adminService, IDeveloperService developerService,IGameService gameService)
        {
            _context = context;
            _adminService = adminService;
            _devService = developerService;
            _gameService = gameService;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
              return _context.Admins != null ? 
                          View(await _context.Admins.ToListAsync()) :
                          Problem("Entity set 'VshopContext.Admins'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                await _adminService.CreateAdmin(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public IActionResult IndexAdmin() { return View(); }

        [HttpPost]
        public async Task<IActionResult> CreatePromotion(string developerName)
        {
            var dev = _devService.GetDevByName(developerName);
            int id = dev.Id;
            
            Console.WriteLine(dev.Name);
            try
            {
                var games = await _gameService.ReadGames2();
                while(await games.HasNextAsync())
                {
                    var game = await games.NextAsync();
                    if (game.Developer == id)
                    {
                        if(game.Price <= 20)
                        {
                            game.Price -= 7;
                        }
                        else if (game.Price >20 && game.Price<= 40)
                        {
                            game.Price -= 15;
                        }
                        else
                        {
                            game.Price -= 20;
                        }
                        await _gameService.UpdateGame(game.Id,game.Name,game.Genre,game.Developer,game.Platform,game.Price,game.ReleaseDate,game.Stock);
                    }
                }
            }
            catch
            {
                throw;
            }
            return RedirectToAction("IndexAdmin");
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _adminService.ReadOneAdmin(id.Value);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                    await _adminService.UpdateAdmin(admin.Id,admin.Email,admin.Password);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(admin);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admins == null)
            {
                return Problem("Entity set 'VshopContext.Admins'  is null.");
            }
            var admin = await _adminService.ReadOneAdmin(id);
            if (admin != null)
            {
                _adminService.DeleteAdmin(admin.Id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
          return (_context.Admins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
