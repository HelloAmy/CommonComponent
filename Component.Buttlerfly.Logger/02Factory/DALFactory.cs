// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-09-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-10-2015
// ***********************************************************************
// <copyright file="DALFactory.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Enum LogRecordType
    /// </summary>
    public enum LogRecordType
    {
        /// <summary>
        /// The DB log
        /// </summary>
        DBLog = 0,

        /// <summary>
        /// The file log
        /// </summary>
        FileLog = 1,
    }

    /// <summary>
    /// Class DALFactory
    /// </summary>
    public class DALFactory
    {
        /// <summary>
        /// Gets the app exception DAL.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IMAppException.</returns>
        public static IMAppException GetAppExceptionDAL(LogRecordType type)
        {
            if (type == LogRecordType.DBLog)
            {
                return new DMAppException();
            }

            return new TMAppException();
        }
    }
}
