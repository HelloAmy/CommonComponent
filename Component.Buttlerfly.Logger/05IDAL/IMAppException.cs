// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-08-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-15-2015
// ***********************************************************************
// <copyright file="IMAppException.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Interface IMAppException
    /// </summary>
    public interface IMAppException
    {
        /// <summary>
        /// Inserts the app exception.
        /// </summary>
        /// <param name="model">The model.</param>
        void InsertAppException(MAppException model);
    }
}
