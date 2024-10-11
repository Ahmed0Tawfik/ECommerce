using ECommerce.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.Models
{
    public class Product : IBaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10,000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, 5000, ErrorMessage = "Stock must be between 0 and 10,000")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        // Relationships
        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
