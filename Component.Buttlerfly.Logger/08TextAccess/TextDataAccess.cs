// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-11-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="TextDataAccess.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class TextDataAccess
    /// </summary>
    public class TextDataAccess
    {
        /// <summary>
        /// The log queue
        /// </summary>
        private static Queue<string> logQueue = new Queue<string>();

        /// <summary>
        /// The syn root
        /// </summary>
        private static object synRoot = new object();

        /// <summary>
        /// The write thread
        /// </summary>
        private static Thread writeThread = null;

        /// <summary>
        /// Initializes static members of the <see cref="TextDataAccess"/> class.
        /// </summary>
        static TextDataAccess()
        {
            Action action = new Action(ThreadMethod);
            action.BeginInvoke(null, null);
        }

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="content">The content.</param>
        public static void AddLog(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    return;
                }

                string temp = string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
                if (logQueue != null && logQueue.Count < 1000)
                {
                    lock (synRoot)
                    {
                        if (logQueue != null && logQueue.Count < 1000)
                        {
                            logQueue.Enqueue(temp);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Threads the method.
        /// </summary>
        public static void ThreadMethod()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Queue<string> myqueue = logQueue;
                        logQueue = new Queue<string>();
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in myqueue)
                        {
                            sb.Append(item).AppendLine().AppendLine();
                        }

                        if (string.IsNullOrEmpty(sb.ToString()))
                        {
                            return;
                        }

                        new FileLoging().WriteAppExceptionFileLog(sb.ToString());
                    }
                    catch
                    {
                    }
                    finally
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(20));
                    }
                }
            }
            catch
            {
            }
        }
    }
}
