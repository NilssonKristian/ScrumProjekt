using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumProjekt.Models
{
    public class CreatePost
    {
        public CreatePost() {

            files = new List<HttpPostedFileBase>();

        }
        public List<HttpPostedFileBase> files { get; set; }
        



        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int ForumId { get; set; }

        public int CategoryID { get; set; }

    }
}