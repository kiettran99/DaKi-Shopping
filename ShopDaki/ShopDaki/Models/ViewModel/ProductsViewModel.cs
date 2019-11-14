using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models.ViewModel
{
    public class ProductsViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<GroupProduct> GroupProducts { get; set; }
    }
}
