using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceA.Pages
{
    public class RabbitMQModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string data { get; set; }

        private readonly DaprClient _daprClient;

        public RabbitMQModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                await _daprClient.PublishEventAsync<string>("pubsub", "newTopic", data);
                ViewData["response"] = "Successfully Published";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Page();



        }
    }
}
