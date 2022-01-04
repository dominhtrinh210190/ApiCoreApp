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
    public class CategoryController : Controller
    {
        private IServiceWrapper _service;

        public CategoryController(IServiceWrapper service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetCategorys()
        {
            try
            {
                var listCategorys = _service.Category.GetAll();
                if (listCategorys != null)
                {
                    return Ok(listCategorys);
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
