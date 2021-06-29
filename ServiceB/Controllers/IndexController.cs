using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        // GET: api/<IndexController>
        [HttpGet]
        public object Get()
        {
            return Request.Headers.ToList();
           // return new string[] { "value1", "value2" };
        }

        // GET api/<IndexController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<IndexController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<IndexController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IndexController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
