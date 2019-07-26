using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor2Pdf.Samples.WebApi.Templates;

namespace Razor2Pdf.Samples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public async Task<FileResult> Get(string stringData = "Test")
        {
            var templateService = new TemplateService();
            var builder = new PdfBuilder(templateService);
            builder.WebSettings.UserStyleSheet = "Templates/css/sample.css";

            var pdfData = await builder.BuildAsync("Templates/SampleTemplate", new SampleViewModel { StringData = stringData });

            return new FileContentResult(pdfData, "application/pdf");            
        }
    }
}
