using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopDaki.Models;
using ShopDaki.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async void initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }

            if (_db.Roles.Any(r => r.Name == SD.SuperAdminEndUser)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Salesman)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();

            var userAdmin = new ApplicationUser()
            {
                UserName = "daki@gmail.com",
                Email = "daki@gmail.com",
                Name = "Super Admin",
                EmailConfirmed = true
            };

            var resultUser = _userManager.CreateAsync(userAdmin, "Daki123@").GetAwaiter().GetResult();

            await _userManager.AddToRoleAsync(userAdmin, SD.SuperAdminEndUser);
        }

    }
}
