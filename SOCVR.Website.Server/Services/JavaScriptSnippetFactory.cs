using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore;

namespace SOCVR.Website.Server.Services
{
    public class JavaScriptSnippetFactory : IJavaScriptSnippetFactory
    {
        private readonly JavaScriptSnippet snippet;

        public JavaScriptSnippetFactory(JavaScriptSnippet snippet = null)
        {
            this.snippet = snippet;
        }

        public bool Exists => snippet != null;

        public JavaScriptSnippet GetJavaScriptSnippet() => snippet;
    }
}
