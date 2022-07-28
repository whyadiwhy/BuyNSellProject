using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommProject.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ECommProject.Controllers
{
    public class ProductsController : Controller
    {
        public IHostingEnvironment _env;

        private readonly ProjectContext _context;

        public ProductsController(ProjectContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.product.Include(p => p.category);
            return View(await projectContext.ToListAsync());
        }
        public ActionResult Category()
        {
            return View(_context.category.ToList());
        }
      
        public async Task<IActionResult> AddedProducts(int id)
        {
                ViewBag.id = new SelectList(_context.category.ToList(), "id", "name");
                var projectContext = _context.product.Where(e => e.CategoryId == id).Include(prod => prod.category);
                return View(await projectContext.ToListAsync());
        }
           // return View(_context.product.Where(e => e.CategoryId == id).ToList());
            
        

        [HttpPost]
        public async Task<IActionResult> AddedProducts(List<Product> prodlist)
        {
            
            foreach (Product prod in prodlist)
            {
                if (prod.check)
                {   
                   
                    int categoryid = _context.product.Find(prod.id).CategoryId;
                    int qty = _context.inventory.Single(e => e.ProductId == prod.id).Quantity;
                    CartItem cp = new CartItem()
                    {
                        categoryId = categoryid,
                        productid= prod.id,
                        Quantity = qty
                    };
                    _context.cart.Add(cp);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "CartItems");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.product == null)
            {
                return NotFound();
            }

            var product = await _context.product
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.category, "id", "id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ProductName,ProductDesc,CategoryId,ProductPrice,formFile,path")] Product product)
        {
            var nam = Path.Combine(_env.WebRootPath + "/Images", Path.GetFileName(product.formFile.FileName));
            product.formFile.CopyTo(new FileStream(nam, FileMode.Create));
            product.path = "Images/" + product.formFile.FileName;
            _context.product.Add(product);
            await _context.SaveChangesAsync();
            int qty = int.Parse(Request.Form["qty"].ToString());
            Inventory inv = new Inventory();
            inv.price = product.ProductPrice;
            inv.Quantity = qty;
            inv.ProductId = product.id;
            _context.Add(inv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));            
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.product == null)
            {
                return NotFound();
            }

            var product = await _context.product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.category, "id", "id", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ProductName,ProductDesc,CategoryId,ProductPrice")] Product product)
        {
            if (id != product.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.id))
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
            return View(product);

        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.product == null)
            {
                return NotFound();
            }

            var product = await _context.product
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.product == null)
            {
                return Problem("Entity set 'ProjectContext.product'  is null.");
            }
            var product = await _context.product.FindAsync(id);
            if (product != null)
            {
                _context.product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.product?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
