using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Services.TagHelpers
{
    [HtmlTargetElement(Attributes = "background-color")]
    public class BackgroundColorTagHelper : TagHelper
    {
        public string? BackgroundColor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", $"btn btn-{BackgroundColor}");
        }
    }
}
