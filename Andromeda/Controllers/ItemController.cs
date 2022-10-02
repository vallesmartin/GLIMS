using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;
using WebApp.BusinessLayer;

namespace WebApp.Controllers
{
    /// <summary>
    /// item controller class for authenticate users
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/item")]
    public class ItemController : ApiController
    {
        private readonly Services _services;
        public ItemController(Services services)
        {
            this._services = services;
        }
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            return Ok(_services.Item_GetAll());
        }

        [HttpGet]
        [Route("getbyid")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_services.Item_GetById(id));
        }

        [HttpPost]
        [Route("assignbarcode")]
        public IHttpActionResult AssignBarcode(ItemModel item)
        {
            return Ok(_services.Item_AssignBarcode(item));
        }

        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(ItemModel item)
        {
            return Ok(_services.Item_SaveById(item));
        }

        [HttpPost]
        [Route("disable")]
        public IHttpActionResult Save(int id)
        {
            return Ok(_services.Item_Disable(id));
        }
    }
}