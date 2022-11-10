using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Services.TagHelpers
{
    [HtmlTargetElement("customer-details")]
    public class CustomerDetailsTagHelper : TagHelper
    {
        [HtmlAttributeName("for-customername")]
        public ModelExpression CustomerName { get; set; }

        [HtmlAttributeName("for-city")]
        public ModelExpression City { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "CustomerDetails";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();

            sb.Append($"Customer Name: {this.CustomerName.Model} ");

            sb.Append($"City: {this.City.Model}");

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}
