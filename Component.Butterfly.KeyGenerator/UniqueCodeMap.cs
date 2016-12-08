// ***********************************************************************
// Assembly         : Lucky.PrimaryKeyGenerator.Component
// Author           : zhujinrong
// Created          : 04-27-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 04-27-2015
// ***********************************************************************
// <copyright file="UniqueCodeMap.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System;

namespace CommonComponent.Butterfly.KeyGenerator
{
    /// <summary>
    /// 唯一码映射
    /// </summary>
    public class UniqueCodeMap
    {
        /// <summary>
        /// 唯一码对应表
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> uniqueCodeMap = null;

        /// <summary>
        /// 锁
        /// </summary>
        private static object lockObj = new object();

        /// <summary>
        /// 类型构造器
        /// </summary>
        static UniqueCodeMap()
        {
            if (uniqueCodeMap == null)
            {
                lock (lockObj)
                {

                    uniqueCodeMap = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { 
                            DataBaseName.UnKnownDataBase, new Dictionary<string,string>()
                            {
                                { TableName.UnknowTableName,"un00" }
                            }
                        },
                        { 
                            DataBaseName.UserManagerDB, new Dictionary<string,string>()
                            { 
                                { TableName.UserManagerDB_RoleInfo, "us00" }
                            }
                        }
                    };

                }
            }
        }

        /// <summary>
        /// 获取唯一码
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <param name="tableName">表名</param>
        /// <returns>唯一码</returns>
        public static string GetUniqueCode(string databaseName, string tableName)
        {
            if (uniqueCodeMap != null
                && uniqueCodeMap.ContainsKey(databaseName) && uniqueCodeMap[databaseName] != null
                && uniqueCodeMap[databaseName].ContainsKey(tableName) && uniqueCodeMap[databaseName][tableName] != null)
            {
                return uniqueCodeMap[databaseName][tableName];
            }
            else
            {
                throw new Exception("没有获取到对应码，数据库名:" + databaseName + ",表名:" + tableName);
            }
        }
    }
}
