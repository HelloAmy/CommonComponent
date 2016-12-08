// ***********************************************************************
// Assembly         : Common.Julius.HttpHelper
// Author           : zhujinrong
// Created          : 04-19-2016
//
// Last Modified By : zhujinrong
// Last Modified On : 04-19-2016
// ***********************************************************************
// <copyright file="ResultCookieType.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Component.Butterfly.HttpHelper
{
    /// <summary>
    /// Cookie返回类型
    /// </summary>
    public enum ResultCookieType
    {
        /// <summary>
        /// 只返回字符串类型的Cookie
        /// </summary>
        String,

        /// <summary>
        /// CookieCollection格式的Cookie集合同时也返回String类型的cookie
        /// </summary>
        CookieCollection
    }  
}
