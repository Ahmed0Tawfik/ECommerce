using ECommerce.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.Models
{
    public class ShoppingCart : IBaseEntity
    {
        public int ID { get; set; }

        // Relationships

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
