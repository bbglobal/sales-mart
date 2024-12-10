using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS
{
    public partial class BranchForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        string connectionString;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
        public BranchForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            //SetTypeComboBox();
            this.rowIndex = rowIndex;
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
            PhoneTB.KeyPress += PhoneTB_KeyPress;

        }

        private void PhoneTB_KeyPress1(object? sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitializeDatabaseConnection()
        {
            if (Session.BranchCode == "PK728")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            else if (Session.BranchCode == "BR001")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
        }


        private void InitializeLabel(Label label, Image image, int newWidth, int newHeight)
        {
            Image resizedImage = ResizeImage(image, newWidth, newHeight);

            label.Image = resizedImage;
        }

        private Image ResizeImage(Image img, int newWidth, int newHeight)
        {
            Bitmap resizedImg = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, newWidth, newHeight);
            }
            return resizedImg;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveData()
        {
            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(BranchNameTB.Text) ||
                string.IsNullOrWhiteSpace(BranchCodeTB.Text) ||
                string.IsNullOrWhiteSpace(PhoneTB.Text) ||
                string.IsNullOrWhiteSpace(AddressTB.Text))
            {
                MessageBox.Show("All fields are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string branchCode = BranchCodeTB.Text;
            string branchName = BranchNameTB.Text;
            string phone = PhoneTB.Text;
            string address = AddressTB.Text;

            // Retrieve connection strings
            string myconnGS = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
            string myconnGSBR001 = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;

            try
            {
                // Function to insert data into a database
                void InsertBranchData(string connectionString)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Insert query
                        string query = "INSERT INTO branches (branch_code, branch_name, phone, address) VALUES (@BranchCode, @BranchName, @Phone, @Address)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters to avoid SQL injection
                            command.Parameters.AddWithValue("@BranchCode", branchCode);
                            command.Parameters.AddWithValue("@BranchName", branchName);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@Address", address);

                            // Execute query
                            command.ExecuteNonQuery();
                        }
                    }
                }

                // Insert into both databases
                InsertBranchData(myconnGS);
                InsertBranchData(myconnGSBR001);

                // Success message
                MessageBox.Show("Branch data saved successfully to both databases!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Prevent letters in PhoneTB
        private void PhoneTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and control characters
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }


    }
}
