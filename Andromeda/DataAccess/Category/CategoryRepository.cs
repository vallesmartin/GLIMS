using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        public List<CategoryModel> Category_GetAll()
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Categories.ToList();
            }
        }
        public int Category_Create(CategoryModel category)
        {
            using (var _context = new AndromedaContext())
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category.CategoryId;
            }
        }
    }
}