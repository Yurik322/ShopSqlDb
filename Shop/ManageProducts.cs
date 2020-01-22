//-----------------------------------------------------------------------
// <copyright file="ManageProducts.cs" company="My">
//    Created by yurik_322 on 20/01/15.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Shop
{
    /// <summary>
    /// Class for managing products.
    /// </summary>
    public partial class ManageProducts : Form
    {
        /// <summary>
        /// Class for connect to database.
        /// </summary>
        Connect connect = new Connect();

        Products products = new Products();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageProducts"/> class.
        /// </summary>
        public ManageProducts()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for manage products.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void ManageProducts_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = products.getProducts();
        }

        /// <summary>
        /// Function for adding new client.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonAddNewCustomer_Click(object sender, EventArgs e)
        {
            NewProduct newProduct = new NewProduct();
            newProduct.ShowDialog();
        }

        /// <summary>
        /// Function for edit client.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            UpdateProduct updateProduct = new UpdateProduct();
            updateProduct.ShowDialog();
        }

        /// <summary>
        /// Function for remove ManageProducts.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);

                if (products.removeProduct(id))
                {
                    dataGridView1.DataSource = products.getProducts();
                    MessageBox.Show("Product deleted successfuly", "Deleted Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Product NOT deleted", "Deleted Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for search products.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Manage Products] WHERE CONCAT([ID], [Name], [Stock Quantity], [Price], [Categories], [Description]) LIKE '%" + textBoxSearch.Text + "%'", connect.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }

        /// <summary>
        /// Function for update and save.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonUpdateAndSave_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            dataGridView1.DataSource = products.getProducts();
        }

        /// <summary>
        /// Function for display the selected product data from dataGrid to textboxes in products.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
