using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Services.Models;

namespace Services.TagHelpers
{
    [HtmlTargetElement("icon-bool-for")]
    public class IconBoolForTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-html")]
        public object? CustomHtmlAttributes { get; set; }

        [HtmlAttributeName("asp-hidden")]
        public bool? Hidden { get; set; }

        [HtmlAttributeName("asp-readonly")]
        public bool ReadOnly { get; set; } = false;

        
        private readonly IHtmlGenerator _generator;

        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        public List<IconSelectListItem> Items { get; set; }

        public IconBoolForTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;

            Items = new List<IconSelectListItem>()
            {
                new IconSelectListItem() { Text = "Ano", Value = "True", Class = "btn-outline-success", Icon = "fas fa-check" },
                new IconSelectListItem() { Text = "Ne", Value = "False", Class = "btn-outline-danger", Icon = "fas fa-times" }
            };
        }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagName = "icon-bool-list";
            output.TagMode = TagMode.StartTagAndEndTag;

            // Attributes for input
            var attributes = new RouteValueDictionary();

            var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(CustomHtmlAttributes);

            // Is readonly. Add attributes
            if (this.ReadOnly)
            {
                attributes.Add("disabled", "disabled");
                attributes.Add("readonly", "readonly");
            }

            // Final attributes for input
            var htmlAttributes = new RouteValueDictionary(attributes.Union(customAttributes).ToDictionary(k => k.Key, k => k.Value));
            
            await using var writer = new StringWriter();

            // INPUT
            await writer.WriteAsync("<div class='form-input'>");

            
            // For each item generate option

            await writer.WriteAsync("<div class='btn-group' role='group'>");
            
            foreach (var option in Items)
            {
                var name = $"{For.Name}_btnoption{Items.IndexOf(option) + 1}";
                name = name.Replace('.', '_');

                var optinput = _generator.GenerateRadioButton(ViewContext, For.ModelExplorer, For.Name, option.Value, null, new { id = name, autocomplete = "off" });
                optinput.AddCssClass("btn-check");
                optinput.WriteTo(writer, HtmlEncoder.Default);

                var optlabel = _generator.GenerateLabel(ViewContext, For.ModelExplorer, name, string.Empty, null);
                optlabel.AddCssClass("btn");
                optlabel.AddCssClass("icon-bool-option");
                optlabel.AddCssClass(option.Class);

                // Nejprve do lablu vložím ikonku
                if (!string.IsNullOrEmpty(option.Icon)) optlabel.InnerHtml.AppendHtml($"<span class='{option.Icon}'></span>");

                // Pak text
                optlabel.InnerHtml.AppendHtml($"<span>{option.Text}</span>");

                // Kontrola readonly
                if (this.ReadOnly)
                {
                    optlabel.AddCssClass("disabled");
                    optlabel.Attributes.Add("aria-disabled", "true");
                }

                optlabel.WriteTo(writer, HtmlEncoder.Default);

            }

            await writer.WriteAsync("</div>");
            
            
            await writer.WriteAsync("</div>");

            // Hide
            if (Hidden.HasValue && Hidden.Value)
            {
                output.SuppressOutput();
                return;
            }

            // Render to output
            output.PreContent.AppendHtml(writer.ToString());
        }
    }
}
