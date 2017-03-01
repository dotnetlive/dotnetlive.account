using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LiuFangJwtAuth.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize(Policy = "admin")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "test")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
