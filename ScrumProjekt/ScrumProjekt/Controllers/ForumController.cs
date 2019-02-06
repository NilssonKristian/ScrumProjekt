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
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return View("Show");
            }

            var forum = DbContext.Forums.Where(i => i.Id == id).Include(p => p.Posts).Include("Posts.SenderId").FirstOrDefault();

            if (forum == null) {
                return View("Show");
            }



            var model = new PostViewModels
            {
                Posts = forum.Posts.ToList(),
                Forum = forum
            };

            return View("Index",model);
        }

       


    }
}