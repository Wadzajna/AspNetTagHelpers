using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Utils.Extensions
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Extension mhetod for ISession that store object T in session as Json string
        /// </summary>
        /// <typeparam name="T">Object to store</typeparam>
        /// <param name="session">This session</param>
        /// <param name="key">Key for session data</param>
        /// <param name="value">Data of the object</param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        /// <summary>
        /// Extension mhetod for ISession object that deserialize session data acording to key.
        /// If session has no data or its not found. Default object is returned
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="session">This session</param>
        /// <param name="key">Key for the session data</param>
        /// <returns></returns>
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
