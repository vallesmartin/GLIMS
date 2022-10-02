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
    /// document controller class
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/inventory")]
    public class InventoryController : ApiController
    {
        private readonly Services _services;
        public InventoryController(Services services)
        {
            this._services = services;
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_services.Inventory_GetOpen());
        }

        [HttpGet]
        [Route("getOpen")]
        public IHttpActionResult GetOpen()
        {
            return Ok(_services.Inventory_GetOpen());
        }

        /*
        get by status

        getdetail

        getdetailbyid

        getdetailbyitem

        create


        */

    }
}