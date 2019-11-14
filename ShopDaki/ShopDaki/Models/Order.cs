using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopDaki.Models
{

    [Table("Order")]
    public class Order
    {
        public int OrderID { get; set; }
        public float TotalMoney { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string Status { get; set; }
    }
}
