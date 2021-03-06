﻿using LeaveMgmt.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt
{
    // Static class at root to add seed data for Roles and Users into the database
    public static class SeedData
    {
        public static void Seed(UserManager<Person> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedUsers(UserManager<Person> userManager)
        {
            var users = userManager.GetUsersInRoleAsync("Member").Result;
            var userAdmins = userManager.FindByNameAsync("admin").Result;
            
            if (users.Count == 0 && userAdmins == null)
            {
                var user = new Person
                {
                    UserName = "admin",
                    Email = "liz@icontoo.com"
                };
                var result = userManager.CreateAsync(user, "passw0RD@123").Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            //logical flow is to add roles first before users, but not necessary
            if(!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
               var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Member").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Member"
                };
              var result =  roleManager.CreateAsync(role).Result;
            }
        }
    }
}
