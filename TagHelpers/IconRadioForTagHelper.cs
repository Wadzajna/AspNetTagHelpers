using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Services.Models;

namespace Services.TagHelpers
{
    /// <summary>
    /// Taghelper, který vykreslí select block s radio buttony. Musím předávat select list objektů IconSelectListItem, protože tento helper zobrazuje i náhledy
    /// možností. Buďto ikonky (např. fontawesome) nebo obrázek. V tom případě je potřeba předat cestu k obrázku
    /// </summary>
    [HtmlTargetElement("icon-radio-for")]
    public class IconRadioForTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-html")]
        public object? CustomHtmlAttributes { get; set; }

        [HtmlAttributeName("asp-hidden")]
        public bool? Hidden { get; set; }

        [HtmlAttributeName("asp-readonly")]
        public bool ReadOnly { get; set; } = false;

        [HtmlAttributeName("asp-items")]
        public List<IconSelectListItem>? Items { get; set; }


        private readonly IHtmlGenerator _generator;

        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        public IconRadioForTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagName = "icon-radio-list";
            output.TagMode = TagMode.StartTagAndEndTag;

            await using var writer = new StringWriter();

            // INPUT
            await writer.WriteAsync("<div class='form-input'>");

            // For each item generate option
            if (Items != null && Items.Any())
            {
                foreach (var option in Items)
                {
                    // Start option
                    await writer.WriteAsync("<div class='radio-list-option'>");

                    // Preview block
                    await writer.WriteAsync("<div class='radio-list-option-preview'>");

                    // App icon if exist
                    if (!string.IsNullOrEmpty(option.Icon) && string.IsNullOrEmpty(option.PreviewImageUrl))
                    {
                        await writer.WriteAsync($"<span class='{option.Icon} radio-list-option-icon'></span>");
                    }
                    else if (!string.IsNullOrEmpty(option.PreviewImageUrl))
                    {
                        await writer.WriteAsync($"<img src='{option.PreviewImageUrl}' alt='{option.Text}' />");
                    }
                    
                    await writer.WriteAsync("</div>");

                    // label block
                    await writer.WriteAsync("<div class='radio-list-option-label'>");
                    var label = _generator.GenerateLabel(ViewContext, For.ModelExplorer, For.Name, option.Text, new { option.Id });
                    label.WriteTo(writer, HtmlEncoder.Default);
                    await writer.WriteAsync("</div>");


                    // Radio block
                    // Attributes for input
                    var attributes = new RouteValueDictionary() { { "Id", option.Id } };
                    var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(CustomHtmlAttributes);

                    // Is readonly
                    if (this.ReadOnly)
                    {
                        attributes.Add("disabled", "disabled");
                        attributes.Add("readonly", "readonly");
                    }
                    var htmlAttributes = new RouteValueDictionary(attributes.Union(customAttributes).ToDictionary(k => k.Key, k => k.Value));

                    await writer.WriteAsync("<div class='radio-list-option-radio'>");
                    var radio = _generator.GenerateRadioButton(ViewContext, For.ModelExplorer, For.Name, option.Value, false, htmlAttributes);
                    radio.WriteTo(writer, HtmlEncoder.Default);
                    await writer.WriteAsync("</div>");

                    // End option
                    await writer.WriteAsync("</div>");
                }
            }


            await writer.WriteAsync("</div>");

            // Hide
            if (Hidden.HasValue && Hidden.Value) {
                output.SuppressOutput();
                return;
            }

            // Render to output
            output.PreContent.AppendHtml(writer.ToString());
        }
    }
}
