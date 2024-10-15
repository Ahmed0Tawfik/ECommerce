using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class OrderPaymentViewModel
    {
        public Order Order { get; set; }

        public string FullName { get; set; }

        public DateOnly CardExpiration { get; set; }

        public int CVV { get; set; }

        public string CardNumber { get; set; }
    }
}
