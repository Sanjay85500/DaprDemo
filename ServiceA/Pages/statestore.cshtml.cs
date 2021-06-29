using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceA.Pages
{
    public class statestoreModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string key { get; set; }

        private readonly DaprClient _daprClient;

        public statestoreModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<ActionResult> OnGetDisplay()
        {
            ViewData["data"] = await _daprClient.GetStateAsync<List<string>>("redis-store", key);
            return Page();
        }
        public async Task<RedirectToPageResult> OnGetAsync(string value)
        {
            
            if(String.IsNullOrEmpty(value))
            {
                return RedirectToPage("statestore","Display");
            }
            await _daprClient.SaveStateAsync("redis-store", key, new List<string>() { value });
            return RedirectToPage("statestore", "Display");

        }
    }
}
