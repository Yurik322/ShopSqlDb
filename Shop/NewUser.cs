//-----------------------------------------------------------------------
// <copyright file="NewUser.cs" company="My">
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
    /// Class for new user.
    /// </summary>
    public partial class NewUser : Form
    {
        User user = new User();

        /// <summary>
        /// Initializes a new instance of the <see cref="NewUser"/> class.
        /// </summary>
        public NewUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for adding new user.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonAddNewUser_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string pass = textBoxPassword.Text;
            string utype = textBoxUserType.Text;

            if (!user.usernameExists(login))
            {
                if (login.Trim().Equals("") || pass.Trim().Equals("") || utype.Trim().Equals(""))
                {
                    MessageBox.Show("Reuired Fields - login & password + user type", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean insertUser = user.insertUser(login, pass, utype);

                    if (insertUser)
                    {
                        MessageBox.Show("New User inserted successfuly", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - User NOT inserted", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("This Login already exists, try another one", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Function for cancel user fields.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxLogin.Text = "";
            textBoxPassword.Text = "";
            textBoxUserType.Text = "";
        }
    }
}
