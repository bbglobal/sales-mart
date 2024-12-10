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
    public partial class ClientForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsForm));
        public ClientForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Client";
                save_button.Text = "Save";
                SetFields(this.rowIndex);
            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }



        private void SaveData()
        {
            if (Name_TextBox.Text == "" || Email_TextBox.Text == "" || Phone_TextBox.Text == "" || Address_TextBox.Text == "" ||  pictureBox1.Image == null)
            {
                MessageBox.Show("Please fill all fields","Error" ,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            try
            {
                connection.Open();
                if (rowIndex == -1)
                {
                    string query = "INSERT INTO clients (image,client_name,email, phone, address,or_image) VALUES (@Image,@Name, @Email, @Phone, @Address, @OR_Image)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                        command.Parameters.AddWithValue("@Email", Email_TextBox.Text);
                        command.Parameters.AddWithValue("@Phone", Convert.ToInt32(Phone_TextBox.Text));
                        command.Parameters.AddWithValue("@Address", Address_TextBox.Text);

                        // Convert image to byte array
                        byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image,60,60));
                        command.Parameters.AddWithValue("@Image", imageData);
                        
                        byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@OR_Image", or_imageData);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Client Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }
                    }


                  
                }
                else
                {
                    string query = "UPDATE clients SET image=@Image,client_name=@Name,email = @Email, phone=@Phone, address=@Address, or_image=@OR_Image WHERE id=@Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                        command.Parameters.AddWithValue("@Email", Email_TextBox.Text);
                        command.Parameters.AddWithValue("@Phone", Convert.ToInt32(Phone_TextBox.Text));
                        command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        // Convert image to byte array
                        byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image, 60,60));
                        command.Parameters.AddWithValue("@Image", imageData);

                        byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@OR_Image", or_imageData);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
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


        //private byte[] ImageToByteArray(Image image)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Adjust format as needed
        //        return ms.ToArray();
        //    }
        //}

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
                string query = $"select * from clients where id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Name_TextBox.Text = (string)reader["client_name"];
                        Email_TextBox.Text = reader["email"].ToString();
                        Phone_TextBox.Text = reader["phone"].ToString();
                        Address_TextBox.Text = reader["address"].ToString();
                        pictureBox1.Image = ByteArraytoImage((byte[])(reader["or_image"]));
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


        private void ClearFields() 
        {
            Name_TextBox.Text = "";
            Email_TextBox.Text = "";
            Phone_TextBox.Text = "";
            Address_TextBox.Text = "";
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
    }
}
