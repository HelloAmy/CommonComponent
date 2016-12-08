// ***********************************************************************
// Assembly         : Lucky.PrimaryKeyGenerator.Component
// Author           : zhujinrong
// Created          : 04-27-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 04-27-2015
// ***********************************************************************
// <copyright file="PrimaryKeyGenerator.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace CommonComponent.Butterfly.KeyGenerator
{
    /// <summary>
    /// 主键生成器
    /// </summary>
    public class PrimaryKeyFactory
    {
        /// <summary>
        /// 新建一个主键
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <param name="tableName">表名</param>
        /// <returns>主键</returns>
        public static string NewPrimaryKey(string databaseName, string tableName)
        {
            string ret = string.Empty;

            // 4位随机数
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            int nextInt = ra.Next(9999);

            // 数据库名和表名对应的唯一码
            string uniqueCode = UniqueCodeMap.GetUniqueCode(databaseName, tableName);

            List<string> ipv6List = IPHelper.GetLocalIpV6List();
            string ip = "00000000000";

            ret = DateTime.Now.ToString("yyyyMMddHHmmssfff") + nextInt.ToString().PadLeft(4, '0') + uniqueCode;
            return ret.ToString();
        }
    }
}
