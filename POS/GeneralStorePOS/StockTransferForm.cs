using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS
{
    public partial class StockTransferForm : Form
    {
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
        public StockTransferForm(int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            //SetTypeComboBox();
            this.rowIndex = rowIndex;
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Edit Staff Details";
                save_button.Text = "Save";
                //SetFields(this.rowIndex);

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
                string query = "select types from staff_category";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //Type_ComboBox.Items.Add(reader["types"].ToString());
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


        //private void SaveData()
        //{
        //    if (StaffName_TextBox.Text == "" || Phone_TextBox.Text == "" || Address_TextBox.Text == "" || Type_ComboBox.SelectedItem == null || Status_ComboBox.SelectedItem == null || Shift_ComboBox.SelectedItem == null)
        //    {
        //        MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return;
        //    }
        //    try
        //    {
        //        connection.Open();
        //        if (rowIndex == -1)
        //        {
        //            string query = "INSERT INTO staff_details (shifts,staff_name, type, phone_number, address , status) VALUES (@Shift,@StaffName, @Type, @Phone, @Address, @Status)";
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@StaffName", StaffName_TextBox.Text);
        //                command.Parameters.AddWithValue("@Type", Type_ComboBox.SelectedItem.ToString());
        //                command.Parameters.AddWithValue("@Phone", Phone_TextBox.Text);
        //                command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
        //                command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());
        //                command.Parameters.AddWithValue("@Shift", Shift_ComboBox.SelectedItem.ToString());

        //                //// Convert image to byte array
        //                //byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image,60,60));
        //                //command.Parameters.AddWithValue("@ImageData", imageData);

        //                //byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
        //                //command.Parameters.AddWithValue("@OR_ImageData", or_imageData);

        //                int rowsAffected = command.ExecuteNonQuery();
        //                if (rowsAffected > 0)
        //                {
        //                    MessageBox.Show("Staff Details Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                }
        //            }



        //        }
        //        else
        //        {
        //            string query = "UPDATE staff_details SET shifts=@Shift,staff_name=@StaffName, type=@Type, phone_number=@Phone,address=@Address,status=@Status WHERE id=@Id";
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@StaffName", StaffName_TextBox.Text);
        //                command.Parameters.AddWithValue("@Type", Type_ComboBox.SelectedItem.ToString());
        //                command.Parameters.AddWithValue("@Phone", Phone_TextBox.Text);
        //                command.Parameters.AddWithValue("@Address", Address_TextBox.Text);
        //                command.Parameters.AddWithValue("@Status", Status_ComboBox.SelectedItem.ToString());
        //                command.Parameters.AddWithValue("@Shift", Shift_ComboBox.SelectedItem.ToString());
        //                command.Parameters.AddWithValue("@Id", rowIndex);

        //                //// Convert image to byte array
        //                //byte[] imageData = ImageToByteArray(ResizeImage(pictureBox1.Image, 60,60));
        //                //command.Parameters.AddWithValue("@ImageData", imageData);

        //                //byte[] or_imageData = ImageToByteArray(pictureBox1.Image);
        //                //command.Parameters.AddWithValue("@OR_ImageData", or_imageData);

        //                int rowsAffected = command.ExecuteNonQuery();
        //                if (rowsAffected > 0)
        //                {
        //                    MessageBox.Show("Staff Details Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Message : " + ex.Message);

        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}






        //private void SetFields(int rowNo)
        //{
        //    try
        //    {
        //        connection.Open();
        //        string query = $"select * from staff_details where id={rowNo}";
        //        command = new SqlCommand(query, connection);
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {

        //            while (reader.Read())
        //            {
        //                StaffName_TextBox.Text = (string)reader["staff_name"];
        //                Type_ComboBox.Text = (string)reader["type"];
        //                Phone_TextBox.Text = Convert.ToInt32(reader["phone_number"]).ToString();
        //                Address_TextBox.Text = (string)reader["address"];
        //                Status_ComboBox.Text = (string)reader["status"];
        //                Shift_ComboBox.Text = (string)reader["shifts"];
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Message : " + ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

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

        //private void ClearFields()
        //{
        //    StaffName_TextBox.Text = "";
        //    Type_ComboBox.Text = "";
        //    Phone_TextBox.Text = "";
        //    Address_TextBox.Text = "";
        //    Status_ComboBox.Text = "";
        //    Shift_ComboBox.Text = "";

        //}


        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void save_button_Click(object sender, EventArgs e)
        {
            //SaveData();
        }


    }
}
