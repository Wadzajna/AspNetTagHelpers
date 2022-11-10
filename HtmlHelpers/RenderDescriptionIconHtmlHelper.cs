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
    /// HtmlHelper, který vykreslí ikonu poznámka s náhledem textu. Lze volat pomocí @Html.RenderDescriptionIcon() nebo RenderDescriptionIconHtmlHelper.RenderDescriptionIcon()
    /// </summary>
    public static class RenderDescriptionIconHtmlHelper
    {
        public static IHtmlContent RenderDescriptionIcon(string? text)
        {
            var content = new HtmlContentBuilder().AppendHtml(GetDesciptionIcon(text));
            return content;
        }

        /// <summary>
        /// Extension method pro HtmlHelper
        /// </summary>
        /// <param name="helper">This Html helper</param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IHtmlContent RenderDescriptionIcon(this IHtmlHelper helper, string? text)
        {
            var content = new HtmlContentBuilder().AppendHtml(GetDesciptionIcon(text));

            //if (SomeCondition())
            //{
            //}

            return content;
        }

        private static string GetDesciptionIcon(string? text)
        {
            return $"<span class='fas fa-question-circle descriptionIcon' title='{text}'></span>";
        }
    }
}
