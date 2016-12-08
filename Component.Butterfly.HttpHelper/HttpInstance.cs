// ***********************************************************************
// Assembly         : Common.Julius.HttpHelper
// Author           : zhujinrong
// Created          : 04-19-2016
//
// Last Modified By : zhujinrong
// Last Modified On : 04-19-2016
// ***********************************************************************
// <copyright file="HttpInstance.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Component.Buttlerfly.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// The HttpHelper.
/// </summary>
namespace Component.Butterfly.HttpHelper
{
    /// <summary>
    /// Http连接操作帮助类
    /// </summary>
    internal class HttpInstance
    {
        #region 变量

        /// <summary>
        /// HttpWebRequest对象用来发起请求
        /// </summary>
        private HttpWebRequest request = null;

        /// <summary>
        /// 获取影响流的数据对象
        /// </summary>
        private HttpWebResponse response = null;

        #endregion

        /// <summary>
        /// 根据传入的参数，得到相应Http请求数据（唯一公开方法）
        /// </summary>
        /// <param name="item">参数类对象</param>
        /// <returns>返回HttpResult类型</returns>
        public HttpResult GetHttpRequestData(HttpRequestParam item)
        {
            ////返回参数  
            HttpResult result = new HttpResult();
            try
            {
                try
                {
                    ////准备参数  
                    this.SetRequest(item);
                }
                catch (Exception ex)
                {
                    // 注释掉，业务层捕获异常，可以根据网络异常重试
                    return new HttpResult() { Cookie = string.Empty, Header = null, Html = ex.Message, StatusDescription = "出现异常：" + ex.Message };
                    throw ex;
                }

                try
                {
                    ////请求数据  
                    using (this.response = (HttpWebResponse)this.request.GetResponse())
                    {
                        this.GetData(item, result);
                    }
                }
                catch (Exception ex)
                {
                    result.Html = ex.ToString();
                    LogManager.Log.WriteAppException(new AppException("获取返回数据异常", ex, ExceptionLevel.Info));
                    throw ex;
                }
            }
            finally
            {
                if (item.IsWriteLog)
                {
                    MInteractionParam param = new MInteractionParam()
                    {
                        DicContext = null,
                        Module = "HttpHelper",
                        Key1 = item.ProxyBusinessName,
                        Key2 = item.Method.ToUpper(),
                        SendAddress = item.URL,
                        SendContent = this.GetRequestInfo(item),
                        ReceiveContent = result.Html,
                        Message = this.GetRequestProxy(item),
                    };

                    LogManager.Log.InteractionLog(param);
                }

                this.AbortRequest();
            }

            return result;
        }

        #region SetRequest

        /// <summary>
        /// 为请求准备参数
        /// </summary>
        /// <param name="item">参数列表</param>
        private void SetRequest(HttpRequestParam item)
        {
            ////设置最大连接数（初始化request对象前设置，则request默认连接数即为ServicePointManager.DefaultConnectionLimit）
            ServicePointManager.DefaultConnectionLimit = item.ConnectionLimit;
            ////https回调证书验证
            if (item.URL.ToLower().IndexOf("https", System.StringComparison.Ordinal) > -1)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
            }

            ////初始化对像，并设置请求的URL地址  
            this.request = (HttpWebRequest)WebRequest.Create(item.URL);
            ////设置证书
            this.SetCert(item);
            ////设置Header参数  
            if (item.Header != null && item.Header.Count > 0)
            {
                foreach (string key in item.Header.AllKeys)
                {
                    this.request.Headers.Add(key, item.Header[key]);
                }
            }

            ////设置代理  
            this.SetProxy(item);
            if (item.ProtocolVersion != null)
            {
                this.request.ProtocolVersion = item.ProtocolVersion;
            }

