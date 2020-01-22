//-----------------------------------------------------------------------
// <copyright file="ManageUsers.cs" company="My">
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
    /// Class for managing users.
    /// </summary>
    public partial class ManageUsers : Form
    {
        User user = new User();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageUsers"/> class.
        /// </summary>
        public ManageUsers()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for load users.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = user.getUsers();
        }

        /// <summary>
        /// Function for edit user.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int id;
            string login = textBoxLogin.Text;
            string pass = textBoxPassword.Text;
            string utype = textBoxUserType.Text;

            try
            {
                id = Convert.ToInt32(textBoxID.Text);

                if (login.Trim().Equals("") || pass.Trim().Equals("") || utype.Trim().Equals(""))
                {
                    MessageBox.Show("Reuired Fields - login & password + user type", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean insertUser = user.editUser(id, login, pass, utype);

                    if (insertUser)
                    {
                        dataGridView1.DataSource = user.getUsers();
                        MessageBox.Show("New User updated successfuly", "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - User NOT updated", "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for remove users.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);

                if (user.removeUser(id))
                {
                    dataGridView1.DataSource = user.getUsers();
                    MessageBox.Show("Customer deleted successfuly", "Deleted User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Customer NOT deleted", "Deleted User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for adding new user.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonAddNewUser_Click(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.ShowDialog();
        }

        /// <summary>
        /// Function for display the selected customer data from dataGrid to textboxes user info.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxLogin.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxPassword.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxUserType.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        /// <summary>
        /// Function for update and save user info.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonUpdateAndSave_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            dataGridView1.DataSource = user.getUsers();
        }
    }
}
