using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models.ViewModel
{
    public class OrderInfomationViewModel
    {
        public Order Order { get; set; }
        public List<ApplicationUser> SalesPerson { get; set; }
        public List<Product> Products { get; set; }
    }
}
