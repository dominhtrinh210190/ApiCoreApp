using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.AppSetting;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ApiBaseController
    { 

        public ProductController(IServiceWrapper service, IOptions<AppSettings> appSettings, IHttpContextAccessor iHttpContextAccessor) : base(service, appSettings, iHttpContextAccessor)
        { 
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
