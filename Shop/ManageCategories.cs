//-----------------------------------------------------------------------
// <copyright file="ManageCategories.cs" company="My">
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
    /// Class for managing categories of products.
    /// </summary>
    public partial class ManageCategories : Form
    {
        Category category = new Category();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageCategories"/> class.
        /// </summary>
        public ManageCategories()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for load ManageCategories.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void ManageCategories_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = category.gerCategories();
        }

        /// <summary>
        /// Function for adding new categories.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonAddNewCategories_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;

            if (name.Trim().Equals(""))
            {
                MessageBox.Show("Reuired Field - Name", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean insertCategory = category.insertCategory(name);

                if (insertCategory)
                {
                    dataGridView1.DataSource = category.gerCategories();
                    MessageBox.Show("New Category inserted successfuly", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Customer NOT inserted", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Function for edit.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int id;
            string name = textBoxName.Text;
          
            try
            {
                id = Convert.ToInt32(textBoxID.Text);

                if (name.Trim().Equals(""))
                {
                    MessageBox.Show("Reuired Field - Name", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean editCategory = category.editCategory(id, name);

                    if (editCategory)
                    {
                        dataGridView1.DataSource = category.gerCategories();
                        MessageBox.Show("New Category updated successfuly", "Edit Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Category NOT updated", "Edit Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for remove. 
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);

                if (category.removeCategory(id))
                {
                    dataGridView1.DataSource = category.gerCategories();
                    MessageBox.Show("Category deleted successfuly", "Deleted Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //// you can clear all textboxes after the delete if you want
                    //// by calling the clear button
                    buttonClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Category NOT deleted", "Deleted Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for clear fields.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxName.Text = "";
        }

        /// <summary>
        /// Function display the selected customer data from dataGrid to textboxes.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="e">e event.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
