// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-08-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-08-2015
// ***********************************************************************
// <copyright file="MAppException.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class MAppException
    /// </summary>
    public class MAppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MAppException"/> class.
        /// </summary>
        public MAppException()
        {
            this.TimePoint = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the log ID.
        /// </summary>
        /// <value>The log ID.</value>
        public string LogID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the track ID.
        /// </summary>
        /// <value>The track ID.</value>
        public string TrackID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public string ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the exception level.
        /// </summary>
        /// <value>The exception level.</value>
        public ExceptionLevel ExceptionLevel
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
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the local IP.
        /// </summary>
        /// <value>The local IP.</value>
        public string LocalIP
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the exception MSG.
        /// </summary>
        /// <value>The exception MSG.</value>
        public string ExceptionMsg
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the exception context.
        /// </summary>
        /// <value>The exception context.</value>
        public string ExceptionContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the orginal exception MSG.
        /// </summary>
        /// <value>The orginal exception MSG.</value>
        public string OrginalExceptionMsg
        {
            get;
            set;
        }
    }
}
