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
            var tempFiles = new List<File>();
            var lista = new List<HttpPostedFileBase>();
            foreach (var i in post.files)
            {
                lista.Add(i);
            }
            foreach (var i in lista)
            {
                if (i != null)
                {

                    string filename = System.IO.Path.GetFileName(i.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Files/"), filename);
                    i.SaveAs(path);
                    var tempFile = new File
                    {
                        Filepath = path
                    };

                    tempFiles.Add(tempFile);
                    DbContext.Files.Add(tempFile);
                    DbContext.SaveChanges();
                }
            }
            
            var forum = DbContext.Forums.Where(m => m.Id == post.ForumId).Include(p => p.Posts).SingleOrDefault();
            var PostModel = new PostModels
            {
                SenderId = UserManager.FindById(User.Identity.GetUserId()),
                Content = post.Content,
                TimeSent = DateTime.Now,
                Files = tempFiles,
                PostedForum = forum
            };

            DbContext.Posts.Add(PostModel);
            DbContext.SaveChanges();

            return RedirectToAction("Index", "Forum", new { id=PostModel.PostedForum.Id});
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            //No id suplied
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Forum");
            }
            //post does not exist
            var post = DbContext.Posts.Include(p => p.SenderId).SingleOrDefault(i => i.Id == id);
            if (post == null)
            {
                return RedirectToAction("Index", "Forum");
            }
            //User is not authenticated
            if (post.SenderId.Id != User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Forum");
            }
            //Remove from database
            DbContext.Posts.Remove(post);
            DbContext.SaveChanges();



            return RedirectToAction("Index", "Forum");
        }



    }
}