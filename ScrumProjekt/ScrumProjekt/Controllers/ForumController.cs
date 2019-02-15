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

        [HttpGet]
        [Authorize]
        public ActionResult Index(int? id, int[] filtrering)
        {

            ViewBag.Categories = DbContext.Categories.ToList();


       

            
            var tempComments = new List<Comment>();

            if (!id.HasValue)
            {
                return View("Show");
            }


            var forum = DbContext.Forums.Where(i => i.Id == id).Include(p => p.Posts).Include("Posts.Category").Include("Posts.SenderId").FirstOrDefault();

            var specifikaPosts = DbContext.Posts.Where(i => i.PostedForum.Id == id).ToList();

            foreach(var p in specifikaPosts)
            {
                var tempList = DbContext.Comments.Where(c => c.Post.Id == p.Id).ToList();

                foreach(var c in tempList)
                {
                    tempComments.Add(c);
                }


            }
            if (forum == null) {
                return View("Show");
            }


            var listaAttSkicka = new List<CategoryModels>();

            if (filtrering == null)
            {


                listaAttSkicka = DbContext.Categories.ToList();
                

            } else
            {

                foreach (var i in filtrering) {

                    listaAttSkicka.Add(DbContext.Categories.SingleOrDefault(c => c.Id == i));
                }

            }

            var categoryList = new Dictionary<CategoryModels, bool>();
            var postLista = new List<PostModels>();

           
                foreach(var post in forum.Posts)
                {

                    
                
                    foreach (var c in listaAttSkicka)
                    {
                       
                        if(post.Category != null && post.Category.Id == c.Id)
                        {
                            postLista.Add(post);
                        }

                    }

                }
            postLista.AddRange(forum.Posts.Where(m => m.Category == null));
            
            foreach(var category in DbContext.Categories)
            {
                bool DoesContain = listaAttSkicka.Any(i => i.Id == category.Id);
                if (DoesContain)
                {
                    categoryList.Add(category, true);
                }
                else
                {
                    categoryList.Add(category, false);
                }
            }
            var userid = User.Identity.GetUserId();
            var user = DbContext.Users.Where(p => p.Id == userid).Include(p => p.Subscriptions).FirstOrDefault();

            var model = new PostViewModels
            {
                Posts = postLista,
                Forum = forum,
                Categories = categoryList,
                ForumId = id,
                CommentList = tempComments,
                User = user
              

            };

            ViewBag.Id = id;


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

            var userid = User.Identity.GetUserId();
            var user = DbContext.Users.Where(p => p.Id == userid).FirstOrDefault();

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
            var userid = User.Identity.GetUserId();
            var user = DbContext.Users.Where(p => p.Id == userid).FirstOrDefault();

                
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