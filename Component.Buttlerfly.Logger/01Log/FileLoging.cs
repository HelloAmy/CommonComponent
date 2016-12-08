// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="FileLoging.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class FileLoging
    /// </summary>
    public class FileLoging
    {
        /// <summary>
        /// Writes the app exception file log.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void WriteAppExceptionFileLog(string msg)
        {
            DateTime time = DateTime.Now;
            string path = string.Format("./Log/AppExceptionLog/{0}", time.ToString("yyyyMMdd"));
            string fileName = string.Format("Exception_{0}.log", time.ToString("HH"));
            FileHelper.WriteFile(path, fileName, msg);
        }
    }
}
