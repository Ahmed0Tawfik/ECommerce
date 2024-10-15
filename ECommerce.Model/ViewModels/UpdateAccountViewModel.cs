using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class UpdateAccountViewModel
    {
        public ApplicationUser? user { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
