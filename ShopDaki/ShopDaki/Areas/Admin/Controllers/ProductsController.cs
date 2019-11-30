using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShopDaki.Models.ViewModel;
using ShopDaki.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopDaki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace ShopDaki.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }

        public ProductsController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            
            ProductsVM = new ProductsViewModel
            {
                GroupProducts = _db.GroupProducts.ToList(),
                Product = new Models.Product()
            };
        }

        public async Task<IActionResult> Index()
        {
            var products = _db.Products.Include(m => m.GroupProduct);
            return View(await products.ToListAsync());
        }

        //Get Create Method
        [HttpGet]
        public IActionResult Create()
        {
            return View(ProductsVM);
        }

        //Post Crate Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(ProductsVM);
            }

            _db.Products.Add(ProductsVM.Product);
            await _db.SaveChangesAsync();

            ////Image being saved
            ////ContentRootPath is same WebRootPath.
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var productsFromDb = _db.Products.Find(ProductsVM.Product.ProductID);

            //have file uploading.
            if (files.Count != 0)
            {
                //image has been loaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);
                using (var filestream = new FileStream(Path.Combine(uploads, ProductsVM.Product.ProductID + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                productsFromDb.Images = @"\" + SD.ImageFolder + @"\" + ProductsVM.Product.ProductID + extension;
            }
            else
            {
                //when user does not upload image 
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductIamge);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + ProductsVM.Product.ProductID + ".jpg");
                productsFromDb.Images = @"\" + SD.ImageFolder + @"\" + ProductsVM.Product.ProductID + ".jpg";

            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Get Edit Method
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductsVM.Product = await _db.Products.Include(m => m.GroupProduct).SingleOrDefaultAsync(m => m.ProductID == id);

            if (ProductsVM.Product == null)
            {
                NotFound();
            }

            return View(ProductsVM);
        }

        //Get Edit Method
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            if (ModelState.IsValid)
            {
                _db.Update(ProductsVM.Product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ProductsVM);
        }

        //Get Details Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductsVM.Product = await _db.Products.Include(m => m.GroupProduct).SingleOrDefaultAsync(m => m.ProductID == id);

            if (ProductsVM.Product == null)
            {
                return NotFound();
            }

            return View(ProductsVM);
        }

        //Get Edit Method
        [HttpGet]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductsVM.Product = await _db.Products.Include(m => m.GroupProduct).SingleOrDefaultAsync(m => m.ProductID == id);

            if (ProductsVM.Product == null)
            {
                NotFound();
            }

            return View(ProductsVM);
        }

        //Get Delete Method
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProduct(int? id)
        {
            if (ModelState.IsValid)
            {
                _db.Remove(ProductsVM.Product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ProductsVM);
        }
    }
}