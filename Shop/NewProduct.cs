//-----------------------------------------------------------------------
// <copyright file="NewProduct.cs" company="My">
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
    /// Class for new product.
    /// </summary>
    public partial class NewProduct : Form
    {
        Products products = new Products();

        /// <summary>
        /// Initializes a new instance of the <see cref="NewProduct"/> class.
        /// </summary>
        public NewProduct()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for insert new product.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string stockq = textBoxStockQuantity.Text;
            string price = textBoxPrise.Text;
            string categories = textBoxCategories.Text;
            string description = textBoxDescription.Text;

            if (verifFields("add new product"))
            {
                MemoryStream pic = new MemoryStream();
                pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
                //// we need to check if the username already exists
                //// we need to insert the new product in the database
                //// we will create that in class Products
                if (!products.productExists(name))
                {
                    if (products.insertProduct(name, stockq, price, categories, pic, description))
                    {
                        MessageBox.Show("Insertion complated successfully", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Something wrong", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("This product name already exists. Try another one", "Invalid product name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("* Required Fields - Name / Stock quantity / Price / Categories / Descriprion / Image", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Function for checking empty fields.
        /// </summary>
        /// <param name="operation">operation sender.</param>
        /// <returns>Return check.</returns>
        public bool verifFields(string operation)
        {
            bool check = false;

            // if the operation is register
            if (operation == "add new product")
            {
                if (textBoxName.Text.Equals("") || textBoxStockQuantity.Text.Equals("") || textBoxPrise.Text.Equals("") || textBoxCategories.Text.Equals("") || textBoxDescription.Text.Equals("") || pictureBox1.Image == null)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }

            return check;
        }

        /// <summary>
        /// Function for cancel product.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxStockQuantity.Text = "";
            textBoxPrise.Text = "";
            textBoxCategories.Text = "";
            textBoxDescription.Text = "";
            pictureBox1.Image = null;
        }

        /// <summary>
        /// Function for browse button.
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
