using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utils.CustomAttributes;

namespace Utils.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// Vrátí attribute Description při použití [Description("Description for ...")]
        /// </summary>
        /// <param name="value">Enum hodnota</param>
        /// <returns>Description attribute</returns>
        public static string? GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Vrátí attribute Color při použití [Color("#ffcc00")]
        /// </summary>
        /// <param name="value">Enum hodnota</param>
        /// <returns>Description attribute</returns>
        public static string? GetColor(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field,
                        typeof(ColorAttribute)) is ColorAttribute attr)
                    {
                        return attr.Color;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Vrátí attribute Icon při použití [Icon("fas fa-user")]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetIcon(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo? field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field,
                        typeof(IconAttribute)) is IconAttribute attr)
                    {
                        return attr.Icon;
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// Převede každý Enum na SELECT LIST. Popis pole si bere z DecriptionFor attributu
        /// Metoda se volá například takto: default(NAZEV_ENUMU).GetSelectList();
        /// </summary>
        /// <typeparam name="TEnum">Objekt typu Enum</typeparam>
        /// <param name="enumValues">Enum</param>
        /// <param name="allowedValues">Povolené hodnoty</param>
        /// <returns></returns>
        public static List<SelectListItem> GetSelectList<TEnum>(this TEnum enumValues, List<int>? allowedValues = null)
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            if (allowedValues != null && allowedValues.Any())
                values = values.Where(t => allowedValues.Contains(Convert.ToInt32(t)));

            var list = values.Where(t => t != null).Select(t => new SelectListItem()
            {
                Value = Convert.ToInt32(t).ToString(),
                Text = ((Enum)((object)t!)).GetDescription()
            }).ToList();

            return list;
        }
    }
}
