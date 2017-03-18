using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SOCVR.Website.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly Configuration config;

        public HomeController(IOptions<Configuration> configOptions)
        {
            config = configOptions.Value;
        }

        public IActionResult Index(string path)
        {
            var pagePath = @"C:\code\socvr\socvr.org\socvr-website-content\pages\index.md";
            var content = System.IO.File.ReadAllText(pagePath);
            return View(model: content);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
