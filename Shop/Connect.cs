//-----------------------------------------------------------------------
// <copyright file="Connect.cs" company="My">
//    Created by yurik_322 on 20/01/15.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Shop
{
    /// <summary>
    /// This class will make the connection between our app and database
    /// </summary>
    class Connect
    {
        /// <summary>
        /// String for SQL-connection.
        /// </summary>
        private SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Shop.mdf; Integrated Security = True; Connect Timeout = 30");

        /// <summary>
        /// Create a function to return our connection.
        /// </summary>
        /// <returns>Return connection.</returns>
        public SqlConnection GetConnection()
        {
            return connection;
        }

        /// <summary>
        /// Create a function to open the connection.
        /// </summary>
        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Create a function to close the connection
        /// </summary>
        public void CloseConnection()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
