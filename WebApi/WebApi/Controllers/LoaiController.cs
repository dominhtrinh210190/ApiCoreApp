using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Data.Entitys;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoaiController : Controller
    {
        private readonly DatabaseContext dbcontext;
        public LoaiController(DatabaseContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = dbcontext.Loais.ToList();
            return Ok(list);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetAll(int id)
        {
            var list = dbcontext.Loais.Where(a=> a.IDLoai == id);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult AddLoai(LoaiModel loai)
        {
            try
            {
                var dataModel = new Loai
                {
                    TenLoai = loai.TenLoai
                };

                dbcontext.Loais.Add(dataModel);
                dbcontext.SaveChanges();
                return Ok(dataModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLoai(int id, LoaiModel loai)
        {
            try
            {
                var dataModel = dbcontext.Loais.SingleOrDefault(a => a.IDLoai == id);

                if (dataModel == null)
                {
                    return NotFound();
                }
                else
                { 
                    dataModel.TenLoai = loai.TenLoai;
                    dbcontext.SaveChanges();
                    return Ok(dataModel);
                } 
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
