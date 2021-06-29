using Dapr.Actors.Runtime;
using MyActor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaprActorService
{
    internal class MyActor : Actor, IMyActor
    {
        public MyActor(ActorHost host)
            : base(host)
        {
        }

        protected override Task OnActivateAsync()
        {
            Console.WriteLine($"Activating actor id: {this.Id}");
            return Task.CompletedTask;
        }

        protected override Task OnDeactivateAsync()
        {
            Console.WriteLine($"Deactivating actor id: {this.Id}");
            return Task.CompletedTask;
        }

        public async Task<string> SetDataAsync(MyData data)
        {
            Console.WriteLine("In SetDataAsync");
            await this.StateManager.SetStateAsync<MyData>(
                "my-data",  // state name
                data);      // data saved for the named state "my_data"
            
            return "Success";
        }
        public Task<MyData> GetDataAsync()
        {
            Console.WriteLine("In GetDataAsync");
            return this.StateManager.GetStateAsync<MyData>("my-data");

        }

    }
}
