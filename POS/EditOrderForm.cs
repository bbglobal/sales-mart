using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace POS
{


    public partial class EditOrderForm : Form
    {
        Image DeleteImage;
        Image AddImage;
        private readonly string _billId;
        SqlConnection connection;
        private decimal FormLoadDifference = 0;
        string connectionString;

        public EditOrderForm(string billId)
        {
            InitializeComponent();
            _billId = billId;

            // Load images
            ImageEditDelLoad();
            // Initialize the DataGridView columns
            AddColumnsToOrderItemsDataGrid();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            LoadOrderItems();
            LoadBillDetails(); InitializeDatabaseConnection();
            // Calculate the initial difference between TotalAmountLabel and NetAmountLabel
            decimal initialTotalAmount = Convert.ToDecimal(TotalAmountLabel.Text.Replace("$", "").Replace(",", ""));
            decimal initialNetAmount = Convert.ToDecimal(NetAmountLabel.Text.Replace("$", "").Replace(",", ""));

            FormLoadDifference = initialTotalAmount - initialNetAmount;
        }


        private void InitializeDatabaseConnection()
        {
            if (Session.SelectedModule == "Restaurant POS")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            else if (Session.SelectedModule == "Hotel Management")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
        }

        private void LoadOrderItems()
        {
            OrderItemsDataGrid.Rows.Clear();
            string billListQuery = "SELECT items FROM bill_list WHERE bill_id = @BillId";
            string productPriceQuery = "SELECT product_price FROM products WHERE product_name = @ItemName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Fetch JSON data
                string jsonItems = string.Empty;
                using (SqlCommand billListCmd = new SqlCommand(billListQuery, connection))
                {
                    billListCmd.Parameters.AddWithValue("@BillId", _billId);
                    jsonItems = billListCmd.ExecuteScalar()?.ToString();
                }

                if (!string.IsNullOrEmpty(jsonItems))
                {
                    // Deserialize JSON
                    var itemsList = jsonItems
                        .Trim('[', ']')
                        .Split(',')
                        .Select(item =>
                        {
                            var parts = item.Replace("\"", "").Split('-');
                            return new { Name = parts[0], Quantity = int.Parse(parts[1]) };
                        }).ToList();

                    foreach (var item in itemsList)
                    {
                        decimal productPrice = 0;

                        // Fetch product price
                        using (SqlCommand priceCmd = new SqlCommand(productPriceQuery, connection))
                        {
                            priceCmd.Parameters.AddWithValue("@ItemName", item.Name);
                            object priceResult = priceCmd.ExecuteScalar();
                            productPrice = priceResult != null ? Convert.ToDecimal(priceResult) : 0;
                        }

                        // Calculate total price (price * quantity)
                        decimal totalPrice = productPrice;

                        // Add the row to the DataGridView
                        OrderItemsDataGrid.Rows.Add(null, item.Name, item.Quantity, totalPrice);
                    }
                }
            }

            // Update SR# column
            for (int i = 0; i < OrderItemsDataGrid.Rows.Count; i++)
            {
                OrderItemsDataGrid.Rows[i].Cells["SR#"].Value = (i + 1).ToString();
            }
        }


        private void LoadBillDetails()
        {
            // Queries to fetch bill details
            string billDetailsQuery = "SELECT customer, total_amount, net_total_amount FROM bill_list WHERE bill_id = @BillId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(billDetailsQuery, connection))
                {
                    command.Parameters.AddWithValue("@BillId", _billId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate labels
                            BillIDLabel.Text = _billId;
                            CustomerNameLabel.Text = reader["customer"]?.ToString() ?? "Unknown";
                            TotalAmountLabel.Text = reader["total_amount"] != DBNull.Value
                                ? Convert.ToDecimal(reader["total_amount"]).ToString("C")
                                : "0.00";
                            NetAmountLabel.Text = reader["net_total_amount"] != DBNull.Value
                                ? Convert.ToDecimal(reader["net_total_amount"]).ToString("C")
                                : "0.00";
                        }
                    }
                }
            }
        }

        private void ImageEditDelLoad()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "POS.Resources.Add.PNG";
            string resourceName1 = "POS.Resources.delete.png";

            // Load Edit image
            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    AddImage = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }

            // Load Delete image
            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName1))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    DeleteImage = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Delete image resource.");
                }
            }
        }

        private void AddColumnsToOrderItemsDataGrid()
        {
            OrderItemsDataGrid.Columns.Clear();

            // SR# column
            DataGridViewTextBoxColumn srColumn = new DataGridViewTextBoxColumn
            {
                Name = "SR#",
                HeaderText = "SR#",
                ReadOnly = true
            };
            OrderItemsDataGrid.Columns.Add(srColumn);

            // Items column
            DataGridViewTextBoxColumn itemsColumn = new DataGridViewTextBoxColumn
            {
                Name = "Items",
                HeaderText = "Items",
                ReadOnly = true
            };
            OrderItemsDataGrid.Columns.Add(itemsColumn);

            // Quantity column
            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity",
                ReadOnly = true
            };
            OrderItemsDataGrid.Columns.Add(quantityColumn);

            // Price column
            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "Price",
                ReadOnly = true
            };
            OrderItemsDataGrid.Columns.Add(priceColumn);

            // Add column (Image)
            DataGridViewImageColumn addColumn = new DataGridViewImageColumn
            {
                Name = "Add",
                HeaderText = "Add",
                Image = ResizeImage(AddImage, 15, 15), // Using the same image for now, replace with "Add" image later
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ToolTipText = "Add this item"
            };

            OrderItemsDataGrid.Columns.Add(addColumn);

            // Remove column (Image)
            DataGridViewImageColumn removeColumn = new DataGridViewImageColumn
            {
                Name = "Remove",
                HeaderText = "Remove",
                Image = ResizeImage(DeleteImage, 15, 15), // Using the same image for now
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ToolTipText = "Delete this item"
            };

            OrderItemsDataGrid.Columns.Add(removeColumn);

            // Add RowPostPaint for SR# update
            OrderItemsDataGrid.RowPostPaint += (s, e) =>
            {
                var grid = s as DataGridView;
                if (grid?.Rows[e.RowIndex] != null)
                {
                    grid.Rows[e.RowIndex].Cells["SR#"].Value = (e.RowIndex + 1).ToString();
                }
            };
        }


        private Image ResizeImage(Image img, int newWidth, int newHeight)
        {
            Bitmap resizedImg = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImg))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, newWidth, newHeight);
            }
            return resizedImg;
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrderCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel this order? This action cannot be undone.",
                                                  "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int billId;
                if (int.TryParse(BillIDLabel.Text, out billId))
                {
                    UpdateOrderStatus(billId);
                    LogActivity("Order Cancellation", $"Order with Bill ID {billId} has been cancelled.", Session.Username);
                    MessageBox.Show("Order cancelled successfully.", "Cancellation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Bill ID format. Please ensure the Bill ID is a valid number.",
                                    "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }

        private void UpdateOrderStatus(int billId)
        {
            string query = "UPDATE bill_list SET status = @Status WHERE bill_id = @BillId";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Status", "Cancelled");
                command.Parameters.AddWithValue("@BillId", billId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating order status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method to log the cancellation event to the activity_log table
        private void LogActivity(string action, string description, string username)
        {
            string query = "INSERT INTO activity_log (time, action, description, username) VALUES (@Time, @Action, @Description, @Username)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Time", DateTime.Now);
                command.Parameters.AddWithValue("@Action", action);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error logging activity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse TotalAmountLabel and NetAmountLabel values
                decimal totalAmount = decimal.Parse(TotalAmountLabel.Text.Replace("$", "").Replace(",", ""));
                decimal netTotal = decimal.Parse(NetAmountLabel.Text.Replace("$", "").Replace(",", ""));
                int billId = int.Parse(BillIDLabel.Text);

                // Prepare the list of items to update
                List<string> itemsToUpdate = new List<string>();
                bool allItemsZero = true; // Flag to check if all items have quantity 0

                foreach (DataGridViewRow row in OrderItemsDataGrid.Rows)
                {
                    if (row.Cells["Items"].Value != null && row.Cells["Quantity"].Value != null)
                    {
                        string itemName = row.Cells["Items"].Value.ToString();
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                        // Format the item as "ItemName-Quantity"
                        string formattedItem = $"{itemName}-{quantity}";
                        itemsToUpdate.Add(formattedItem);

                        // If any item has quantity greater than 0, set the flag to false
                        if (quantity > 0)
                        {
                            allItemsZero = false;
                        }
                    }
                }

                // If all quantities are 0, confirm cancellation
                if (allItemsZero && totalAmount == 0)
                {
                    DialogResult result = MessageBox.Show(
                        "All items have been removed, and the total amount is 0. Do you want to cancel the order?",
                        "Cancel Order",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        // Update the status to Cancelled and set total_amount and net_total_amount to 0
                        string cancelQuery = @"
                    UPDATE bill_list 
                    SET status = 'Cancelled', 
                        total_amount = 0, 
                        net_total_amount = 0 
                    WHERE bill_id = @billId";

                        using (SqlCommand cmd = new SqlCommand(cancelQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@billId", billId);

                            await connection.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                            connection.Close();
                        }

                        MessageBox.Show("Order has been cancelled and the amounts have been reset to 0.", "Order Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Close the form after cancellation
                        this.Close();
                        return;
                    }
                    else
                    {
                        // If user chooses not to cancel, exit the method without changes
                        return;
                    }
                }

                // Update the bill_list table with the items, total amount, and net total
                string updateQuery = @"
            UPDATE bill_list
            SET total_amount = @totalAmount,
                net_total_amount = @netTotal,
                items = @items
            WHERE bill_id = @billId";

                // Serialize items to JSON format
                string serializedItems = System.Text.Json.JsonSerializer.Serialize(itemsToUpdate);

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@billId", billId);
                    cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@netTotal", netTotal);
                    cmd.Parameters.AddWithValue("@items", serializedItems);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }

                MessageBox.Show("Order has been updated successfully.", "Order Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the form after update
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OrderItemsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is on a valid row and not the header row
            if (e.RowIndex >= 0)
            {
                // Check if the clicked column is "Add"
                if (OrderItemsDataGrid.Columns[e.ColumnIndex].HeaderText == "Add")
                {
                    // Increment the quantity
                    int currentQuantity = Convert.ToInt32(OrderItemsDataGrid.Rows[e.RowIndex].Cells["Quantity"].Value ?? 0);
                    OrderItemsDataGrid.Rows[e.RowIndex].Cells["Quantity"].Value = currentQuantity + 1;
                }
                // Check if the clicked column is "Remove"
                else if (OrderItemsDataGrid.Columns[e.ColumnIndex].HeaderText == "Remove")
                {
                    // Decrement the quantity but ensure it doesn't go below 0
                    int currentQuantity = Convert.ToInt32(OrderItemsDataGrid.Rows[e.RowIndex].Cells["Quantity"].Value ?? 0);
                    if (currentQuantity > 0)
                    {
                        OrderItemsDataGrid.Rows[e.RowIndex].Cells["Quantity"].Value = currentQuantity - 1;
                    }
                }

                // Update TotalAmountLabel and NetTotalLabel after any quantity change
                UpdateTotals();
            }
        }
        private async void UpdateTotals()
        {
            decimal totalAmount = 0;
            decimal discount = 0; // Discount to be fetched from the database
            decimal netTotal = 0;

            // Calculate the total amount by summing up (Quantity * Price) for each row
            foreach (DataGridViewRow row in OrderItemsDataGrid.Rows)
            {
                if (row.Cells["Quantity"].Value != null && row.Cells["Price"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                    totalAmount += quantity * price; // Price is per unit
                }
            }

            // Update the TotalAmountLabel
            TotalAmountLabel.Text = totalAmount.ToString("C");

            // Fetch the discount based on bill_id
            int billId = int.Parse(BillIDLabel.Text);
            string query = "SELECT discount FROM bill_list WHERE bill_id = @billId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@billId", billId);

                await connection.OpenAsync();
                var discountObj = await cmd.ExecuteScalarAsync();
                connection.Close();

                if (discountObj != null && decimal.TryParse(discountObj.ToString(), out decimal discountValue))
                {
                    discount = discountValue;
                }
            }

            // Calculate Net Total
            if (discount > 0)
            {
                // Apply discount as a percentage of the total amount
                netTotal = totalAmount - (totalAmount * (discount / 100));
            }
            else
            {
                if (FormLoadDifference == 0)
                {
                    // When difference is 0, NetTotal and TotalAmount should match
                    netTotal = totalAmount;
                }
                else
                {
                    // Subtract the difference only once at the start
                    if (NetAmountLabel.Text == TotalAmountLabel.Text)
                    {
                        netTotal = totalAmount - FormLoadDifference;
                    }
                    else
                    {
                        netTotal = totalAmount - FormLoadDifference;  // Increment both properly beyond initial
                    }
                }
            }

            // Update the NetTotalLabel
            NetAmountLabel.Text = netTotal.ToString("C");
        }




    }
}
