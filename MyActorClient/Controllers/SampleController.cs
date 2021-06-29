using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyActor.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyActorClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        public async Task<string> Get()
        {
            var actorType = "MyActor";

            var actorId = ActorId.CreateRandom();

            var proxy = ActorProxy.Create<IMyActor>(actorId, actorType);

            Console.WriteLine($"Calling SetDataAsync on {actorType}:{actorId}...");
            var response = await proxy.SetDataAsync(new MyData()
            {
                PropertyA = "ValueA",
                PropertyB = "ValueB",
            });
            Console.WriteLine($"Got response: {response}");

            Console.WriteLine($"Calling GetDataAsync on {actorType}:{actorId}...");
            var savedData = await proxy.GetDataAsync();
            var data = JsonConvert.SerializeObject(savedData);
            Console.WriteLine($"Got response: {data}");
            return data;
        }
    }
}
