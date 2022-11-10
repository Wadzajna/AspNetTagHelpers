using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.HtmlHelpers;

namespace Services.TagHelpers
{
    [HtmlTargetElement("label", Attributes = "asp-for")]
    public class LabelForTagHelper : LabelTagHelper
    {
        public LabelForTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            // Metadata oředávaného modelu
            var metadata = For.Metadata as DefaultModelMetadata;

            // Is required
            var hasRequired = metadata?.Attributes.PropertyAttributes.Any(a => a.GetType() == typeof(RequiredAttribute)) ?? false;

            // Has description
            var hasDescription = metadata != null && !string.IsNullOrEmpty(metadata.Description);


            if (hasRequired)
            {
                output.PostElement.AppendHtml(RenderRequiredIconHtmlHelper.RenderRequiredIcon());
            }

            if (hasDescription)
            {
                output.PostElement.AppendHtml(RenderDescriptionIconHtmlHelper.RenderDescriptionIcon(metadata?.Description));
            }
        }
    }
}
