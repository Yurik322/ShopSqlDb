//-----------------------------------------------------------------------
// <copyright file="ManageClientsForm.cs" company="My">
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
    /// Class for managing clients.
    /// </summary>
    public partial class ManageClientsForm : Form
    {
        Customer customer = new Customer();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageClientsForm"/> class.
        /// </summary>
        public ManageClientsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for load ManageClients.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void ManageClientsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = customer.getCustomers();
        }

        /// <summary>
        /// Function for adding new client.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonAddNewCustomer_Click(object sender, EventArgs e)
        {
            string fname = textBoxFirstName.Text;
            string lname = textBoxLastName.Text;
            string phone = textBoxPhone.Text;
            string email = textBoxEmail.Text;

            if (fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals("") || email.Trim().Equals(""))
            {
                MessageBox.Show("Reuired Fields - First & Last Name + Phone Number", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean insertCustomer = customer.insertCustomer(fname, lname, phone, email);

                if (insertCustomer)
                {
                    dataGridView1.DataSource = customer.getCustomers();
                    MessageBox.Show("New Customer inserted successfuly", "Add Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Customer NOT inserted", "Add Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Function for edit client.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int id;
            string fname = textBoxFirstName.Text;
            string lname = textBoxLastName.Text;
            string phone = textBoxPhone.Text;
            string email = textBoxEmail.Text;

            try
            {
                id = Convert.ToInt32(textBoxID.Text);

                if (fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals("") || email.Trim().Equals(""))
                {
                    MessageBox.Show("Reuired Fields - First & Last Name + Phone Number", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean editCustomer = customer.editCustomer(id, fname, lname, phone, email);

                    if (editCustomer)
                    {
                        dataGridView1.DataSource = customer.getCustomers();
                        MessageBox.Show("New Customer updated successfuly", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Customer NOT updated", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for remove client.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);

                if (customer.removeCustomer(id))
                {
                    dataGridView1.DataSource = customer.getCustomers();
                    MessageBox.Show("Customer deleted successfuly", "Deleted Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //// you can clear all textboxes after the delete if you want
                    //// by calling the clear button
                    buttonClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Customer NOT deleted", "Deleted Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function clear fields client.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxEmail.Text = "";
        }

        /// <summary>
        /// Function for display the selected customer data from dataGrid to textboxes.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
