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
    public partial class POSIngredientScreen : Form
    {
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        public POSIngredientScreen(string product_name,string category,string product_price,Image product_pic)
        { 

            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
            ProductName_Label.Text = product_name;
            Category_Label.Text = category;
            ProductPrice_Label.Text = product_price;
            pictureBox1.Image = product_pic;
            SetFields(product_name);
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }


        
        



        private void SetFields(string product_name)
        {
            try
            {
                connection.Open();
                string query = $"select * from ingredients where product ='"+ product_name +"'";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string ingredients = (string)reader["ingredients"];
                        string[] IngredientsSplit = ingredients.Split(',');
                        foreach (var item in IngredientsSplit)
                        {
                            IngredientsListBox.Items.Add(item.ToString());
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

        
 

    }
}
