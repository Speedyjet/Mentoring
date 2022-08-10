using Mentoring.Models;

namespace Mentoring.BL
{
    public interface IProductService
    {
        public Task<IList<Product>> GetProducts();
    }
}