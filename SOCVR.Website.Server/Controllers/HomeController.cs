﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SOCVR.Website.Server.Services;
using SOCVR.Website.Server.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.IO;

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
        private readonly IGitPullCache gitCache;
        private readonly IContentPageTitleProvider contentPageTitleProvider;

        public HomeController(IContentFileProvider contentFileProviderService, INavigationMenusProvider navMenuProviderService, 
            IContentFilePathTranslator translatorService, IGitManager gitManagerService, ILogger<HomeController> loggerService, IGitPullCache gitCacheService,
            IContentPageTitleProvider contentPageTitleProviderService)
        {
            contentFileProvider = contentFileProviderService;
            navMenusProvider = navMenuProviderService;
            translator = translatorService;
            gitManager = gitManagerService;
            logger = loggerService;
            gitCache = gitCacheService;
            contentPageTitleProvider = contentPageTitleProviderService;
        }

        public IActionResult Index(string path)
        {
            logger.LogInformation($"Start of index action, path '{path}'");
            if (!gitManager.DoesRepositoryExist())
            {
                logger.LogDebug("Repo does not exist");
                gitManager.Clone();
            }

            if (gitCache.ShouldPullRepository())
            {
                logger.LogDebug("pulling");

                try
                {
                    gitManager.Pull();
                }
                catch
                {
                    //if the pull fails, the next load needs to try pulling again
                    gitCache.Invalidate();
                    throw;
                }
            }

            var model = new IndexViewModel();

            if (!contentFileProvider.TryGetContentFileContents(path, ContentFilePathType.MarkdownFile, out string content))
            {
                logger.LogWarning($"markdown file '{path}' was not found");
                return NotFound();
            }

            model.ContentMarkdown = content;
            model.NavMenues = navMenusProvider.GetNavigationMenus(path);
            model.PageTitle = contentPageTitleProvider.GetContentPageTitle(path);

            return View(model);
        }

        public IActionResult Css(string path)
        {
            if (!contentFileProvider.TryGetContentFileBytes(path, ContentFilePathType.StylesFile, out byte[] fileBytes))
            {
                logger.LogWarning($"styles file '{path}' was not found");
                return NotFound();
            }

            return File(fileBytes, "text/css");
        }

        public IActionResult Image(string path)
        {
            if (!contentFileProvider.TryGetContentFileBytes(path, ContentFilePathType.ImageFile, out byte[] fileBytes))
            {
                logger.LogWarning($"image file '{path}' was not found");
                return NotFound();
            }

            string mimeType;
            switch (Path.GetExtension(path))
            {
                case ".png":
                    mimeType = "image/png";
                    break;
                case ".gif":
                    mimeType = "image/gif";
                    break;
                default:
                    throw new Exception("Unsupported image mime type");
            }

            return File(fileBytes, mimeType);
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
