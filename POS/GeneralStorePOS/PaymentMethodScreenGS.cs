using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace POS
{   //yet to be added to activity log
    public partial class PaymentMethodScreenGS : Form
    {
        private string statusUpdated = "";
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.Windows.Forms.TextBox targetTextBox;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentMethodScreen));
        private decimal total_amount;
        private NumberKeypad Nk;

        public PaymentMethodScreenGS(decimal total_amount, int rowIndex)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            this.total_amount = total_amount;
            this.rowIndex = rowIndex;
            SetFields(this.total_amount);
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

            foreach (Control control in panel2.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.Enter += TextBox_Enter;
                    textBox.Click += TextBox_Click;
                }
            }
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

        public string StatusUpdated
        {
            get { return statusUpdated; }
        }

        private void TextBox_Click(object sender, EventArgs e)
        {
            targetTextBox = (System.Windows.Forms.TextBox)sender;
            if (targetTextBox == Discount_TextBox || targetTextBox == CashReceived_TextBox)
            {
                if (!Nk.Visible)
                {
                    Nk.Show();
                }
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            targetTextBox = (System.Windows.Forms.TextBox)sender;
            targetTextBox.SelectionStart = targetTextBox.Text.Length;
            if (!Nk.Visible)
            {
                Nk.Show();
            }
        }

        private void SaveData()
        {
            decimal netTotal = Convert.ToDecimal(NetAmount_TextBox.Text);
            decimal change = Convert.ToDecimal(Change_TextBox.Text);

            if (netTotal < 0 || change < 0)
            {
                MessageBox.Show("Net total amount or change cannot be negative. Please adjust the values.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(CustomerIDTB.Text))
            {
                try
                {
                    connection.Open();

                    // Initialize email and username variables
                    string email = null;
                    string username = Session.Username; // Assuming Session.Username contains the logged-in user's username

                    // Get email from database based on customer ID
                    string emailQuery = "SELECT email FROM customers WHERE customer_id = @CustomerId";
                    using (SqlCommand emailCommand = new SqlCommand(emailQuery, connection))
                    {
                        emailCommand.Parameters.AddWithValue("@CustomerId", CustomerIDTB.Text);
                        var result = emailCommand.ExecuteScalar();
                        if (result != null)
                        {
                            email = result.ToString();
                        }
                    }

                    if (string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Customer not found. Data will not be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    decimal discountAmount = string.IsNullOrEmpty(Discount_TextBox.Text) ? 0 : Convert.ToDecimal(Discount_TextBox.Text);
                    decimal originalBillAmount = Convert.ToDecimal(BillAmount_TextBox.Text);
                    decimal adjustedNetTotal = originalBillAmount - discountAmount;

                    if (adjustedNetTotal < 0)
                    {
                        MessageBox.Show("Discount amount is greater than the total bill. Please adjust the discount.", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (AvailableCreditCB.Checked)
                    {
                        decimal customerCredit = Convert.ToDecimal(CustomerCreditTB.Text);
                        decimal creditUsed = Convert.ToDecimal(UseCredit.Text);

                        // Use only as much credit as necessary, considering remaining adjustedNetTotal
                        decimal creditToDeduct = Math.Min(creditUsed, adjustedNetTotal);
                        creditToDeduct = Math.Min(creditToDeduct, customerCredit);

                        // Deduct credit from adjustedNetTotal and calculate remaining credit
                        adjustedNetTotal -= creditToDeduct;
                        decimal remainingCredit = customerCredit - creditToDeduct;

                        string creditUpdateQuery = "UPDATE customers SET credit = @RemainingCredit WHERE email = @Email";
                        using (SqlCommand creditCommand = new SqlCommand(creditUpdateQuery, connection))
                        {
                            creditCommand.Parameters.AddWithValue("@RemainingCredit", remainingCredit);
                            creditCommand.Parameters.AddWithValue("@Email", email);
                            creditCommand.ExecuteNonQuery();
                        }
                    }

                    // After using credit, determine the final amount owed
                    decimal cashReceived = Convert.ToDecimal(CashReceived_TextBox.Text);
                    decimal finalChange = cashReceived - adjustedNetTotal;

                    if (finalChange < 0)
                    {
                        MessageBox.Show("Cash received is less than the final total after applying credit and discount.", "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string updateQuery = "UPDATE bill_list SET status=@Status, discount=@Discount, net_total_amount=@NetTotal, cash_received=@CashReceived, change=@Change WHERE bill_id=@Id";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Status", "Paid");
                        command.Parameters.AddWithValue("@Discount", discountAmount);
                        command.Parameters.AddWithValue("@NetTotal", adjustedNetTotal);
                        command.Parameters.AddWithValue("@CashReceived", cashReceived);
                        command.Parameters.AddWithValue("@Change", finalChange);
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Calculate 2% of the total bill (adjustedNetTotal) and add it to the customer's points
                            decimal pointsToAdd = adjustedNetTotal * 0.02m;  // 2% of the bill amount
                            pointsToAdd = Math.Round(pointsToAdd, 2);
                            string pointsQuery = "SELECT points FROM customers WHERE email = @Email";
                            decimal currentPoints = 0;
                            using (SqlCommand pointsCommand = new SqlCommand(pointsQuery, connection))
                            {
                                pointsCommand.Parameters.AddWithValue("@Email", email);
                                var result = pointsCommand.ExecuteScalar();
                                if (result != null)
                                {
                                    currentPoints = Convert.ToDecimal(result);
                                }
                            }

                            // Add the 2% of the bill to the current points
                            decimal newPoints = currentPoints + pointsToAdd;

                            // Update the customer's points in the database
                            string updatePointsQuery = "UPDATE customers SET points = @NewPoints WHERE email = @Email";
                            using (SqlCommand updatePointsCommand = new SqlCommand(updatePointsQuery, connection))
                            {
                                updatePointsCommand.Parameters.AddWithValue("@NewPoints", newPoints);
                                updatePointsCommand.Parameters.AddWithValue("@Email", email);
                                updatePointsCommand.ExecuteNonQuery();
                            }

                            // Log the successful payment to the activity log
                            string logQuery = "INSERT INTO activity_log (time, action, username, description) VALUES (@ActivityDate, @Action, @Username, @Description)";
                            using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                            {
                                logCommand.Parameters.AddWithValue("@ActivityDate", DateTime.Now);  // Log current date and time
                                logCommand.Parameters.AddWithValue("@Action", "Payment Successful");
                                logCommand.Parameters.AddWithValue("@Username", username); // Add the username here
                                logCommand.Parameters.AddWithValue("@Description", "Payment successful for Bill ID: " + rowIndex); // Include Bill ID in description
                                logCommand.ExecuteNonQuery();
                            }

                            // Insert into purchase history
                            string purchaseHistoryQuery = "INSERT INTO purchase_history (customer_id, customer_name, bill_id, purchase_date, purchase_time) " +
                                                          "VALUES (@CustomerId, @CustomerName, @BillId, @PurchaseDate, @PurchaseTime)";
                            using (SqlCommand purchaseHistoryCommand = new SqlCommand(purchaseHistoryQuery, connection))
                            {
                                purchaseHistoryCommand.Parameters.AddWithValue("@CustomerId", CustomerIDTB.Text);
                                purchaseHistoryCommand.Parameters.AddWithValue("@CustomerName", CustomerNameTB.Text);
                                purchaseHistoryCommand.Parameters.AddWithValue("@BillId", rowIndex);  // Assuming rowIndex is the Bill ID
                                purchaseHistoryCommand.Parameters.AddWithValue("@PurchaseDate", DateTime.Now.Date);
                                purchaseHistoryCommand.Parameters.AddWithValue("@PurchaseTime", DateTime.Now.TimeOfDay);
                                purchaseHistoryCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show($"Payment Successful! {pointsToAdd} Points added to Customer: {CustomerNameTB.Text}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE bill_list SET status=@Status, discount=@Discount, net_total_amount=@NetTotal, cash_received=@CashReceived, change=@Change WHERE bill_id=@Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        string disc = Discount_TextBox.Text;
                        if (string.IsNullOrEmpty(disc))
                        {
                            disc = "0";
                        }
                        decimal discountAmount = Convert.ToDecimal(disc);
                        decimal originalBillAmount = Convert.ToDecimal(BillAmount_TextBox.Text);
                        decimal adjustedNetTotal = originalBillAmount - discountAmount;

                        if (adjustedNetTotal < 0)
                        {
                            MessageBox.Show("Discount amount is greater than the total bill. Please adjust the discount.", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        command.Parameters.AddWithValue("@Status", "Paid");
                        command.Parameters.AddWithValue("@Discount", discountAmount);
                        command.Parameters.AddWithValue("@NetTotal", adjustedNetTotal);
                        command.Parameters.AddWithValue("@CashReceived", Convert.ToDecimal(CashReceived_TextBox.Text));
                        command.Parameters.AddWithValue("@Change", change);
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        string username = Session.Username;
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Log the successful payment to the activity log
                            string logQuery = "INSERT INTO activity_log (time, action, username, description) VALUES (@ActivityDate, @Action, @Username, @Description)";
                            using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                            {
                                logCommand.Parameters.AddWithValue("@ActivityDate", DateTime.Now);  // Log current date and time
                                logCommand.Parameters.AddWithValue("@Action", "Payment Successful");
                                logCommand.Parameters.AddWithValue("@Username", username); // Add the username here
                                logCommand.Parameters.AddWithValue("@Description", "Payment successful for Bill ID: " + rowIndex); // Include Bill ID in description
                                logCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
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
        }

        private void SetFields(decimal rowNo)
        {
            BillAmount_TextBox.Text = rowNo.ToString();
            NetAmount_TextBox.Text = BillAmount_TextBox.Text;
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

        #region All KeyPress Events Functions for Allowing only numbers
        private void BillAmount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Discount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void NetAmount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void CashReceived_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Change_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
        #endregion

        private void PaymentMethodScreen_Load(object sender, EventArgs e)
        {
            int x = this.Location.X + cancel_button.Location.X;
            int y = this.Location.Y + Change_TextBox.Location.Y + Change_TextBox.Height + 20;
            Nk = new NumberKeypad(x, y);
            Nk.NumberButtonPressed += Nk_NumberButtonPressed;
            LoadCustomerName(rowIndex);
        }

        private bool LoadCustomerName(int billId)
        {
            try
            {
                connection.Open();
                string query = "SELECT customer, phone FROM bill_list WHERE bill_id = @BillId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BillId", billId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string customerName = reader["customer"].ToString();
                        string customerPhone = reader["phone"].ToString();
                        CustomerNameTB.Text = customerName;
                        reader.Close(); 

                        string customerQuery = "SELECT customer_id, points, credit FROM customers WHERE customer_name = @CustomerName AND phone_number = @PhoneNumber";
                        using (SqlCommand customerCommand = new SqlCommand(customerQuery, connection))
                        {
                            customerCommand.Parameters.AddWithValue("@CustomerName", customerName);
                            customerCommand.Parameters.AddWithValue("@PhoneNumber", customerPhone);

                            SqlDataReader customerReader = customerCommand.ExecuteReader();
                            if (customerReader.Read())
                            {
                                CustomerIDTB.Text = customerReader["customer_id"].ToString();
                                CustomerPointsTB.Text = customerReader["points"].ToString();
                                CustomerCreditTB.Text = customerReader["credit"].ToString();
                                decimal netAmount = Convert.ToDecimal(NetAmount_TextBox.Text);
                                decimal customerCredit = Convert.ToDecimal(CustomerCreditTB.Text);
                                if (customerCredit <= netAmount)
                                {
                                    UseCredit.Text = customerCredit.ToString("0.00");
                                }
                                else if (customerCredit > netAmount){
                                    UseCredit.Text = netAmount.ToString("0.00");
                                }

                                customerReader.Close();
                                return true; 
                            }
                            else
                            {
                                MessageBox.Show("Customer not found in database. Store credit and loyalty points will not be available for this order's payment.",
                                                "Customer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                customerReader.Close();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        CustomerNameTB.Text = "";
                        return false; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


        private void Nk_NumberButtonPressed(object sender, int number)
        {
            if (targetTextBox == Discount_TextBox || targetTextBox == CashReceived_TextBox)
            {
                if (number == -1)
                {
                    if (targetTextBox.Text.Length > 0)
                    {
                        targetTextBox.Text = targetTextBox.Text.Substring(0, targetTextBox.Text.Length - 1);
                        targetTextBox.SelectionStart = targetTextBox.Text.Length;
                    }
                }
                else if (number == -2)
                {
                    targetTextBox.Text = "";
                    targetTextBox.SelectionStart = targetTextBox.Text.Length;
                }
                else if (number == -3)
                {
                    targetTextBox.Text += ".";
                    targetTextBox.SelectionStart = targetTextBox.Text.Length;
                }
                else if (number != -5)
                {
                    targetTextBox.Text += number.ToString();
                    targetTextBox.SelectionStart = targetTextBox.Text.Length;
                }
            }
        }

        private void Discount_TextBox_TextChanged(object sender, EventArgs e)
        {
            string text = Discount_TextBox.Text;

            if (!string.IsNullOrEmpty(text))
            {
                decimal discountPercentage;
                if (decimal.TryParse(text, out discountPercentage))
                {
                    if (discountPercentage > 100)
                    {
                        MessageBox.Show("Discount cannot be more than 100.", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Discount_TextBox.Text = "0";
                        discountPercentage = 0;
                    }
                    decimal billAmount = Convert.ToDecimal(BillAmount_TextBox.Text);
                    decimal discountAmount = (discountPercentage / 100) * billAmount; 
                    decimal netAmount = billAmount - discountAmount;
                    NetAmount_TextBox.Text = netAmount.ToString();
                }
                else
                {

                    Discount_TextBox.Text = "0";
                    NetAmount_TextBox.Text = Convert.ToDecimal(BillAmount_TextBox.Text).ToString();
                }
            }
            else
            {

                decimal billAmount = Convert.ToDecimal(BillAmount_TextBox.Text);
                NetAmount_TextBox.Text = billAmount.ToString();
            }
        }


        private void CashReceived_TextBox_TextChanged(object sender, EventArgs e)
        {
            string text = CashReceived_TextBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                decimal cashReceived = Convert.ToDecimal(text);
                decimal netAmount = Convert.ToDecimal(NetAmount_TextBox.Text);
                decimal change = cashReceived - netAmount;
                Change_TextBox.Text = change.ToString();
            }
        }

        private void CustomerPointsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomerPointsCB.Checked)
            {
                decimal points = Convert.ToDecimal(CustomerPointsTB.Text); 
                decimal netAmount = Convert.ToDecimal(NetAmount_TextBox.Text);  
                netAmount -= points;  
                NetAmount_TextBox.Text = netAmount.ToString("F2");  
            }
            else
            {
                decimal points = Convert.ToDecimal(CustomerPointsTB.Text); 
                decimal netAmount = Convert.ToDecimal(NetAmount_TextBox.Text);  
                netAmount += points;  
                NetAmount_TextBox.Text = netAmount.ToString("F2");  
            }
        }

        private void AvailableCreditCB_CheckedChanged(object sender, EventArgs e)
        {
            if (AvailableCreditCB.Checked)
            {
                decimal credit = Convert.ToDecimal(UseCredit.Text); 
                decimal netAmount = Convert.ToDecimal(NetAmount_TextBox.Text);  
                netAmount -= credit;  
                NetAmount_TextBox.Text = netAmount.ToString("F2");  
            }
            else
            {
                decimal credit = Convert.ToDecimal(UseCredit.Text);
                decimal netAmount = Convert.ToDecimal(NetAmount_TextBox.Text);  
                netAmount += credit;  
                NetAmount_TextBox.Text = netAmount.ToString("F2");  
            }
        }

    }
}
