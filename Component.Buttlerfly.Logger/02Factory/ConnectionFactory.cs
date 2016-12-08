// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-09-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-09-2015
// ***********************************************************************
// <copyright file="ConnectionFactory.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class ConnectionFactory
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// AmyDB读连接
        /// </summary>
        /// <value>The log exception write.</value>
        /// <returns>读连接</returns>
        public static MySqlConnection LogExceptionWrite
        {
            get { return GetConnection("LogExceptionWrite"); }
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectiongKey">连接Key</param>
        /// <returns>MySql数据库连接</returns>
        /// <exception cref="System.Exception">ExplorePagerService Only Support MySql Database!</exception>
        private static MySqlConnection GetConnection(string connectiongKey)
        {
            ConnectionStringSettings settings = ConfigMonitor.Config.ConnectionStrings.ConnectionStrings[connectiongKey];
            string ori = settings.ConnectionString;
            string decrypt = ori;
            string providerName = settings.ProviderName;
            if (!string.IsNullOrEmpty(providerName) && !providerName.Equals("MySql.Data.MySqlClient"))
            {
                throw new Exception("ExplorePagerService Only Support MySql Database!");
            }

            return new MySqlConnection(decrypt);
        }
    }
}
