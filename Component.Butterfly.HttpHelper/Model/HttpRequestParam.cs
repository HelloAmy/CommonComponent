// ***********************************************************************
// Assembly         : Common.Julius.HttpHelper
// Author           : zhujinrong
// Created          : 04-19-2016
//
// Last Modified By : zhujinrong
// Last Modified On : 04-19-2016
// ***********************************************************************
// <copyright file="HttpRequestParam.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Component.Butterfly.HttpHelper
{
    /// <summary>  
    /// Http请求参数类  
    /// </summary>  
    public class HttpRequestParam
    {
        /// <summary>  
        /// 请求URL(必填)  
        /// </summary> 
        private string url = string.Empty;

        /// <summary>  
        /// 请求方式(默认为GET方式,当为POST方式时必须设置Postdata的值) 
        /// </summary>  
        private string method = "GET";

        /// <summary>  
        /// 请求超时时间(以毫秒为单位，默认180秒)  
        /// </summary>  
        private int timeout = 180000;

        /// <summary>  
        /// 写入Post数据超时间(以毫秒为单位，默认180秒) 
        /// </summary>  
        private int readWriteTimeout = 180000;

        /// <summary>  
        /// 是否与Internet资源建立持久性连接(默认true) 
        /// </summary>
        private bool keepAlive = true;

        /// <summary>  
        /// 请求标头值(默认text/html, application/xhtml+xml, */*)  
        /// </summary>  
        private string accept = "text/html, application/xhtml+xml, */*";

        /// <summary>  
        /// 请求返回类型(默认text/html)
        /// </summary>  
        private string contentType = "text/html";

        /// <summary>  
        /// 客户端访问信息(默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0))  
        /// </summary>  
        private string userAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

        /// <summary>  
        /// 返回数据编码(默认为null,可以自动识别,一般为utf-8,gbk,gb2312)
        /// </summary>  
        private Encoding encoding = null;

        /// <summary>  
        /// Post的数据类型(默认string)
        /// </summary>  
        private PostDataType postDataType = PostDataType.String;

        /// <summary>  
        /// Post请求时要发送的字符串Post数据  
        /// </summary>  
        private string postdata = string.Empty;

        /// <summary>  
        /// Post请求时要发送的Byte类型的Post数据  
        /// </summary>
        private byte[] postdataByte = null;

        /// <summary>  
        /// Cookie对象集合  
        /// </summary>  
        private CookieCollection cookiecollection = null;

        /// <summary>  
        /// 请求时的Cookie  
        /// </summary>  
        private string cookie = string.Empty;

        /// <summary>  
        /// 来源地址，上次访问地址  
        /// </summary>  
        private string referer = string.Empty;

        /// <summary>
        /// 是否使用IE代理，默认false即不使用IE代理，此时Fiddler无法抓包
        /// </summary>
        private bool isIEProxy = false;

        /// <summary>
        /// 代理IP业务名称，若要使用代理，则此属性必须赋值 本类库提供2种使用代理IP的方式，1仅赋值ProxyBusinessName不赋值ProxyIP 则自动获取代理IP，但每次请求均使用随机IP 2赋值ProxyBusinessName且赋值ProxyIP，由使用方自行控制代理IP的使用及更换
        /// </summary>
        private string proxyBusinessName = string.Empty;

        /// <summary>
        /// 代理IP地址
        /// </summary>
        private string proxyIP = string.Empty;

        /// <summary>
        /// 代理IP用户名
        /// </summary>
        private string proxyUserName = string.Empty;

        /// <summary>
        /// 代理IP密码
        /// </summary>
        private string proxyPwd = string.Empty;

        /// <summary>  
        /// 支持跳转页面，查询结果将是跳转后的页面(默认是自动跳转)
        /// </summary>  
        private bool allowAutoRedirect = true;

        /// <summary>  
        /// 最大连接数(默认1024)
        /// </summary>
        private int connectionLimit = 1024;

        /// <summary>  
        /// 设置返回类型String和Byte(默认string)
        /// </summary>  
        private ResultType resultType = ResultType.String;

        /// <summary>  
        /// header对象  
        /// </summary>
        private WebHeaderCollection header = new WebHeaderCollection();

        /// <summary>  
        /// 用于请求的HTTP版本(默认System.Net.HttpVersion.Version11)
        /// </summary>
        private Version protocolVersion;

        /// <summary>  
        /// 是否使用100-Continue行为。如果POST请求需要100-Continue响应，则为true；否则为false(默认false)  
        /// </summary> 
        private bool expect100continue = false;

        /// <summary>  
        /// Post参数编码(默认Default编码) 
        /// </summary>
        private Encoding postEncoding;

        /// <summary>  
        /// Cookie返回类型(默认字符串类型)
        /// </summary> 
        private ResultCookieType resultCookieType = ResultCookieType.String;

        /// <summary>
        /// 是否记录交互日志
        /// </summary>
        private bool isWriteLog = false;

        /// <summary>  
        /// 509证书集合  
        /// </summary>  
        private X509CertificateCollection clientCertificates;

        #region 扩展字段

        /// <summary>  
        /// 是否设置为全文小写(默认false)
        /// </summary>  
        ////private bool isToLower = false;

        /// <summary>  
        /// 证书绝对路径  
        /// </summary>
        ////private string cerPath = string.Empty;        

        /// <summary>  
        /// 请求的身份验证信息。  
        /// </summary>  
        ////private ICredentials icredentials = CredentialCache.DefaultCredentials;

        /// <summary>  
        /// 请求将跟随的重定向的最大数目  
        /// </summary>  
        ////private int maximumAutomaticRedirections;

        /// <summary>  
        /// 获取和设置IfModifiedSince(默认为当前日期和时间) 
        /// </summary>  
        ////private DateTime? ifModifiedSince = null;

        #endregion

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public HttpRequestParam()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="cookie">cookies</param>
        public HttpRequestParam(string url, string cookie)
        {
            this.url = url;
            this.cookie = cookie;
        }

        /// <summary>  
        /// 请求URL(必填)  
        /// </summary>  
        public string URL
        {
            get { return this.url; }
            set { this.url = value; }
        }

        /// <summary>  
        /// 请求方式(默认为GET方式,当为POST方式时必须设置Postdata的值)
        /// </summary>  
        public string Method
        {
            get { return this.method; }
            set { this.method = value; }
        }

        /// <summary>  
        /// 请求超时时间(以毫秒为单位，默认30秒)
        /// </summary>  
        public int Timeout
        {
            get { return this.timeout; }
            set { this.timeout = value; }
        }

        /// <summary>  
        /// 写入Post数据超时间(以毫秒为单位，默认30秒) 
        /// </summary>  
        public int ReadWriteTimeout
        {
            get { return this.readWriteTimeout; }
            set { this.readWriteTimeout = value; }
        }

        /// <summary>  
        ///  获取或设置一个值，该值指示是否与Internet资源建立持久性连接(默认true)
        /// </summary>  
        public bool KeepAlive
        {
            get { return this.keepAlive; }
            set { this.keepAlive = value; }
        }

        /// <summary>  
        /// 请求标头值(默认text/html, application/xhtml+xml, */*)  
        /// </summary>  
        public string Accept
        {
            get { return this.accept; }
            set { this.accept = value; }
        }

        /// <summary>  
        /// 请求返回类型(默认text/html)
        /// </summary>  
        public string ContentType
        {
            get { return this.contentType; }
            set { this.contentType = value; }
        }

        /// <summary>  
        /// 客户端访问信息(默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0))
        /// </summary>  
        public string UserAgent
        {
            get { return this.userAgent; }
            set { this.userAgent = value; }
        }

        /// <summary>  
        /// 返回数据编码(默认为null,可以自动识别,一般为utf-8,gbk,gb2312)
        /// </summary>  
        public Encoding Encoding
        {
            get { return this.encoding; }
            set { this.encoding = value; }
        }

        /// <summary>  
        /// Post的数据类型(默认string)
        /// </summary>  
        public PostDataType PostDataType
        {
            get { return this.postDataType; }
            set { this.postDataType = value; }
        }

        /// <summary>  
        /// Post请求时要发送的字符串Post数据  
        /// </summary>  
        public string Postdata
        {
            get { return this.postdata; }
            set { this.postdata = value; }
        }

        /// <summary>  
        /// Post请求时要发送的Byte类型的Post数据  
        /// </summary>  
        public byte[] PostdataByte
        {
            get { return this.postdataByte; }
            set { this.postdataByte = value; }
        }

        /// <summary>  
        /// Cookie对象集合  
        /// </summary>  
        public CookieCollection CookieCollection
        {
            get { return this.cookiecollection; }
            set { this.cookiecollection = value; }
        }

        /// <summary>  
        /// 请求时的Cookie  
        /// </summary>  
        public string Cookie
        {
            get { return this.cookie; }
            set { this.cookie = value; }
        }

        /// <summary>  
        /// 来源地址，上次访问地址  
        /// </summary>  
        public string Referer
        {
            get { return this.referer; }
            set { this.referer = value; }
        }

        /// <summary>
        /// 是否使用IE代理，默认false即不使用IE代理，此时Fiddler无法抓包
        /// </summary>
        public bool IsIEProxy
        {
            get { return this.isIEProxy; }
            set { this.isIEProxy = value; }
        }

        /// <summary>
        /// 代理IP业务名称，若要使用代理，则此属性必须赋值 本类库提供2种使用代理IP的方式，1仅赋值ProxyBusinessName不赋值ProxyIP 则自动获取代理IP，但每次请求均使用随机IP 2赋值ProxyBusinessName且赋值ProxyIP，由使用方自行控制代理IP的使用及更换
        /// </summary>
        public string ProxyBusinessName
        {
            get { return this.proxyBusinessName; }
            set { this.proxyBusinessName = value; }
        }

        /// <summary>
        /// 代理IP地址
        /// </summary>
        public string ProxyIP
        {
            get { return this.proxyIP; }
            set { this.proxyIP = value; }
        }

        /// <summary>
        /// 代理IP用户名
        /// </summary>
        public string ProxyUserName
        {
            get { return this.proxyUserName; }
            set { this.proxyUserName = value; }
        }

        /// <summary>
        /// 代理IP密码
        /// </summary>
        public string ProxyPwd
        {
            get { return this.proxyPwd; }
            set { this.proxyPwd = value; }
        }

        /// <summary>  
        /// 支持跳转页面，查询结果将是跳转后的页面(默认是自动跳转)
        /// </summary>
        public bool AllowAutoRedirect
        {
            get { return this.allowAutoRedirect; }
            set { this.allowAutoRedirect = value; }
        }

        /// <summary>  
        /// 最大连接数(默认1024)
        /// </summary>  
        public int ConnectionLimit
        {
            get { return this.connectionLimit; }
            set { this.connectionLimit = value; }
        }

        /// <summary>  
        /// 设置返回类型String和Byte(默认string)
        /// </summary>  
        public ResultType ResultType
        {
            get { return this.resultType; }
            set { this.resultType = value; }
        }

        /// <summary>  
        /// header对象  
        /// </summary>  
        public WebHeaderCollection Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        /// <summary>  
        /// 用于请求的HTTP版本(默认System.Net.HttpVersion.Version11)
        /// </summary>
        public Version ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }

        /// <summary>  
        /// 获取或设置一个System.Boolean值，该值确定是否使用100-Continue行为。如果POST请求需要100-Continue响应，则为true；否则为false(默认false)
        /// </summary>  
        public bool Expect100Continue
        {
            get { return this.expect100continue; }
            set { this.expect100continue = value; }
        }

        /// <summary>  
        /// Post参数编码(默认Default编码) 
        /// </summary>  
        public Encoding PostEncoding
        {
            get { return this.postEncoding; }
            set { this.postEncoding = value; }
        }

        /// <summary>  
        /// Cookie返回类型(默认字符串类型)
        /// </summary>  
        public ResultCookieType ResultCookieType
        {
            get { return this.resultCookieType; }
            set { this.resultCookieType = value; }
        }

        /// <summary>
        /// 是否记录交互日志
        /// </summary>
        public bool IsWriteLog
        {
            get { return this.isWriteLog; }
            set { this.isWriteLog = value; }
        }

        /// <summary>  
        /// 设置509证书集合  
        /// </summary>  
        public X509CertificateCollection ClientCertificates
        {
            get { return this.clientCertificates; }
            set { this.clientCertificates = value; }
        }

        #region 扩展属性

        /// <summary>  
        /// 是否设置为全文小写(默认false)
        /// </summary>  
        ////public bool IsToLower
        ////{
        ////    get { return this.isToLower; }
        ////    set { this.isToLower = value; }
        ////}

        /// <summary>  
        /// 证书绝对路径  
        /// </summary>  
        ////public string CerPath
        ////{
        ////    get { return this.cerPath; }
        ////    set { this.cerPath = value; }
        ////}        

        /// <summary>  
        /// 获取或设置请求的身份验证信息。  
        /// </summary>  
        ////public ICredentials ICredentials
        ////{
        ////    get { return this.icredentials; }
        ////    set { this.icredentials = value; }
        ////}

        /// <summary>  
        /// 设置请求将跟随的重定向的最大数目  
        /// </summary>  
        ////public int MaximumAutomaticRedirections
        ////{
        ////    get { return this.maximumAutomaticRedirections; }
        ////    set { this.maximumAutomaticRedirections = value; }
        ////}

        /// <summary>  
        /// 获取和设置IfModifiedSince(默认为当前日期和时间) 
        /// </summary>  
        ////public DateTime? IfModifiedSince
        ////{
        ////    get { return this.ifModifiedSince; }
        ////    set { this.ifModifiedSince = value; }
        ////}

        #endregion
    }
}
