using Newtonsoft.Json;
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
    public partial class ProductIngredientsForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        public ProductIngredientsForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            SetComboBox();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Ingredients";
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
            if (Product_ComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (IngredientsListBox.Items.Count == 0)
            {
                MessageBox.Show("Please Add Ingredients", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string Ingredients = "";
            foreach (var item in IngredientsListBox.Items)
            {
                if (Ingredients == "")
                {
                    Ingredients += item.ToString();
                }
                else
                {
                    Ingredients += "," + item.ToString();
                }
            }

            try
            {
                connection.Open();
                if (rowIndex == -1)
                {
                    string query = "INSERT INTO product_ingredients (product,ingredients) VALUES (@Product,@Ingredients)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Product", Product_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Ingredients", Ingredients);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product Ingredients Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }
                    }



                }
                else
                {
                    string query = "UPDATE product_ingredients SET product=@Product,ingredients=@Ingredients WHERE id=@Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Product", Product_ComboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Ingredients", Ingredients);
                        command.Parameters.AddWithValue("@Id", rowIndex);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product Ingredients Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SetComboBox()
        {
            try
            {
                connection.Open();
                string query = $"select product_name from products";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Product_ComboBox.Items.Add((string)reader["product_name"]);
                    }
                }
                
                string query2 = $"select ingredient_name from ingredients";
                command = new SqlCommand(query2, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Ingredients_ComboBox.Items.Add((string)reader["ingredient_name"]);
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
                string query = $"select * from product_ingredients where id={rowNo}";
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Product_ComboBox.Text = (string)reader["product"];
                        string Ingredients = (string)reader["ingredients"];
                        string[] IngredientsSplit = Ingredients.Split(',');
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
           
            Qty_TextBox.Text = "";
            IngredientsListBox.Items.Clear();
        }


        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void AddIngredientButton_Click(object sender, EventArgs e)
        {
            if (Ingredients_ComboBox.SelectedItem != null && Qty_TextBox.Text != "" && Unit_ComboBox.SelectedItem != null)
            {
                string Full_Ing = Ingredients_ComboBox.SelectedItem.ToString() + "|" + Qty_TextBox.Text + " " +Unit_ComboBox.SelectedItem.ToString();
                IngredientsListBox.Items.Add(Full_Ing);
                Qty_TextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Something is Missing!");
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (IngredientsListBox.SelectedItem != null)
            {
                IngredientsListBox.Items.RemoveAt(IngredientsListBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please Select Something First");
            }
        }
    }
}
