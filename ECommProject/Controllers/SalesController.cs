using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommProject.Models;

namespace ECommProject.Controllers
{
    public class SalesController : Controller
    {
        private readonly ProjectContext _context;

        public SalesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
              return _context.sales != null ? 
                          View(await _context.sales.ToListAsync()) :
                          Problem("Entity set 'ProjectContext.sales'  is null.");
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.sales == null)
            {
                return NotFound();
            }

            var sales = await _context.sales
                .FirstOrDefaultAsync(m => m.id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,totalPrice,timeStamp")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sales);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.sales == null)
            {
                return NotFound();
            }

            var sales = await _context.sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,totalPrice,timeStamp")] Sales sales)
        {
            if (id != sales.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.id))
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
            return View(sales);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.sales == null)
            {
                return NotFound();
            }

            var sales = await _context.sales
                .FirstOrDefaultAsync(m => m.id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.sales == null)
            {
                return Problem("Entity set 'ProjectContext.sales'  is null.");
            }
            var sales = await _context.sales.FindAsync(id);
            if (sales != null)
            {
                _context.sales.Remove(sales);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
          return (_context.sales?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
