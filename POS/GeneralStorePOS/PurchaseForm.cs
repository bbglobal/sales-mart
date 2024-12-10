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
    public partial class PurchaseForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
        public PurchaseForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            SetTypeComboBox();
            this.rowIndex = rowIndex;
            PaymentStatus_ComboBox.Text = "Pending";
            PaymentStatus_ComboBox.Enabled = false;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Purchase Details";
                save_button.Text = "Save";
                ShowPaidAndDueTextFields();
                SetFields(this.rowIndex);
                PaymentStatus_ComboBox.Enabled = true;
            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

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

        private void ShowPaidAndDueTextFields()
        {
            PaidAmount_label.Visible = true;
            PaidAmount_TextBox.Visible = true;
            DueAmount_label.Visible = true;
            DueAmount_TextBox.Visible = true;
        }



        private void SetTypeComboBox()
        {
            try
            {
                string query = "select supplier_name from suppliers";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Supplier_ComboBox.Items.Add(reader["supplier_name"].ToString());
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
            if (TotalAmount_TextBox.Text == "" || Product_TextBox.Text == "" || Supplier_ComboBox.SelectedItem == null || Unit_ComboBox.SelectedItem == null || PaymentStatus_ComboBox.SelectedItem == null || PurchaseStatus_ComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                connection.Open();

                if (rowIndex == -1)
                {
                    // INSERT Logic
                    string query = "INSERT INTO purchases (date, supplier, product, quantity, unit, purchase_status, total_amount, paid_amount, due_amount, payment_status) " +
                                   "VALUES (@Date, @Supplier, @Product, @Quantity, @Unit, @PurchaseStatus, @TotalAmount, @PaidAmount, @DueAmount, @PaymentStatus)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        command.Parameters.AddWithValue("@Supplier", Supplier_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Product", Product_TextBox.Text);
                        command.Parameters.AddWithValue("@Quantity", Convert.ToDecimal(Quantity_TextBox.Text));
                        command.Parameters.AddWithValue("@Unit", Unit_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@TotalAmount", Convert.ToDecimal(TotalAmount_TextBox.Text));
                        command.Parameters.AddWithValue("@PaidAmount", 0);
                        command.Parameters.AddWithValue("@DueAmount", Convert.ToDecimal(TotalAmount_TextBox.Text));
                        command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@PurchaseStatus", PurchaseStatus_ComboBox.SelectedItem.ToString());
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            LogActivity("Insert", $"Added purchase for {Product_TextBox.Text} (Quantity: {Quantity_TextBox.Text})");
                            MessageBox.Show("Purchase Details Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                            this.Close();
                        }
                    }
                }
                else
                {
                    // Fetch current purchase_status
                    string currentStatusQuery = "SELECT purchase_status FROM purchases WHERE id = @Id";
                    string currentPurchaseStatus = string.Empty;
                    using (SqlCommand statusCommand = new SqlCommand(currentStatusQuery, connection))
                    {
                        statusCommand.Parameters.AddWithValue("@Id", rowIndex);
                        object result = statusCommand.ExecuteScalar();
                        if (result != null)
                        {
                            currentPurchaseStatus = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show($"No record found for ID {rowIndex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string newPurchaseStatus = PurchaseStatus_ComboBox.SelectedItem.ToString();

                    // UPDATE Logic
                    string query = "UPDATE purchases SET supplier = @Supplier, product = @Product, quantity = @Quantity, unit = @Unit, " +
                                   "total_amount = @TotalAmount, paid_amount = @PaidAmount, due_amount = @DueAmount, " +
                                   "purchase_status = @PurchaseStatus, payment_status = @PaymentStatus WHERE id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Supplier", Supplier_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Product", Product_TextBox.Text);
                        command.Parameters.AddWithValue("@Quantity", Convert.ToDecimal(Quantity_TextBox.Text));
                        command.Parameters.AddWithValue("@Unit", Unit_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@TotalAmount", Convert.ToDecimal(TotalAmount_TextBox.Text));
                        command.Parameters.AddWithValue("@PaidAmount", Convert.ToDecimal(PaidAmount_TextBox.Text));
                        command.Parameters.AddWithValue("@DueAmount", Convert.ToDecimal(DueAmount_TextBox.Text));
                        command.Parameters.AddWithValue("@PurchaseStatus", newPurchaseStatus);
                        command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            LogActivity("Update", $"Updated purchase for {Product_TextBox.Text} (Quantity: {Quantity_TextBox.Text})");
                            MessageBox.Show("Purchase Details Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();

                            // Check purchase status change
                            if (currentPurchaseStatus != "Received" && newPurchaseStatus == "Received")
                            {
                                string productName = Product_TextBox.Text;
                                decimal receivedQuantity = Convert.ToDecimal(Quantity_TextBox.Text);

                                // Check and update items table
                                string updateItemsQuery = "UPDATE items SET quantity = quantity + @ReceivedQuantity WHERE item_name = @ProductName";
                                using (SqlCommand updateItemsCommand = new SqlCommand(updateItemsQuery, connection))
                                {
                                    updateItemsCommand.Parameters.AddWithValue("@ReceivedQuantity", receivedQuantity);
                                    updateItemsCommand.Parameters.AddWithValue("@ProductName", productName);

                                    int itemsRowsAffected = updateItemsCommand.ExecuteNonQuery();

                                    if (itemsRowsAffected == 0)
                                    {
                                        MessageBox.Show($"Product '{productName}' not found in the items section. Please add the product with the received quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Product '{productName}' updated with quantity {receivedQuantity}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }
                                }
                            }
                        }
                    }
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

        // Method to log activity
        private void LogActivity(string action, string description)
        {
            string username = Session.Username; // Assuming Session.Username is available
            string logQuery = "INSERT INTO activity_log (time, action, description, username) VALUES (@Time, @Action, @Description, @Username)";
            using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
            {
                logCommand.Parameters.AddWithValue("@Time", DateTime.Now);
                logCommand.Parameters.AddWithValue("@Action", action);
                logCommand.Parameters.AddWithValue("@Description", description);
                logCommand.Parameters.AddWithValue("@Username", username);
                logCommand.ExecuteNonQuery();
            }
        }



        private void SetFields(int rowNo)
        {
            try
            {
                connection.Open();
                string query = $"select * from purchases where id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Supplier_ComboBox.Text = (string)reader["supplier"];
                        Product_TextBox.Text = (string)reader["product"];
                        Quantity_TextBox.Text = reader["quantity"].ToString();
                        Unit_ComboBox.Text = reader["unit"].ToString();
                        TotalAmount_TextBox.Text = reader["total_amount"].ToString();
                        PaidAmount_TextBox.Text = reader["paid_amount"].ToString();
                        DueAmount_TextBox.Text = reader["due_amount"].ToString();
                        PaymentStatus_ComboBox.Text = (string)reader["payment_status"];
                        PurchaseStatus_ComboBox.Text = (string)reader["purchase_status"];
                        if (PurchaseStatus_ComboBox.Text == "Received")
                        {
                            PurchaseStatus_ComboBox.Enabled = false;
                        }
                        if (PaymentStatus_ComboBox.Text == "Paid")
                        {
                            Supplier_ComboBox.Enabled = false;
                            Product_TextBox.Enabled = false;
                            Quantity_TextBox.Enabled = false;
                            Unit_ComboBox.Enabled = false;
                            TotalAmount_TextBox.Enabled = false;
                            PaidAmount_TextBox.Enabled = false;
                            DueAmount_TextBox.Enabled = false;
                            PaymentStatus_ComboBox.Enabled = false;
                            PurchaseStatus_ComboBox.Enabled = false;
                            save_button.Enabled = false;
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

        private void ClearFields()
        {
            Supplier_ComboBox.Text = "";
            Product_TextBox.Text = "";
            Unit_ComboBox.Text = "";
            TotalAmount_TextBox.Text = "";
            PaidAmount_TextBox.Text = "";
            DueAmount_TextBox.Text = "";
            PaymentStatus_ComboBox.Text = "";
            PurchaseStatus_ComboBox.Text = "";

        }


        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }



        private void PaidAmount_TextBox_TextChanged(object sender, EventArgs e)
        {
            string text = PaidAmount_TextBox.Text;

            if (PaidAmount_TextBox.Text != "")
            {
                if (text[text.Length - 1] == Convert.ToChar("."))
                {
                    return;
                }
               
            }


            if (PaidAmount_TextBox.Text != "")
            {

                decimal total = Convert.ToDecimal(TotalAmount_TextBox.Text);
                decimal paid = Convert.ToDecimal(PaidAmount_TextBox.Text);
                DueAmount_TextBox.Text = (total-paid).ToString();

            }
            else
            {
                DueAmount_TextBox.Text = TotalAmount_TextBox.Text;
            }
        }

        private void TotalAmount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Quantity_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void PaidAmount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
