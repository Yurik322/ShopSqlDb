//-----------------------------------------------------------------------
// <copyright file="Main_Form.cs" company="My">
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

namespace Shop
{
    /// <summary>
    /// Class for Main_Form.
    /// </summary>
    public partial class Main_Form : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Main_Form"/> class.
        /// </summary>
        public Main_Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Exit from main form.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// ManageProducts (Product).
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageProducts manageProducts = new ManageProducts();

            manageProducts.ShowDialog();
        }

        /// <summary>
        /// ManageCategories (Category).
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void manageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageCategories manageCategories = new ManageCategories();

            manageCategories.ShowDialog();
        }

        /// <summary>
        /// ManageClientsForm (Customer).
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageClientsForm manageClientsForm = new ManageClientsForm();

            manageClientsForm.ShowDialog();
        }

        /// <summary>
        /// ManageOrder (Order).
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageOrder manageOrder = new ManageOrder();

            manageOrder.ShowDialog();
        }

        /// <summary>
        /// ManageUser (User).
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers manageUsers = new ManageUsers();

            manageUsers.ShowDialog();
        }
    }
}
