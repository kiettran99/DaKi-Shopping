using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopDaki.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }

        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
