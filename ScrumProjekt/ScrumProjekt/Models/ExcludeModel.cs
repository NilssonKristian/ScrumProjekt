using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class ExcludeModel
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public CategoryModels Category { get; set; }

    }
}