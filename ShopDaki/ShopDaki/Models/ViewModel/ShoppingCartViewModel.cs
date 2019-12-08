using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<Product> Products { get; set; }
        public Order Order { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}
