// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-14-2015
// ***********************************************************************
// <copyright file="AppKeySettings.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class AppKeySettings
    /// </summary>
    public class AppKeySettings
    {
        /// <summary>
        /// Initializes static members of the <see cref="AppKeySettings"/> class.
        /// </summary>
        static AppKeySettings()
        {
            InitAppKeySettings();

            // 配置监控
            ConfigMonitor.AfterModifyEvent += new ConfigMonitor.AfterConfigModifyEvent(InitAppKeySettings);
        }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public static string ApplicationName
        {
            get;
            set;
        }

        /// <summary>
        /// Inits the app key settings.
        /// </summary>
        public static void InitAppKeySettings()
        {
            try
            {
                ApplicationName = ConfigMonitor.Config.AppSettings.Settings["ApplicationName"].Value;
            }
            catch (Exception ex)
            {
                TextDataAccess.AddLog(string.Format("AppNameStr配置获取异常 {0}", ex.ToString()));
            }
        }
    }
}
