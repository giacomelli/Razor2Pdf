using RazorLight;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Razor2Pdf
{
    /// <summary>
    /// Renders content based on Razor templates.
    /// </summary>
    public class TemplateService : ITemplateService
    {
        /// <summary>
        /// Renders a template given the provided view model.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="templateFileName">The Razor template file name.</param>
        /// <param name="viewModel">View model to use for rendering the template</param>
        /// <returns>Returns the rendered template content.</returns>
        public async Task<string> RenderTemplateAsync<TViewModel>(string templateFileName, TViewModel viewModel)
        {
            if(!Path.IsPathRooted(templateFileName))
            {
                templateFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templateFileName);
            }
            
            var templatesFolder = Path.GetDirectoryName(templateFileName);

            var engine = new RazorLightEngineBuilder()
              .UseFilesystemProject(templatesFolder)
              .UseMemoryCachingProvider()
              .Build();

            return await engine.CompileRenderAsync(templateFileName, viewModel); 
        }
    }
}