using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mentoring.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Mentoring.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _context;
        private readonly int MaxProductsToShow;

        public ProductsController(NorthwindContext context, IConfiguration configuration, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            logger.Log(LogLevel.Information, "Products controller is running");
            if (!int.TryParse(_configuration.GetRequiredSection(nameof(MaxProductsToShow)).Value, out MaxProductsToShow)) {
                logger.Log(LogLevel.Warning, "Cannot read the maximum amout of products to show");
            }
            logger.Log(LogLevel.Information, $"MaxProductsToShow is set to {MaxProductsToShow}");
            logger.LogInformation($"The application location is {configuration.GetValue<string>(WebHostDefaults.ContentRootKey)}");
        }

        // GET: Products
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var northwindContext = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier);
            if (MaxProductsToShow > 0)
            {
                return View(await northwindContext.Take(MaxProductsToShow).OrderBy(x => x.ProductName).ToListAsync());
            }
            return View(await northwindContext.ToListAsync());
        }

        // GET: Products/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.Categories.Select(x => x.CategoryName)); ;
            ViewData["Supplier"] = new SelectList(_context.Suppliers.Select(x => x.CompanyName));
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Supplier,Category,QuantityPerUnit,UnitPrice," +
            "UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            product.CategoryId = _context.Categories.FirstOrDefault(x => x.CategoryName == product.Category.CategoryName).CategoryId;
            product.SupplierId = _context.Suppliers.FirstOrDefault(x => x.CompanyName == product.Supplier.CompanyName).SupplierId;
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.Categories.Select(x => x.CategoryName)); ;
            ViewData["Supplier"] = new SelectList(_context.Suppliers.Select(x => x.CompanyName));
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // TODO: t3 After receiving server-side error will not pop up the display values for select
            ViewData["Category"] = new SelectList(_context.Categories.Select(x => x.CategoryName), product.CategoryId);
            ViewData["Supplier"] = new SelectList(_context.Suppliers.Select(x => x.CompanyName), product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Supplier,Category,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (id != product.ProductId)
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
                    if (!ProductExists(product.ProductId))
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
            ViewData["Category"] = new SelectList(_context.Categories.Select(x => x.CategoryName), product.CategoryId);
            ViewData["Supplier"] = new SelectList(_context.Suppliers.Select(x => x.CompanyName), product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
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
            if (_context.Products == null)
            {
                return Problem("Entity set 'NorthwindContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;
            _logger.LogError("Server throws an exception", exception);
            return View("~/Views/Shared/Error.cshtml", exception);
        }


        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
