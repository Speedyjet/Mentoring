using Mentoring.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Mentoring.BL
{
    public class CategoryService : ICategoryService
    {
        private readonly IHddCache _hddCache;
        private readonly NorthwindContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(NorthwindContext context, ILogger<CategoryService> logger, IHddCache hddCache)
        {
            _hddCache = hddCache;
            _context = context;
            _logger = logger;
        }

        public async Task AddCategory(Category category)
        {
            _logger.LogInformation("adding category", category);
            await _context.AddAsync(category);
            _context.SaveChanges();
        }

        public bool CategoryExists(int id)
        {
            _logger.LogInformation("checking if the category exists", id);
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }

        public async Task<IList<Category>> GetCategories()
        {
            _logger.LogInformation("getting all the categories");
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int? id)
        {
            _logger.LogInformation("getting category by id", id);
            if (id != null)
            {
                return await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
            }
            return null;
        }

         public async Task<byte[]> GetImageById(int id)
        {
            _logger.LogInformation("getting image by id", id);
            byte[]? imageData = null;
            if (_hddCache.TryGetValue(id, out imageData))
            {
                return imageData ?? new byte[0];
            }
            else
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (category?.Picture == null)
                {
                    return null;
                }
                imageData = category.Picture.ToArray();
                _hddCache.SaveItem(id, imageData);//, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                return imageData;
            }
        }

        public async Task RemoveCategory(Category category)
        {
            _logger.LogInformation("removing category", category);
            _context.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _logger.LogInformation("updating category", category);
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public void UpdateImage(byte[] image, int id)
        {
            var currentImage = GetImageById(id).Result;
            if (currentImage != null)
            {
                image.CopyTo(currentImage, 0);
                _context.Update(currentImage);
                _context.SaveChanges();
            }
        }
    }
}
