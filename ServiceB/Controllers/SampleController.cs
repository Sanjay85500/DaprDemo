using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        public SampleController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        [HttpPost]
        public async Task<string> Get()
        {
            
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
            var a = await reader.ReadToEndAsync();
                Console.WriteLine(a);
            }
            return "Value from Sample Controller of ServiceB";
        }

        [Topic("pubsub", "newTopic")]
        [HttpPost("sub")]
        public async Task SavetoStateStore([FromBody] string value)
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadLine();
                Console.WriteLine(body);

            }
           
            try
            {
                Console.WriteLine("Value : " + value);
                await _daprClient.SaveStateAsync("redis-store", "mykey", value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
