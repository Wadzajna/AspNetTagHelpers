using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace Services.TagHelpers
{
    /// <summary>
    /// Tag helper, který vykreslí textový input a k tomu také label a validační blok pro parametr. Vše pod sebe.
    /// </summary>
    [HtmlTargetElement("text-for")]
    public class TextForTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-html")]
        public object CustomHtmlAttributes { get; set; }

        [HtmlAttributeName("asp-hidden")]
        public bool? Hidden { get; set; }

        [HtmlAttributeName("asp-readonly")]
        public bool ReadOnly { get; set; } = false;

        [HtmlAttributeName("asp-input-class")]
        public string ExtraInputClass { get; set; }

        [HtmlAttributeName("asp-placeholder")] 
        public bool ShowPlaceholder { get; set; } = false;


        private readonly IHtmlGenerator _generator;

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public TextForTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";
            //output.TagMode = TagMode.StartTagAndEndTag;

            var desciptionElement = "<span class='fas fa-question-circle'><span>";
            var requiredElement = "<span class='fas fa-exclamation-circle text-danger'><span>";

            using var writer = new StringWriter();

            //writer.Write($"<div class='row {this.ExtraRowClass}'>");
            //writer.Write("<div class='col-12'>");

            // LABEL
            writer.Write("<div class='form-label'>");
            var label = _generator.GenerateLabel(ViewContext, For.ModelExplorer, For.Name, null, null);
            label.WriteTo(writer, HtmlEncoder.Default);

            if (!string.IsNullOrEmpty(For.Metadata.Description))
                writer.Write(desciptionElement);
            
            if(For.Metadata.IsRequired)
                writer.Write(requiredElement);

           
            writer.Write("</div>");


            
            // INPUT
            writer.Write("<div class='form-input'>");

            // Attributes for input
            var attributes = new RouteValueDictionary();

            // Add placeholder
            if(this.ShowPlaceholder)
                attributes.Add("Placeholder", For.Metadata.DisplayName);

            // Add extra input class
            if (!string.IsNullOrEmpty(this.ExtraInputClass))
                attributes.Add("class", ExtraInputClass);

            var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(CustomHtmlAttributes);

            if (this.ReadOnly)
            {
                attributes.Add("disabled", "disabled");
                attributes.Add("readonly", "readonly");
            }
            var htmlAttributes = new RouteValueDictionary(attributes.Union(customAttributes)
                .ToDictionary(k => k.Key, k => k.Value));

            var input = _generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, null, htmlAttributes);
            input.WriteTo(writer, HtmlEncoder.Default);
            writer.Write("</div>");


            // VALIDACE
            writer.Write("<div class='form-validation'>");
            var validation = _generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, null, null, null);
            validation.WriteTo(writer, HtmlEncoder.Default);
            writer.Write("</div>");


            //writer.Write($"</div>");
            //writer.Write($"</div>");

            // HIDE OUTPUT
            if(this.Hidden.HasValue && this.Hidden.Value)
                output.SuppressOutput();


            output.Content.AppendHtml(writer.ToString());
        }
    }
}
