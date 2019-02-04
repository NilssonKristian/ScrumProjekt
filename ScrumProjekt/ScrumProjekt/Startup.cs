using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ScrumProjekt.Models;
using System;
using System.Data.Entity.Migrations;

[assembly: OwinStartupAttribute(typeof(ScrumProjekt.Startup))]
namespace ScrumProjekt
{
    public partial class Startup
    {
        protected ApplicationDbContext ctx { get; set; }
        protected UserManager<ApplicationUser> UM { get; set; }


        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ctx = new ApplicationDbContext();
            UM = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
           
        }

       
    }
}
