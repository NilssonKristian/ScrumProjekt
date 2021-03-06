﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace ScrumProjekt.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public List<Forum> Subscriptions { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<PostModels> DbInlägg { get; set; }

        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)

        //{
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();

        }
        public System.Data.Entity.DbSet<ScrumProjekt.Models.File> Files { get; set; }
        public System.Data.Entity.DbSet<ScrumProjekt.Models.PostModels> Posts { get; set; }
        public System.Data.Entity.DbSet<ScrumProjekt.Models.Forum> Forums { get; set; }
        public DbSet<CategoryModels> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CalendarModels> Calendars { get; set; }
    }
}