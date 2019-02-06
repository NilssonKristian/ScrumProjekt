using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class PostViewModels
    {
        public List<PostModels> Posts { get; set; }
        public Forum Forum { get; set; }

        public PostViewModels()
        {
            Posts = new List<PostModels>();
        }
    }
}