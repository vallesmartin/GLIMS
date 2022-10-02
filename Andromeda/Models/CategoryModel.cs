using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryOrder { get; set; }
        public string CategoryImageName { get; set; }

    }
}