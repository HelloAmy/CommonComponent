// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-11-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-14-2015
// ***********************************************************************
// <copyright file="LogManager.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class LogManager
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// The log
        /// </summary>
        private static ILoging log = GetLoging();

        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        public static ILoging Log
        {
            get
            {
                return log;
            }
        }

        /// <summary>
        /// Gets the loging.
        /// </summary>
        /// <returns>ILoging.</returns>
        private static ILoging GetLoging()
        {
            return new LogingThread();
        }
    }
}
