using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        public long Log_Create(LogModel log)
        {
            using (var _context = new AndromedaContext())
            {
                _context.Logs.Add(log);
                _context.SaveChanges();
                return log.LogId;
            }
        }
    }
}