using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .ConfigureAppConfiguration(b =>
                 {
                     if (File.Exists("config/config.json"))
                     {
                         b.AddJsonFile("config/config.json", optional: true, reloadOnChange: true);
                     }
                     if (File.Exists("secret/secret.json"))
                     {
                         b.AddJsonFile("secret/secret.json", optional: true, reloadOnChange: true);
                     }
                     // var daprClient = new DaprClientBuilder().Build();
                     // var secretDescriptors = new List<DaprSecretDescriptor>
                     //{
                     //    new DaprSecretDescriptor("test-secret"),
                     //    new DaprSecretDescriptor("test-secret-file")
                     //};
                     // b.AddDaprSecretStore("kubernetes", secretDescriptors, daprClient);
                 });
    }
}
