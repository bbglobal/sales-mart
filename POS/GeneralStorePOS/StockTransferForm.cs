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
    public partial class StockTransferForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        decimal originalStock;
        string connectionString;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
        public StockTransferForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Staff Details";
                save_button.Text = "Save";
                //SetFields(this.rowIndex);

            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
            SetSourceBranch();
            PopulateDestinationBranch();
            PopulateProductComboBox();
            ProductComboBox.SelectedIndexChanged += ProductComboBox_SelectedIndexChanged;
            TransferAmountTB.KeyPress += TransferStockTB_KeyPress;


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

        private void SaveData()
        {
            if (string.IsNullOrWhiteSpace(ProductComboBox.Text))
            {
                MessageBox.Show("Product selection is required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TransferAmountTB.Text))
            {
                MessageBox.Show("Transfer amount is required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(TransferAmountTB.Text, out decimal transferAmount) || transferAmount <= 0)
            {
                MessageBox.Show("Transfer amount is 0 or invalid. Please enter a valid amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string product = ProductComboBox.Text;
            string currentBranch = Session.BranchCode; // Current branch from session
            string sourceCode = currentBranch; // Source branch is the current branch
            string destinationCode = sourceCode == "PK728" ? "BR001" : "PK728"; // Set destination based on source
            string sourceConnectionName = sourceCode == "PK728" ? "myconnGS" : "myconnGSBR001";
            string destinationConnectionName = destinationCode == "PK728" ? "myconnGS" : "myconnGSBR001";

            decimal currentStock = originalStock; // Stock in the source branch
            decimal newStockSource = currentStock - transferAmount; // Updated source branch stock
            DateTime transferDate = DateTime.Now;
            string username = Session.Username;

            try
            {
                // Retrieve connection strings from configuration
                string sourceConnectionString = ConfigurationManager.ConnectionStrings[sourceConnectionName].ConnectionString;
                string destinationConnectionString = ConfigurationManager.ConnectionStrings[destinationConnectionName].ConnectionString;

                string sourceName = GetBranchName(sourceCode, sourceConnectionString);
                string destinationName = GetBranchName(destinationCode, destinationConnectionString);

                using (SqlConnection sourceConnection = new SqlConnection(sourceConnectionString))
                using (SqlConnection destinationConnection = new SqlConnection(destinationConnectionString))
                {
                    sourceConnection.Open();
                    destinationConnection.Open();

                    // 1. Insert into `transfers` table in both databases
                    string transferQuery = @"INSERT INTO transfers (source, destination, product, current_stock, transferred, transfer_date)
                                     VALUES (@source, @destination, @product, @currentStock, @transferred, @transferDate)";

                    using (SqlCommand sourceTransferCommand = new SqlCommand(transferQuery, sourceConnection))
                    using (SqlCommand destinationTransferCommand = new SqlCommand(transferQuery, destinationConnection))
                    {
                        foreach (var cmd in new[] { sourceTransferCommand, destinationTransferCommand })
                        {
                            cmd.Parameters.AddWithValue("@source", sourceName);
                            cmd.Parameters.AddWithValue("@destination", destinationName);
                            cmd.Parameters.AddWithValue("@product", product);
                            cmd.Parameters.AddWithValue("@currentStock", currentStock);
                            cmd.Parameters.AddWithValue("@transferred", transferAmount);
                            cmd.Parameters.AddWithValue("@transferDate", transferDate);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 2. Insert into `activity_log` in both databases
                    string activityLogQuery = @"INSERT INTO activity_log (action, description, time, username)
                                        VALUES (@action, @description, @time, @username)";
                    string action = "Transfer";
                    string description = $"Transferred {transferAmount} units of {product} from {sourceName} to {destinationName}";

                    using (SqlCommand sourceLogCommand = new SqlCommand(activityLogQuery, sourceConnection))
                    using (SqlCommand destinationLogCommand = new SqlCommand(activityLogQuery, destinationConnection))
                    {
                        foreach (var cmd in new[] { sourceLogCommand, destinationLogCommand })
                        {
                            cmd.Parameters.AddWithValue("@action", action);
                            cmd.Parameters.AddWithValue("@description", description);
                            cmd.Parameters.AddWithValue("@time", transferDate);
                            cmd.Parameters.AddWithValue("@username", username);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 3. Update stock in the source branch database
                    string updateSourceStockQuery = @"UPDATE items SET quantity = @newQuantity WHERE item_name = @product";
                    using (SqlCommand updateSourceStockCommand = new SqlCommand(updateSourceStockQuery, sourceConnection))
                    {
                        updateSourceStockCommand.Parameters.AddWithValue("@newQuantity", newStockSource);
                        updateSourceStockCommand.Parameters.AddWithValue("@product", product);

                        updateSourceStockCommand.ExecuteNonQuery();
                    }

                    // 4. Update stock in the destination branch database
                    string updateDestinationStockQuery = @"UPDATE items SET quantity = quantity + @transferred WHERE item_name = @product";
                    using (SqlCommand updateDestinationStockCommand = new SqlCommand(updateDestinationStockQuery, destinationConnection))
                    {
                        updateDestinationStockCommand.Parameters.AddWithValue("@transferred", transferAmount);
                        updateDestinationStockCommand.Parameters.AddWithValue("@product", product);

                        updateDestinationStockCommand.ExecuteNonQuery();
                    }

                    // Success message
                    MessageBox.Show("Transfer successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields and reset
                    TransferAmountTB.Clear();
                    ProductComboBox.SelectedIndex = -1;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing transfer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to get branch name by branch code
        private string GetBranchName(string branchCode, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT branch_name FROM branches WHERE branch_code = @branchCode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@branchCode", branchCode);
                    object result = command.ExecuteScalar();
                    return result?.ToString() ?? branchCode; // Fallback to code if name is not found
                }
            }
        }



        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SetSourceBranch()
        {
            if (!string.IsNullOrEmpty(Session.BranchCode))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT branch_name FROM branches WHERE branch_code = @BranchCode";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@BranchCode", Session.BranchCode);
                            object result = command.ExecuteScalar();

                            if (result != null)
                            {
                                SourceBranchComboBox.Text = result.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Branch not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching the branch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No branch code found in the session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PopulateDestinationBranch()
        {
            if (!string.IsNullOrEmpty(Session.BranchCode))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT branch_code, branch_name FROM branches WHERE branch_code != @BranchCode";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@BranchCode", Session.BranchCode);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                DestinationBranchComboBox.Items.Clear();

                                while (reader.Read())
                                {
                                    DestinationBranchComboBox.Items.Add(reader["branch_name"].ToString());
                                }

                                if (DestinationBranchComboBox.Items.Count > 0)
                                {
                                    DestinationBranchComboBox.SelectedIndex = 0;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching the branch names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No branch code found in the session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PopulateProductComboBox()
        {
            string query = "SELECT item_name FROM items";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ProductComboBox.Items.Clear();
                            while (reader.Read())
                            {
                                ProductComboBox.Items.Add(reader["item_name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating product combo box: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProductComboBox.SelectedItem == null)
                return;

            string selectedItemName = ProductComboBox.SelectedItem.ToString();
            string query = "SELECT quantity FROM items WHERE item_name = @itemName";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@itemName", selectedItemName);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            originalStock = Convert.ToInt32(result);
                            CurrentStockTextBox.Text = originalStock.ToString(); // Update the TextBox
                        }
                        else
                        {
                            originalStock = 0;
                            CurrentStockTextBox.Text = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching item quantity: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TransferStockTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TransferAmountTB_TextChanged(object sender, EventArgs e)
        {
            // Ensure TransferAmountTB is not empty
            if (string.IsNullOrWhiteSpace(TransferAmountTB.Text))
            {
                CurrentStockTextBox.Text = originalStock.ToString();
                return;
            }

            // Parse the transfer amount
            if (decimal.TryParse(TransferAmountTB.Text, out decimal transferAmount))
            {
                // Check if transfer amount exceeds current stock
                if (transferAmount > originalStock)
                {
                    MessageBox.Show("Enter a valid number. Transfer amount cannot exceed current stock.",
                                    "Invalid Input",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    // Clear the TransferAmountTB and reset current stock display
                    TransferAmountTB.Clear();
                    CurrentStockTextBox.Text = originalStock.ToString();
                    return;
                }

                // Calculate the new stock dynamically
                decimal updatedStock = originalStock - transferAmount;

                // Update current stock, ensuring it doesn't go negative
                CurrentStockTextBox.Text = updatedStock < 0 ? "0" : updatedStock.ToString();
            }
            else
            {
                // If parsing fails, reset TransferAmountTB and currentstockTB
                MessageBox.Show("Please enter a valid numeric value.",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TransferAmountTB.Clear();
                CurrentStockTextBox.Text = originalStock.ToString();
            }
        }


    }
}
