using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace POS
{
    public partial class AddCustomerInfoGS : Form
    {
        private string json;
        private string insertStatus = "";
        private decimal total = 0;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));

        public AddCustomerInfoGS(string json, decimal total)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            this.json = json;
            this.total = total;
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

            // Restrict Phone_TextBox to numeric input only
            Phone_TextBox.KeyPress += Phone_TextBox_KeyPress;
        }

        public AddCustomerInfoGS()
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
            if (Session.BranchCode == "PK728")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }

            else if (Session.BranchCode == "BR001")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;
            }
        }

        private void SaveData()
        {
            // Check for empty fields and confirm if user wants to proceed without customer details
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) || string.IsNullOrWhiteSpace(Phone_TextBox.Text))
            {
                string missingField = string.IsNullOrWhiteSpace(Name_TextBox.Text) ? "Name" : "Phone Number";
                var result = MessageBox.Show($"{missingField} is not provided. Do you want to proceed without it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return; // Stop if the user chooses not to proceed
                }
                else if (result == DialogResult.Yes)
                {
                    // Proceed without customer details (i.e., do not insert Name and Phone)
                    Name_TextBox.Text = string.Empty;  // Optional: Clear the Name field
                    Phone_TextBox.Text = string.Empty; // Optional: Clear the Phone field
                }
                // If Yes, proceed with saving without customer details.
            }

            try
            {
                // Open the database connection
                connection.Open();

                // Start a SQL transaction to ensure atomicity (both insert and stock deduction are done together)
                SqlTransaction transaction = connection.BeginTransaction();

                // Deserialize the JSON data to get item names and quantities
                List<string> columnValues = JsonConvert.DeserializeObject<List<string>>(json);

                // Check stock availability for all items
                foreach (var item in columnValues)
                {
                    string[] itemDetails = item.Split('-'); // ItemName-Quantity
                    string itemName = itemDetails[0];
                    int orderedQuantity = Convert.ToInt32(itemDetails[1]);

                    // Check the stock for the item
                    SqlCommand checkStockCommand = new SqlCommand("SELECT quantity FROM items WHERE item_name = @ItemName", connection, transaction);
                    checkStockCommand.Parameters.AddWithValue("@ItemName", itemName);
                    int availableStock = Convert.ToInt32(checkStockCommand.ExecuteScalar());

                    // If stock is less than ordered quantity, show a message and rollback the transaction
                    if (availableStock < orderedQuantity)
                    {
                        throw new Exception($"Item '{itemName}' is out of stock. Available: {availableStock}, Ordered: {orderedQuantity}");
                    }
                }

                // Deduct stock for all items after confirming stock is available
                foreach (var item in columnValues)
                {
                    string[] itemDetails = item.Split('-'); // ItemName-Quantity
                    string itemName = itemDetails[0];
                    int orderedQuantity = Convert.ToInt32(itemDetails[1]);

                    // Deduct the ordered quantity from the stock
                    SqlCommand updateStockCommand = new SqlCommand("UPDATE items SET quantity = quantity - @OrderedQty WHERE item_name = @ItemName", connection, transaction);
                    updateStockCommand.Parameters.AddWithValue("@OrderedQty", orderedQuantity);
                    updateStockCommand.Parameters.AddWithValue("@ItemName", itemName);
                    updateStockCommand.ExecuteNonQuery();
                }

                // Prepare the SQL command for inserting data into bill_list
                SqlCommand command;
                if (string.IsNullOrWhiteSpace(Name_TextBox.Text) || string.IsNullOrWhiteSpace(Phone_TextBox.Text))
                {
                    // Insert without customer details (NULL for Name and Phone)
                    command = new SqlCommand("INSERT INTO bill_list (items, customer, phone, date, type, status, total_amount, net_total_amount) VALUES (@Items, NULL, NULL, @Date, @Type, @Status, @Total, @NetTotal)", connection, transaction);
                }
                else
                {
                    // Insert with customer details
                    command = new SqlCommand("INSERT INTO bill_list (items, customer, phone, date, type, status, total_amount, net_total_amount) VALUES (@Items, @Name, @Phone, @Date, @Type, @Status, @Total, @NetTotal)", connection, transaction);
                    command.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(Phone_TextBox.Text) ? (object)DBNull.Value : Convert.ToInt32(Phone_TextBox.Text));
                }

                // Add the rest of the parameters for the order
                command.Parameters.AddWithValue("@Items", json);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@Type", "Take Away");
                command.Parameters.AddWithValue("@Status", "Incomplete");
                command.Parameters.AddWithValue("@Total", total);
                command.Parameters.AddWithValue("@NetTotal", total);

                // Execute the insert query
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Commit the transaction after both insert and stock deduction are successful
                    transaction.Commit();

                    MessageBox.Show("Order Saved Successfully");
                    insertStatus = "Inserted";
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any error occurs
                connection.Close();
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                // Close the connection to the database
                connection.Close();
                this.Close(); // Close the form after operation
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
