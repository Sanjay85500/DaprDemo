using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ServiceA.Pages
{
    public class SecretModel : PageModel
    {
        private readonly DaprClient _daprClient;
        private readonly IConfigurationRoot _configuration;

        public SecretModel(DaprClient daprClient, IConfiguration configuration)
        {
            _daprClient = daprClient;
            _configuration = (IConfigurationRoot)configuration;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                Dictionary<string, string> secrets = await _daprClient.GetSecretAsync("kubernetes", "test-secret");
                ViewData["secretValue"] = secrets["secret1"];

                Dictionary<string, string> secrets1 = await _daprClient.GetSecretAsync("kubernetes", "test-secret-file");
                ViewData["secretValue1"] = secrets1["secret.json"];

                ViewData["secretValue2"] = _configuration.GetValue<string>("secret1");

                ViewData["secretValue3"] = _configuration["secret.json"];


            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return Page();
        }
    }
}
