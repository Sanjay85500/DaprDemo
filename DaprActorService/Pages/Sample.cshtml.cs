using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyActor.Interfaces;

namespace DaprActorService.Pages
{
    public class SampleModel : PageModel
    {
        public async Task<ActionResult> OnGetAsync()
        {
            Console.WriteLine("Startup up...");

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
            Console.WriteLine($"Got response: {response}");
            Console.ReadLine();
            return Page();
        }
    }
}
