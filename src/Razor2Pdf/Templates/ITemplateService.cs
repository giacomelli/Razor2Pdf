using System.Threading.Tasks;

namespace Razor2Pdf
{
    /// <summary>
    /// Renders content based on razor templates
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Renders a template given the provided view model
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="templateFileName">The Razor template file name.</param>
        /// <param name="viewModel">View model to use for rendering the template</param>
        /// <returns>Returns the rendered template content.</returns>
        Task<string> RenderTemplateAsync<TViewModel>(string templatefileName, TViewModel viewModel);
    }
}