﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProjekt.Models
{
    public class CreatePost
    {
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}