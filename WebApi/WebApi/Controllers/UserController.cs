using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.AppSetting; 
using WebApi.Response;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiBaseController
    {
        public UserController(IServiceWrapper service, IOptions<AppSettings> appSettings, IHttpContextAccessor iHttpContextAccessor) : base(service, appSettings, iHttpContextAccessor)
        {
        }

        [HttpPost]
        public IActionResult Validate(NguoiDungViewModel model)
        {
            NguoiDungViewModel user = serviceWrapper.NguoiDung.GetByUserNamePassWord(model);
            if(user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "invalid username/password",
                    Data = null
                }); 
            }

            // cấp token
            return Ok(new ApiResponse
            {
                Success = false,
                Message = "Authenticate success",
                Data = GerenateToken(user)
            }); ;
        }
    }
}
