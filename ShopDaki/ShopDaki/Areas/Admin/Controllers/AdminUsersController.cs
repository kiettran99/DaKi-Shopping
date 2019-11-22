using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopDaki.Data;
using ShopDaki.Models;
using Microsoft.AspNetCore.Authorization;
using ShopDaki.Utility;

namespace GrainteHouseASP.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminUsersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ApplicationUsers.ToList());
        }

        //Get Edit Method
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDB = await _db.ApplicationUsers.FindAsync(id);

            if (userFromDB == null)
            {
                return NotFound();
            }

            return View(userFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = _db.ApplicationUsers.Where(m => m.Id == id).FirstOrDefault();

                user.Name = applicationUser.Name;
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.Email = applicationUser.Email;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }

        //Get Remove Method
        public async Task<IActionResult> Remove(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDB = await _db.ApplicationUsers.FindAsync(id);

            if (userFromDB == null)
            {
                return NotFound();
            }

            return View(userFromDB);
        }

        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePost(string id)
        {

            var user = _db.ApplicationUsers.Where(m => m.Id == id).FirstOrDefault();
            user.LockoutEnd = DateTime.Now.AddYears(1000);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}