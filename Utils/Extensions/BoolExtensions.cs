using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class BoolExtensions
    {
        /// <summary>
        /// This method return if the value is default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }

        /// <summary>
        /// This method returns if nullable bool value have true value
        /// You dont need to write value != null && value.Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasTrueValue(this bool? value)
        {
            return value.HasValue && value.Value;
        }


        /// <summary>
        /// This method returns if nullable bool value have false value
        /// You dont need to write value != null && !value.Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasFalseValue(this bool? value)
        {
            return value.HasValue && !value.Value;
        }
    }
}
