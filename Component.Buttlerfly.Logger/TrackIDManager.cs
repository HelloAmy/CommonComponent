// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-04-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-04-2015
// ***********************************************************************
// <copyright file="TrackIDManager.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class TrackIDManager
    /// </summary>
    public class TrackIDManager
    {
        /// <summary>
        /// 当前跟踪ID
        /// </summary>
        /// <value>The current track ID.</value>
        public static TrackID CurrentTrackID
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items["TrackID"] as TrackID;
                }

                return CallContext.LogicalGetData("TrackID") as TrackID;
            }

            set
            {
                try
                {
                    string trackId = value == null ? string.Empty : value.TrackIdStr ?? string.Empty;
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.Items["TrackID"] == null)
                        {
                            HttpContext.Current.Items.Add("TrackID", value);
                        }
                        else
                        {
                            HttpContext.Current.Items["TrackID"] = value;
                        }

                        if (HttpContext.Current.Items["TrackIDStr"] == null)
                        {
                            HttpContext.Current.Items.Add("TrackIDStr", value);
                        }
                        else
                        {
                            HttpContext.Current.Items["TrackIDStr"] = value;
                        }
                    }

                    CallContext.LogicalSetData("TrackID", value);
                    CallContext.LogicalSetData("TrackIDStr", trackId);
                }
                catch
                {
                    // 出现异常不影响业务
                }
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns>TrackID.</returns>
        public static TrackID GetInstance(string loginName)
        {
            TrackID trackID = new TrackID(loginName);
            CurrentTrackID = trackID;
            return trackID;
        }
    }
}
