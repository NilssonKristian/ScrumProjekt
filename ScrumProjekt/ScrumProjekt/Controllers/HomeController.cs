using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScrumProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumProjekt.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext DbContext;
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public HomeController()
        {
            DbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Calendar()
        {
            ViewBag.Message = "Calendar Page";
            return View();
        }

        public JsonResult GetEvents()
        {


            var events = DbContext.Calendars.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public JsonResult SaveEvent(CalendarModels e)
        {
            var status = false;
          
          
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = DbContext.Calendars.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    DbContext.Calendars.Add(e);
                }
                DbContext.SaveChanges();
                status = true;
            
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent (int EventID)
        {
            var status = false;
            
                var v = DbContext.Calendars.Where(a => a.EventID == EventID).FirstOrDefault();
                if (v != null)
                {
                    DbContext.Calendars.Remove(v);
                    DbContext.SaveChanges();
                    status = true;
                }
           
            return new JsonResult { Data = new { status = status } };
        }

    }
}


    
