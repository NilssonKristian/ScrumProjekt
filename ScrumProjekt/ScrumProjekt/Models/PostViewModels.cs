using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class PostViewModels
    {
        public ApplicationUser User { get; set; }
        public List<PostModels> Posts { get; set; }
        public Dictionary<CategoryModels, bool> Categories { get; set; }
        public Forum Forum { get; set; }

        public int? ForumId { get; set; }


        public List<Comment> CommentList { get; set; }

        public PostViewModels()
        {
            Posts = new List<PostModels>();
            CommentList = new List<Comment>();
        }
    }
}