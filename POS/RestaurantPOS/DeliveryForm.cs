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
    public partial class DeliveryForm : Form
    {
        private string json;
        private string insertStatus = "";
        private decimal total = 0;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        public DeliveryForm(string json,decimal total)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.json = json;
            this.total = total;
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

        }
        public DeliveryForm()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
        }
        public string InsertStatus
        { 
            get { return insertStatus; }
        }

        private void InitializeDatabaseConnection()
        {
            if (Session.SelectedModule == "Restaurant POS")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            else if (Session.SelectedModule == "Hotel Management")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
        }

        private void SaveData()
        {
            if (Name_TextBox.Text == "" && Phone_TextBox.Text == "" && Address_TextBox.Text == "")
            {
                MessageBox.Show("Please fill the field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into bill_list(items, customer, phone, address, date, type, status, total_amount, net_total_amount) values(@Items, @Name, @Phone, @Address, @Date, @Type, @Status, @Total, @NetTotal); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Items", json);
                command.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                command.Parameters.AddWithValue("@Phone", Convert.ToInt32(Phone_TextBox.Text));
                command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@Type", "Delivery");
                command.Parameters.AddWithValue("@Status", "In Complete");
                command.Parameters.AddWithValue("@Total", total);
                command.Parameters.AddWithValue("@NetTotal", total);

                // Get the generated BillID
                int billId = Convert.ToInt32(command.ExecuteScalar());

                if (billId > 0)
                {
                    MessageBox.Show("Saved Successfully");
                    string currentUsername = Session.Username; ; 

                    // Insert into Activity Log table
                    string activityDescription = $"New delivery order added to bill list (BillID: {billId}), Total: {total}";
                    SqlCommand logCommand = new SqlCommand("INSERT INTO activity_log (action, description, time, username) VALUES (@ActionType, @Description, @ActionDate, @Username)", connection);
                    logCommand.Parameters.AddWithValue("@ActionType", "Insert");
                    logCommand.Parameters.AddWithValue("@Description", activityDescription);
                    logCommand.Parameters.AddWithValue("@ActionDate", DateTime.Now);
                    logCommand.Parameters.AddWithValue("@Username", currentUsername); // Insert the username

                    logCommand.ExecuteNonQuery();

                    insertStatus = "Inserted";
                }
                else
                {
                    MessageBox.Show("There was a problem saving");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                this.Close();
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
