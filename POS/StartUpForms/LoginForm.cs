using System.Data.SqlClient;
using System.Configuration;

namespace POS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AdjustFormSize();
        }

        private void AdjustFormSize()
        {
            var screenBounds = Screen.PrimaryScreen.Bounds;
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            int taskbarHeight = screenBounds.Height - workingArea.Height;

            this.Width = screenBounds.Width;
            this.Height = screenBounds.Height - taskbarHeight;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string pass = textBox2.Text;
            string module = LoginModuleComboBox.Text;
            string branch = BranchComboBox.Text;
            string query = $"SELECT * FROM users WHERE email=@Email AND password=@Password";
            string connectionString = "";

            if (string.IsNullOrEmpty(module))
            {
                MessageBox.Show("Please select a category from the dropdown.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.Equals(BranchComboBox.Text?.ToString(), "Branch") && BranchComboBox.Enabled && BranchComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select the branch code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains("@") || !email.EndsWith(".com"))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (module == "Restaurant POS")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            }
            else if (module == "General Store" && branch == "PK728")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
            }
            else if (module == "General Store" && branch == "BR001")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;
            }
            else if (module == "Hotel Management")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
            }
            else
            {
                MessageBox.Show("Please select a module from the dropdown menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", pass);
                    SqlDataReader r = cmd.ExecuteReader();

                    if (r.HasRows)
                    {
                        while (r.Read())
                        {
                            Session.Username = r.GetString(r.GetOrdinal("username"));
                        }

                        Session.BranchCode = BranchComboBox.Text;
                        Session.SelectedModule = LoginModuleComboBox.Text;
                        PerformCheckIn(email, module, connectionString);
                        InsertActivityLog("Login", "User logged in successfully", connectionString, Session.Username);

                        Form dashboard = module switch
                        {
                            "Restaurant POS" => new Dashboard(),
                            "General Store" => new GSDashboard(),
                            "Hotel Management" => new HotelDashboard(),
                            _ => null
                        };

                        dashboard?.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Your credentials do not match an account in our system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PerformCheckIn(string email, string module, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string checkQuery = @"SELECT COUNT(*) FROM timesheet 
                                      WHERE email = @Email AND CAST(clock_in_time AS DATE) = CAST(GETDATE() AS DATE) 
                                      AND clock_out_time IS NULL";
                string insertQuery = @"INSERT INTO timesheet (name, email, clock_in_time) 
                                       VALUES (@Username, @Email, GETDATE())";

                connection.Open();
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@Email", email);

                int checkInCount = (int)checkCmd.ExecuteScalar();

                if (checkInCount > 0)
                {
                    MessageBox.Show("Already checked in for today.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@Username", Session.Username);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Automatic check-in completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InsertActivityLog("Check-In", "User checked in automatically on login", connectionString, Session.Username);
                }
            }
        }

        private void InsertActivityLog(string action, string description, string connectionString, string username)
        {
            if (BranchComboBox.Enabled == false)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string logQuery = @"INSERT INTO activity_log (time, action, description, username) 
                                    VALUES (GETDATE(), @Action, @Description, @Username)";

                    connection.Open();
                    SqlCommand logCmd = new SqlCommand(logQuery, connection);
                    logCmd.Parameters.AddWithValue("@Action", action);
                    logCmd.Parameters.AddWithValue("@Description", description);
                    logCmd.Parameters.AddWithValue("@Username", username);
                    logCmd.ExecuteNonQuery();
                }
            }
            else if (Session.BranchCode == "PK728")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string logQuery = @"INSERT INTO activity_log (time, action, description, username) 
                                    VALUES (GETDATE(), @Action, @Description, @Username)";

                    connection.Open();
                    SqlCommand logCmd = new SqlCommand(logQuery, connection);
                    logCmd.Parameters.AddWithValue("@Action", action);
                    logCmd.Parameters.AddWithValue("@Description", description);
                    logCmd.Parameters.AddWithValue("@Username", username);
                    logCmd.ExecuteNonQuery();
                }
            }

            else if (Session.BranchCode == "BR001")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string logQuery = @"INSERT INTO activity_log (time, action, description, username) 
                                    VALUES (GETDATE(), @Action, @Description, @Username)";

                    connection.Open();
                    SqlCommand logCmd = new SqlCommand(logQuery, connection);
                    logCmd.Parameters.AddWithValue("@Action", action);
                    logCmd.Parameters.AddWithValue("@Description", description);
                    logCmd.Parameters.AddWithValue("@Username", username);
                    logCmd.ExecuteNonQuery();
                }
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void BranchComboBoxPopulate()
        {
                // Check if the selected value of the LoginModuleComboBox is "General Store"
                if (LoginModuleComboBox.SelectedItem != null &&
                    LoginModuleComboBox.SelectedItem.ToString() == "General Store")
                {
                    // Enable the BranchComboBox
                    BranchComboBox.Enabled = true;

                    // Clear existing items
                    BranchComboBox.Items.Clear();

                    try
                    {
                        // Connection string for the General Store
                        string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Fetch branch codes from the branches table
                            string query = "SELECT branch_code FROM branches";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        // Add each branch_code to the BranchComboBox
                                        BranchComboBox.Items.Add(reader["branch_code"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while fetching branch codes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Disable the BranchComboBox and clear its items
                    BranchComboBox.Enabled = false;
                    BranchComboBox.Items.Clear();
                }
            


        }

        private void LoginModuleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchComboBoxPopulate();
        }
    }
}
