using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, 100000.00, ErrorMessage = "Total amount must be between 0.01 and 100,000")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Order status is required")]
        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }

        // Navigation property

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
