using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private static List<HangHoa> hanghoas = new List<HangHoa>();
         
        [Route("/GetFirstData")]
        [HttpGet]
        public IActionResult GetFirstData()
        {
            HangHoa hhs = new HangHoa
            {
                ID = Guid.NewGuid().ToString(),
                TenSanPham = "galaxy s21 untral 5G",
                DonGia = 21000000
            };
            return Ok(hhs);
        }
         
        // post lên dạng json object
        [HttpPost("/add")]
        public IActionResult Add(HangHoa hhs)
        { 
            hanghoas.Add(hhs);
            return Ok(hhs);
        }
         
        [HttpGet("/getall")]
        public IActionResult GetAll()
        { 
            return Ok(hanghoas);
        }

        [HttpGet("/getbyid/{id}")] 
        public IActionResult Getbyid(string id)
        {
            try
            {
                var hanghoa = hanghoas.SingleOrDefault(a => a.ID == id);
                if (hanghoa == null)
                {
                    return NotFound(); // 404
                }

                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest(); // dành cho case lỗi
            }
        }
    }
}
