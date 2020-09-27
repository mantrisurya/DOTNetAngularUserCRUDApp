namespace LNUserListingApp.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using LNUserListingApp.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using LNUserListingApp.Controllers;
    using LNUserListingApp.Data;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Social feed data access class
    /// </summary>
    public class UsersService
    {
        private static string connectionString;
        private static IConfiguration dataBaseConnection;
        private readonly LogException objLogException;
        public UsersService(IConfiguration config)
        {
            dataBaseConnection = config;
            objLogException = new LogException(dataBaseConnection);
            connectionString = dataBaseConnection.GetSection("DefaultConnection").Value;
        }
        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        private static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public DataTable fetchusers(int userId,int loginId)
        {
            DataTable usersDT = new DataTable();
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    // calling sp
                    using (SqlCommand usersCmd = new SqlCommand("dbo.GetUserData", con))
                    {
                        usersCmd.CommandType = CommandType.StoredProcedure;
                        usersCmd.Parameters.AddWithValue("@id", userId);
                        // reading data
                        SqlDataAdapter socialFeedDA = new SqlDataAdapter();
                        socialFeedDA.SelectCommand = usersCmd;
                        socialFeedDA.Fill(usersDT);
                    }
                }
            }
            catch (Exception feedException)
            {
                this.objLogException.LogExceptionDetails(feedException, feedException.Source, loginId, "GetRSSFeed");
                return usersDT;
            }

            return usersDT;
        }
        public int insertUser(User user, int loginUser)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    // calling sp
                    using (SqlCommand usersCmd = new SqlCommand("dbo.InsertUserData", con))
                    {
                        usersCmd.CommandType = CommandType.StoredProcedure;
                        usersCmd.Parameters.AddWithValue("@name", user.Name);
                        usersCmd.Parameters.AddWithValue("@mobileNumber", user.MobileNumber);
                        usersCmd.Parameters.AddWithValue("@status", user.status);
                        usersCmd.Parameters.AddWithValue("@createdBy", loginUser);
                        usersCmd.ExecuteNonQuery();
                    }
                }
                return result;
            }
            catch (Exception feedException)
            {
                this.objLogException.LogExceptionDetails(feedException, feedException.Source, loginUser, "GetRSSFeed");
                return result;
            }

            return result;
        }
        public int updateUser(User user, int loginUser, int userId)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    // calling sp
                    using (SqlCommand usersCmd = new SqlCommand("dbo.InsertUserData", con))
                    {
                        usersCmd.CommandType = CommandType.StoredProcedure;
                        usersCmd.Parameters.AddWithValue("@userId", userId);
                        usersCmd.Parameters.AddWithValue("@name", user.Name);
                        usersCmd.Parameters.AddWithValue("@mobileNumber", user.MobileNumber);
                        usersCmd.Parameters.AddWithValue("@status", user.status);
                        usersCmd.Parameters.AddWithValue("@createdBy", loginUser);
                        usersCmd.ExecuteNonQuery();
                    }
                }
                return result;
            }
            catch (Exception feedException)
            {
                this.objLogException.LogExceptionDetails(feedException, feedException.Source, loginUser, "GetRSSFeed");
                return result;
            }
        }
        public int removeUser(int userId)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    // calling sp
                    using (SqlCommand usersCmd = new SqlCommand("dbo.DeleteUserData", con))
                    {
                        usersCmd.CommandType = CommandType.StoredProcedure;
                        usersCmd.Parameters.AddWithValue("@userId", userId);
                        usersCmd.ExecuteNonQuery();
                    }
                }
                return result;
            }
            catch (Exception feedException)
            {
                this.objLogException.LogExceptionDetails(feedException, feedException.Source, 0, "GetRSSFeed");
                return result;
            }
        }
    }
}