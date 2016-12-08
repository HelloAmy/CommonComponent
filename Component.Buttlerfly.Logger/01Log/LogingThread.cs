// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-14-2015
// ***********************************************************************
// <copyright file="LogingThread.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Threading;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class LogingThread
    /// </summary>
    public class LogingThread : ILoging
    {
        /// <summary>
        /// The logbusiness
        /// </summary>
        private LogBusiness logbusiness = new LogBusiness();

        /// <summary>
        /// Writes the app exception.
        /// </summary>
        /// <param name="appexception">The appexception.</param>
        public void WriteAppException(AppException appexception)
        {
            if (TrackIDManager.CurrentTrackID == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(TrackIDManager.CurrentTrackID.TrackIdStr))
            {
                return;
            }

            appexception.Data["TrackID"] = TrackIDManager.CurrentTrackID.TrackIdStr;
            TrackID trackId = TrackIDManager.CurrentTrackID;
            trackId.ExceptionID = appexception.ID.ToString();
            GlobalLog globalLog = this.logbusiness.BulidAppExceptionGlobalLog(trackId, appexception);

            ThreadPool.QueueUserWorkItem(this.logbusiness.DoWorkWithArg, globalLog);
        }

        /// <summary>
        /// InteractionLog
        /// </summary>
        /// <param name="model">model</param>
        public void InteractionLog(MInteractionParam model)
        {
            // 暂未实现
        }
    }
}
