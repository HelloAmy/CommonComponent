// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="TMAppException.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class TMAppException
    /// </summary>
    public class TMAppException : IMAppException
    {
        /// <summary>
        /// Inserts the app exception.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertAppException(MAppException model)
        {
            string log = JsonHelper.JsonSerializer<MAppException>(model);

            // 想了一下不能直接往文件中加，多线程的时候容易错误。还是先去队列中排队吧！
            TextDataAccess.AddLog(log);
        }
    }
}
