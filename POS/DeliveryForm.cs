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
        private int total = 0;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        public DeliveryForm(string json,int total)
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
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }


        private void SaveData()
        {
            if (Name_TextBox.Text == "" && Phone_TextBox.Text == "" && Address_TextBox.Text == "")
            {
                MessageBox.Show("Please fill the field","Error" ,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into bill_list(items,customer,phone,address,date,type,status,total_amount) values(@Items,@Name,@Phone,@Address,@Date,@Type,@Status,@Total)", connection);
                command.Parameters.AddWithValue("@Items", json);
                command.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                command.Parameters.AddWithValue("@Phone", Convert.ToInt32(Phone_TextBox.Text));
                command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@Type", "Delivery");
                command.Parameters.AddWithValue("@Status", "In Complete");
                command.Parameters.AddWithValue("@Total", total);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
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
