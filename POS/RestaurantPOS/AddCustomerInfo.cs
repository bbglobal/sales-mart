using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace POS
{
    public partial class AddCustomerInfo : Form
    {
        private string json;
        private string insertStatus = "";
        private decimal total = 0;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));

        public AddCustomerInfo(string json, decimal total)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            this.json = json;
            this.total = total;
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

            // Restrict Phone_TextBox to numeric input only
            Phone_TextBox.KeyPress += Phone_TextBox_KeyPress;
        }

        public AddCustomerInfo()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

            // Restrict Phone_TextBox to numeric input only
            Phone_TextBox.KeyPress += Phone_TextBox_KeyPress;
        }

        public string InsertStatus
        {
            get { return insertStatus; }
        }

        private void InitializeDatabaseConnection()
        {
            if (Session.SelectedModule == "Restaurant POS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            else if (Session.SelectedModule == "Hotel Management")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
        }

        private void SaveData()
        {
            // Check for empty fields and confirm if user wants to proceed
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) || string.IsNullOrWhiteSpace(Phone_TextBox.Text))
            {
                string missingField = string.IsNullOrWhiteSpace(Name_TextBox.Text) ? "Name" : "Phone Number";
                var result = MessageBox.Show($"{missingField} is not provided. Do you want to proceed without it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return; // Stop if the user chooses not to proceed
                }
                else if (result == DialogResult.Yes && string.IsNullOrWhiteSpace(Name_TextBox.Text) && string.IsNullOrWhiteSpace(Phone_TextBox.Text))
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return; // Close form without inserting data if both fields are empty
                }
            }

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO bill_list (items, customer, phone, date, type, status, total_amount, net_total_amount) VALUES (@Items, @Name, @Phone, @Date, @Type, @Status, @Total, @NetTotal)", connection);
                command.Parameters.AddWithValue("@Items", json);
                command.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(Phone_TextBox.Text) ? (object)DBNull.Value : Convert.ToInt32(Phone_TextBox.Text));
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@Type", "Delivery");
                command.Parameters.AddWithValue("@Status", "In Complete");
                command.Parameters.AddWithValue("@Total", total);
                command.Parameters.AddWithValue("@NetTotal", total);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Saved Successfully");
                    insertStatus = "Inserted";
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                this.Close();
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

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Phone_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
