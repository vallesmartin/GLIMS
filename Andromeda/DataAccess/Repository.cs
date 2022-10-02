using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;

namespace WebApp.DataAccess
{
    /*
        La clase Repository contiene la implementacion de la interface IRepository
        Su definicion es parcial porque se comparte con WebApp.DataAccess separando su logica
    */
    public partial class Repository : IRepository
    {
        public List<ChangeModel> Changes_Get()
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Changes.ToList();
            }
        }
        public ChangeModel Changes_GetLastForSynchro()
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Changes.ToList().Where(c => c.ChangeObjectId == "Items" || c.ChangeObjectId == "Entity").OrderByDescending(c => c.ChangeLastAt).FirstOrDefault();
            }
        }
        void Changes_Set(string obj)
        {
            using (var _context = new AndromedaContext())
            {
                try
                {
                    var changeRecord = _context.Changes.Where(c => c.ChangeObjectId == obj).First();
                    changeRecord.ChangeLastAt = DateTime.Now;
                }
                catch (Exception)
                {
                    _context.Changes.Add(new ChangeModel { ChangeObjectId = obj, ChangeLastAt = DateTime.Now });
                }
                _context.SaveChanges();
            }
        }
    }
}