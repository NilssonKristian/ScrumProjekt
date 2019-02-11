using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        public string Filepath { get; set; }
    }
}