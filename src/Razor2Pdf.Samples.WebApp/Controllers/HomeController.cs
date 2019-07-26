using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor2Pdf.Samples.WebApp.Models;
using Razor2Pdf.Samples.WebApp.Templates;

namespace Razor2Pdf.Samples.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<FileResult> Pdf()
        {
            var templateService = new TemplateService();
            var builder = new PdfBuilder(templateService);
            builder.WebSettings.UserStyleSheet = "Templates/css/sample.css";

            var pdfData = await builder.BuildAsync("Templates/SampleTemplate", new SampleViewModel());

            return new FileContentResult(pdfData, "application/pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
