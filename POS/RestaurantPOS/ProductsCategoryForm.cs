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
    public partial class ProductsCategoryForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        public ProductsCategoryForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Product Category Types";
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
            if (CategoryTypes_TextBox.Text == "" )
            {
                MessageBox.Show("Please fill the field","Error" ,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            try
            {
                connection.Open();
                if (rowIndex == -1)
                {
                    string query = "INSERT INTO product_category (types) VALUES (@Types)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Types", CategoryTypes_TextBox.Text);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product Category Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }
                    }


                  
                }
                else
                {
                    string query = "UPDATE product_category SET types=@Types WHERE id=@Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Types", CategoryTypes_TextBox.Text);
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product Category Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        private void SetFields(int rowNo)
        {
            try
            {
                connection.Open();
                string query = $"select * from product_category where id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        CategoryTypes_TextBox.Text = (string)reader["types"];
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
            CategoryTypes_TextBox.Text = "";

        }


        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
 

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}
