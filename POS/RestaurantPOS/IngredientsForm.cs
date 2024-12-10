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
    public partial class IngredientsForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsForm));
        public IngredientsForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Ingredient";
                save_button.Text = "Save";
                SetFields(this.rowIndex);
            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

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


        #region  All Conversion Functions 


        // From Standard Units to Precise Units

        //public string KilogramsToOunces(double kilograms)
        //{
        //    const double ouncesPerKg = 35.27396; // 1 kg = 35.27396 ounces
        //    double result = kilograms * ouncesPerKg;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}

        //public string KilogramsToGrams(double kilograms)
        //{
        //    const double gramsPerKg = 1000; // 1 kg = 1000 grams
        //    double result = kilograms * gramsPerKg;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}

        //public string PoundsToOunces(double pounds)
        //{
        //    const double ouncesPerLb = 16; // 1 lb = 16 ounces
        //    double result = pounds * ouncesPerLb;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}

        //public string PoundsToGrams(double pounds)
        //{
        //    const double gramsPerLb = 453.592; // 1 lb = 453.592 grams
        //    double result = pounds * gramsPerLb;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}


        //public string LitersToFluidOunces(double liters)
        //{
        //    const double fluidOuncesPerLiter = 33.814; // 1 liter = 33.814 fluid ounces
        //    double result = liters * fluidOuncesPerLiter;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}

        //// From Precise Units to Standard Units

        //public string OuncesToKilograms(double ounces)
        //{
        //    const double kgPerOunce = 0.02834952; // 1 ounce = 0.02834952 kilograms
        //    double result = ounces * kgPerOunce;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}

        //public string GramsToKilograms(double grams)
        //{
        //    const double kgPerGram = 0.001; // 1 gram = 0.001 kilograms
        //    double result = grams * kgPerGram;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}

        //public string OuncesToPounds(double ounces)
        //{
        //    const double lbsPerOunce = 0.0625; // 1 ounce = 0.0625 pounds
        //    double result = ounces * lbsPerOunce;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}

        //public string FluidOuncesToLiters(double fluidOunces)
        //{
        //    const double litersPerFluidOunce = 0.02957353; // 1 fluid ounce = 0.02957353 liters
        //    double result = fluidOunces * litersPerFluidOunce;
        //    return result.ToString("0.000"); // Format result to 3 decimal places
        //}




        #endregion


        private void SaveData()
        {
            if (IngName_TextBox.Text == "" || STQuantity_TextBox.Text == "" || STUnit_ComboBox.SelectedItem == null || MinimumQuantity_TextBox.Text == "" || ProductPrice_TextBox.Text == "")
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string currentUsername = Session.Username; 
            string actionType = rowIndex == -1 ? "Add Ingredient" : "Update Ingredient";
            string description = $"{actionType} - {IngName_TextBox.Text}";
            DateTime currentTime = DateTime.Now;

            try
            {
                connection.Open();

                string query;

                if (rowIndex == -1) // Add Ingredient
                {
                    query = "INSERT INTO ingredients (ingredient_name, standard_quantity, standard_unit, cost_per_unit, min_quantity) " +
                            "VALUES (@IngName, @STQuantity, @STUnit, @CostPerUnit, @MinQuantity)";
                }
                else // Update Ingredient
                {
                    query = "UPDATE ingredients SET ingredient_name = @IngName, standard_quantity = @STQuantity, " +
                            "standard_unit = @STUnit, cost_per_unit = @CostPerUnit, min_quantity = @MinQuantity " +
                            "WHERE id = @Id";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IngName", IngName_TextBox.Text);
                    command.Parameters.AddWithValue("@STQuantity", Convert.ToDecimal(STQuantity_TextBox.Text));
                    command.Parameters.AddWithValue("@STUnit", STUnit_ComboBox.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@CostPerUnit", Convert.ToDecimal(ProductPrice_TextBox.Text));
                    command.Parameters.AddWithValue("@MinQuantity", Convert.ToDecimal(MinimumQuantity_TextBox.Text));

                    if (rowIndex != -1)
                    {
                        command.Parameters.AddWithValue("@Id", rowIndex);
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show(rowIndex == -1 ? "Ingredient Added Successfully" : "Ingredient Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Insert log into activity_log table
                        string logQuery = "INSERT INTO activity_log (action, description, time, username) VALUES (@Action, @Description, @Time, @Username)";
                        using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                        {
                            logCommand.Parameters.AddWithValue("@Action", actionType);
                            logCommand.Parameters.AddWithValue("@Description", description);
                            logCommand.Parameters.AddWithValue("@Time", currentTime);
                            logCommand.Parameters.AddWithValue("@Username", currentUsername);
                            logCommand.ExecuteNonQuery();
                        }

                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message);
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
                string query = $"select * from ingredients where id={rowNo}";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    IngName_TextBox.Text = reader["ingredient_name"].ToString();
                    STQuantity_TextBox.Text = reader["standard_quantity"].ToString();
                    STUnit_ComboBox.Text = reader["standard_unit"].ToString();
                    MinimumQuantity_TextBox.Text = reader["min_quantity"].ToString();
                    ProductPrice_TextBox.Text = reader["cost_per_unit"].ToString();
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


        private void ClearFields()
        {
            IngName_TextBox.Text = "";
            STQuantity_TextBox.Text = "";
            STUnit_ComboBox.Text = "";
            ProductPrice_TextBox.Text = "";
            MinimumQuantity_TextBox.Text = "";
 
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



        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }



        #region Convert Button Function ( Must be deleted if not used )

        //private void Convert_Button_Click(object sender, EventArgs e)
        //{

        //    if (STQuantity_TextBox.Text != "" && STQuantity_TextBox.Text.Last().ToString() != ".")
        //    {
        //        if (STUnit_ComboBox.SelectedItem != null && PRUnit_ComboBox.SelectedItem != null )
        //        {
        //            if (STUnit_ComboBox.SelectedItem == "kg" && PRUnit_ComboBox.SelectedItem == "oz")
        //            {
        //                PRQuantity_TextBox.Text = KilogramsToOunces(Convert.ToDouble(STQuantity_TextBox.Text));
        //            }

        //            else if (STUnit_ComboBox.SelectedItem == "kg" && PRUnit_ComboBox.SelectedItem == "grams")
        //            {
        //                PRQuantity_TextBox.Text = KilogramsToGrams(Convert.ToDouble(STQuantity_TextBox.Text));
        //            }

        //            else if (STUnit_ComboBox.SelectedItem == "lbs" && PRUnit_ComboBox.SelectedItem == "oz")
        //            {
        //                PRQuantity_TextBox.Text = PoundsToOunces(Convert.ToDouble(STQuantity_TextBox.Text));
        //            }

        //            else if (STUnit_ComboBox.SelectedItem == "lbs" && PRUnit_ComboBox.SelectedItem == "grams")
        //            {
        //                PRQuantity_TextBox.Text = PoundsToGrams(Convert.ToDouble(STQuantity_TextBox.Text));
        //            }

        //            else if (STUnit_ComboBox.SelectedItem == "litres" && PRUnit_ComboBox.SelectedItem == "fl oz")
        //            {
        //                PRQuantity_TextBox.Text = LitersToFluidOunces(Convert.ToDouble(STQuantity_TextBox.Text));
        //            }


        //        }
        //        else
        //        {
        //            MessageBox.Show("The Standard Unit or Precise Unit is not selected","Failed",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        //        }
        //    }
        //}
        #endregion



        private void STQuantity_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Attached this event function to All fields which should allow only numbers so don't get confused by the name

            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
