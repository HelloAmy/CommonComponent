// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-10-2015
// ***********************************************************************
// <copyright file="JsonHelper.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class JsonHelper
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Jsons the serializer.
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">The t.</param>
        /// <returns>System.String.</returns>
        public static string JsonSerializer<T>(T t) where T : class
        {
            if (t == null)
            {
                return string.Empty;
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            string p = @"///Date/((/d+)/+/d+/)///"; /*////Date/((([/+/-]/d+)|(/d+))[/+/-]/d+/)////*/
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jsonString">The json string.</param>
        /// <returns>结果</returns>
        public static T JsonDeserialize<T>(string jsonString) where T : class
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return default(T);
            }

            // 将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"//Date(1294499956278+0800)//"格式  
            string p = @"/d{4}-/d{2}-/d{2}/s/d{2}:/d{2}:/d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns>System.String.</returns>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns>System.String.</returns>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("///Date({0}+0800)///", ts.TotalMilliseconds);
            return result;
        }
    }
}
