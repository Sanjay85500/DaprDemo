using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyActorClient.Controllers
{
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpPost("mybinding")]
        public async Task<ActionResult> Post()
        {
            Console.WriteLine("In mybinding controller");
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var a = await reader.ReadToEndAsync();
                Console.WriteLine(a);
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("--------------------------------------------------");
            return Ok();
        }
    }
}
