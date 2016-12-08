// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="ILoging.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Interface ILoging
    /// </summary>
    public interface ILoging
    {
        /// <summary>
        /// Writes the app exception.
        /// </summary>
        /// <param name="appexception">The appexception.</param>
        void WriteAppException(AppException appexception);

        /// <summary>
        /// InteractionLog
        /// </summary>
        /// <param name="model">model</param>
        void InteractionLog(MInteractionParam model);
    }
}
