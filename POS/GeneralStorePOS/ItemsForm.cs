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
    public partial class ItemsForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsForm));
        public ItemsForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            SetTypeComboBox();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Item";
                save_button.Text = "Save";
                SetFields(this.rowIndex);
            }
            Quantity_TextBox.KeyPress += NumericTextBox_KeyPress;
            CostPriceTB.KeyPress += NumericTextBox_KeyPress;
            SellingPriceTB.KeyPress += NumericTextBox_KeyPress;
            TAXTB.KeyPress += NumericTextBox_KeyPress;
            SellingPriceTaxTB.KeyPress += NumericTextBox_KeyPress;


            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

        }

        private void Quantity_TextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
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

        private void SetTypeComboBox()
        {
            try
            {
                string query = "select types from item_category";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category_ComboBox.Items.Add(reader["types"].ToString());
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
            if (ProductName_TextBox.Text == "" || CostPriceTB.Text == "" || SellingPriceTB.Text == "" || Category_ComboBox.SelectedItem == null || Unit_ComboBox.SelectedItem == null || Status_ComboBox.SelectedItem == null || pictureBox1.Image == null)
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                connection.Open();
                if (rowIndex == -1)
                {
                    // Calculate SellingPriceTax based on SellingPrice and TAXTB
                    decimal sellingPrice = Convert.ToDecimal(SellingPriceTB.Text);
                    decimal taxPercentage = Convert.ToDecimal(TAXTB.Text);
                    decimal sellingPriceTax = sellingPrice - (sellingPrice * (taxPercentage / 100));

                    // Updated query with expiry_date field
                    string query = "INSERT INTO items (item_name, quantity, unit, cost_price, selling_price, selling_price_tax, category, status, image, or_image, expiry_date) " +
                                   "VALUES (@ProductName, @Quantity, @Unit, @CostPrice, @SellingPrice, @SellingPriceTax, @Category, @Status, @ImageData, @OR_ImageData, @ExpiryDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adding parameters to the SQL command
                        command.Parameters.AddWithValue("@ProductName", ProductName_TextBox.Text);
                        command.Parameters.AddWithValue("@Quantity", Convert.ToDouble(Quantity_TextBox.Text));
                        command.Parameters.AddWithValue("@Unit", Unit_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@CostPrice", Convert.ToDouble(CostPriceTB.Text)); // Corresponds to cost_price
                        command.Parameters.AddWithValue("@SellingPrice", sellingPrice); // Using calculated selling price
                        command.Parameters.AddWithValue("@SellingPriceTax", sellingPriceTax); // Using calculated selling price tax
                        command.Parameters.AddWithValue("@Category", Category_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());

                        // Convert image to byte array
                        byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image, 60, 60));
                        command.Parameters.AddWithValue("@ImageData", imageData);

                        byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@OR_ImageData", or_imageData);

                        // Handling expiry_date based on perishable checkbox
                        DateTime? expiryDate = null;
                        if (!PerishableCB.Checked)
                        {
                            // If not perishable, set the expiry date to the value entered in the text box
                            expiryDate = DateTime.Parse(ExpiryDateTB.Text).Date; // Only date part, not time
                        }

                        // Add the expiry date parameter to the SQL command
                        command.Parameters.AddWithValue("@ExpiryDate", expiryDate.HasValue ? (object)expiryDate.Value : DBNull.Value);

                        // Execute the insert query
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }
                    }



                }
                else
                {
                    string query = "UPDATE items SET item_name = @ProductName, " +
                                   "quantity = @Quantity, " +
                                   "unit = @Unit, " +
                                   "cost_price = @CostPrice, " +
                                   "selling_price = @SellingPrice, " +
                                   "selling_price_tax = @SellingPriceTax, " +
                                   "category = @Category, " +
                                   "status = @Status, " +
                                   "image = @ImageData, " +
                                   "or_image = @OR_ImageData, " +
                                   "expiry_date = @ExpiryDate " +
                                   "WHERE id = @Id";  

                    decimal sellingPrice = Convert.ToDecimal(SellingPriceTB.Text);
                    decimal taxPercentage = Convert.ToDecimal(TAXTB.Text);
                    decimal sellingPriceTax = sellingPrice - (sellingPrice * (taxPercentage / 100));

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the SQL command
                        command.Parameters.AddWithValue("@ProductName", ProductName_TextBox.Text);
                        command.Parameters.AddWithValue("@Quantity", Convert.ToDouble(Quantity_TextBox.Text));
                        command.Parameters.AddWithValue("@Unit", Unit_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@CostPrice", Convert.ToDouble(CostPriceTB.Text));
                        command.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                        command.Parameters.AddWithValue("@SellingPriceTax", sellingPriceTax);
                        command.Parameters.AddWithValue("@Category", Category_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());

                        // Convert images to byte arrays
                        byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image, 60, 60));
                        command.Parameters.AddWithValue("@ImageData", imageData);

                        byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@OR_ImageData", or_imageData);

                        // Handle expiry_date based on perishable checkbox
                        DateTime? expiryDate = null;
                        if (!PerishableCB.Checked)
                        {
                            expiryDate = DateTime.Parse(ExpiryDateTB.Text).Date;
                        }
                        command.Parameters.AddWithValue("@ExpiryDate", expiryDate.HasValue ? (object)expiryDate.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@Id", rowIndex); 

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
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


        private void UpdateSellingPriceTax()
        {
            if (decimal.TryParse(SellingPriceTB.Text, out decimal sellingPrice) && decimal.TryParse(TAXTB.Text, out decimal taxPercentage))
            {
                // Calculate the SellingPriceTax (Selling Price minus Tax)
                decimal sellingPriceTax = sellingPrice - (sellingPrice * (taxPercentage / 100));
                SellingPriceTaxTB.Text = sellingPriceTax.ToString("F2"); // Format as currency or two decimal places
            }
            else
            {
                SellingPriceTaxTB.Text = "0.00"; // Default to 0 if invalid input
            }
        }


        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Clone the image to prevent it from being locked
                using (Image clonedImage = (Image)image.Clone())
                {
                    clonedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Adjust format as needed
                }
                return ms.ToArray();
            }
        }


        private Image ByteArraytoImage(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                Image image = new Bitmap(Image.FromStream(ms));
                return image;
            }
        }

        private void SetFields(int rowNo)
        {
            try
            {
                connection.Open();
                string query = $"SELECT * FROM items WHERE id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Disable the Perishable checkbox initially
                        PerishableCB.Enabled = false;

                        // Populate fields from the database
                        ProductName_TextBox.Text = reader["item_name"].ToString();
                        CostPriceTB.Text = reader["cost_price"].ToString(); // Assuming this maps to cost_price
                        Category_ComboBox.Text = reader["category"].ToString();
                        Status_ComboBox.Text = reader["status"].ToString();
                        Quantity_TextBox.Text = reader["quantity"].ToString();
                        Unit_ComboBox.Text = reader["unit"].ToString();
                        pictureBox1.Image = ByteArraytoImage((byte[])(reader["or_image"]));

                        // Set selling price and selling price with tax
                        SellingPriceTB.Text = reader["selling_price"].ToString();
                        SellingPriceTaxTB.Text = reader["selling_price_tax"].ToString();

                        // Check for expiry_date and set PerishableCB and ExpiryDateTB
                        if (reader["expiry_date"] == DBNull.Value)
                        {
                            PerishableCB.Checked = true;
                            ExpiryDateTB.Enabled = false;
                        }
                        else
                        {
                            PerishableCB.Checked = false;
                            ExpiryDateTB.Enabled = true;
                            ExpiryDateTB.Text = Convert.ToDateTime(reader["expiry_date"]).ToString("yyyy-MM-dd"); // Format as needed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void ClearFields()
        {
            ProductName_TextBox.Text = "";
            CostPriceTB.Text = "";
            Category_ComboBox.Text = "";
            Unit_ComboBox.Text = "";
            Status_ComboBox.Text = "";
            pictureBox1.Image = null;
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
        private void browse_button_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filepath = file.FileName;
                pictureBox1.Image = ResizeImage(new Bitmap(filepath), 125, 125);
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void PerishableCB_CheckedChanged(object sender, EventArgs e)
        {
            if (PerishableCB.Checked == true)
            {
                ExpiryDateTB.Enabled = false;
            }
            else
            {
                ExpiryDateTB.Enabled = true;
            }
        }

        private void SellingPriceTB_TextChanged(object sender, EventArgs e)
        {
            UpdateSellingPriceTax();
        }

        private void TAXTB_TextChanged(object sender, EventArgs e)
        {
            UpdateSellingPriceTax();
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow backspace
            if (Char.IsControl(e.KeyChar)) return;

            // Check if the entered character is a digit or a decimal point
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }


    }
}
