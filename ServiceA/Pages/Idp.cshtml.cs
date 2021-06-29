using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceA.Pages
{
    public class IdpModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public IdpModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                //var response =  _daprClient.InvokeMethodAsync<>(HttpMethod.Get,"serviceb","api/sample");
                var request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "serviceb", "api/index");
                var res = await _daprClient.InvokeMethodWithResponseAsync(request);
                var data = await res.Content.ReadAsStringAsync();
                //var client = DaprClient.CreateInvokeHttpClient();
                //var response = await client.GetStringAsync("http://serviceb/api/sample");
                Console.WriteLine(data);
                ViewData["sampleData"] = data;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Page();



        }
    }
}
