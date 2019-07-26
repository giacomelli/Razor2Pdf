using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Razor2Pdf.Samples.ConsoleApp.Templates;

namespace Razor2Pdf.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Razor2Pdf] Razor2Pdf.Samples.ConsoleApp");
            Console.WriteLine("[Razor2Pdf] Building PDF...");
            var templateService = new TemplateService();
            var builder = new PdfBuilder(templateService);
            builder.WebSettings.UserStyleSheet = "Templates/css/sample.css";

            var pdfData = builder.BuildAsync("Templates/SampleTemplate", new SampleViewModel()).GetAwaiter().GetResult();
            var pdfFileName = "Razor2Pdf.Samples.ConsoleApp.pdf";

            File.WriteAllBytes(pdfFileName, pdfData);
            Console.WriteLine($"[Razor2Pdf] File generated at {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFileName)}.");
            Console.WriteLine("[Razor2Pdf] Done.");                       
        }
    }
}
