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
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/document")]
    public class DocumentController : ApiController
    {
        private readonly Services _services;
        public DocumentController(Services services)
        {
            this._services = services;
        }

        [HttpGet]
        [Route("getlastactivity")]
        public IHttpActionResult GetLastActivity(int limit)
        {
            return Ok(_services.Document_GetByActivity(limit));
        }

        [HttpGet]
        [Route("getbystatus")]
        public IHttpActionResult GetByStatus(DocumentStatusEnum status)
        {
            return Ok(_services.Document_GetAllByStatus(status));
        }

        [HttpPost]
        [Route("getbyfilter")]
        public IHttpActionResult GetByFilter(DocumentFilter filter)
        {
            return Ok(_services.Document_GetByFilter(filter));
        }

        [HttpGet]
        [Route("getbyid")]
        public IHttpActionResult GetById(long id)
        {
            return Ok(_services.Document_GetById(id));
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(DocumentModel document)
        {
            return Ok(_services.Document_Create(document));
        }

        [HttpPost]
        [Route("setprepared")]
        public IHttpActionResult SetPrepared(DocumentModel document)
        {
            return Ok(_services.Document_SetPrepared(document));
        }

        [HttpPost]
        [Route("setbilled")]
        public IHttpActionResult SetBilled(DocumentModel document)
        {
            //var use = User.Identity.Name;
            return Ok(_services.Document_SetBilled(document));
        }

        [HttpPost]
        [Route("setdelivered")]
        public IHttpActionResult SetDelivered(DocumentModel document)
        {
            //var use = User.Identity.Name;
            return Ok(_services.Document_SetDelivered(document));
        }

        [HttpPost]
        [Route("setnotincome")]
        public IHttpActionResult SetNotIncome(DocumentModel document)
        {
            //var use = User.Identity.Name;
            return Ok(_services.Document_SetSigned(document));
        }

        [HttpPost]
        [Route("updateline")]
        public IHttpActionResult UpdateLine(DocumentLineModel line)
        {
            return Ok(_services.Document_UpdateLine(line));
        }

        [HttpGet]
        [Route("getLast")]
        public IHttpActionResult GetLast()
        {
            return Ok(_services.Document_GetLast());
        }
    }
}