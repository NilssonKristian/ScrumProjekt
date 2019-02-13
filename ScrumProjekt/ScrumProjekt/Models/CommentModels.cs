using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
    
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public PostModels Post { get; set; }
        public ApplicationUser User { get; set; }
       
    }
}