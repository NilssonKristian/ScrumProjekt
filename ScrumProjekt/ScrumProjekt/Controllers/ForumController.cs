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
        [Authorize]
        public ActionResult Index(int? id)
        {

            ViewBag.Categories = new SelectList(DbContext.Categories, "Id", "Name");

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

        [Authorize]
        public ActionResult Subscribe(int? ForumID)
        {
            if (!ForumID.HasValue)
            {
                return null;
            }

            var forum = DbContext.Forums.Where(p => p.Id == ForumID).Include(p => p.Subscribers).FirstOrDefault();

            var user = DbContext.Users.Where(p => p.Id == User.Identity.GetUserId()).FirstOrDefault();

            if (!forum.AllowPushNotifications)
            {
                return null;
            }
            //allready subscribed
            if(forum.Subscribers.Any(p => p.Id == user.Id))
            {
                return null;
            }

            forum.Subscribers.Add(user);

            DbContext.SaveChanges();

            return RedirectToAction("Index", "Forum", new { id = forum.Id });
        }

        [Authorize]
        public ActionResult UnSubscribe(int? ForumID)
        {

           
                if (!ForumID.HasValue)
                {
                    return null;
                }

                var forum = DbContext.Forums.Where(p => p.Id == ForumID).Include(p => p.Subscribers).FirstOrDefault();

                var user = DbContext.Users.Where(p => p.Id == User.Identity.GetUserId()).FirstOrDefault();

                
                //Is not subsribed
                if (!forum.Subscribers.Any(p => p.Id == user.Id))
                {
                    return null;
                }

            forum.Subscribers.Remove(user);

                DbContext.SaveChanges();

            return RedirectToAction("Index", "Forum", new { id= forum.Id});
            
        }


    }
}