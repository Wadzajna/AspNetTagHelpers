using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Services.TagHelpers
{
    /// <summary>
    /// Taghelper, který vykreslí ikonku jako span. Volá se pomocí elementu <icon />
    /// </summary>
    [HtmlTargetElement("icon", TagStructure = TagStructure.WithoutEndTag)]
    public class IconTagHelper : TagHelper
    {
        public string? Name { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            var classAttributes = output.Attributes["class"];
            var classes = (classAttributes?.Value as HtmlString)?.Value ?? string.Empty;

            classes += $" fas fa-{Name}";
            classes = classes.Trim();

            output.Attributes.SetAttribute("class", classes);
        }
    }
}
