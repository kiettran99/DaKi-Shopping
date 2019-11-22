using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopDaki.Models;
using Microsoft.AspNetCore.Authorization;

namespace ShopDaki.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupProductController : Controller
    {

        public readonly ApplicationDbContext _db;

        public GroupProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.GroupProducts.ToList());
        }

        //Get Create Action Method 
        public IActionResult Create()
        {
            return View();
        }

        //Post Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupProduct groupProduct)
        {
            if (ModelState.IsValid)
            {
                _db.Add(groupProduct);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupProduct);
        }

        //Get Edit Action Method 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupProduct = await _db.GroupProducts.FindAsync(id);

            if (groupProduct == null)
            {
                return NotFound();
            }

            return View(groupProduct);
        }

        //Post Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupProduct groupProduct)
        {
            if (id != groupProduct.GroupProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(groupProduct);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupProduct);
        }

        //Get Details Action Method 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupProduct = await _db.GroupProducts.FindAsync(id);

            if (groupProduct == null)
            {
                return NotFound();
            }

            return View(groupProduct);
        }

        //Get Remove Action Method 
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupProduct = await _db.GroupProducts.FindAsync(id);

            if (groupProduct == null)
            {
                return NotFound();
            }

            return View(groupProduct);
        }

        //Post Create Action Method
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var groupProduct = await _db.GroupProducts.FindAsync(id);
            _db.GroupProducts.Remove(groupProduct);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}