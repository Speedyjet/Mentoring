using Mentoring.Models;

namespace Mentoring.BL
{
    public interface ICategoryService
    {
        public Task<IList<Category>> GetCategories();
        public Task<Category> GetCategory(int? id);
        public Task AddCategory(Category category);
        public Task UpdateCategory(Category category);
        public Task RemoveCategory(Category category);
        bool CategoryExists(int id);
        public Task<byte[]> GetImageById(int id);
        void UpdateImage(byte[] image, int id);
    }
}