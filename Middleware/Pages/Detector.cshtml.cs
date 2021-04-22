using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shyjus.BrowserDetection;

namespace Middleware.Pages
{
    public class DetectorModel : PageModel
    {
        private RequestDelegate next;

        public DetectorModel(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,
                                      IBrowserDetector detector)
        {
            var browser = detector.Browser;

            if (browser.Name == BrowserNames.Edge || browser.Name == BrowserNames.EdgeChromium || browser.Name == BrowserNames.InternetExplorer)
            {
                await httpContext.Response
                      .WriteAsync("Przegladarka " + browser.Name + " nie jest obslugiwana");
            }
            else
            {
                await this.next.Invoke(httpContext);
            }
        }
        public void OnGet()
        {
        }
    }
}
