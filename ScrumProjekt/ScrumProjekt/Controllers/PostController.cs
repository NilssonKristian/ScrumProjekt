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
    public class PostController : Controller
    {

        private ApplicationDbContext DbContext;
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public PostController()
        {
            DbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }
        [Authorize]
        public ActionResult Create()
        {
            return View(new CreatePost());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(CreatePost post)
        {
            var PostModel = new PostModels
            {
                SenderId = UserManager.FindById(User.Identity.GetUserId()),
                Content = post.Content,
                TimeSent = DateTime.Now
            };

            DbContext.Posts.Add(PostModel);
            DbContext.SaveChanges();

            return RedirectToAction("Index", "Forum");
        }
    

        
    }
}