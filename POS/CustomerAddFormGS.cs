using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS
{
    public partial class CustomerAddFormGS : Form
    {
        private int rowIndex;
        SqlConnection connection;

        public CustomerAddFormGS(int rowIndex = -1)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            this.PhoneTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_TextBox_KeyPress);
            this.CreditTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Credit_TextBox_KeyPress);
            this.PointsTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Points_TextBox_KeyPress);
            this.rowIndex = rowIndex;
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
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

        // Insert data into the database
        private void SaveData()
        {
            // Get the values from the textboxes
            string customerName = CustomerNameTB.Text;
            string phone = PhoneTB.Text;
            string email = emailTB.Text;
            string address = AddressTB.Text;
            string credit = CreditTB.Text;
            string points = PointsTB.Text;

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
            string actionType = "Add Customer";
            string description = $"Added Customer - {customerName}";
            DateTime currentTime = DateTime.Now;

            try
            {
                connection.Open();

                // Insert query for the customers table with last_purchase_date set to 1/1/2024
                string query = "INSERT INTO customers (customer_name, phone_number, email, address, credit, points, last_purchase_date) " +
                               "VALUES (@CustomerName, @Phone, @Email, @Address, @Credit, @Points, @LastPurchaseDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for the SQL query
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Credit", credit);
                    command.Parameters.AddWithValue("@Points", points);
                    command.Parameters.AddWithValue("@LastPurchaseDate", new DateTime(2024, 1, 1));

                    // Execute the query
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                        // Clear fields after saving
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save customer data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Clear the fields after saving
        private void ClearFields()
        {
            CustomerNameTB.Text = "";
            PhoneTB.Text = "";
            emailTB.Text = "";
            AddressTB.Text = "";
            CreditTB.Text = "";
            PointsTB.Text = "";
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
            // Empty method as functionality is removed
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Empty method as functionality is removed
        }
    }
}
