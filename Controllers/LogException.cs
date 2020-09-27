// <copyright file = "Logexception.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System;
using System.Data;
using System.Reflection;
using LNUserListingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LNUserListingApp.Controllers
{
    /// <summary>
    /// Log Exception
    /// </summary>
    public class LogException
    {
        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        private static IConfiguration dataBaseConnectionString;

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        private static SqlConnection connExceptionLog;

        /// <summary>
        /// Initializes a new instance of the<see cref="LogException" /> class.
        /// </summary>
        /// <param name="connectionString">The connectionString</param>
        public LogException(IConfiguration connectionString)
        {
            dataBaseConnectionString = connectionString;
            connExceptionLog = new SqlConnection(dataBaseConnectionString.GetSection("ConnectionString").Value);
        }

        /// <summary>
        /// Logs Exception Details
        /// </summary>
        /// <param name="exception">Exception Details</param>
        /// <param name="errorSource">Error Details</param>
        /// <param name="id">associate number</param>
        /// <param name="eventMethod">event Method</param>
        public void LogExceptionDetails(Exception exception, string errorSource, int id, string eventMethod)
        {
            string exceptionMessage = string.Empty;
            string exceptionStackTrace = string.Empty;
            string exceptionInnerException = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    exceptionMessage = exception.Message;
                }

                if (!string.IsNullOrEmpty(exception.StackTrace))
                {
                    exceptionStackTrace = exception.StackTrace;
                }

                using (connExceptionLog)
                {
                    using (SqlCommand cmdExceptionLog = new SqlCommand("[dbo].[SPExceptionLogger]", connExceptionLog))
                    {
                        cmdExceptionLog.CommandType = CommandType.StoredProcedure;
                        cmdExceptionLog.Parameters.AddWithValue("@AssociateID", id);
                        cmdExceptionLog.Parameters.AddWithValue("@Source", errorSource);
                        cmdExceptionLog.Parameters.AddWithValue("@Event", eventMethod);
                        cmdExceptionLog.Parameters.AddWithValue("@EventID", string.Empty);
                        cmdExceptionLog.Parameters.AddWithValue("@Status", string.Empty);
                        cmdExceptionLog.Parameters.AddWithValue("@StatusMode", string.Empty);
                        cmdExceptionLog.Parameters.AddWithValue("@ExceptionMsg", exceptionMessage);
                        cmdExceptionLog.Parameters.AddWithValue("@InnerException", exceptionInnerException);
                        cmdExceptionLog.Parameters.AddWithValue("@StackTrace", exceptionStackTrace);
                        cmdExceptionLog.Parameters.AddWithValue("@Warning", string.Empty);
                        cmdExceptionLog.Parameters.AddWithValue("@WarningType", string.Empty);
                        cmdExceptionLog.Parameters.AddWithValue("@WarningLevel", string.Empty);
                        connExceptionLog.Open();
                        cmdExceptionLog.ExecuteNonQuery();
                        cmdExceptionLog.Dispose();
                    }
                }
            }
            catch (Exception exceptions)
            {
                ////this.LogExceptionDetails(exceptions, MethodBase.GetCurrentMethod().Name.ToString());
                Console.WriteLine(exceptions.StackTrace);
            }
            finally
            {
                if (connExceptionLog != null)
                {
                    connExceptionLog.Close();
                }
            }
        }
    }
}