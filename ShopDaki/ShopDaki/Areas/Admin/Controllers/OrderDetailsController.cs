using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ShopDaki.Utility;
using ShopDaki.Models.ViewModel;
using ShopDaki.Data;
using ShopDaki.Models;

namespace ShopDaki.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser + "," + SD.AdminEndUser)]
    [Area("Admin")]
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private int PageSize = 5;

        public OrderDetailsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int productPage = 1, string searchName = null, string searchEmail = null, string searchPhone = null, string searchDate = null)
        {
            //Get ID Curent User
            ClaimsPrincipal currenUser = this.User;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            OrderDetailsViewModel orderDetailsVM = new OrderDetailsViewModel()
            {
                Orders = new List<Models.Order>()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Admin/OrderDetails?productPage=:");

            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }

            param.Append("&searchEmail=");
            if (searchEmail != null)
            {
                param.Append(searchEmail);
            }

            param.Append("&searchPhone=");
            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }

            param.Append("&searchDate=");
            if (searchDate != null)
            {
                param.Append(searchDate);
            }

            orderDetailsVM.Orders = _db.Orders.Include(m => m.SalesPerson).ToList();

            if (User.IsInRole(SD.AdminEndUser))
            {
                orderDetailsVM.Orders = orderDetailsVM.Orders.Where(m => m.SalesPersonId == claims.Value).ToList();
            }

            if (searchName != null)
            {
                orderDetailsVM.Orders = orderDetailsVM.Orders.Where(m => m.CustomerName.ToLower().Trim().Contains(searchName.ToLower().Trim())).ToList();
            }
            if (searchEmail != null)
            {
                orderDetailsVM.Orders = orderDetailsVM.Orders.Where(m => m.CustomerEmail.ToLower().Trim().Contains(searchEmail.ToLower().Trim())).ToList();
            }
            if (searchPhone != null)
            {
                orderDetailsVM.Orders = orderDetailsVM.Orders.Where(m => m.CustomerPhoneNumber.ToLower().Trim().Contains(searchPhone.ToLower().Trim())).ToList();
            }

            if (searchDate != null)
            {
                try
                {
                    DateTime appDate = Convert.ToDateTime(searchDate);
                    orderDetailsVM.Orders = orderDetailsVM.Orders.Where(a => a.Date.ToShortDateString().Equals(appDate.ToShortDateString())).ToList();
                }
                catch
                {

                }

            }

            var count = orderDetailsVM.Orders.Count;

            orderDetailsVM.Orders = orderDetailsVM.Orders.OrderBy(p => p.Date)
                .Skip((productPage - 1) * PageSize).Take(PageSize).ToList();

            orderDetailsVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString()
            };

            return View(orderDetailsVM);
        }

        //Get Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Product = (IEnumerable<Product>)(from p in _db.Products
                                                 join od in _db.OrderDetails on p.ProductID equals od.ProductID
                                                 where od.OrderID == id
                                                 select p).Include("GroupProduct");

            OrderInfomationViewModel objOrderInfomation = new OrderInfomationViewModel()
            {
                Order = await _db.Orders.Include(m => m.SalesPerson).Where(m => m.OrderID == id).FirstOrDefaultAsync(),
                SalesPerson = _db.ApplicationUsers.ToList(),
                Products = Product.ToList()
            };

            return View(objOrderInfomation);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderInfomationViewModel objOrderInfomation)
        {
            if (ModelState.IsValid)
            {
                var order = await _db.Orders.Where(m => m.OrderID == id).FirstOrDefaultAsync();

                order.CustomerName = objOrderInfomation.Order.CustomerName;
                order.CustomerEmail = objOrderInfomation.Order.CustomerEmail;
                order.CustomerPhoneNumber = objOrderInfomation.Order.CustomerPhoneNumber;
                order.Date = objOrderInfomation.Order.Date;
                order.Status = objOrderInfomation.Order.Status;

                if (User.IsInRole(SD.SuperAdminEndUser))
                {
                    order.SalesPersonId = objOrderInfomation.Order.SalesPersonId;
                }

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(objOrderInfomation);
        }

        //Get Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Product = (IEnumerable<Product>)(from p in _db.Products
                                                 join od in _db.OrderDetails on p.ProductID equals od.ProductID
                                                 where od.OrderID == id
                                                 select p).Include("GroupProduct");

            OrderInfomationViewModel objOrderInfomation = new OrderInfomationViewModel()
            {
                Order = await _db.Orders.Include(m => m.SalesPerson).Where(m => m.OrderID == id).FirstOrDefaultAsync(),
                SalesPerson = _db.ApplicationUsers.ToList(),
                Products = Product.ToList()
            };

            return View(objOrderInfomation);
        }

        //Get Remove
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Product = (IEnumerable<Product>)(from p in _db.Products
                                                 join od in _db.OrderDetails on p.ProductID equals od.ProductID
                                                 where od.OrderID == id
                                                 select p).Include("GroupProduct");

            OrderInfomationViewModel objOrderInfomation = new OrderInfomationViewModel()
            {
                Order = await _db.Orders.Include(m => m.SalesPerson).Where(m => m.OrderID == id).FirstOrDefaultAsync(),
                SalesPerson = _db.ApplicationUsers.ToList(),
                Products = Product.ToList()
            };

            return View(objOrderInfomation);
        }

        //Post Remove
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePost(int id)
        {

            var order = await _db.Orders.FindAsync(id);

            _db.Orders.Remove(order);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}