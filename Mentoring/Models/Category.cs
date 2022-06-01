using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mentoring.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please provide a valid category name")]
        [MinLength(1)]
        [MaxLength(20)]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please provide a valid description")]
        [MinLength(1)]
        [MaxLength(20)]
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
