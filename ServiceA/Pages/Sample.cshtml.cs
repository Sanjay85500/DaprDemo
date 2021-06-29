using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceA.Pages
{
    public class SampleModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public SampleModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                //var response =  _daprClient.InvokeMethodAsync<>(HttpMethod.Get,"serviceb","api/sample");
                //Dictionary<string, string> secrets = await _daprClient.GetSecretAsync("kubernetes", "test-secret");
                var request = _daprClient.CreateInvokeMethodRequest("serviceb", "api/sample");
                //request.Headers.Authorization = new AuthenticationHeaderValue("bearer", secrets["secret1"]);
                //request.Content.add
                var res = await _daprClient.InvokeMethodWithResponseAsync(request);
                var data = await res.Content.ReadAsStringAsync();
                var client = DaprClient.CreateInvokeHttpClient();
                var r = await client.PostAsync("http://serviceb/api/sample", new StringContent("raw content", Encoding.UTF8, "application/json"));
                //var response = await client.GetStringAsync("http://serviceb/api/sample");
                Console.WriteLine(data);
                ViewData["sampleData"] = data;

                //var client = new HttpClient();
                //var response = await client.PostAsync("http://127.0.0.1:3500/v1/invoke/serviceb/method/api/sample", null);
                //ViewData["sampleData"] = await response.Content.ReadAsStringAsync();
                //return Redirect("http://127.0.0.1:3500/v1/invoke/serviceb/method/api/sample");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Page();


        }
    }
}
