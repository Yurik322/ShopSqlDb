//-----------------------------------------------------------------------
// <copyright file="Category.cs" company="My">
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
    /// Create a class category to add a new one
    /// edit category data
    /// remove
    /// get all category
    /// </summary>
    class Category
    {
        /// <summary>
        /// Class for connect to database.
        /// </summary>
        Connect connect = new Connect();

        /// <summary>
        /// Function to insert a category.
        /// </summary>
        /// <param name="name">name sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool insertCategory(string name)
        {
            SqlCommand command = new SqlCommand();
            string insertQuery = "INSERT INTO [Manage Categories]([Name]) VALUES (@Name)";
            command.CommandText = insertQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

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
        /// Function for getting the categories.
        /// </summary>
        /// <returns>Return table.</returns>
        public DataTable gerCategories()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Manage Categories]", connect.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Function for edit the categories.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <param name="name">name sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool editCategory(int id, string name)
        {
            SqlCommand command = new SqlCommand();
            string editQuery = "UPDATE [Manage Categories] SET [Name]=@Name WHERE [ID]=@ID";
            command.CommandText = editQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

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
        /// Function for remove the categories.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool removeCategory(int id)
        {
            SqlCommand command = new SqlCommand();
            string removeQuery = "DELETE FROM [Manage Categories] WHERE [ID]=@ID";
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
