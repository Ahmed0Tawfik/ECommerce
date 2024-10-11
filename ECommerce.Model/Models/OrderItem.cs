using ECommerce.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.Models
{
    public class OrderItem : IBaseEntity
    {
            public int ID { get; set; }

            [Required(ErrorMessage = "Quantity is required")]
            [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1,000")]
            public int Quantity { get; set; }


            [Required(ErrorMessage = "Price is required")]
            [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10,000")]
            public decimal Price { get; set; }

            //navigation property

            [Required(ErrorMessage = "Order ID is required")]
            public int OrderId { get; set; }
            public virtual Order Order { get; set; }


            [Required(ErrorMessage = "Product ID is required")]
            public int ProductId { get; set; }
            public virtual Product Product { get; set; }
    }
}
