﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScrumProjekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
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

                var forum = DbContext.Forums.Where(m => m.Id == post.ForumId).Include(p => p.Posts).Include(p => p.Subscribers).SingleOrDefault();
                var category = DbContext.Categories.Where(m => m.Id == post.CategoryID).SingleOrDefault();

                var PostModel = new PostModels
                {
                    SenderId = UserManager.FindById(User.Identity.GetUserId()),
                    Content = post.Content,
                    TimeSent = DateTime.Now,
                    Files = tempFiles,
                    PostedForum = forum,
                    Category = category,
                    Title = post.Title
                };
            

            DbContext.Posts.Add(PostModel);
            DbContext.SaveChanges();

            //Notify
            if (forum.AllowPushNotifications)
            {
                foreach (var subscriber in forum.Subscribers)
                {
                    var body = "<p> {0} ({1})</p><p>Message:</p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(subscriber.Email)); //replace with valid value
                    message.Subject = "Email Notifaction";
                    message.Body = string.Format(body, PostModel.SenderId.FirstName, PostModel.SenderId.Email, PostModel.Content);
                    message.IsBodyHtml = true;
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        
                    }
                }
            }
            

            return RedirectToAction("Index", "Forum", new { id=PostModel.PostedForum.Id});
        }

        
        
        
        
        [Authorize]
        public ActionResult Delete(int? id)
        {
            //No id suplied
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Forum");
            }
            //post does not exist
            var post = DbContext.Posts.Include(p => p.SenderId).Include(p=>p.PostedForum).SingleOrDefault(i => i.Id == id);
            if (post == null)
            {
                return RedirectToAction("Index", "Forum");
            }
            //User is not authenticated
            if (!(User.Identity.GetUserId() == post.SenderId.Id || User.IsInRole("Admin")))
            {
                return RedirectToAction("Index", "Forum");
            }
            var forum = post.PostedForum;

            //Remove from database
            DbContext.Posts.Remove(post);
            DbContext.SaveChanges();



            return RedirectToAction("Index", "Forum", new { id = forum.Id });
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var post = DbContext.Posts.Where(p => p.Id == id).FirstOrDefault();

            if (post == null)
            {
                return null;
            }
            return View(post);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, PostModels post)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var dbpost = DbContext.Posts.Where(p => p.Id == id).Include(p => p.PostedForum).Include(p=>p.SenderId).FirstOrDefault();

            if (dbpost == null)
            {
                return null;
            }

            if (    !(User.Identity.GetUserId() == dbpost.SenderId.Id || User.IsInRole("Admin")))
            {
                return null;
            }
                dbpost.Content = post.Content;
                DbContext.SaveChanges();
            
            return RedirectToAction("Index","Forum", new {id = dbpost.PostedForum.Id });

        }

        [HttpPost]
        public ActionResult PostComment(CommentViewModel comment)
        {


            var post = DbContext.Posts.Where(p => p.Id == comment.PostID).Include(p => p.PostedForum).FirstOrDefault();
            if (post == null) {
                //Gör något, posten finns inte
            }
            
                var c = new Comment
                {
                    User = UserManager.FindById(User.Identity.GetUserId()),
                    Content = comment.Content,
                    TimeSent = DateTime.Now,
                    Post = post
                };

                
            
        
                DbContext.Comments.Add(c);
                DbContext.SaveChanges();

            return RedirectToAction("Index", "Forum", new { id = post.PostedForum.Id });

        }




    }
}