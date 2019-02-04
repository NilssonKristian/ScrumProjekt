namespace ScrumProjekt.Migrations
{

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ScrumProjekt.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ScrumProjekt.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            ContextKey = "ScrumProjekt.Models.ApplicationDbContext";

        }

        protected override void Seed(ScrumProjekt.Models.ApplicationDbContext context)
        {

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@oru.se"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    Email = "admin@oru.se",
                    UserName = "admin@oru.se"
                };

                IdentityResult result = manager.Create(user, "Admin123");

                if (result.Succeeded == false) { throw new Exception(result.Errors.First()); }
                manager.AddToRole(user.Id, "Admin");


            }
        }
    }
}
