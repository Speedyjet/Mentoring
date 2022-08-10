using Mentoring.Models;
using Microsoft.EntityFrameworkCore;

namespace Mentoring.BL
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<CategoryService> _logger;

        public ProductService(NorthwindContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IList<Product>> GetProducts()
        {
            _logger.LogInformation("getting all the categories");
            return await _context.Products.ToListAsync();
        }
    }
}
