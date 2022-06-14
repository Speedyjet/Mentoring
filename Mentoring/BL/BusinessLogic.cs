using Mentoring.Models;
using Microsoft.EntityFrameworkCore;

namespace Mentoring.BL
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<BusinessLogic> _logger;

        public BusinessLogic(NorthwindContext context, ILogger<BusinessLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddCategory(Category category)
        {
            await _context.AddAsync(category);
            _context.SaveChanges();
        }

        public bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }

        public async Task<IList<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int? id)
        {
            if (id != null)
            {
                return await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
            }
            return null;
        }

        async Task<byte[]> IBusinessLogic.GetImageById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category?.Picture == null)
            {
                return null;
            }
            return category.Picture.ToArray();
        }

        public async Task RemoveCategory(Category category)
        {
            _context.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
