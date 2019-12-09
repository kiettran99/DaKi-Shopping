using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopDaki.Models;
using ShopDaki.Models.ViewModel;

namespace ShopDaki.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderStatisticsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public OrderStatisticsViewModel OrderStatisticVM { get; set; }

        public OrderStatisticsController(ApplicationDbContext db)
        {
            _db = db;

            OrderStatisticVM = new OrderStatisticsViewModel()
            {
                Orders = new List<Order>()
            };
        }

        public IActionResult Index(int SelectedYear = 0)
        {
            OrderStatisticVM.Orders = _db.Orders.ToList();

            if (SelectedYear != 0)
            {
                OrderStatisticVM.SelectedYear = SelectedYear;
            }

            return View(OrderStatisticVM);
        }
    }
}