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
    public class ProductSoldsController : Controller
    {
        private readonly ProjectContext _context;

        public ProductSoldsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: ProductSolds
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.productSold.Include(p => p.sale);
            return View(await projectContext.ToListAsync());
        }

        // GET: ProductSolds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.productSold == null)
            {
                return NotFound();
            }

            var productSold = await _context.productSold
                .Include(p => p.sale)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSold == null)
            {
                return NotFound();
            }

            return View(productSold);
        }

        // GET: ProductSolds/Create
        public IActionResult Create()
        {
            ViewData["saleId"] = new SelectList(_context.sales, "id", "id");
            return View();
        }

        // POST: ProductSolds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,saleId,Name")] ProductSold productSold)
        {
           
                _context.Add(productSold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: ProductSolds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.productSold == null)
            {
                return NotFound();
            }

            var productSold = await _context.productSold.FindAsync(id);
            if (productSold == null)
            {
                return NotFound();
            }
            ViewData["saleId"] = new SelectList(_context.sales, "id", "id", productSold.saleId);
            return View(productSold);
        }

        // POST: ProductSolds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,saleId,Name")] ProductSold productSold)
        {
            if (id != productSold.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(productSold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSoldExists(productSold.Id))
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

        // GET: ProductSolds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.productSold == null)
            {
                return NotFound();
            }

            var productSold = await _context.productSold
                .Include(p => p.sale)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSold == null)
            {
                return NotFound();
            }

            return View(productSold);
        }

        // POST: ProductSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.productSold == null)
            {
                return Problem("Entity set 'ProjectContext.productSold'  is null.");
            }
            var productSold = await _context.productSold.FindAsync(id);
            if (productSold != null)
            {
                _context.productSold.Remove(productSold);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSoldExists(int id)
        {
          return (_context.productSold?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
