using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;
using WebApp.BusinessLayer;
using WebApp.DataAccess;

namespace WebApp.Controllers
{
    /// <summary>
    /// item controller class for authenticate users
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/system")]
    public class SystemController : ApiController
    {
        private readonly Services _services;
        private readonly Repository _repository;
        public SystemController(Services services, Repository repository )
        {
            this._services = services;
            this._repository = repository;
        }
        [HttpGet]
        [Route("version")]
        public IHttpActionResult GetAll()
        {
            return Ok("2.5");
        }

        [HttpGet]
        [Route("changesLog")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_repository.Changes_Get());
        }

        [HttpGet]
        [Route("lastChange")]
        public IHttpActionResult GetLastChange()
        {
            return Ok(_repository.Changes_GetLastForSynchro());
        }
    }
}