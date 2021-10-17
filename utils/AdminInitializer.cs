﻿using hh.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.utils
{
    public class AdminInitializer
    {
        public static async Task SeedAdminUser(
        RoleManager<IdentityRole> _roleManager,
        UserManager<Account> _userManager)
        {
            string adminEmail = "admin@admin.com";
            string adminPassword = "password";

            var roles = new[] { "admin", "Соискатель", "Компания" };
            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) is null)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
            if (await _userManager.FindByNameAsync(adminEmail) == null)
            {
                Account admin = new Account { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await _userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(admin, "admin");
            }

        }
    }
}
