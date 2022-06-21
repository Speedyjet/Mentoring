using Mentoring.Models;

namespace Mentoring.BL
{
    public interface IBusinessLogic
    {
        public Task<IList<Category>> GetCategories();
        public Task<Category> GetCategory(int? id);
        public Task AddCategory(Category category);
        public Task UpdateCategory(Category category);
        public Task RemoveCategory(Category category);
        bool CategoryExists(int id);
        Task<byte[]> GetImageById(int id);
    }
}