            ////请求方式Get或者Post  
            this.request.Method = item.Method;
            this.request.KeepAlive = item.KeepAlive;
            this.request.Timeout = item.Timeout;
            this.request.ReadWriteTimeout = item.ReadWriteTimeout;
            ////Accept  
            this.request.Accept = item.Accept;
            ////ContentType返回类型  
            this.request.ContentType = item.ContentType;
            ////UserAgent客户端的访问类型，包括浏览器版本和操作系统信息  
            this.request.UserAgent = item.UserAgent;
            ////设置Cookie
            this.SetCookie(item);
            ////来源地址  
            this.request.Referer = item.Referer;
            ////是否执行跳转功能  
            this.request.AllowAutoRedirect = item.AllowAutoRedirect;
            ////100-Continue
            this.request.ServicePoint.Expect100Continue = item.Expect100Continue;
            ////设置Post数据  
            this.SetPostData(item);
        }

        /// <summary>  
        /// 设置证书  
        /// </summary>  
        /// <param name="item">The item.</param>  
        private void SetCert(HttpRequestParam item)
        {
            if (item.ClientCertificates != null && item.ClientCertificates.Count > 0)
            {
                foreach (X509Certificate c in item.ClientCertificates)
                {
                    this.request.ClientCertificates.Add(c);
                }
            }
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="item">The item.</param>
        private void SetCookie(HttpRequestParam item)
        {
            if (!string.IsNullOrEmpty(item.Cookie))
            {
                this.request.Headers[HttpRequestHeader.Cookie] = item.Cookie;
            }

            ////设置CookieCollection  
            if (item.ResultCookieType == ResultCookieType.CookieCollection)
            {
                this.request.CookieContainer = new CookieContainer();
                if (item.CookieCollection != null && item.CookieCollection.Count > 0)
                {
                    this.request.CookieContainer.Add(item.CookieCollection);
                }
            }
        }

        /// <summary>
        /// 设置Post数据
        /// </summary>
        /// <param name="item">Http参数</param>
        private void SetPostData(HttpRequestParam item)
        {
            ////验证在得到结果时是否有传入数据  
            if (this.request.Method.Trim().ToLower().Contains("post"))
            {
                Encoding postEncoding = Encoding.Default;
                if (item.PostEncoding != null)
                {
                    postEncoding = item.PostEncoding;
                }

                byte[] buffer = null;
                ////写入Byte类型  
                if (item.PostDataType == PostDataType.Byte && item.PostdataByte != null && item.PostdataByte.Length > 0)
                {
                    ////验证在得到结果时是否有传入数据  
                    buffer = item.PostdataByte;
                }
                else if (item.PostDataType == PostDataType.FilePath && !string.IsNullOrEmpty(item.Postdata))
                {
                    ////写入文件
                    using (StreamReader r = new StreamReader(item.Postdata, postEncoding))
                    {
                        buffer = postEncoding.GetBytes(r.ReadToEnd());
                    }

                    ////r.Close();
                }
                else if (!string.IsNullOrEmpty(item.Postdata))
                {
                    ////写入字符串
                    buffer = postEncoding.GetBytes(item.Postdata);
                }

                if (buffer != null)
                {
                    item.PostdataByte = buffer;
                    this.request.ContentLength = buffer.Length;
                    using (Stream requestStream = this.request.GetRequestStream())
                    {
                        requestStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="item">参数对象</param>
        private void SetProxy(HttpRequestParam item)
        {
            if (!string.IsNullOrEmpty(item.ProxyBusinessName))
            {
                WebProxy webProxy = null;
                if (item.ProxyIP.Contains(":"))
                {
                    string[] ipAndPort = item.ProxyIP.Split(':');
                    webProxy = new WebProxy(ipAndPort[0].Trim(), Convert.ToInt32(ipAndPort[1].Trim()));
                }
                else
                {
                    webProxy = new WebProxy(item.ProxyIP, false);
                }

                if (!string.IsNullOrEmpty(item.ProxyUserName))
                {
                    webProxy.Credentials = new NetworkCredential(item.ProxyUserName, item.ProxyPwd);
                    this.request.UseDefaultCredentials = true;
                }

                this.request.Proxy = webProxy;
            }
            else
            {
                if (!item.IsIEProxy)
                {
                    ////Proxy属性置为null后，即不使用IE代理，Fiddler无法抓包
                    this.request.Proxy = null;
                }
            }
        }

        #endregion

        #region GetData

        /// <summary>
        /// 获取数据的并解析的方法
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="result">The result.</param>
        private void GetData(HttpRequestParam item, HttpResult result)
        {
            ////获取StatusCode  
            result.StatusCode = this.response.StatusCode;
            ////获取StatusDescription  
            result.StatusDescription = this.response.StatusDescription;
            ////获取Headers  
            result.Header = this.response.Headers;
            ////获取重定向路径
            if (this.response.ResponseUri != null && this.response.ResponseUri.ToString() != item.URL)
            {
                result.ResponseUrl = this.response.ResponseUri.ToString();
            }

            ////获取CookieCollection  
            if (item.ResultCookieType == ResultCookieType.CookieCollection && this.request.CookieContainer != null)
            {
                result.CookieCollection = this.request.CookieContainer.GetCookies(this.request.RequestUri);
            }

            ////获取set-cookie  
            if (this.response.Headers["set-cookie"] != null)
            {
                result.Cookie = this.ProcessCookies(item.Cookie, this.response.Headers["set-cookie"]);
            }

            ////处理网页Byte  
            byte[] responseByte = this.GetByte();
            if (responseByte != null && responseByte.Length > 0)
            {
                ////设置编码  
                this.SetEncoding(item, result, responseByte);
                ////得到返回的HTML  
                result.Html = item.Encoding.GetString(responseByte);
            }
            else
            {
                ////没有返回任何Html代码  
                result.Html = string.Empty;
            }
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="cookies">cookie</param>
        /// <param name="setCookies">setCookies</param>
        /// <returns>新cookie</returns>
        private string ProcessCookies(string cookies, string setCookies)
        {
            setCookies = Regex.Replace(setCookies, ",JSESSIONID", ";JSESSIONID", RegexOptions.IgnoreCase);
            Dictionary<string, string> newCookies = new Dictionary<string, string>();
            if (cookies != null)
            {
                string[] tmpCookies = cookies.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tmpCookies.Length; i++)
                {
                    string[] cookie = tmpCookies[i].Split('=');
                    if (cookie.Length != 2)
                    {
                        continue;
                    }

                    // 解决业两个相同KEY报异常
                    string key = cookie[0].Trim();
                    if (!newCookies.ContainsKey(key))
                    {
                        newCookies.Add(key, cookie[1]);
                    }
                }
            }

            if (setCookies != null)
            {
                string[] tmpCookies = setCookies.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tmpCookies.Length; i++)
                {
                    // 解决获取不到重定向多Cookies问题
                    string[] redirectCookeis = tmpCookies[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (redirectCookeis != null)
                    {
                        foreach (var strCookie in redirectCookeis)
                        {
                            // 解决有空格获取不到Cookie问题
                            string[] cookie = strCookie.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                            if (cookie.Length != 2)
                            {
                                continue;
                            }

                            if (newCookies.ContainsKey(cookie[0].Trim()) == false)
                            {
                                newCookies.Add(cookie[0].Trim(), cookie[1]);
                            }
                            else
                            {
                                newCookies[cookie[0].Trim()] = cookie[1];
                            }
                        }
                    }
                }
            }

            string sznewCookies = string.Empty;
            Dictionary<string, string>.Enumerator it = newCookies.GetEnumerator();
            while (it.MoveNext())
            {
                sznewCookies = sznewCookies + " " + it.Current.Key + "=" + it.Current.Value + ";";
            }

            if (sznewCookies.Length != 0)
            {
                sznewCookies = sznewCookies.Substring(1, sznewCookies.Length - 1);
            }

            return sznewCookies;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="item">HttpItem</param>
        /// <param name="result">HttpResult</param>
        /// <param name="responseByte">byte[]</param>
        private void SetEncoding(HttpRequestParam item, HttpResult result, byte[] responseByte)
        {
            ////是否返回Byte类型数据  
            if (item.ResultType == ResultType.Byte)
            {
                result.ResultByte = responseByte;
            }

            if (item.Encoding == null)
            {
                Match meta = Regex.Match(Encoding.Default.GetString(responseByte), "<meta[^<]*charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
                string c = string.Empty;
                if (meta != null && meta.Groups.Count > 0)
                {
                    c = meta.Groups[1].Value.ToLower().Trim();
                }

                if (c.Length > 2)
                {
                    try
                    {
                        item.Encoding = Encoding.GetEncoding(c.Replace("\"", string.Empty).Replace("'", string.Empty).Replace(";", string.Empty).Replace("iso-8859-1", "gbk").Trim());
                    }
                    catch
                    {
                        if (string.IsNullOrEmpty(this.response.CharacterSet))
                        {
                            item.Encoding = Encoding.UTF8;
                        }
                        else
                        {
                            item.Encoding = Encoding.GetEncoding(this.response.CharacterSet);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.response.CharacterSet))
                    {
                        item.Encoding = Encoding.UTF8;
                    }
                    else
                    {
                        item.Encoding = Encoding.GetEncoding(this.response.CharacterSet);
                    }
                }
            }

            if (item.Encoding == null)
            {
                item.Encoding = Encoding.UTF8;
            }
        }

        /// <summary>
        /// 提取网页Byte
        /// </summary>
        /// <returns>System.Byte[][].</returns>
        private byte[] GetByte()
        {
            byte[] responseByte = null;
            Stream streamResponse = null;
            try
            {
                ////GZIIP处理  
                if (this.response.ContentEncoding != null && this.response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    streamResponse = new GZipStream(this.response.GetResponseStream(), CompressionMode.Decompress);
                }
                else
                {
                    streamResponse = this.response.GetResponseStream();
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    if (streamResponse != null)
                    {
                        int length = 1024;
                        byte[] buffer = new byte[length];
                        int bytesRead = 0;
                        while ((bytesRead = streamResponse.Read(buffer, 0, length)) > 0)
                        {
                            stream.Write(buffer, 0, bytesRead);
                        }

                        ////获取Byte
                        responseByte = stream.ToArray();
                    }
                }
            }
            finally
            {
                if (streamResponse != null)
                {
                    streamResponse.Close();
                }
            }

            return responseByte;
        }

        #endregion

        #region 其他私有方法

        /// <summary>
        /// 回调验证证书问题
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="certificate">certificate</param>
        /// <param name="chain">chain</param>
        /// <param name="errors">errors</param>
        /// <returns>true</returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受   
            return true;
        }

        /// <summary>
        /// 获取请求信息（记录交互日志用）
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>请求信息字符串</returns>
        private string GetRequestInfo(HttpRequestParam request)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string method = request.Method.Trim().ToUpper();
                sb.Append(method + " " + request.URL + "\r\n");
                if (method == "POST")
                {
                    sb.Append("Content-Type: " + request.ContentType + "\r\n");
                    sb.Append("Content-Length: " + request.PostdataByte.Length + "\r\n");
                    sb.Append("Post-Data: " + request.Postdata + "\r\n");
                }

                sb.Append("Cookie: " + request.Cookie);
                return sb.ToString();
            }
            catch
            {
                return request.URL;
            }
        }

        /// <summary>
        /// 获取请求使用的代理IP
        /// </summary>
        /// <param name="item">请求参数</param>
        /// <returns>请求使用的代理IP</returns>
        private string GetRequestProxy(HttpRequestParam item)
        {
            try
            {
                if (item != null && !string.IsNullOrEmpty(item.ProxyIP))
                {
                    return item.ProxyIP;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 中止请求，释放连接
        /// </summary>
        private void AbortRequest()
        {
            if (this.request != null)
            {
                this.request.Abort();
            }

            if (this.response != null)
            {
                this.response.Close();
            }

            this.request = null;
            this.response = null;
        }

        #endregion
    }
}
