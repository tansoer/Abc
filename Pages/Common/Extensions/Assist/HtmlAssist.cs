using Microsoft.AspNetCore.Html;

namespace Abc.Pages.Common.Extensions.Assist
{
    public class HtmlAssist {
        internal HtmlString StartContainer(string containerName, string[] attributes = null) {
            var body = $"<{containerName}";
            if (attributes != null)
                foreach (var attribute in attributes) body = $"{body} {attribute}";
            return new HtmlString($"{body}>");
        }

        internal HtmlString EndDiv() => new HtmlString("</div>");

        internal string Attribute(string attributeName, string attributeValue)
            => $"{attributeName}=\"{attributeValue}\"";

        internal string ClassAttribute(string attributeValue)
            => Attribute("class", attributeValue);

        internal  string IdAttribute(string attributeValue)
            => Attribute("id", attributeValue);

        internal string DataToggleAttribute(string attributeValue)
            => Attribute("data-toggle", attributeValue);

        internal string HrefAttribute(string attributeValue) 
            => Attribute("href", attributeValue);

        internal string AspPageAttribute(string attributeValue)
            => Attribute("asp-page", attributeValue);

        internal string MethodAttribute(string attributeValue)
            => Attribute("method", attributeValue);

        internal HtmlString SelfClosingTag(string tagName, string innerText,
            string[] attributes = null) {
            var tag = $"<{tagName}";
            foreach (var attribute in attributes) tag = $"{tag} {attribute}";
            return new HtmlString($"{tag}>{innerText}</{tagName}>");
        }

        internal HtmlString SelfClosingTag(string v, IHtmlContent htmlContent, string[] attributes = null) =>
            SelfClosingTag(v, htmlContent.ToString(), attributes);

        internal HtmlString Input(string name, string value, string type = "hidden") =>
            new HtmlString($"<input type=\"{type}\" name=\"{name}\" value=\"{value}\" />");
    }
}