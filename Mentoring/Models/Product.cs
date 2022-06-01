using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mentoring.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Please provide a valid product name")]
        [MinLength(1)]
        [MaxLength(20)]
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        [Display(Name = "Quantity per unit")]
        [Required(ErrorMessage = "Please provide a valid quantity per unit")]
        [MinLength(1)]
        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }

        [Display(Name = "Unit price")]
        [Required(ErrorMessage = "Please provide a valid price per unit")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Units in stock")]
        public short? UnitsInStock { get; set; }

        [Display(Name = "Units in order")]
        public short? UnitsOnOrder { get; set; }

        [Display(Name = "Reorder level")]
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
