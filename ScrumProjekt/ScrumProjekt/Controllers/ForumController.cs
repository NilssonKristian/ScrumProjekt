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
        public ActionResult Index(int? id, int[] filtrering)
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

            var listaAttSkicka = new List<CategoryModels>();

            if (filtrering == null)
            {


                listaAttSkicka = DbContext.Categories.ToList();
                listaAttSkicka.RemoveAt(1);

            } else
            {

                foreach (var i in filtrering) {

                    listaAttSkicka.Add(DbContext.Categories.SingleOrDefault(c => c.Id == i));
                }

            }

            var categoryList = new Dictionary<CategoryModels, bool>();
            var postLista = new List<PostModels>();

           
                foreach(var c in listaAttSkicka)
                {

                    var tempPosts = forum.Posts.Where(p => Convert.ToInt32(p.CategoryPostModels) == c.Id);
                
                    foreach (var tp in tempPosts)
                    {

                        postLista.Add(tp);

                    }

                }
            
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

            var model = new PostViewModels
            {
                Posts = postLista,
                Forum = forum,
                Categories = categoryList,
                ForumId = id
              
            };

            ViewBag.Id = id;


            return View("Index",model);
        }

        //[HttpPost]
        //public ActionResult Index(PostViewModels model)
        //{

        //    System.Diagnostics.Debug.WriteLine(model.Forum.ForumName);
        //    return View(model);


        //}
        




    }
}