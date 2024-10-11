using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Review content is required")]
        [StringLength(500, ErrorMessage = "Review content cannot be longer than 500 characters")]
        public string Content { get; set; }

        // Navigation property
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
