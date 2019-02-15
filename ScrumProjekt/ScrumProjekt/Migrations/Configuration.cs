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
                    UserName = "admin@oru.se",
                    FirstName = "FirstNameAdmin",
                    LastName = "LastNameAdmin"
                };

                IdentityResult result = manager.Create(user, "Admin123");

                if (result.Succeeded == false) { throw new Exception(result.Errors.First()); }
                manager.AddToRole(user.Id, "Admin");

            }

            if (!context.Users.Any(u => u.UserName == "test1@oru.se"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    Email = "test1@oru.se",
                    UserName = "test1@oru.se",
                    FirstName = "Anders",
                    LastName = "Andersson"
                };

                IdentityResult result = manager.Create(user, "Test123");

                if (result.Succeeded == false) { throw new Exception(result.Errors.First()); }
                
            }

            if (!context.Forums.Any(u => u.ForumName == "Formal Forum"))
            {
                
                var forum = new Forum
                {
                    ForumName = "Formal Forum",
                    AllowPushNotifications = false

                };

                context.Forums.Add(forum);


            }
            
            if (!context.Forums.Any(u => u.ForumName == "Informal Forum"))
            {
                
                var forum = new Forum
                {
                    ForumName = "Informal Forum",
                    AllowPushNotifications = false

                };

                context.Forums.Add(forum);


            }
            if (!context.Forums.Any(u => u.ForumName == "Research Forum"))
            {
                
                var forum = new Forum
                {
                    ForumName = "Research Forum",
                    AllowPushNotifications = true

                };

                context.Forums.Add(forum);


            }
            if (!context.Forums.Any(u => u.ForumName == "Education Forum"))
            {

                var forum = new Forum
                {
                    ForumName = "Education Forum",
                    AllowPushNotifications = true
                };

                context.Forums.Add(forum);


            }
            if (!context.Categories.Any(u => u.Name == "Education"))
            {

                var category = new CategoryModels
                {
                    Name = "Education"

                };

                context.Categories.Add(category);

            }
            if (!context.Categories.Any(u => u.Name == "Research"))
            {

                var category = new CategoryModels
                {
                    Name = "Research"

                };

                context.Categories.Add(category);

            }
            if (!context.Categories.Any(u => u.Name == "General"))
            {

                var category = new CategoryModels
                {
                    Name = "General"

                };

                context.Categories.Add(category);

            }
            if (!context.Categories.Any(u => u.Name == "After Work"))
            {

                var category = new CategoryModels
                {
                    Name = "After Work"

                };

                context.Categories.Add(category);

            }
            if (!context.Categories.Any(u => u.Name == "Meetings"))
            {

                var category = new CategoryModels
                {
                    Name = "Meetings"

                };

                context.Categories.Add(category);

            }
        }
    }
}
