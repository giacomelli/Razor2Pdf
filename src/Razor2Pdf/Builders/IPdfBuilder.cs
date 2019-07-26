using System.Threading.Tasks;

namespace Razor2Pdf
{
    /// <summary>
    /// Define an interface for a PDF builder.
    /// </summary>
    public interface IPdfBuilder
    {
        /// <summary>
        /// Builds the PDF using the Razor template template file and a view model.
        /// </summary>
        /// <param name="templateFileName">The Razor template file name.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The bytes of built PDF file.</returns>
        Task<byte[]> BuildAsync(string templateFileName, object viewModel);
    }
}