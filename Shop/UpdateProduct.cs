//-----------------------------------------------------------------------
// <copyright file="UpdateProduct.cs" company="My">
//    Created by yurik_322 on 20/01/15.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop
{
    /// <summary>
    /// Class for updating a product.
    /// </summary>
    public partial class UpdateProduct : Form
    {
        Products products = new Products();

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProduct"/> class.
        /// </summary>
        public UpdateProduct()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for update info about products.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = products.getProducts();
        }

        /// <summary>
        /// Function for edit product.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int id;
            string name = textBoxName.Text;
            int stockq;
            decimal price;
            int categories;
            string description = textBoxDescription.Text;

            MemoryStream pic = new MemoryStream();
            pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);

            id = Convert.ToInt32(textBoxID.Text);

            stockq = Convert.ToInt32(textBoxStockQuantity.Text);
            price = Convert.ToDecimal(textBoxPrise.Text);
            categories = Convert.ToInt32(textBoxCategories.Text);

            if (textBoxName.Text.Equals("") || textBoxStockQuantity.Text.Equals("") || textBoxPrise.Text.Equals("") || textBoxCategories.Text.Equals("") || textBoxDescription.Text.Equals("") || pictureBox1.Image == null)
            {
                MessageBox.Show("* Required Fields - Name / Stock quantity / Price / Categories / Descriprion / Image", "Edit Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (products.editProduct(id, name, stockq, price, categories, pic, description))
                {
                    MessageBox.Show("Product updated successfuly", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Product NOT updated", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Function for cancel text in fields.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxName.Text = "";
            textBoxStockQuantity.Text = "";
            textBoxPrise.Text = "";
            textBoxCategories.Text = "";
            textBoxDescription.Text = "";
            pictureBox1.Image = null;
        }

        /// <summary>
        /// Function for display the selected customer data from dataGrid to textboxes info about product.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxStockQuantity.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPrise.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCategories.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBoxDescription.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            // System.InvalidCastException: 'Unable to cast object of type 'System.DBNull' to type 'System.Byte[]'.'
            byte[] pic = (byte[])dataGridView1.CurrentRow.Cells[5].Value;
            MemoryStream picture = new MemoryStream(pic);
            pictureBox1.Image = Image.FromStream(picture);
        }

        /// <summary>
        /// Function for browse image.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonBrowseImg_Click(object sender, EventArgs e)
        {
            // Select and display image in the picturebox
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
