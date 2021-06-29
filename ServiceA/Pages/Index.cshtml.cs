using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceA.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly DaprClient _daprClient;

        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger,DaprClient daprClient,IConfiguration configuration)
        {
            _logger = logger;
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _configuration = configuration;
        }

        public async void OnGet()
        {
            ViewData["configValues"] = _configuration["value1"] +" "+ _configuration["value2"];
            
        }
    }
}
