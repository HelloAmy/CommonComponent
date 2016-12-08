// ***********************************************************************
// Assembly         : Common.Julius.HttpHelper
// Author           : zhujinrong
// Created          : 04-19-2016
//
// Last Modified By : zhujinrong
// Last Modified On : 04-19-2016
// ***********************************************************************
// <copyright file="HttpHelper.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The HttpHelper.
/// </summary>
namespace Component.Butterfly.HttpHelper
{
    /// <summary>
    /// Class HttpHelper.
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// Gets the HTTP request data.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>HttpResult.</returns>
        public static HttpResult GetHttpRequestData(HttpRequestParam param)
        {
            HttpInstance instance = new HttpInstance();
            return instance.GetHttpRequestData(param);
        }
    }
}
