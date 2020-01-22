//-----------------------------------------------------------------------
// <copyright file="LoginForm.cs" company="My">
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
    /// This class will make the LoginForm for my application.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Class for connect to database.
        /// </summary>
        Connect connect = new Connect();

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginForm"/> class.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Login button.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonLog_Click(object sender, EventArgs e)
        {
            Connect connect = new Connect();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            string query = "SELECT * FROM [USERS] WHERE [Login]=@Login AND [Password]=@Password";

            command.CommandText = query;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = textBoxName.Text;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBoxPass.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            // If the username and the password exists
            if (table.Rows.Count > 0)
            {
                // Show the main form
                this.Hide();
                Main_Form mForm = new Main_Form();
                mForm.Show();
            }
            else
            {
                if (textBoxUsername.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Username to Login", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxPassword.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Password to Login", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("This Username or Password doesn't exists", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Create a function to check the Username
        /// </summary>
        /// <param name="username">fname sender.</param>
        /// <returns>If the user exists - return true.</returns>
        public bool usernameExists(string username)
        {
            string query = "SELECT * FROM [USERS] WHERE [Login]=@Login";
            SqlCommand command = new SqlCommand(query, connect.GetConnection());
            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = username;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            // If the user exists - return true
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
        /// Function to insert a new User with registration.
        /// </summary>
        /// <param name="login">login sender.</param>
        /// <param name="pass">pass sender.</param>
        /// <param name="utype">utype sender.</param>
        /// <returns>Return true - command execute, another way false.</returns>
        public bool insertUserReg(string login, string pass, string utype)
        {
            SqlCommand command = new SqlCommand();
            string insertQuery = "INSERT INTO [USERS]([Login], [Password], [User Type]) VALUES (@Login, @Password, @UserType)";
            command.CommandText = insertQuery;
            command.Connection = connect.GetConnection();

            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = utype;

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
        /// Register button.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonReg_Click(object sender, EventArgs e)
        {
            string login = textBoxNameRegistration.Text;
            string pass = textBoxPassRegistration.Text;
            string utype = textBoxTypeOfUser.Text;

            if (!usernameExists(login))
            {
                if (login.Trim().Equals("") || pass.Trim().Equals("") || utype.Trim().Equals(""))
                {
                    MessageBox.Show("Reuired Fields - login & password + user type", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean insertUser = insertUserReg(login, pass, utype);

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
        /// Label go to the register section
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void labelGoToReg_Click(object sender, EventArgs e)
        {
            timer1.Start();
            labelGoToReg.Enabled = false;
            labelGoToLog.Enabled = false;
        }

        /// <summary>
        /// Label go to the login section.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void labelGoToLog_Click(object sender, EventArgs e)
        {
            timer2.Start();
            labelGoToLog.Enabled = false;
            labelGoToReg.Enabled = false;
        }

        /// <summary>
        /// When this timer will start we will show only the register part.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel6.Location.X > -420)
            {
                panel6.Location = new Point(panel6.Location.X - 10, panel6.Location.Y);
                panel2.Location = new Point(panel2.Location.X - 10, panel2.Location.Y);
                panel1.Location = new Point(panel1.Location.X - 10, panel1.Location.Y);
                pictureBox1.Location = new Point(pictureBox1.Location.X - 10, pictureBox1.Location.Y);
                pictureBox2.Location = new Point(pictureBox2.Location.X - 10, pictureBox2.Location.Y);
            }
            else
            {
                timer1.Stop();
                labelGoToLog.Enabled = true;
                labelGoToReg.Enabled = true;
            }
        }

        /// <summary>
        /// When this timer will start we will show only the login part.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel6.Location.X < 0)
            {
                panel6.Location = new Point(panel6.Location.X + 10, panel6.Location.Y);
                panel2.Location = new Point(panel2.Location.X + 10, panel2.Location.Y);
                panel1.Location = new Point(panel1.Location.X + 10, panel1.Location.Y);
                pictureBox1.Location = new Point(pictureBox1.Location.X + 10, pictureBox1.Location.Y);
                pictureBox2.Location = new Point(pictureBox2.Location.X + 10, pictureBox2.Location.Y);
            }
            else
            {
                timer2.Stop();
                labelGoToLog.Enabled = true;
                labelGoToReg.Enabled = true;
            }
        }
    }
}
