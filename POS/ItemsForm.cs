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

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
            connection = new SqlConnection(connectionString);
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
            if (ProductName_TextBox.Text == "" || Category_ComboBox.SelectedItem == null || Unit_ComboBox.SelectedItem == null || Status_ComboBox.SelectedItem == null || pictureBox1.Image == null)
            {
                MessageBox.Show("Please fill all fields","Error" ,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            try
            {
                connection.Open();
                if (rowIndex == -1)
                {
                    string query = "INSERT INTO items (item_name,quantity,unit,item_price, category, status, image, or_image) VALUES (@ProductName,@Quantity,@Unit,@Price, @Category, @Status, @ImageData, @OR_ImageData)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", ProductName_TextBox.Text);
                        command.Parameters.AddWithValue("@Quantity", Convert.ToDouble(Quantity_TextBox.Text));
                        command.Parameters.AddWithValue("@Unit", Unit_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Price", Convert.ToDouble(ProductPrice_TextBox.Text));
                        command.Parameters.AddWithValue("@Category", Category_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());

                        // Convert image to byte array
                        byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image,60,60));
                        command.Parameters.AddWithValue("@ImageData", imageData);
                        
                        byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@OR_ImageData", or_imageData);

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
                    string query = "UPDATE items SET item_name=@ProductName,quantity=@Quantity,unit=@Unit,item_price = @Price, category=@Category, status=@Status, image=@ImageData, or_image=@OR_ImageData WHERE id=@Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", ProductName_TextBox.Text);
                        command.Parameters.AddWithValue("@Quantity", Convert.ToDouble(Quantity_TextBox.Text));
                        command.Parameters.AddWithValue("@Unit", Unit_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Price", Convert.ToDouble(ProductPrice_TextBox.Text));
                        command.Parameters.AddWithValue("@Category", Category_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        // Convert image to byte array
                        byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image, 60,60));
                        command.Parameters.AddWithValue("@ImageData", imageData);

                        byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
                        command.Parameters.AddWithValue("@OR_ImageData", or_imageData);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
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
                string query = $"select * from items where id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ProductName_TextBox.Text = (string)reader["item_name"];
                        ProductPrice_TextBox.Text = reader["item_price"].ToString();
                        Category_ComboBox.Text = (string)reader["category"];
                        Status_ComboBox.Text = (string)reader["status"];
                        Quantity_TextBox.Text = reader["quantity"].ToString();
                        Unit_ComboBox.Text = (string)reader["unit"];
                        //ProductName_TextBox.Text = reader.GetString(reader.GetOrdinal("product_name"));
                        //Category_ComboBox.Text = reader.GetString(reader.GetOrdinal("category"));
                        //Status_ComboBox.Text = reader.GetString(reader.GetOrdinal("status"));
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
            ProductName_TextBox.Text = "";
            ProductPrice_TextBox.Text = "";
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
    }
}
