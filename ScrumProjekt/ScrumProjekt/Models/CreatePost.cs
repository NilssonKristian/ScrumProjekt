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

        [Required(ErrorMessage = "You have to enter a title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int ForumId { get; set; }

        [Required]
        public int CategoryID { get; set; }

    }
}