using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class CreatePost
    {
        public CreatePost() {

            files = new List<HttpPostedFileBase>();

        }
        public string Content { get; set; }
        public List<HttpPostedFileBase> files { get; set; }
    }
}