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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS
{
    public partial class StaffForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));

        public StaffForm(int rowIndex = -1)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            SetTypeComboBox();
            this.rowIndex = rowIndex;

            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Staff Details";
                save_button.Text = "Save";
                SetFields(this.rowIndex);

                // Make email read-only and update the label
                emailTB.ReadOnly = true;
                label7.Text = "             Email cannot be changed.";
            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
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

        private void SetTypeComboBox()
        {
            try
            {
                string query = "select types from staff_category";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Type_ComboBox.Items.Add(reader["types"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }
            finally
            {
                connection.Close();
            }
        }

        private void SaveData()
        {
            // Check if all fields are filled
            if (string.IsNullOrWhiteSpace(StaffName_TextBox.Text) || string.IsNullOrWhiteSpace(Phone_TextBox.Text) ||
                string.IsNullOrWhiteSpace(Address_TextBox.Text) || Type_ComboBox.SelectedItem == null ||
                Status_ComboBox.SelectedItem == null || Shift_ComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(emailTB.Text))
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Email validation
            string email = emailTB.Text;
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Initialize actionType and description with default values
            string actionType = "None";
            string description = "No action taken";
            DateTime currentTime = DateTime.Now;
            string currentUsername = Session.Username; // Replace with session username retrieval

            try
            {
                // Open connection if not already open
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                // Check if adding new staff (rowIndex == -1) or updating existing staff
                if (rowIndex == -1)
                {
                    // Insert query for new staff entry
                    string insertQuery = "INSERT INTO staff_details (shifts, staff_name, type, phone_number, address, status, email) " +
                                         "VALUES (@Shift, @StaffName, @Type, @Phone, @Address, @Status, @Email)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StaffName", StaffName_TextBox.Text);
                        command.Parameters.AddWithValue("@Type", Type_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Phone", Phone_TextBox.Text);
                        command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Shift", Shift_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Staff Details Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Log activity for adding staff
                            actionType = "Add Staff";
                            description = $"Added new staff: {StaffName_TextBox.Text}";
                        }
                    }
                }
                else
                {
                    // Update query for existing staff entry
                    string updateQuery = "UPDATE staff_details SET shifts = @Shift, staff_name = @StaffName, type = @Type, " +
                                         "phone_number = @Phone, address = @Address, status = @Status, email = @Email WHERE id = @Id";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StaffName", StaffName_TextBox.Text);
                        command.Parameters.AddWithValue("@Type", Type_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Phone", Phone_TextBox.Text);
                        command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Shift", Shift_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Staff Details Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Log activity for updating staff
                            actionType = "Update Staff";
                            description = $"Updated staff: {StaffName_TextBox.Text}";
                        }
                    }
                }

                // Insert the log entry
                string logQuery = "INSERT INTO activity_log (action, description, time, username) VALUES (@Action, @Description, @Time, @Username)";
                using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                {
                    logCommand.Parameters.AddWithValue("@Action", actionType);
                    logCommand.Parameters.AddWithValue("@Description", description);
                    logCommand.Parameters.AddWithValue("@Time", currentTime);
                    logCommand.Parameters.AddWithValue("@Username", currentUsername);

                    logCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }


        // Email validation method
        private bool IsValidEmail(string email)
        {
            // Regular expression to check email format
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private void SetFields(int rowNo)
        {
            try
            {
                connection.Open();
                string query = $"select * from staff_details where id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StaffName_TextBox.Text = (string)reader["staff_name"];
                        Type_ComboBox.Text = (string)reader["type"];
                        Phone_TextBox.Text = Convert.ToInt32(reader["phone_number"]).ToString();
                        Address_TextBox.Text = (string)reader["address"];
                        Status_ComboBox.Text = (string)reader["status"];
                        Shift_ComboBox.Text = (string)reader["shifts"];
                        emailTB.Text = (string)reader["email"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void InitializeLabel(Label label, Image image, int newWidth, int newHeight)
        {
            // Load the image
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

        private void ClearFields()
        {
            StaffName_TextBox.Text = "";
            Type_ComboBox.Text = "";
            Phone_TextBox.Text = "";
            Address_TextBox.Text = "";
            Status_ComboBox.Text = "";
            Shift_ComboBox.Text = "";
            emailTB.Text = "";
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Phone_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
