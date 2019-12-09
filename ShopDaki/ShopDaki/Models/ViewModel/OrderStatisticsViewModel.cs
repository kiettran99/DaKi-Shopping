using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models.ViewModel
{
    public class OrderStatisticsViewModel
    {
        public List<Order> Orders { get; set; }
        public int SelectedYear { get; set; }

        public OrderStatisticsViewModel()
        {
            SelectedYear = DateTime.Now.Year;
        }
    }
}
