using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Razor2Pdf
{
    /// <summary>
    /// A builder that use an ITemplateService output and converted it to PDF.
    /// </summary>
    /// <seealso cref="Razor2Pdf.IPdfBuilder" />
    public class PdfBuilder : IPdfBuilder
    {
        private static IConverter converter = new SynchronizedConverter(new PdfTools());
        private readonly ITemplateService _templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfBuilder"/> class.
        /// </summary>
        /// <param name="templateService">The template service.</param>
        public PdfBuilder(ITemplateService templateService)
        {
            _templateService = templateService;

            GlobalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.A4,
                ImageQuality = 100,
                Margins = new MarginSettings { Unit = Unit.Centimeters, Top = 1.1, Bottom = 0.10, Left = 1.8, Right = 1.8 }
            };

            LoadSettings = new LoadSettings
            {
                ZoomFactor = 1.0
            };

            WebSettings = new WebSettings();
        }

        /// <summary>
        /// Gets or sets the global settings used to generate the PDF.
        /// </summary>
        public GlobalSettings GlobalSettings { get; set; }

        /// <summary>
        /// Gets or sets the load settings used to generate the PDF.
        /// </summary>
        public LoadSettings LoadSettings { get; set; }

        /// <summary>
        /// Gets or sets the web settings used to generate the PDF.
        /// </summary>
        public WebSettings WebSettings { get; set; }

        /// <summary>
        /// Builds the PDF using the output of template rendering.
        /// </summary>
        /// <param name="templateFileName">The Razor template file name.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The PDF file bytes.</returns>
        public async Task<byte[]> BuildAsync(string templateFileName, object viewModel)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Loads the native library.
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"libwkhtmltox");
                var context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(path);
            }

            // Use the DinkToPdf converter to call the native library and perform the 
            // the conversion from HTML to PDF.
            var output = converter.Convert(new HtmlToPdfDocument
            {
                GlobalSettings = GlobalSettings,
                Objects =
                {
                    new ObjectSettings
                    {
                        LoadSettings = LoadSettings,
                        WebSettings = WebSettings,
                        HtmlContent = await _templateService.RenderTemplateAsync(templateFileName, viewModel)
                    } 
                }
            });

            return output;
        }         
    }
}