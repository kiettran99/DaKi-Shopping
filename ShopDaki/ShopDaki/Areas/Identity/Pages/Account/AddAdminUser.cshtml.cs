using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopDaki.Models;
using ShopDaki.Utility;

namespace ShopDaki.Areas.Identity.Pages.Account
{
    public class AddAdminUserModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AddAdminUserModel(
            UserManager<IdentityUser> userManager,
             RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGet()
        {
            //create Roles for our website and create super admin user 
            if(!await _roleManager.RoleExistsAsync(SD.AdminEndUser))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
            }
            if (!await _roleManager.RoleExistsAsync(SD.SuperAdminEndUser))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser));
                var userAdmin = new ApplicationUser
                {
                    UserName = "daki@gmail.com",
                    Email = "daki@gmail.com",
                    PhoneNumber = "0909090909",
                    Name = "DAKI"
                };

                var resultUser = await _userManager.CreateAsync(userAdmin, "daki123@");
                await _userManager.AddToRoleAsync(userAdmin, SD.SuperAdminEndUser);
            }
            return Page();
        }

       
    }
}
