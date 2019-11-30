using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models.ViewModel
{
    public class OrderDetailsViewModel
    {
        public List<Order> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
