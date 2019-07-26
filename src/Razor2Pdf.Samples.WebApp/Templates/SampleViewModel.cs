using System;

namespace Razor2Pdf.Samples.WebApp.Templates
{
    public class SampleViewModel
    {
        public int IntData { get; set; } = 123;
        public string StringData { get; set; } = "Test";

        public DateTime DateData { get; set; } = DateTime.Now;

        public string[] ArrayData { get; set; } = new string[] { "111", "222", "333" };
    }
}
