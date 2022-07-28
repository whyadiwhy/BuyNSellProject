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
    public class CartItemsController : Controller
    {
        private readonly ProjectContext _context;

        public CartItemsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.cart.Include(c => c.category);
            return View(await projectContext.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.cart == null)
            {
                return NotFound();
            }

            var cartItem = await _context.cart
                .Include(c => c.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["categoryId"] = new SelectList(_context.category, "id", "id");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,categoryId,productid,Quantity")] CartItem cartItem)
        {
            
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.cart == null)
            {
                return NotFound();
            }

            var cartItem = await _context.cart.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["categoryId"] = new SelectList(_context.category, "id", "id", cartItem.categoryId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,categoryId,productid,Quantity")] CartItem cartItem)
        {
            if (id != cartItem.Id)
            {
                return NotFound();
            }

            
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

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.cart == null)
            {
                return NotFound();
            }

            var cartItem = await _context.cart
                .Include(c => c.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.cart == null)
            {
                return Problem("Entity set 'ProjectContext.cart'  is null.");
            }
            var cartItem = await _context.cart.FindAsync(id);
            if (cartItem != null)
            {
                _context.cart.Remove(cartItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
          return (_context.cart?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
