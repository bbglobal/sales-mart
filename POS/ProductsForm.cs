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
    public partial class ProductsForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsForm));
        public ProductsForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Product";
                save_button.Text = "Save";
                SetFields(this.rowIndex);
            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }


        private void SaveData()
        {
            try
            {
                connection.Open();
                if (rowIndex == -1)
                {
                    string query = "INSERT INTO products (product_name, category, status, image) VALUES (@ProductName, @Category, @Status, @ImageData)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", ProductName_TextBox.Text);
                        command.Parameters.AddWithValue("@Category", Category_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());

                        // Convert image to byte array
                        byte[] imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@ImageData", imageData);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                    }


                  
                }
                else
                {
                    string query = "UPDATE products SET product_name=@ProductName, category=@Category, status=@Status, image=@ImageData WHERE id=@Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", ProductName_TextBox.Text);
                        command.Parameters.AddWithValue("@Category", Category_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        // Convert image to byte array
                        byte[] imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@ImageData", imageData);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Adjust format as needed
                return ms.ToArray();
            }
        }

        private Image ByteArraytoImage(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private void SetFields(int rowNo)
        {
            try
            {
                connection.Open();
                string query = $"select * from products where id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ProductName_TextBox.Text = (string)reader["product_name"];
                        Category_ComboBox.Text = (string)reader["category"];
                        Status_ComboBox.Text = (string)reader["status"];
                        //ProductName_TextBox.Text = reader.GetString(reader.GetOrdinal("product_name"));
                        //Category_ComboBox.Text = reader.GetString(reader.GetOrdinal("category"));
                        //Status_ComboBox.Text = reader.GetString(reader.GetOrdinal("status"));
                        pictureBox1.Image = ByteArraytoImage((byte[])(reader["image"]));
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
