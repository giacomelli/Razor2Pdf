using NUnit.Framework;
using Razor2Pdf.Tests.Stubs;
using System;
using System.Threading.Tasks;

namespace Razor2Pdf.Tests
{
    [TestFixture]
    public class TemplateServiceTest
    {
        [Test]
        public async Task RenderTemplateAsync_TemplateWithViewModel_Html()
        {
            var target = new TemplateService();
            var model = new StubViewModel();
            var actual = await target.RenderTemplateAsync("Stubs/TemplateWithModel.cshtml", model);
            StringAssert.Contains("IntData: 123", actual);
            StringAssert.Contains("StringData: Test", actual);
            StringAssert.Contains($"DateData: {model.DateData:dd/MM/yyyy}", actual);
            StringAssert.Contains("Item: 111", actual);
            StringAssert.Contains("Item: 222", actual);
            StringAssert.Contains("Item: 333", actual);
        }
    }
}
