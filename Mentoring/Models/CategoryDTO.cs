using System.ComponentModel.DataAnnotations;

namespace Mentoring.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please provide a valid category name")]
        [MinLength(1)]
        [MaxLength(20)]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public IFormFile Picture { get; set; }
    }
}
