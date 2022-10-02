using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.DataAccess;
using WebApp.Models;
using WebApp.BusinessLayer;

namespace WebApp.Controllers
{
    /// <summary>
    /// entity controller class
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/entity")]
    public class EntityController : ApiController
    {
        private readonly Services _services;
        public EntityController(Services services)
        {
            this._services = services;
        }

        [HttpGet]
        [Route("getbytype")]
        public IHttpActionResult GetByType(EntityTypeEnum type)
        {
            return Ok(_services.Entity_GetAllByType(type));
        }

        [HttpGet]
        [Route("getbyid")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_services.Entity_GetById(id));
        }

        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(EntityModel entity)
        {
            return Ok(_services.Entity_SaveById(entity));
        }
    }
}
