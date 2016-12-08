// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-14-2015
// ***********************************************************************
// <copyright file="Logging.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Threading;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class Logging
    /// </summary>
    internal class Logging
    {
        /// <summary>
        /// Initializes static members of the <see cref="Logging"/> class.
        /// </summary>
        static Logging()
        {
            try
            {
                // 设置默认线程池大小
                ThreadPool.SetMaxThreads(1000, 1000);
            }
            catch (Exception ex)
            {
                // 记录日志
                TextDataAccess.AddLog(ex.StackTrace.ToString());
            }
        }

        /// <summary>
        /// Writes the app exception.
        /// </summary>
        /// <param name="appex">The appex.</param>
        /// <param name="timepoint">The timepoint.</param>
        /// <param name="routeModuleName">Name of the route module.</param>
        public void WriteAppException(AppException appex, DateTime timepoint, string routeModuleName)
        {
            try
            {
                string trackID = appex.Data["TrackID"] as string;
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                if (appex.Data != null)
                {
                    foreach (var key in appex.Data.Keys)
                    {
                        if (key is string)
                        {
                            string keyStr = key as string;
                            parameters.Add(keyStr, appex.Data[key]);
                        }
                    }
                }

                string originalException = appex.InnerException != null ? appex.InnerException.Message : string.Empty;
                string exceptionContext = JsonHelper.JsonSerializer<Dictionary<string, object>>(parameters);
                string localip = IPAddressHelper.GetLocalIpAddress();
                MAppException mappex = new MAppException()
                {
                    OrginalExceptionMsg = originalException,
                    ExceptionContext = exceptionContext,
                    ExceptionMsg = appex.ToString(),
                    ErrorCode = appex.ErrorCode,
                    LocalIP = localip,
                    ApplicationName = AppKeySettings.ApplicationName,
                    LogID = Guid.NewGuid().ToString(),
                    TrackID = trackID,
                    TimePoint = DateTime.Now,
                    ExceptionLevel = appex.Level,
                };

                DALFactory.GetAppExceptionDAL(LogRecordType.DBLog).InsertAppException(mappex);
            }
            catch (Exception ex)
            {
                TextDataAccess.AddLog(ex.StackTrace.ToString());
            }
        }
    }
}
