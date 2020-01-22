//-----------------------------------------------------------------------
// <copyright file="Order.cs" company="My">
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
    /// Create a class Order to add a new one
    /// edit order data
    /// remove
    /// get total amount
    /// </summary>
    class Order
    {
        /// <summary>
        /// Class for connect to database.
        /// </summary>
        Connect connect = new Connect();

        /// <summary>
        /// Function to insert a new Order.
        /// </summary>
        /// <param name="product">product sender.</param>
        /// <param name="quantity">quantity sender.</param>
        /// <param name="check">check sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool insertOrder(string product, string quantity, string check)
        {
            SqlCommand command = new SqlCommand();
            string insertQuery = "INSERT INTO [Manage Order]([Product], [Quantity], [Check]) VALUES (@Product, @Quantity, @Check)";
            command.CommandText = insertQuery;
            command.Connection = connect.GetConnection();
            
            command.Parameters.Add("@Product", SqlDbType.VarChar).Value = product;
            command.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = quantity;
            command.Parameters.Add("@Check", SqlDbType.VarChar).Value = check;

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
        /// Function for getting all orders.
        /// </summary>
        /// <returns>Return table.</returns>
        public DataTable getOrders()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Manage Order]", connect.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Function for edit order.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <param name="product">product sender.</param>
        /// <param name="quantity">quantity sender.</param>
        /// <param name="check">check sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool editOrder(int id, string product, string quantity, string check)
        {
            SqlCommand command = new SqlCommand();
            string editQuery = "UPDATE [Manage Order] SET [Product]=@Product, [Quantity]=@Quantity, [Check]=@Check WHERE [OrderID]=@OrderID";
            command.CommandText = editQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = id;
            command.Parameters.Add("@Product", SqlDbType.VarChar).Value = product;
            command.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = quantity;
            command.Parameters.Add("@Check", SqlDbType.VarChar).Value = check;

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
        /// Function for remove order.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool removeOrder(int id)
        {
            SqlCommand command = new SqlCommand();
            string removeQuery = "DELETE FROM [Manage Order] WHERE [OrderID]=@OrderID";
            command.CommandText = removeQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = id;

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
