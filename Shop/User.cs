//-----------------------------------------------------------------------
// <copyright file="User.cs" company="My">
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
    /// Create a class User to add a new one
    /// edit user data
    /// remove
    /// get all users
    /// </summary>
    class User
    {
        /// <summary>
        /// Class for connect to database.
        /// </summary>
        Connect connect = new Connect();

        /// <summary>
        /// Fuction to check the Username.
        /// </summary>
        /// <param name="username">username sender</param>
        /// <returns>If the user exists - return true.</returns>
        public bool usernameExists(string username)
        {
            string query = "SELECT * FROM [USERS] WHERE [Login]=@Login";
            SqlCommand command = new SqlCommand(query, connect.GetConnection());
            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = username;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            // If the user exists - return true
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Function to insert a new user.
        /// </summary>
        /// <param name="login">login sender.</param>
        /// <param name="pass">pass sender.</param>
        /// <param name="utype">utype sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool insertUser(string login, string pass, string utype)
        {
            SqlCommand command = new SqlCommand();
            string insertQuery = "INSERT INTO [USERS]([Login], [Password], [User Type]) VALUES (@Login, @Password, @UserType)";
            command.CommandText = insertQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = utype;

            connect.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnection();
                return true;
            }
            else
            {
                connect.CloseConnection();
                return false;
            }
        }

        /// <summary>
        /// Function for getting users.
        /// </summary>
        /// <returns>Return table.</returns>
        public DataTable getUsers()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [USERS]", connect.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Function to edit a user.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <param name="login">login sender.</param>
        /// <param name="pass">pass sender.</param>
        /// <param name="utype">utype sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool editUser(int id, string login, string pass, string utype)
        {
            SqlCommand command = new SqlCommand();
            string editQuery = "UPDATE [USERS] SET [Login]=@Login, [Password]=@Password, [User Type]=@UserType WHERE [ID]=@ID";
            command.CommandText = editQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = utype;

            connect.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnection();
                return true;
            }
            else
            {
                connect.CloseConnection();
                return false;
            }
        }

        /// <summary>
        /// Function to remove user.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool removeUser(int id)
        {
            SqlCommand command = new SqlCommand();
            string removeQuery = "DELETE FROM [USERS] WHERE [ID]=@ID";
            command.CommandText = removeQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            connect.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnection();
                return true;
            }
            else
            {
                connect.CloseConnection();
                return false;
            }
        }
    }
}
