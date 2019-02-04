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



        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
           
        }

       
    }
}
