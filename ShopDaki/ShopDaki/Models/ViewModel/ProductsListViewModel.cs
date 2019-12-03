using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models.ViewModel
{
    public class ProductsListViewModel
    {
        public List<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public List<GroupProduct> GroupProducts { get; set; }
    }
}
