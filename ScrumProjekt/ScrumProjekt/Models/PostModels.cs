using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class PostModels
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser SenderId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public List<File> Files { get; set; }
        public Forum PostedForum { get; set; }
        public String CategoryPostModels { get; set; }

    }
}