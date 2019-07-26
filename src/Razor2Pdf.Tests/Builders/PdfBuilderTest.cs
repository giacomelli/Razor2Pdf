using NUnit.Framework;
using Razor2Pdf.Tests.Stubs;
using System.IO;
using System.Threading.Tasks;

namespace Razor2Pdf.Tests
{
    [TestFixture]
    public class PdfBuilderTest
    {
        [Test]
        public async Task BuildAsync_Template_Pdf()
        {
            var templateService = new TemplateService();
            var target = new PdfBuilder(templateService);
            var actual = await target.BuildAsync("Stubs/TemplateWithoutModel.cshtml", null);

            Assert.AreEqual(65394, actual.Length);
            File.WriteAllBytes("BuildAsync_Template_Pdf.pdf", actual);
        }

        [Test]
        public async Task BuildAsync_TemplateAndViewModel_Pdf()
        {
            var templateService = new TemplateService();
            var target = new PdfBuilder(templateService);

            var actual = await target.BuildAsync("Stubs/TemplateWithModel", new StubViewModel());

            Assert.AreEqual(67143, actual.Length);
            File.WriteAllBytes("BuildAsync_TemplateAndViewModel_Pdf.pdf", actual);
        }

        [Test]
        public async Task BuildAsync_TemplateCssAndViewModel_Pdf()
        {
            var templateService = new TemplateService();
            var target = new PdfBuilder(templateService);
            target.WebSettings.UserStyleSheet = "Stubs/css/stub.css";

            var actual = await target.BuildAsync("Stubs/TemplateWithModel.cshtml", new StubViewModel());

            Assert.AreEqual(59646, actual.Length);
            File.WriteAllBytes("BuildAsync_TemplateCssAndViewModel_Pdf.pdf", actual);
        }
    }
}
