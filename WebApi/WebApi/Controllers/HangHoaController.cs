using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;
using WebApi.Constants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaRepository hangHoaRepository;

        public HangHoaController(IHangHoaRepository _hangHoaRepository)
        {
            hangHoaRepository = _hangHoaRepository;
        }

        [Route("/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = hangHoaRepository.GetAll();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("/Add")]
        [HttpPost]
        public IActionResult Add(HangHoaModel hangHoaVM)
        {
            try
            {
                var model = hangHoaRepository.Add(hangHoaVM);
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("/Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var response = new ApiResponse();
            try
            {
                var data = hangHoaRepository.Delete(id);

                if (data != 0)
                {
                    response.Data = data; 
                    return Ok(response);
                }
                else
                {
                    response.Result = new ResponseResult
                    {
                        Code = "404",
                        Message = "Not Found2"
                    };
                    return NotFound(response);  
                }
            }
            catch (Exception ex)
            {
                response.Result = new ResponseResult
                {
                    Code = WebApi.Constants.Constants.RESPONSE_CODE_STATUS_FAILED,
                    Message = WebApi.Constants.Constants.REQUEST_ERROR_MESSAGE
                };

                return BadRequest(response);
            }
        }

        [Route("/Update")]
        [HttpPost]
        public IActionResult Update(HangHoaModel hangHoaVM)
        {
            try
            {
                var model = hangHoaRepository.Update(hangHoaVM);
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
