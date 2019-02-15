using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScrumProjekt.Models;

namespace ScrumProjekt.Controllers
{
    public class CategoriesController : Controller
    {

        private ApplicationDbContext DbContext;
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public CategoriesController()
        {
            DbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddCategory(CategoryModels Category)
        {
            var CategoryModel = new CategoryModels
            {
                Name = Category.Name
            };

            DbContext.Categories.Add(CategoryModel);
            DbContext.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }
    }
}