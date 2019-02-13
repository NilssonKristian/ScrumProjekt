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
        public ActionResult Index(int? id, List<CategoryModels> categoryIdList = null)
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

            var tempCategories = new List<CategoryModels>();

            if (categoryIdList == null) { 
             tempCategories = DbContext.Categories.ToList();

            } else {

                foreach (var i in categoryIdList) {

                    var tempCategory = DbContext.Categories.SingleOrDefault(c => c == i);
                    tempCategories.Add(tempCategory);
                    System.Diagnostics.Debug.WriteLine(tempCategory.Id);

                }

            }

            var model = new PostViewModels
            {
                Posts = forum.Posts.ToList(),
                Forum = forum,
                Categories = tempCategories
            };

            ViewBag.Id = id;


            return View("Index",model);
        }


        public ActionResult FilterCategories (List<int> categories)
        {

            ViewBag.CategoriesToRender = categories;

            return RedirectToAction("Index");


        }
        




    }
}