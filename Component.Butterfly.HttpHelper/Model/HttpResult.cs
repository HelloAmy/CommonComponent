// ***********************************************************************
// Assembly         : Common.Julius.HttpHelper
// Author           : zhujinrong
// Created          : 04-19-2016
//
// Last Modified By : zhujinrong
// Last Modified On : 04-19-2016
// ***********************************************************************
// <copyright file="HttpResult.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Net;

/// <summary>
/// The Helper namespace.
/// </summary>
namespace Component.Butterfly.HttpHelper
{
    /// <summary>
    /// Class HttpResult.
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// The cookie
        /// </summary>
        private string cookie;

        /// <summary>
        /// The cookie collection
        /// </summary>
        private CookieCollection cookieCollection;

        /// <summary>
        /// The HTML
        /// </summary>
        private string html;

        /// <summary>
        /// The result byte
        /// </summary>
        private byte[] resultByte;

        /// <summary>
        /// The header
        /// </summary>
        private WebHeaderCollection header;

        /// <summary>
        /// The status description
        /// </summary>
        private string statusDescription;

        /// <summary>
        /// The status code
        /// </summary>
        private HttpStatusCode statusCode;

        /// <summary>
        /// 重定向路径
        /// </summary>
        private string responseUrl = string.Empty;

        /// <summary>
        /// Http请求返回的Cookie
        /// </summary>
        /// <value>The cookie.</value>
        public string Cookie
        {
            get { return this.cookie; }
            set { this.cookie = value; }
        }

        /// <summary>
        /// Cookie对象集合
        /// </summary>
        /// <value>The cookie collection.</value>
        public CookieCollection CookieCollection
        {
            get { return this.cookieCollection; }
            set { this.cookieCollection = value; }
        }

        /// <summary>
        /// 返回的String类型数据 只有ResultType.String时才返回数据，其它情况为空
        /// </summary>
        /// <value>The HTML.</value>
        public string Html
        {
            get { return this.html; }
            set { this.html = value; }
        }

        /// <summary>
        /// 返回的Byte数组 只有ResultType.Byte时才返回数据，其它情况为空
        /// </summary>
        /// <value>The result byte.</value>
        public byte[] ResultByte
        {
            get { return this.resultByte; }
            set { this.resultByte = value; }
        }

        /// <summary>
        /// header对象
        /// </summary>
        /// <value>The header.</value>
        public WebHeaderCollection Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        /// <summary>
        /// 返回状态说明
        /// </summary>
        /// <value>The status description.</value>
        public string StatusDescription
        {
            get { return this.statusDescription; }
            set { this.statusDescription = value; }
        }

        /// <summary>
        /// 返回状态码,默认为OK
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        /// <summary>
        /// 重定向路径
        /// </summary>
        /// <value>The response URL.</value>
        public string ResponseUrl
        {
            get { return this.responseUrl; }
            set { this.responseUrl = value; }
        }
    }
}
