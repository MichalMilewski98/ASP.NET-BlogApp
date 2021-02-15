using System;
using System.Linq;
using System.Threading.Tasks;
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Projekt.Helpers
{
    [HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        public string Source { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var childContent = await output.GetChildContentAsync();
            var lines = childContent.GetContent()
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim());
            var content = string.Join(" ", lines);
            var transformedContent = CommonMark.CommonMarkConverter.Convert(content);
            output.Content.SetHtmlContent(transformedContent);
        }
    }
}
