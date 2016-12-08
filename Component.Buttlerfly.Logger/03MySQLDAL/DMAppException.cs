// ***********************************************************************
// Assembly         : Common.Julius.LoggerComponent
// Author           : zhujinrong
// Created          : 12-08-2015
//
// Last Modified By : zhujinrong
// Last Modified On : 12-11-2015
// ***********************************************************************
// <copyright file="DMAppException.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MySql.Data.MySqlClient;

namespace Component.Buttlerfly.Logger
{
    /// <summary>
    /// Class DMAppException
    /// </summary>
    public class DMAppException : IMAppException
    {
        /// <summary>
        /// Inserts the app exception.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertAppException(MAppException model)
        {
            MySqlConnection conn = ConnectionFactory.LogExceptionWrite;
            try
            {
                string sql = string.Format(
@"INSERT INTO AppException{0}(LogID,TrackID,ErrorCode,ExceptionLevel,ApplicationName,LocalIP,ExceptionMsg,ExceptionContext,OrginalExceptionMsg)
VALUES(@LogID,@TrackID,@ErrorCode,@ExceptionLevel,@ApplicationName,@LocalIP,@ExceptionMsg,@ExceptionContext,@OrginalExceptionMsg);", 
                                                                                                                                   model.TimePoint.ToString("yyyyMMdd"));

                model.LogID = Guid.NewGuid().ToString();
                MySqlParameter[] param = 
                {
                     new MySqlParameter("@LogID", MySqlDbType.VarChar) { Value = model.LogID },
                     new MySqlParameter("@TrackID", MySqlDbType.VarChar) { Value = model.TrackID },
                     new MySqlParameter("@ErrorCode", MySqlDbType.VarChar) { Value = model.ErrorCode },
                     new MySqlParameter("@ExceptionLevel", MySqlDbType.Int32) { Value = (int)model.ExceptionLevel },
                     new MySqlParameter("@ApplicationName", MySqlDbType.VarChar) { Value = model.ApplicationName },
                     new MySqlParameter("@LocalIP", MySqlDbType.VarChar) { Value = model.LocalIP },
                     new MySqlParameter("@ExceptionMsg", MySqlDbType.VarChar) { Value = model.ExceptionMsg },
                     new MySqlParameter("@ExceptionContext", MySqlDbType.VarChar) { Value = model.ExceptionContext },
                     new MySqlParameter("@OrginalExceptionMsg", MySqlDbType.VarChar) { Value = model.OrginalExceptionMsg },
                };
                if (conn != null)
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    MySqlHelper.ExecuteNonQuery(conn, sql, param);
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}
