using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopDaki.Models;
using ShopDaki.Utility;

namespace ShopDaki.Areas.Admin
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Contacts.ToList());
        }

        //Get Details Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _db.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        //Get Remove Method
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _db.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePost(int id)
        {

            var contact = await _db.Contacts.FindAsync(id);
            _db.Contacts.Remove(contact);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}