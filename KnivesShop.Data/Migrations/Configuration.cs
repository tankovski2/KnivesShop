using System.Collections.Generic;

namespace KnivesShop.Data.Migrations
{
    using KnivesShop.Data;
    using KnivesShop.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //If in the database there aren't any users we crate role superadmin and create user with name SuperAdmin and password "superadmin"
            //and role group admin so the super admin can create group admins
            if (!context.Users.Any())
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin" };
                var superAdmin = new ApplicationUser { UserName = "superadmin" };

                manager.Create(user, "administrator");
                manager.Create(superAdmin, "superadmin123");

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                // Check to see if Role Exists, if not create it
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole { Name = "Admin" };
                    roleManager.Create(role);
                    user.Roles.Add(new IdentityUserRole { RoleId=role.Id, UserId = user.Id});
                }
                if (!roleManager.RoleExists("SuperAdmin"))
                {
                    var superadminRole = new IdentityRole { Name = "SuperAdmin" };
                    roleManager.Create(superadminRole);
                    superAdmin.Roles.Add(new IdentityUserRole { RoleId = superadminRole.Id, UserId = superAdmin.Id });
                }

                //context.SaveChanges();
            }

            base.Seed(context);
        }
    }
}
