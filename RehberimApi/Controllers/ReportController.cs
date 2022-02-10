using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RehberimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly DbContext _db;
        public ReportController(DbContext db)
        {
            _db = db;
        }
        [HttpGet("GetReportLocation")]
        public IActionResult Index()
        {
            var sorgu = _db.Set<UserInformation>().AsQueryable().GroupBy(u =>new {u.Lang,u.Lat}).Select(g => new { KonumBilgisi = g.Key, KisiSayisi = g.Count() });

            return Ok(sorgu);
        }
    }

}
