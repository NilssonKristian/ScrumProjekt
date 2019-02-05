using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScrumProjekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumProjekt.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext DbContext;
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ForumController()
        {
            DbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }

        // GET: Forum
        public ActionResult Index()
        {

            var result = DbContext.Posts.Include(p => p.SenderId).ToList();
            
            var model = new PostViewModels
            {
                Posts = result
            };

            return View(model);
        }

        // GET: Forum/Index
        public ActionResult CreatePostPartialView()
        {
         
            return PartialView("CreatePostPartialView");
        }


    }
}