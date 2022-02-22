using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LackingWebSecurityCore.Controllers
{
    public class CommandInjectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Rank()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TryIt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TryIt(string hostname)
        {
            if (!IsValidHostName(hostname))
            {
                ModelState.AddModelError("hostname", "The hostname provided was invalid");
            } else
            {
                ViewBag.Output = Ping(hostname);
            }

            return View();
        }

        public ActionResult WhyItsImportant()
        {
            return View();
        }

        public ActionResult Defenses()
        {
            return View();
        }

        public ActionResult KnownAssociates()
        {
            return View();
        }

        private bool IsValidHostName(string hostname)
        {
            string pattern = @"^[A-Za-z0-9\.\-]+$";
            return Regex.IsMatch(hostname, pattern);
        }

        private string Ping(string hostname)
        {
            string result = "";
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c \"ping {hostname}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };


                process.Start();
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            return result;

        }
    }
}