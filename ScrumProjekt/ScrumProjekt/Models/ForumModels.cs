namespace ScrumProjekt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class Forum
    {
        [Key]
        public int Id { get; set; }

        public ICollection<PostModels> Posts { get; set; }

        public String ForumName { get; set; }

        public List<ApplicationUser> Subscribers { get; set; }

        public Boolean AllowPushNotifications { get; set; }

    }

    
}