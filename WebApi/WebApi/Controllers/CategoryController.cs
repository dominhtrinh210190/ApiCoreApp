using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using WebApi.AppSetting;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ApiBaseController
    {  
        // đưa tất cả các dịch vụ cần inject vào ApiBaseController
        public CategoryController(IServiceWrapper service, IOptions<AppSettings> appSettings, IHttpContextAccessor iHttpContextAccessor) : base(service, appSettings, iHttpContextAccessor) 
        {  
        }

        [HttpGet] 
        [Authorize]
        public IActionResult GetCategorys()
        {
            try
            {
                session.SetString("name", "trinhdm");
                var getSession = session.GetString("name");

                var listCategorys = serviceWrapper.Category.GetAll();
                if (listCategorys != null)
                { 
                    return Ok(getSession); 
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
