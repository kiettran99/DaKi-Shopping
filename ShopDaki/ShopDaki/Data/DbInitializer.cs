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
    public class DbInitializer :IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityUser> _roleManager;
        
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser>userManager,RoleManager<IdentityUser>roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async void initialize()
        {
            if(_db.Database.GetPendingMigrations().Count()>0)
            {
                _db.Database.Migrate();
            }
            _db.Database.Migrate();
            if (_db.Roles.Any(r => r.Name == SD.SuperAdminEndUser))return;
            _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "daki@gmail.com",
                Email = "daki@gmail.com",
                Name = "daki",
                EmailConfirmed = true

            }, "daki1@").GetAwaiter().GetResult();
            IdentityUser user = await _db.Users.Where(u => u.Email == "daki@gamil.com").FirstOrDefaultAsync();
            await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync("daki@gmail.com"), SD.SuperAdminEndUser);


        }
             
    }
}
