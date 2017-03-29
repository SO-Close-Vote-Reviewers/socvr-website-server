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
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables("App_")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddOptions();
            services.Configure<Configuration>(Configuration);

            services.AddTransient<IContentFilePathSplitter, ContentFilePathSplitter>();
            services.AddTransient<IContentFilePathTranslator, ContentFilePathTranslator>();
            services.AddTransient<IContentFileProvider, ContentFileProvider>();
            services.AddTransient<IContentPageTitleProvider, ContentPageTitleProvider>();
            services.AddTransient<IFileProvider, FileProvider>();
            services.AddTransient<INavigationDataFileProvider, NavigationDataFileProvider>();
            services.AddTransient<INavigationMenusProvider, NavigationMenusProvider>();
            services.AddTransient<IGitManager, GitManager>();
            services.AddTransient<IProcessRunner, ProcessRunner>();
            services.AddSingleton<IGitPullCache, GitPullCache>();

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var loggingLevel = Configuration.GetValue<LogLevel>("LoggingLevel");
            loggerFactory.AddConsole(loggingLevel);
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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
                    name: "default",
                    template: "{*path}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
