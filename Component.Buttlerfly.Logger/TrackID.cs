// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-04-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-04-2015
// ***********************************************************************
// <copyright file="TrackID.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class TrackID
    /// </summary>
    public class TrackID
    {
        /// <summary>
        /// 跟踪ID
        /// </summary>
        private string trackId = string.Empty;

        /// <summary>
        /// 登陆名
        /// </summary>
        private string logginName = string.Empty;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logginName">Name of the loggin.</param>
        public TrackID(string logginName)
        {
            this.logginName = logginName;
            this.Init();
        }

        /// <summary>
        /// Gets or sets the track id STR.
        /// </summary>
        /// <value>The track id STR.</value>
        public string TrackIdStr
        {
            get { return this.trackId; }
            set { this.trackId = value; }
        }

        /// <summary>
        /// Gets or sets the exception ID.
        /// </summary>
        /// <value>The exception ID.</value>
        public string ExceptionID
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if (string.IsNullOrEmpty(this.logginName))
            {
                this.logginName = "DefaultZhu";
            }

            string ret = string.Empty;
            ret = this.logginName;
            if (ret.Length <= 10)
            {
                ret = ret.PadLeft(10, '6');
            }
            else
            {
                ret = ret.Substring(ret.Length - 10, 10);
            }

            string timeStr = DateTime.Now.ToString("yyMMddHHmmssfff");

            string randomCodeStr = new Random().Next(0, 99999).ToString();

            if (randomCodeStr.Length < 5)
            {
                randomCodeStr = randomCodeStr.PadLeft(5, '0');
            }

            this.trackId = ret + timeStr + randomCodeStr;
        }
    }
}
