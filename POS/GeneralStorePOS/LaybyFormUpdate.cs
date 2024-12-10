using POS.Properties;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class LaybyFormUpdate : Form
    {
        private int laybyNo; // Layby number passed when form is opened
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
        string connectionString;

        public LaybyFormUpdate(int laybyNo)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            this.laybyNo = laybyNo;

            if (laybyNo != 0)
            {
                Title_label.Text = "Layby Payment Update";
                save_button.Text = "Save";
                FetchLaybyDetails(laybyNo);
            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
            CurrentDepositTB.KeyPress += NumericTextBox_KeyPress;
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
        private void save_button_Click(object sender, EventArgs e)
        {
            // Retrieve values from the textboxes
            decimal currentDeposit = decimal.Parse(CurrentDepositTB.Text);
            decimal outstandingAmount = decimal.Parse(OutstandingAmountTB.Text);
            decimal deposit = decimal.Parse(DepositTB.Text);

            // Check if the CurrentDeposit is greater than Outstanding Amount
            if (currentDeposit > outstandingAmount)
            {
                // Display warning message if CurrentDeposit is greater than Outstanding Amount
                MessageBox.Show("Deposit cannot be greater than Outstanding balance.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Subtract CurrentDeposit from Outstanding Amount and add it to Deposit
                outstandingAmount -= currentDeposit;
                deposit += currentDeposit;

                // Update the textboxes with the new values
                DepositTB.Text = deposit.ToString("F2"); // Format as currency or 2 decimal places
                OutstandingAmountTB.Text = outstandingAmount.ToString("F2");

                // Prepare the query to update the deposit and outstanding amount
                string query = "UPDATE layby SET deposit = @Deposit, outstanding_amount = @OutstandingAmount WHERE layby_no = @LaybyNo";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters for Deposit, Outstanding Amount, and Layby No
                            command.Parameters.AddWithValue("@Deposit", deposit);
                            command.Parameters.AddWithValue("@OutstandingAmount", outstandingAmount);
                            command.Parameters.AddWithValue("@LaybyNo", laybyNo); // Assuming rowIndex holds the current layby number

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    // Check if the Outstanding Amount is now zero, indicating the payment is complete
                    if (outstandingAmount == 0)
                    {
                        // Set the status to "Complete"
                        string statusQuery = "UPDATE layby SET status = @Status WHERE layby_no = @LaybyNo";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand command = new SqlCommand(statusQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Status", "Complete");
                                command.Parameters.AddWithValue("@LaybyNo", laybyNo); // Assuming rowIndex holds the current layby number

                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }

                        // Log the successful payment and status update action into the activity log
                        LogActivity("Layby payment successfully completed and status updated to 'Complete'.");

                        // Display success message
                        MessageBox.Show("Payment has been completed and status updated to 'Complete'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Log the successful payment action into the activity log
                        LogActivity("Layby payment successfully saved");

                        // Display success message
                        MessageBox.Show("Layby payment successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Close the form after saving
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating layby details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void LogActivity(string actionDescription)
        {
            // Get the current username from the session (assuming Session.Username exists)
            string username = Session.Username; // Replace with actual session management if needed
            DateTime currentTime = DateTime.Now;
            string action = "Layby Payment"; // Example action, can be dynamic based on the context

            // Define the query to insert the log into the activity_log table
            string query = @"
        INSERT INTO activity_log (time, action, description, username)
        VALUES (@Time, @Action, @Description, @Username)";

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Time", currentTime);
                        command.Parameters.AddWithValue("@Action", action);
                        command.Parameters.AddWithValue("@Description", actionDescription);
                        command.Parameters.AddWithValue("@Username", username);

                        // Open the connection and execute the query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error logging activity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void FetchLaybyDetails(int laybyNo)
        {
            try
            {
                string query = "SELECT client_name, total_amount, deposit, total_amount - deposit AS outstanding_amount " +
                               "FROM layby WHERE layby_no = @LaybyNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LaybyNo", laybyNo);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ClientNameTB.Text = reader["client_name"].ToString();
                            TotalAmountTB.Text = Convert.ToDecimal(reader["total_amount"]).ToString("F2");
                            DepositTB.Text = Convert.ToDecimal(reader["deposit"]).ToString("F2");
                            OutstandingAmountTB.Text = Convert.ToDecimal(reader["outstanding_amount"]).ToString("F2");
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching layby details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' && textBox.Text.Contains("."))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' && textBox.Text.Length == 0)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
