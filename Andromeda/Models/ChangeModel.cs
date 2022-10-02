﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ChangeModel
    {
        [Key]
        public string ChangeObjectId { get; set; }
        public DateTime ChangeLastAt { get; set; }
    }
}