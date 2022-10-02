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
    /// user controller class for testing security token
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly Services _services;
        public UserController(Services services)
        {
            this._services = services;
        }

        [HttpGet]
        [Route("getbyid")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_services.User_GetById(id));
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            return Ok(_services.User_GetAll());
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(UserModel user)
        {
            return Ok(_services.User_New(user));
        }

        [HttpPost]
        [Route("changepassword")]
        public IHttpActionResult ChangePassword(UserModel user)
        {
            return Ok(_services.User_ChangePassword(user));
        }
    }
}
