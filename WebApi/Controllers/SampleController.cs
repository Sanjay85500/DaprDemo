using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class SampleController : ControllerBase
    {

        [HttpPost("mybinding")]
        public async Task<ActionResult> Post()
        {
           
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var a = await reader.ReadToEndAsync();
                var b = JObject.Parse(a);
                if(b.GetValue("event").ToString() == "add" && b.SelectToken("newVal.involvedObject.kind").ToString() == "Pod")
                {

                    if (b.SelectToken("newVal.reason").ToString() == "Started")
                    {
                        Console.WriteLine(b.SelectToken("newVal.message"));
                        Console.WriteLine("--------------------------------------------------");
                    }
                    else if(b.SelectToken("newVal.reason").ToString() == "Killing")
                    {
                        Console.WriteLine(b.SelectToken("newVal.message"));
                        Console.WriteLine("--------------------------------------------------");
                    }
                }
                
            }
            
            return Ok();
        }
    }
}
