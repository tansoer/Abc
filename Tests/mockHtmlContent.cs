using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Abc.Tests {

    internal class mockHtmlContent : IHtmlContent {
        private readonly string content;
        public mockHtmlContent(string s) => content = s;
        public void WriteTo(TextWriter writer, HtmlEncoder encoder) => writer.WriteLine(content);
        public override string ToString() => content;
    }
}