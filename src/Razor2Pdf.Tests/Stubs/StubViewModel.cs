using System;
using System.Collections.Generic;
using System.Text;

namespace Razor2Pdf.Tests.Stubs
{
    public class StubViewModel
    {
        public int IntData { get; set; } = 123;
        public string StringData { get; set; } = "Test";

        public DateTime DateData { get; set; } = new DateTime(2019, 7, 24);

        public string[] ArrayData { get; set; } = new string[] { "111", "222", "333" };
    }
}
