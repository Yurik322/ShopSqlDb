//-----------------------------------------------------------------------
// <copyright file="Products.cs" company="My">
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
using System.IO;

namespace Shop
{
    /// <summary>
    /// Create a class Product to add a new one
    /// edit product data
    /// remove
    /// get all products
    /// </summary>
    class Products
    {
        /// <summary>
        /// Class for connect to database.
        /// </summary>
        Connect connect = new Connect();

        /// <summary>
        /// Function for checkig the product.
        /// </summary>
        /// <param name="name">name sender.</param>
        /// <returns>If the product exists return true.</returns>
        public bool productExists(string name)
        {
            SqlCommand command = new SqlCommand();
            string query = "SELECT * FROM [Manage Products] WHERE [Name]=@Name";
            command.CommandText = query;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);

            // if the product exists return true
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
        /// Function to insert a new product.
        /// </summary>
        /// <param name="name">name sender.</param>
        /// <param name="stockq">stockq sender.</param>
        /// <param name="price">price sender.</param>
        /// <param name="categories">categories sender.</param>
        /// <param name="image">image sender.</param>
        /// <param name="description">description sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool insertProduct(string name, string stockq, string price, string categories, MemoryStream image, string description)
        {
            SqlCommand command = new SqlCommand();
            string insertQuery = "INSERT INTO [Manage Products]([Name], [Stock Quantity], [Price], [Categories], [Image], [Description]) VALUES (@Name, @StockQuantity, @Price, @Categories, @Image, @Description)";
            command.CommandText = insertQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
            command.Parameters.Add("@StockQuantity", SqlDbType.VarChar).Value = stockq;
            command.Parameters.Add("@Price", SqlDbType.VarChar).Value = price;
            command.Parameters.Add("@Categories", SqlDbType.VarChar).Value = categories;
            command.Parameters.Add("@Image", SqlDbType.Binary).Value = image.ToArray();
            command.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;
            
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
        /// Function for getting all products.
        /// </summary>
        /// <returns>Return table.</returns>
        public DataTable getProducts()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Manage Products]", connect.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Function for return the product data using his id.
        /// </summary>
        /// <param name="productid">productid sender.</param>
        /// <returns>Return table.</returns>
        public DataTable getProductId(Int32 productid)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand command = new SqlCommand("SELECT * FROM [Manage Products] WHERE [ID]=@ID", connect.GetConnection());
            adapter.SelectCommand = command;

            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Function to edit a product.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <param name="name">name sender.</param>
        /// <param name="stockq">stockq sender.</param>
        /// <param name="price">price sender.</param>
        /// <param name="categories">categories sender.</param>
        /// <param name="image">image sender.</param>
        /// <param name="description">description sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool editProduct(int id, string name, int stockq, decimal price, int categories, MemoryStream image, string description)
        {
            SqlCommand command = new SqlCommand();
            string editQuery = "UPDATE [Manage Products] SET [Name]=@Name, [Stock Quantity]=@StockQuantity, [Price]=@Price, [Categories]=@Categories, [Image]=@Image, [Description]=@Description WHERE [ID]=@ID";
            command.CommandText = editQuery;
            command.Connection = connect.GetConnection();

            // ще отут відформатувати
            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
            command.Parameters.Add("@StockQuantity", SqlDbType.Int).Value = stockq;
            command.Parameters.Add("@Price", SqlDbType.Int).Value = price;
            command.Parameters.Add("@Categories", SqlDbType.Int).Value = categories;
            command.Parameters.Add("@Image", SqlDbType.Binary).Value = image.ToArray();
            command.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;

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
        /// Function for remove product.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool removeProduct(int id)
        {
            SqlCommand command = new SqlCommand();
            string removeQuery = "DELETE FROM [Manage Products] WHERE [ID]=@ID";
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
