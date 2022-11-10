using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Newtonsoft.Json.Linq;

namespace Services.HtmlHelpers
{
    /// <summary>
    /// HtmlHelper, který vykreslí ikonu povinného pole. Lze volat pomocí @Html.RenderRequiredIcon() nebo RenderRequiredIconHtmlHelper.RenderRequiredIcon()
    /// </summary>
    public static class RenderRequiredIconHtmlHelper
    {
        private const string RequiredElement = "<span class='fas fa-exclamation-circle text-danger requiredIcon' title='Povinné pole'></span>";


        public static IHtmlContent RenderRequiredIcon()
        {
            var content = new HtmlContentBuilder().AppendHtml(RequiredElement);
            return content;
        }

        /// <summary>
        /// Extension method pro HtmlHelper
        /// </summary>
        /// <param name="helper">This Html helper</param>
        /// <returns></returns>
        public static IHtmlContent RenderRequiredIcon(this IHtmlHelper helper)
        {
            var content = new HtmlContentBuilder().AppendHtml(RequiredElement);

            //if (SomeCondition())
            //{
            //}

            return content;
        }

        /// <summary>
        /// Extension method pro HtmlHelper
        /// </summary>
        /// <param name="helper">This Html helper</param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IHtmlContent RenderRequiredIconFor<TModel, TValue>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var content = new HtmlContentBuilder();

            // Model data
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, helper.ViewData, helper.MetadataProvider);
            
            // Is required
            var hasRequired = modelExplorer.Metadata?.IsRequired ?? false;

            if (hasRequired)
                return new HtmlContentBuilder().AppendHtml(RequiredElement);


            return new HtmlContentBuilder().AppendHtml(string.Empty);
        }
    }
}
