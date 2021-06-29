using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class InstallServiceModel : PageModel
    {
        public ActionResult OnGet()
        {
            //ViewData["data"]
            var list = Bash("helm","repo list");
            if(list.Contains("bitnami"))
            {
                ViewData["data"] = Bash("helm","install redis bitnami/redis -n binding-namespace");
            }
            else
            {
                var h = Bash("helm", "repo add bitnami https://charts.bitnami.com/bitnami");
                ViewData["data"] = Bash("helm", "install redis bitnami/redis -n binding-namespace");
            }
            return Page();
        }

        public string Bash(string cmd,string arguments)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = cmd,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            }; 
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }
    }
}
