using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class CheckOutViewModel
    {
  
        public ApplicationUser? User { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string OrderFullName { get; set; }
        public string OrderPhoneNumber { get; set; }
        public string? OrderOptionalEmail { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


    }
}
