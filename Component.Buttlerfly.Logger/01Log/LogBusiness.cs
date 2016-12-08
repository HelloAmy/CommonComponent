// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-11-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="LogBusiness.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class LogBusiness
    /// </summary>
    public class LogBusiness
    {
        /// <summary>
        /// Does the work with arg.
        /// </summary>
        /// <param name="arg">The arg.</param>
        public void DoWorkWithArg(object arg)
        {
            Logging logging = new Logging();
            GlobalLog log = arg as GlobalLog;
            if (log != null)
            {
                try
                {
                    switch (log.LogType)
                    {
                        case LogType.AppExceptionLog:
                            logging.WriteAppException(log.AppException, log.TimePoint, string.Empty);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    TextDataAccess.AddLog(ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Bulids the app exception global log.
        /// </summary>
        /// <param name="trackId">The track id.</param>
        /// <param name="appex">The appex.</param>
        /// <returns>GlobalLog.</returns>
        public GlobalLog BulidAppExceptionGlobalLog(TrackID trackId, AppException appex)
        {
            GlobalLog log = new GlobalLog()
            {
                CurrentTrackID = trackId,
                AppException = appex,
                LogType = LogType.AppExceptionLog,
                TimePoint = DateTime.Now,
            };

            return log;
        }
    }
}
