//-----------------------------------------------------------------------
// <copyright file="Customer.cs" company="My">
//    Created by yurik_322 on 20/01/15.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Shop
{
    /// <summary>
    /// Create a class Customer to add a new one
    /// edit customer data
    /// remove
    /// get all customers
    /// </summary>
    class Customer
    {
        /// <summary>
        /// Class for connect to database.
        /// </summary>
        Connect connect = new Connect();

        /// <summary>
        /// Function to insert a new customer.
        /// </summary>
        /// <param name="fname">fname sender.</param>
        /// <param name="lname">lname sender.</param>
        /// <param name="phone">phone sender.</param>
        /// <param name="email">email sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool insertCustomer(string fname, string lname, string phone, string email)
        {
            SqlCommand command = new SqlCommand();
            string insertQuery = "INSERT INTO [Manage Customer]([First Name], [Last Name], [Phone Number], [Email]) VALUES (@FirstName, @LastName, @PhoneNumber, @Email)";
            command.CommandText = insertQuery;
            command.Connection = connect.GetConnection();
            
            command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;

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
        /// Function for getting clients.
        /// </summary>
        /// <returns>Return table.</returns>
        public DataTable getCustomers()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Manage Customer]", connect.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Function to edit a new customer.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <param name="fname">fname sender.</param>
        /// <param name="lname">lname sender.</param>
        /// <param name="phone">phone sender.</param>
        /// <param name="email">email sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool editCustomer(int id, string fname, string lname, string phone, string email)
        {
            SqlCommand command = new SqlCommand();
            string editQuery = "UPDATE [Manage Customer] SET [First Name]=@FirstName, [Last Name]=@LastName, [Phone Number]=@PhoneNumber, [Email]=@Email WHERE [UserID]=@UserID";
            command.CommandText = editQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
            command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;

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
        /// Function for remove client.
        /// </summary>
        /// <param name="id">id sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool removeCustomer(int id)
        {
            SqlCommand command = new SqlCommand();
            string removeQuery = "DELETE FROM [Manage Customer] WHERE [UserID]=@UserID";
            command.CommandText = removeQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = id;

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
