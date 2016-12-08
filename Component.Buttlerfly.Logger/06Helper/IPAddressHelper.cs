// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-11-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="IPAddressHelper.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Net;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class IPAddressHelper
    /// </summary>
    public class IPAddressHelper
    {
        /// <summary>
        /// Gets the local ip address.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetLocalIpAddress()
        {
            IPAddress ipaddr = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            string ip = ipaddr.ToString();
            return ip;
        }
    }
}
