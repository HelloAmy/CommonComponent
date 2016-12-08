// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-10-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-10-2015
// ***********************************************************************
// <copyright file="AppException.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class AppException
    /// </summary>
    public class AppException : ApplicationException
    {
        /// <summary>
        /// The inner code
        /// </summary>
        private const string InnerCode = "99999999";

        /// <summary>
        /// The inner message
        /// </summary>
        private const string InnerMessage = "内部异常";

        /// <summary>
        /// The customer message
        /// </summary>
        private string customerMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        /// <param name="level">The level.</param>
        public AppException(string message, Exception inner, ExceptionLevel level)
            : base(message, inner)
        {
            this.ID = Guid.NewGuid();
            this.Level = level;
            this.ErrorCode = InnerCode;
        }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public Guid ID
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The level.</value>
        public ExceptionLevel Level
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public string ErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the customer message.
        /// </summary>
        /// <value>The customer message.</value>
        public string CustomerMessage
        {
            get
            {
                return string.IsNullOrEmpty(this.customerMessage.Trim()) ? this.Message : this.customerMessage.Trim();
            }

            private set
            {
                this.customerMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public Dictionary<string, object> Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// To the customer message.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToCustomerMessage()
        {
            return string.Format("{0}-{1}", this.ErrorCode, this.customerMessage);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("CustomerMessage:{0}", this.ToCustomerMessage()));
            sb.AppendLine(string.Format("ExceptionID:{0}", this.ID.ToString()));
            sb.AppendLine(this.GetDataStr());
            return sb.ToString() + base.ToString();
        }

        /// <summary>
        /// Gets the data STR.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetDataStr()
        {
            StringBuilder sb = new StringBuilder();
            Exception e = this;
            while (e != null)
            {
                foreach (var key in e.Data.Keys)
                {
                    sb.AppendFormat("Data:{0}>>{1}", key, e.Data[key]).AppendLine();
                }

                e = e.InnerException;
            }

            return sb.ToString();
        }
    }
}
