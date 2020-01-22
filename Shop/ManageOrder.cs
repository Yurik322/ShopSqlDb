//-----------------------------------------------------------------------
// <copyright file="ManageOrder.cs" company="My">
//    Created by yurik_322 on 20/01/15.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop
{
    /// <summary>
    /// Class for managing orders.
    /// </summary>
    public partial class ManageOrder : Form
    {
        Order order = new Order();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageOrder"/> class.
        /// </summary>
        public ManageOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for load ManageOrder.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void ManageOrder_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = order.getOrders();
        }

        /// <summary>
        /// Function for insert new order.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonInsertNewOrder_Click(object sender, EventArgs e)
        {
            string product = textBoxProduct.Text;
            string quantity = textBoxQuantity.Text;
            string check = textBoxCheck.Text;

            if (product.Trim().Equals("") || quantity.Trim().Equals("") || check.Trim().Equals(""))
            {
                MessageBox.Show("Reuired Fields - Product & Quantity + check", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean insertOrder = order.insertOrder(product, quantity, check);

                if (insertOrder)
                {
                    dataGridView1.DataSource = order.getOrders();
                    MessageBox.Show("New Customer inserted successfuly", "Add Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Customer NOT inserted", "Add Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Function for edit ManageOrder.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int id;
            string product = textBoxProduct.Text;
            string quantity = textBoxQuantity.Text;
            string check = textBoxCheck.Text;

            try
            {
                id = Convert.ToInt32(textBoxID.Text);

                if (product.Trim().Equals("") || quantity.Trim().Equals("") || check.Trim().Equals(""))
                {
                    MessageBox.Show("Reuired Fields - Product & Quantity + Check", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean editOrder = order.editOrder(id, product, quantity, check);

                    if (editOrder)
                    {
                        dataGridView1.DataSource = order.getOrders();
                        MessageBox.Show("New Order updated successfuly", "Edit Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Order NOT updated", "Edit Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for remove ManageOrder.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);

                if (order.removeOrder(id))
                {
                    dataGridView1.DataSource = order.getOrders();
                    MessageBox.Show("Customer deleted successfuly", "Deleted Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //// you can clear all textboxes after the delete if you want
                    //// by calling the clear button
                    buttonClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Order NOT deleted", "Deleted Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for clear fields ManageOrder.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxProduct.Text = "";
            textBoxQuantity.Text = "";
            textBoxCheck.Text = "";
            textBoxPaid.Text = "";
        }

        /// <summary>
        /// Function for display the selected customer data from dataGrid to textboxes ManageOrder.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxProduct.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxQuantity.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxCheck.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        /// <summary>
        /// Function for generating field total amount.
        /// </summary> 
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonPaid_Click(object sender, EventArgs e)
        {
            Connect connect = new Connect();
            SqlCommand command = new SqlCommand();
            string sql = "SELECT (dbo.TotalSum(CheckID)) FROM[Check] WHERE CheckID = (SELECT MAX([Check]) FROM[Manage Order])";
            command.CommandText = sql;
            command.Connection = connect.GetConnection();
            
            try
            {
                connect.OpenConnection();
                int total = Convert.ToInt32(command.ExecuteScalar());
                textBoxPaid.Text = total.ToString();
                command.Dispose();
                connect.CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }
    }
}
