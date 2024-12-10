using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class UserEditForm : Form
    {
        private int rowIndex; 
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserEditForm));

        public UserEditForm(string email, string username, string password, int rowIndex = -1)
        {
            InitializeComponent();
            this.rowIndex = rowIndex;

            // Initialize the save button to be enabled from the start
            save_button.Enabled = true;

            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit User";
                save_button.Text = "Update";
                emailTB.Text = email;
                usernameTB.Text = username; 
                passwordTB.Text = password; 
                emailTB.ReadOnly = true;
            }

            try
            {
                InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing label image: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            emailTB.Text = "";
            usernameTB.Text = "";
            passwordTB.Text = "";
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            UpdateData();
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

        private void UpdateData()
        {
            string updatedUsername = usernameTB.Text;
            string updatedPassword = passwordTB.Text;
            string email = emailTB.Text; 


            if (string.IsNullOrEmpty(updatedUsername) || string.IsNullOrEmpty(updatedPassword))
            {
                MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isAdmin = false; 

            try
            {
                UpdateUserInDatabase("myconn", email, updatedUsername, updatedPassword, isAdmin);
                UpdateUserInDatabase("myconnGS", email, updatedUsername, updatedPassword, isAdmin);
                UpdateUserInDatabase("myconnHM", email, updatedUsername, updatedPassword, isAdmin);

                MessageBox.Show("User information successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUserInDatabase(string connectionStringName, string email, string username, string password, bool isAdmin)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE users SET username = @Username, password = @Password WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", email); 


                    int rowsAffected = command.ExecuteNonQuery();
                    if (isAdmin && rowsAffected == 0)
                    {
                        throw new Exception($"No user found with the provided email in the {connectionStringName} database.");
                    }
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
   
        }
    }
}
