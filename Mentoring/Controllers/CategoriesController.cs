using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mentoring.Models;
using Microsoft.AspNetCore.Diagnostics;
using Mentoring.BL;

namespace Mentoring.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService businessLogic, IConfiguration configuration, NorthwindContext context, ILogger<CategoriesController> logger)
        {
            _categoryService = businessLogic;
            _configuration = configuration;
            _logger = logger;
            logger.LogInformation($"The application location is {configuration.GetValue<string>(WebHostDefaults.ContentRootKey)}");

        }

        // GET: Categories
        [ToBeLoggedFilter(true)]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetCategories());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _categoryService.GetCategories() == null)
            {
                _logger.Log(LogLevel.Warning, "Categories are empty");
                return NotFound();
            }

            var category = await _categoryService.GetCategory(id);
            if (category == null)
            {
                _logger.Log(LogLevel.Warning, "Category not found");
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,Picture")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _categoryService.GetCategories() == null)
            {
                _logger.Log(LogLevel.Warning, "Cannot find category");
                return NotFound();
            }

            var category = await _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture")] CategoryDTO categoryDto)
        {
            if (!_categoryService.CategoryExists(id))
            {
                _logger.Log(LogLevel.Warning, "Cannot find category");
                return NotFound();
            }
            
            var currentCategory = await _categoryService.GetCategory(id);
            byte[]? imageData = null;
            using (var binaryReader = new BinaryReader(categoryDto.Picture.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)categoryDto.Picture.Length);
            }
            currentCategory.Picture = imageData;
            
            if (ModelState.IsValid)
            {
                try
                {

                    await _categoryService.UpdateCategory(currentCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(id))
                    {
                        _logger.Log(LogLevel.Warning, "Cannot find category");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("Something went wrong");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(currentCategory);
        }

        public async Task<byte[]> GetImageById(int id)
        {
            return await _categoryService.GetImageById(id);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _categoryService.GetCategories() == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_categoryService.GetCategories() == null)
            {
                return Problem("Entity set 'NorthwindContext.Categories'  is null.");
            }
            var category = await _categoryService.GetCategory(id);
            if (category != null)
            {
                await _categoryService.RemoveCategory(category);
            }
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;
            _logger.LogError("Server throws an exception", exception);
            return View("~/Views/Shared/Error.cshtml", exception);
        }

        private bool CategoryExists(int id)
        {
            return _categoryService.CategoryExists(id);
        }
    }
}
