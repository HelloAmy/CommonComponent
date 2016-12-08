// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-10-2015
// ***********************************************************************
// <copyright file="FileHelper.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class FileHelper
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 向一个文件末尾追加内容
        /// </summary>
        /// <param name="path">文件的路径和文件名</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="msg">追加的消息</param>
        public static void WriteFile(string path, string fileName, string msg)
        {
            try
            {
                string fileAllPath = path + "/" + fileName;
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception ex)
                    {
                        TextDataAccess.AddLog(ex.StackTrace);
                    }
                }

                if (!File.Exists(fileAllPath))
                {
                    FileStream fs1 = new FileStream(fileAllPath, FileMode.Create);
                    fs1.Close();
                }

                using (StreamWriter sw = File.AppendText(fileAllPath))
                {
                    sw.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Console.Write("追加文件异常:" + ex.Message.ToString());
            }
        }
    }
}
