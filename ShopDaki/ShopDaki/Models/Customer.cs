using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomersID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }

        public string Provice { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
