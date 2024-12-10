using Newtonsoft.Json;
using System;
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

namespace POS
{
    public partial class GSDeliveryForm : Form
    {
        private string json;
        private string insertStatus = "";
        private decimal total = 0;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        public GSDeliveryForm(string json,decimal total)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.json = json;
            this.total = total;
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

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
                connection = new SqlConnection(connectionString);
            }
        }


        private void SaveData()
        {
            if (Name_TextBox.Text == "" && Phone_TextBox.Text == "" && Address_TextBox.Text == "")
            {
                MessageBox.Show("Please fill the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                // Deserialize the JSON data to get item names and quantities
                List<string> columnValues = JsonConvert.DeserializeObject<List<string>>(json);

                // Open the database connection
                connection.Open();

                // Start a transaction for atomic operations (stock deduction and data insertion)
                SqlTransaction transaction = connection.BeginTransaction();

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

                // Insert order into the bill_list
                SqlCommand command = new SqlCommand("INSERT INTO bill_list (items, customer, phone, address, date, type, status, total_amount, net_total_amount) VALUES (@Items, @Name, @Phone, @Address, @Date, @Type, @Status, @Total, @NetTotal)", connection, transaction);
                command.Parameters.AddWithValue("@Items", json);
                command.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                command.Parameters.AddWithValue("@Phone", Convert.ToInt32(Phone_TextBox.Text));
                command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@Type", "Delivery");
                command.Parameters.AddWithValue("@Status", "Incomplete");
                command.Parameters.AddWithValue("@Total", total);
                command.Parameters.AddWithValue("@NetTotal", total);

                // Execute the insert query
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Commit the transaction after both stock deduction and insert
                    transaction.Commit();
                    MessageBox.Show("Saved Successfully");
                    insertStatus = "Inserted";
                }
                else
                {
                    MessageBox.Show("There was a problem saving");
                }
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of any error
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                this.Close(); // Close the form after operation
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

        string filepath;
 

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}
