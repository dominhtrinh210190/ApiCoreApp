using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IServiceWrapper _service;

        public ProductController(IServiceWrapper service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var listProducts = _service.Product.GetAll();
                if (listProducts != null)
                {
                    return Ok(listProducts);
                }

                return NotFound();
            }
            catch
            {
                return Forbid();
            } 
        }
    }
}
