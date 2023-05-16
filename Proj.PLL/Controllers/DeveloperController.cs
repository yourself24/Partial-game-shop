using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj.BLL.Services.Contracts;
using Proj.DAL.DataContext;
using Proj.DAL.Models;

namespace Proj.PLL.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly VshopContext _context;
        private readonly IDeveloperService _devService;

        public DeveloperController(VshopContext context,IDeveloperService developerService)
        {
            _context = context;
            _devService = developerService;
        }

        // GET: Developer
        public async Task<IActionResult> Index()
        {
              return _context.Developers != null ? 
                          View(await _context.Developers.ToListAsync()) :
                          Problem("Entity set 'VshopContext.Developers'  is null.");
        }

        // GET: Developer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Developers == null)
            {
                return NotFound();
            }

            var developer = await _context.Developers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (developer == null)
            {
                return NotFound();
            }

            return View(developer);
        }

        // GET: Developer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Developer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Founded")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                await _devService.AddDev(developer);
                return RedirectToAction(nameof(Index));
            }
            return View(developer);
        }

        // GET: Developer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Developers == null)
            {
                return NotFound();
            }

            var developer = await _context.Developers.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }
            return View(developer);
        }

        // POST: Developer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Founded")] Developer developer)
        {
            if (id != developer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _devService.UpdateDev(developer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeveloperExists(developer.Id))
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
            return View(developer);
        }

        // GET: Developer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Developers == null)
            {
                return NotFound();
            }

            var developer = await _devService.GetDev(id.Value);
            if (developer == null)
            {
                return NotFound();
            }

            return View(developer);
        }

        // POST: Developer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Developers == null)
            {
                return Problem("Entity set 'VshopContext.Developers'  is null.");
            }
            var developer = await _devService.GetDev(id);
            if (developer != null)
            {
               await _devService.DeleteDev(id);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeveloperExists(int id)
        {
          return (_context.Developers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
