using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Westwind.AspNetCore.Markdown;

namespace NewsSite.WebUi.TagHelpers
{
    [HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        [HtmlAttributeName("normalize-whitespace")]
        public bool NormalizeWhitespace { get; set; } = true;

        [HtmlAttributeName("markdown")]
        public ModelExpression Markdown { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            string content = null;
            if (Markdown != null)
                content = Markdown.Model?.ToString();

            if (content == null)
                content = (await output.GetChildContentAsync(NullHtmlEncoder.Default))
                                .GetContent(NullHtmlEncoder.Default);

            if (string.IsNullOrEmpty(content))
                return;

            content = content.Trim('\n', '\r');

            //string markdown = NormalizeWhiteSpaceText(content);
            string markdown = (content);

            var parser = MarkdownParserFactory.GetParser();
            var html = parser.Parse(markdown);

            output.TagName = null;  // Remove the <markdown> element
            output.Content.SetHtmlContent(html);
        }
        private string NormalizeWhiteSpaceText(string text)
        {
            if (!NormalizeWhitespace || string.IsNullOrEmpty(text))
                return text;

            var lines = GetLines(text);
            if (lines.Length < 1)
                return text;

            string line1 = null;

            // find first non-empty line
            for (int i = 0; i < lines.Length; i++)
            {
                line1 = lines[i];
                if (!string.IsNullOrEmpty(line1))
                    break;
            }

            if (string.IsNullOrEmpty(line1))
                return text;

            string trimLine = line1.TrimStart();
            int whitespaceCount = line1.Length - trimLine.Length;
            if (whitespaceCount == 0)
                return text;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > whitespaceCount)
                    sb.AppendLine(lines[i].Substring(whitespaceCount));
                else
                    sb.AppendLine(lines[i]);
            }

            return sb.ToString();
        }

        string[] GetLines(string s, int maxLines = 0)
        {
            if (s == null)
                return null;

            s = s.Replace("\r\n", "\n");

            if (maxLines < 1)
                return s.Split(new char[] { '\n' });

            return s.Split(new char[] { '\n' }).Take(maxLines).ToArray();
        }
    }
}
