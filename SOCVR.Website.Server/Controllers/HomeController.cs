using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Models;

namespace SOCVR.Website.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentPageProvider contentPageProvider;

        public HomeController(IContentPageProvider contentPageProviderService)
        {
            contentPageProvider = contentPageProviderService;
        }

        public IActionResult Index(string path)
        {
            var model = new IndexViewModel();

            if (!contentPageProvider.TryGetContentPageContents(path, out string content))
            {
                return NotFound();
            }

            model.ContentMarkdown = content;

            model.NavMenues = new List<List<NavLink>>();

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
