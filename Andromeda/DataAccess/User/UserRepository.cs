using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        public List<UserModel> User_GetAll()
        {
            using (var _context = new AndromedaContext())
            {
                var users = _context.Users.ToList();
                try
                {
                    foreach (var user in users)
                    {
                        user.Role = User_GetById(user.UserId).Role;
                        user.UserPassword = "";
                    }
                }
                catch (Exception ex)
                {

                }
                return users;
            }
        }
        public UserModel User_GetById(int id)
        {
            using (var _context = new AndromedaContext())
            {
                var user = _context.Users.Where(u => u.UserId == id).First();
                try
                {
                    user.Role = _context.UserRoles.Where(r => r.UserId == user.UserId).First();
                }
                catch (Exception)
                {
                    UserRoleModel defaultRole = new UserRoleModel
                    {
                        UserId = user.UserId,
                        UserRoleIsAdmin = false,
                        UserRoleIsBiller = false,
                        UserRoleIsDeliverer = false,
                        UserRoleIsPicker = false,
                        UserRoleIsSeller = false
                    };
                    _context.UserRoles.Add(defaultRole);
                    _context.SaveChanges();
                    user.Role = defaultRole;
                }
                return user;
            }
        }
        public bool User_New(UserModel user)
        {
            using (var _context = new AndromedaContext())
            {
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public UserRoleModel UserRole_Get(string user)
        {
            using (var _context = new AndromedaContext())
            {
                var userModel = _context.Users.Where(u => u.UserName == user).FirstOrDefault();
                return _context.UserRoles.Where(u => u.UserId == userModel.UserId).FirstOrDefault();
            }
        }
        public bool UserRole_Set(UserRoleModel role)
        {
            using (var _context = new AndromedaContext())
            {
                _context.UserRoles.Add(role);
                return (_context.SaveChanges() > 0);
            }
        }
        public bool User_SavePassword(UserModel user)
        {
            using (var _context = new AndromedaContext())
            {
                if (user.UserPassword.Length > 0)
                {
                    UserModel recordUpdate = _context.Users.Where(u => u.UserName.ToUpper() == user.UserName.ToUpper()).First();
                    recordUpdate.UserPassword = user.UserPassword;
                    return (_context.SaveChanges() > 0);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}