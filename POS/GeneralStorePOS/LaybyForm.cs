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
    public partial class LaybyForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
        string connectionString;
        public LaybyForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            PopulateClientNames();
            DepositTB.KeyPress += NumericTextBox_KeyPress;
            DurationTB.KeyPress += NumericTextBox_KeyPress;
            TotalAmountTB.KeyPress += NumericTextBox_KeyPress;
            OutstandingAmountTB.ReadOnly = true;
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Layby Payment";
                save_button.Text = "Save";

            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

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
            // Load the image
            //string fullPath = Path.Combine(GetPro, RelativePath);
            //Image image = Image.FromFile(path);
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
        private void PopulateClientNames()
        {
            try
            {
                string query = "SELECT client_name FROM clients";

                // Use connectionString instead of connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientNameComboBox.Items.Add(reader["client_name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching client names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveData()
        {
            // Validate that all fields are filled
            if (string.IsNullOrWhiteSpace(ClientNameComboBox.Text))
            {
                MessageBox.Show("Please select a client name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(DepositTB.Text))
            {
                MessageBox.Show("Please enter the deposit amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(PaymentScheduleComboBox.Text))
            {
                MessageBox.Show("Please select a payment schedule.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(DurationTB.Text))
            {
                MessageBox.Show("Please enter the duration.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(TotalAmountTB.Text))
            {
                MessageBox.Show("Please enter the total amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse numeric fields and calculate Outstanding Amount
            decimal deposit = decimal.Parse(DepositTB.Text);
            decimal totalAmount = decimal.Parse(TotalAmountTB.Text);
            decimal outstandingAmount = totalAmount - deposit;

            // Display the calculated Outstanding Amount in the read-only textbox
            OutstandingAmountTB.Text = outstandingAmount.ToString("F2"); // Format as currency or 2 decimal places

            // Parse other fields
            int duration = int.Parse(DurationTB.Text);
            DateTime expiryDate = DateTime.Parse(ExpiryDateTB.Text);

            try
            {
                // Insert data into the database, including payment_date
                string query = "INSERT INTO layby (client_name, deposit, payment_schedule, duration, total_amount, outstanding_amount, expiry_date, payment_date, status) " +
                               "VALUES (@ClientName, @Deposit, @PaymentSchedule, @Duration, @TotalAmount, @OutstandingAmount, @ExpiryDate, @PaymentDate, @Status)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@ClientName", ClientNameComboBox.Text);
                        command.Parameters.AddWithValue("@Deposit", deposit);
                        command.Parameters.AddWithValue("@PaymentSchedule", PaymentScheduleComboBox.Text);
                        command.Parameters.AddWithValue("@Duration", duration);
                        command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        command.Parameters.AddWithValue("@OutstandingAmount", outstandingAmount);
                        command.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                        command.Parameters.AddWithValue("@PaymentDate", DateTime.Now); // Current date
                        command.Parameters.AddWithValue("@Status", "Pending");

                        // Execute the query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                // Inform the user and close the form
                MessageBox.Show("Layby record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Ensure sender is a System.Windows.Forms.TextBox
            if (sender is System.Windows.Forms.TextBox textBox)
            {
                // Allow digits, control characters (e.g., backspace), and a single decimal point
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true; // Reject the input
                }

                // Allow only one decimal point
                if (e.KeyChar == '.' && textBox.Text.Contains("."))
                {
                    e.Handled = true; // Reject the input
                }

                // Prevent leading decimal point (e.g., ".5" instead of "0.5")
                if (e.KeyChar == '.' && textBox.Text.Length == 0)
                {
                    e.Handled = true; // Reject the input
                }
            }
            else
            {
                e.Handled = true; // Reject if sender is not a TextBox
            }
        }

    }
}
