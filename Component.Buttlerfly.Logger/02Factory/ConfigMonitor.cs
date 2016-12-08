// ***********************************************************************
// Assembly         : Explore.Pager.Factory
// Author           : zhujinrong
// Created          : 08-05-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 08-07-2015
// ***********************************************************************
// <copyright file="ConfigMonitor.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Configuration;
using System.IO;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// 配置文件监听类
    /// </summary>
    public sealed class ConfigMonitor
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        private static string filePath = string.Empty;

        /// <summary>
        /// file map
        /// </summary>
        private static ExeConfigurationFileMap configFileMap;

        /// <summary>
        /// 类型构造器
        /// </summary>
        static ConfigMonitor()
        {
            MonitorConfigFile();
            InitConfigInfo();
        }

        /// <summary>
        /// 定义一个代理
        /// </summary>
        public delegate void AfterConfigModifyEvent();

        /// <summary>
        /// 定义代理的实例
        /// </summary>
        public static event AfterConfigModifyEvent AfterModifyEvent;

        /// <summary>
        /// 配置文件对象
        /// </summary>
        public static Configuration Config
        {
            get;
            set;
        }

        /// <summary>
        /// 监控配置文件类
        /// </summary>
        public static void MonitorConfigFile()
        {
            FileSystemWatcher fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            fileWatcher.Filter = "LoggerComponent.config";
            fileWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;
            fileWatcher.Changed += new FileSystemEventHandler(OnChanged);
            fileWatcher.Created += new FileSystemEventHandler(OnChanged);
            fileWatcher.Deleted += new FileSystemEventHandler(OnChanged);
            fileWatcher.Renamed += new RenamedEventHandler(OnChanged);
            fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// 修改事件
        /// </summary>
        /// <param name="source">对象</param>
        /// <param name="arg">事件</param>
        public static void OnChanged(object source, FileSystemEventArgs arg)
        {
            InitConfigInfo();
            RaiseEvent();
        }

        /// <summary>
        /// 初始化配置文件类
        /// </summary>
        public static void InitConfigInfo()
        {
            filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\LoggerComponent.config";
            configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = filePath;
            Config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 事件
        /// </summary>
        private static void RaiseEvent()
        {
            if (AfterModifyEvent != null)
            {
                AfterModifyEvent();
            }
        }
    }
}
