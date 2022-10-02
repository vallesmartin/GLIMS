using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;
using WebApp.BusinessLayer;

namespace WebApp.Controllers
{
    /// <summary>
    /// document controller class
    /// </summary>
    [System.Web.Http.Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("api/logging")]
    public class LoggingController : ApiController
    {
        private readonly Services _services;
        public LoggingController(Services services)
        {
            this._services = services;
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(LogModel log)
        {
            return Ok(_services.Log_Create(log));
        }
    }
}
