using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.BusinessLayer;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// user roles controller class
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/userrole")]
    public class UserRoleController : ApiController
    {
        private readonly Services _services;
        public UserRoleController(Services services)
        {
            this._services = services;
        }

        [HttpGet]
        [Route("getbyuser")]
        public IHttpActionResult GetByUser(string user)
        {
            return Ok(_services.UserRole_Get(user));
        }

        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(UserRoleModel role)
        {
            return Ok(_services.UserRole_Set(role));
        }
    }
}