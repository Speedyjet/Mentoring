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
        public string CategoryName { get; set; } = null!;
        [Display(Name = "Description")]
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
