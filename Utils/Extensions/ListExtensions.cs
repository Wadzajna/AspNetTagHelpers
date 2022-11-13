using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class ListExtensions
    {
        public static bool NotNullNotEmpty<T>(this List<T>? value)
        {
            return value != null && value.Any();
        }

        public static bool NotNullNotEmpty<T>(this IEnumerable<T>? value)
        {
            return value != null && value.Any();
        }

        public static bool IsNullOrEmpty<T>(this List<T>? value)
        {
            return value == null || !value.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? value)
        {
            return value == null || !value.Any();
        }


        /// <summary>
        /// Zjistí jestli list obsahuje nějakou z hodnot v druhém listu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny<T>(this List<T>? list, IEnumerable<T> values)
        {
            return list != null && values.Any(list.Contains);
        }

        /// <summary>
        /// Zjistí jestli IEnumerable obsahuje nějakou z hodnot v druhém listu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny<T>(this IEnumerable<T>? list, IEnumerable<T> values)
        {
            return list != null && values.Any(list.Contains);
        }
    }
}
