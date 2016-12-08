// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-11-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="GlobalLog.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class GlobalLog
    /// </summary>
    public class GlobalLog
    {
        /// <summary>
        /// Gets or sets the type of the log.
        /// </summary>
        /// <value>The type of the log.</value>
        public LogType LogType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time point.
        /// </summary>
        /// <value>The time point.</value>
        public DateTime TimePoint
        {
            get;
            set;
        }

        /// <summary>
        /// 异常
        /// </summary>
        public AppException AppException
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current track ID.
        /// </summary>
        /// <value>The current track ID.</value>
        public TrackID CurrentTrackID
        {
            get;
            set;
        }
    }
}
