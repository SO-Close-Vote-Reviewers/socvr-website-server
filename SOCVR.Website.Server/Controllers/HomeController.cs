﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Models;
using Microsoft.Extensions.Logging;

namespace SOCVR.Website.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentFileProvider contentFileProvider;
        private readonly INavigationMenusProvider navMenusProvider;
        private readonly IContentFilePathTranslator translator;
        //private readonly IContentPageTitleProvider contentPageTitleProvider;
        private readonly IGitManager gitManager;
        private readonly ILogger<HomeController> logger;

        public HomeController(IContentFileProvider contentFileProviderService, INavigationMenusProvider navMenuProviderService, 
            IContentFilePathTranslator translatorService, IGitManager gitManagerService, ILogger<HomeController> logger)
        {
            contentFileProvider = contentFileProviderService;
            navMenusProvider = navMenuProviderService;
            translator = translatorService;
            //contentPageTitleProvider = contentPageTitleProviderService;
            gitManager = gitManagerService;
            this.logger = logger;
        }

        public IActionResult Index(string path)
        {
            logger.LogInformation($"Start of index action, path '{path}'");
            if (!gitManager.DoesRepositoryExist())
            {
                logger.LogDebug("Repo does not exist");
                gitManager.Clone();
            }

            logger.LogDebug("pulling");
            gitManager.Pull();

            var model = new IndexViewModel();

            if (!contentFileProvider.TryGetContentFileContents(path, ContentFilePathType.MarkdownFile, out string content))
            {
                logger.LogInformation($"markdown file '{path}' was not found");
                return NotFound();
            }

            model.ContentMarkdown = content;
            model.NavMenues = navMenusProvider.GetNavigationMenus(path);
            //model.PageTitle = contentPageTitleProvider.GetContentPageTitle(path);

            return View(model);
        }

        public IActionResult Css(string path)
        {
            var physicalPath = translator.TranslatePath(path, ContentFilePathType.StylesFile);
            var bytes = System.IO.File.ReadAllBytes(physicalPath);

            return File(bytes, "text/css");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Errors(string errorCode)
        {
            if (errorCode == "404")
            {
                return View("NotFound");
            }

            return RedirectToAction("Error");
        }
    }
}
