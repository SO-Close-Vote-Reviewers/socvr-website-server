using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SOCVR.Website.Server.Services;

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
            if (contentPageProvider.TryGetContentPageContents(path, out string content))
            {
                return View(model: content);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
