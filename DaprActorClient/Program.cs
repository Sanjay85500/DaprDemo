using Dapr.Actors;
using Dapr.Actors.Client;
using MyActor.Interfaces;
using System;
using System.Threading.Tasks;

namespace DaprActorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Startup up...");

                var actorType = "MyActor";

                var actorId = new ActorId("1");

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
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
