using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopDaki.Data;
using ShopDaki.Models.ViewModel;
using ShopDaki.Extensions;
using ShopDaki.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ShopDaki.Utility;

namespace GrainteHouseASP.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;

            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Product>(),
                Customer = new ApplicationUser()
            };
        }

        //Get Index Shopping cart
        public async Task<IActionResult> Index()
        {
            //Get ID Curent User
            ClaimsPrincipal currenUser = this.User;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(SD.Customer))
            {
                ShoppingCartVM.Customer = _db.ApplicationUsers.Where(m => m.Id == claims.Value).FirstOrDefault();
            }

            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart != null)
            {
                foreach (var itemID in lstShoppingCart)
                {
                    Product product = await _db.Products.Include(m => m.GroupProduct).Where(m => m.ProductID == itemID).FirstOrDefaultAsync();
                    ShoppingCartVM.Products.Add(product);
                }
            }

            return View(ShoppingCartVM);
        }

        //Post Index Shopping Cart
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public IActionResult IndexPost()
        {
            List<int> lstShoppingCast = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            //Get ID Curent User
            ClaimsPrincipal currenUser = this.User;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(SD.Customer))
            {
                ShoppingCartVM.Order.SalesPersonId = claims.Value;
            }

            ShoppingCartVM.Order.Date = DateTime.Now;

            foreach (var item in lstShoppingCast)
            {
                int quantity = HttpContext.Session.Get<int>(item.ToString());

                ShoppingCartVM.Order.TotalMoney += _db.Products.Include(m => m.GroupProduct).Where(m => m.ProductID == item).FirstOrDefault().Price * quantity;
            }

            ShoppingCartVM.Order.Status = "Ordered";

            _db.Orders.Add(ShoppingCartVM.Order);
            _db.SaveChanges();


            foreach (var item in lstShoppingCast)
            {
                int quantity = HttpContext.Session.Get<int>(item.ToString());

                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductID = item,
                    OrderID = ShoppingCartVM.Order.OrderID,
                    Quantity = quantity
                };

                _db.OrderDetails.Add(orderDetail);
            }

            _db.SaveChanges();

            lstShoppingCast.Clear();

            HttpContext.Session.Set("ssShoppingCart", lstShoppingCast);

            return RedirectToAction("OrderConfirmation", "ShoppingCart", new { id = ShoppingCartVM.Order.OrderID });
        }

        public IActionResult Remove(int id)
        {
            List<int> lstShoppingCast = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCast != null)
            {
                if (lstShoppingCast.Contains(id))
                {
                    lstShoppingCast.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", lstShoppingCast);

            return RedirectToAction(nameof(Index));
        }

        //Get OrderConfirmation Method
        public IActionResult OrderConfirmation(int id)
        {
            ShoppingCartVM.Order = _db.Orders.Where(o => o.OrderID == id).FirstOrDefault();

            List<OrderDetail> orderDetails = _db.OrderDetails.Where(od => od.OrderID == id).ToList();

            foreach (var item in orderDetails)
            {
                ShoppingCartVM.Products.Add(_db.Products.Include(m => m.GroupProduct).Where(m => m.ProductID == item.ProductID).FirstOrDefault());
            }

            return View(ShoppingCartVM);
        }
    }
}