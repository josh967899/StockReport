using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StockReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StockReport.Function.CreateRoot
{
    public class Root
    {
        public static async void CreateAllRoot()
        {
            CreateRoles();
            await CreateRootAdmin();
           
        }

        public static void CreateRoles()
        {
            using (var db = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("User"))
                {
                    var role = new IdentityRole();
                    role.Name = "User";
                    roleManager.Create(role);
                }
            }
        }

        public static async Task<string> CreateRootAdmin()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Users.Where(u => u.Email == "Admin@Artrium.com").FirstOrDefault() == null)
                {
                    var user = new ApplicationUser { UserName = "Admin@Artrium.com", Email = "Admin@Artrium.com" };
                    UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var result = await userManager.CreateAsync(user, "abc123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user.Id, "Admin"); //<-Here                 
                        return "Done_Created";
                    }
                }
            }
            return "Done_NoCreate";
        }
    }
}