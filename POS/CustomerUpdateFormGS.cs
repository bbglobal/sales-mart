using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS
{
    public partial class CustomerUpdateFormGS : Form
    {
        private int rowIndex;
        private string customerId;
        SqlConnection connection;

        public CustomerUpdateFormGS(int rowIndex = -1)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            this.PhoneTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_TextBox_KeyPress);
            this.CreditTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Credit_TextBox_KeyPress);
            this.PointsTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Points_TextBox_KeyPress);
            this.rowIndex = rowIndex;
        }

        // Constructor for updating customer information
        public CustomerUpdateFormGS(string customerId, string customerName, string phoneNumber, string email, string address, DateTime lastPurchaseDate, string credit, string points, int rowIndex)
        {
            InitializeComponent();

            // Set the fields with the current data
            this.customerId = customerId;
            idtb.Text = customerId;  // Make the customer ID textbox readonly
            idtb.ReadOnly = true;
            CustomerNameTB.Text = customerName;
            PhoneTB.Text = phoneNumber;
            emailTB.Text = email;
            AddressTB.Text = address;
            LastPurchaseTB.Value = lastPurchaseDate;  // Use DateTimePicker control for last purchase date
            CreditTB.Text = credit;
            PointsTB.Text = points;

            // Save the row index for later use (e.g., updating the correct record)
            this.rowIndex = rowIndex;

            // Initialize the database connection
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"]?.ConnectionString;

            // Check if the connection string is null or empty
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Connection string is missing or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            connection = new SqlConnection(connectionString);
        }

        // Restrict numeric input for phone, credit, and points textboxes
        private void Phone_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Credit_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }

        private void Points_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Update data in the database
        private void SaveData()
        {
            // Get the values from the textboxes
            string customerName = CustomerNameTB.Text;
            string phone = PhoneTB.Text;
            string email = emailTB.Text;
            string address = AddressTB.Text;
            string credit = CreditTB.Text;
            string points = PointsTB.Text;
            DateTime lastPurchaseDate = LastPurchaseTB.Value;  // Use DateTimePicker value

            // Check if the selected last purchase date is in the future
            if (lastPurchaseDate > DateTime.Now)
            {
                MessageBox.Show("Please select a valid date. The last purchase date cannot be in the future.",
                                "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure all fields are not empty
            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(credit) || string.IsNullOrWhiteSpace(points))
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure email format is valid
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string currentUsername = Session.Username;  // Replace with actual session username retrieval logic
            string actionType = "Update Customer";
            string description = $"Updated Customer - {customerName}";
            DateTime currentTime = DateTime.Now;

            try
            {
                // Ensure connection is open
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                // Update query for the customers table (including LastPurchaseDate)
                string query = "UPDATE customers SET customer_name = @CustomerName, phone_number = @Phone, email = @Email, " +
                               "address = @Address, credit = @Credit, points = @Points, last_purchase_date = @LastPurchaseDate " +
                               "WHERE customer_id = @CustomerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for the SQL query
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Credit", credit);
                    command.Parameters.AddWithValue("@Points", points);
                    command.Parameters.AddWithValue("@LastPurchaseDate", lastPurchaseDate);

                    // Execute the update query
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Log the action in the activity_log table
                        string logQuery = "INSERT INTO activity_log (action, description, time, username) " +
                                          "VALUES (@Action, @Description, @Time, @Username)";

                        using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                        {
                            logCommand.Parameters.AddWithValue("@Action", actionType);
                            logCommand.Parameters.AddWithValue("@Description", description);
                            logCommand.Parameters.AddWithValue("@Time", currentTime);
                            logCommand.Parameters.AddWithValue("@Username", currentUsername);

                            logCommand.ExecuteNonQuery();
                        }

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }


        // Email validation method
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            return regex.IsMatch(email);
        }

        // Cancel button click
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Save button click
        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}
