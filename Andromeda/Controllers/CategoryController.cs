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
    /// category controller class
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        private readonly Services _services;
        public CategoryController(Services services)
        {
            this._services = services;
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            return Ok(_services.Category_GetAll());
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(CategoryModel category)
        {
            return Ok(_services.Category_Create(category));
        }

        [HttpPost]
        [Route("updateorder")]
        public IHttpActionResult UpdateOrder(CategoryModel category)
        {
            return Ok(_services.Category_UpdateOrder(category));
        }
    }
}
