using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using SOCVR.Website.Server.Services;

namespace SOCVR.Website.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddResponseCompression();

            services.Configure<Configuration>(Configuration.GetSection(nameof(Configuration)));

            services.AddTransient<IContentFilePathSplitter, ContentFilePathSplitter>();
            services.AddTransient<IContentFilePathTranslator, ContentFilePathTranslator>();
            services.AddTransient<IContentFileProvider, ContentFileProvider>();
            services.AddTransient<IContentPageTitleProvider, ContentPageTitleProvider>();
            services.AddTransient<IFileProvider, FileProvider>();
            services.AddTransient<INavigationDataFileProvider, NavigationDataFileProvider>();
            services.AddTransient<INavigationMenusProvider, NavigationMenusProvider>();
            services.AddTransient<IGitManager, GitManager>();
            services.AddTransient<IProcessRunner, ProcessRunner>();
            services.AddTransient<IDirectoryProvider, DirectoryProvider>();

            services.AddSingleton<IGitPullCache, GitPullCache>();

            services.AddMemoryCache();

            // app insights
            if (Configuration.GetValue<bool>("AppInsights:Enabled"))
            {
                services.AddApplicationInsightsTelemetry(o =>
                {
                    o.ApplicationVersion = Configuration["Meta:AppVersion"] ?? "";
                    o.DeveloperMode = Configuration.GetValue<bool?>("AppInsights:DeveloperMode").GetValueOrDefault(false);
                    o.EnableAdaptiveSampling = Configuration.GetValue<bool?>("AppInsights:AdaptiveSampling").GetValueOrDefault(false);
                    o.EnableQuickPulseMetricStream = true;
                    o.InstrumentationKey = Configuration["AppInsights:InstrumentationKey"];
                });
            }

            services.AddTransient<IJavaScriptSnippetFactory, JavaScriptSnippetFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IGitManager gitManager)
        {
            // do the first pull here at startup
            if (!gitManager.DoesRepositoryExist())
            {
                gitManager.Clone();
            }

            gitManager.Pull();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseResponseCompression();
            app.UseStatusCodePagesWithReExecute("/Home/Errors/{0}");
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "errorGeneral",
                    template: "Home/Error",
                    defaults: new { controller = "Home", action = "Error" });

                routes.MapRoute(
                    name: "errorWithCode",
                    template: "Home/Errors/{errorCode}",
                    defaults: new { controller = "Home", action = "Errors" });

                routes.MapRoute(
                    name: "css",
                    template: "css/{path}",
                    defaults: new { controller = "Home", action = "Css" });

                routes.MapRoute(
                    name: "images",
                    template: "images/{path}",
                    defaults: new { controller = "Home", action = "Image" });

                routes.MapRoute(
                    name: "default",
                    template: "{*path}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
