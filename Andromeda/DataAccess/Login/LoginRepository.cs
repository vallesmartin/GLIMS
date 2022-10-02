using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        public bool Login(LoginRequestModel login)
        {
            using (var _context = new AndromedaContext())
            {
                bool isOk = _context.Users.Where(u => u.UserName == login.Username &&
                                                      u.UserPassword == login.Password &&
                                                      u.UserActive).Any();
                if (!isOk)
                    LoginFailed(login);
                return isOk;
            }
        }
        private void LoginFailed(LoginRequestModel login)
        {
            // Agrega un try de inicio de sesion
            if (AddTry(login) > 7)
                LoginUnable(login);
                
             
        }
        private int AddTry(LoginRequestModel login)
        {
            using (var _context = new AndromedaContext())
            {
                var user = _context.Users.Where(u => u.UserName == login.Username).FirstOrDefault();

                if (user.UserId > 0)
                {
                    user.UserLoginTry++;
                    user.UserLastActivity = DateTime.UtcNow;
                    _context.SaveChanges();
                }

                return user.UserLoginTry;
            }
        }
        private void LoginUnable(LoginRequestModel login)
        {
            using (var _context = new AndromedaContext())
            {
                var user = _context.Users.Where(u => u.UserName == login.Username).FirstOrDefault();
                user.UserActive = false;
                _context.SaveChanges();
            }
        }
    }
}