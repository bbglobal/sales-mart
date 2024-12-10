using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace POS
{
    public partial class AccountsAddForm : Form
    {   //yet to be added into activity log
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsAddForm));

        public AccountsAddForm(int rowIndex = -1)
        {
            InitializeComponent();
            InitializeComboBox(); // Initialize the ComboBox with options
            this.rowIndex = rowIndex;

            // Initialize the save button to be enabled from the start
            save_button.Enabled = true;

            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit User";
                save_button.Text = "Update";
                SetFields(this.rowIndex);
            }

            // Initialize label1 with an image
            try
            {
                InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing label image: " + ex.Message);
            }
        }

        private void InitializeComboBox()
        {
            // Populate the ComboBox with options
            Access_ComboBox.Items.Add("Restaurant");
            Access_ComboBox.Items.Add("General Store");
            Access_ComboBox.Items.Add("Hotel Management");
            Access_ComboBox.Items.Add("Admin");
            Access_ComboBox.SelectedIndexChanged += ConnectionTypeComboBox_SelectedIndexChanged;
        }

        private void ConnectionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ValidateInputs();
        }


        private void SaveData()
        {
            // Check for empty fields and show a message for each one
            if (Access_ComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select access type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(emailTB.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(usernameTB.Text))
            {
                MessageBox.Show("Email cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (!IsValidEmail(usernameTB.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(passwordTB.Text))
            {
                MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Check if email exists in the appropriate staff_details table based on the selected access type
            string email = usernameTB.Text;
            string selectedAccess = Access_ComboBox.SelectedItem.ToString();

            if (!IsEmailInStaffDetails(email, selectedAccess))
            {
                MessageBox.Show("Email not found in staff records. Please use the email submitted during staff registration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Determine access value based on the selected access type
            string role = "user"; // Default role
            string accessValue = selectedAccess switch
            {
                "Admin" => "Complete",
                "General Store" => "General",
                "Restaurant" => "Restaurant",
                "Hotel Management" => "Hotel" // Default to "user" if no match (for unexpected values)
            };

            // Change role to admin if admin access type is selected
            if (selectedAccess == "Admin")
            {
                role = "admin"; // Set role to admin

                // Insert into all databases for admin role
                foreach (string connStringName in new[] { "myconn", "myconnGS", "myconnHM" })
                {
                    InsertUserIntoDatabase(connStringName, role, accessValue);
                }

                MessageBox.Show("Admin user added to all databases successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Insert into only the selected database
                string connStringName = GetConnectionStringName();
                if (connStringName != null)
                {
                    InsertUserIntoDatabase(connStringName, role, accessValue);
                    MessageBox.Show("User added successfully to the selected database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error retrieving connection string.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ClearFields();
        }

        private bool IsEmailInStaffDetails(string email, string selectedAccess)
        {
            try
            {
                string connectionString = string.Empty;

                // Determine the connection string based on the access type
                if (selectedAccess == "Restaurant")
                {
                    connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                }
                else if (selectedAccess == "General Store")
                {
                    connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
                }
                // Commenting out the Hotel functionality for now
                // else if (selectedAccess == "Hotel Managment")
                // {
                //     connectionString = ConfigurationManager.ConnectionStrings["myconnHS"].ConnectionString;
                // }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to check if email exists in staff_details
                    string query = "SELECT COUNT(*) FROM staff_details WHERE email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        int emailCount = (int)command.ExecuteScalar();
                        return emailCount > 0; // If email count is greater than 0, email exists in the staff_details table
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void InsertUserIntoDatabase(string connStringName, string role, string accessValue)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert data into users table including Access
                    string query = "INSERT INTO users (username, password, email, role, Access) VALUES (@Username, @Password, @Email, @Role, @Access)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", emailTB.Text);
                        command.Parameters.AddWithValue("@Password", passwordTB.Text);
                        command.Parameters.AddWithValue("@Email", usernameTB.Text);
                        command.Parameters.AddWithValue("@Role", role); // Use the updated role
                        command.Parameters.AddWithValue("@Access", accessValue); // Always set to 'complete'

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected <= 0)
                        {
                            MessageBox.Show("User could not be added to " + connStringName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message);
            }
        }



        private bool IsValidEmail(string email)
        {
            // Basic email format validation
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private string GetConnectionStringName()
        {
            switch (Access_ComboBox.SelectedItem.ToString())
            {
                case "Restaurant":
                    return "myconn";
                case "General Store":
                    return "myconnGS";
                case "Hotel Management":
                    return "myconnHM";
                default:
                    return null;
            }
        }

        private void SetFields(int rowNo)
        {
            // Implementation to set fields if editing
        }

        private void ClearFields()
        {
            usernameTB.Text = "";
            emailTB.Text = "";
            passwordTB.Text = "";
            Access_ComboBox.SelectedIndex = -1; // Reset ComboBox
            //ValidateInputs(); // Re-evaluate button state
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}