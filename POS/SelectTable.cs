using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class SelectTable : Form
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        SqlConnection connection;
        string json = "";
        int total_amount = 0;
        string updatedString = "";
        public SelectTable(string json,int total_amount)
        {
            this.json = json;
            this.total_amount = total_amount;
            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLabel(label2, (Image)resources.GetObject("label1.Image"), 45, 60);
            LoadTables();
        }

        public string UpdatedString
        {
            get { return updatedString; }
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            connection = new SqlConnection(connectionString);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void LoadTables() 
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SelectTableFlowLayoutPanel.Controls.Clear();
                SqlCommand command = new SqlCommand("select * from tables",connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
                

                if (dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        SqlCommand cmd = new SqlCommand($"select * from bill_list where table_name='{row["table_name"]}' and status='In Complete'", connection);
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {

                            if (sqlDataReader.HasRows)
                            {
                                Button TableButton1 = new Button();
                                TableButton1.BackColor = Color.FromArgb(0, 119, 195);
                                TableButton1.FlatAppearance.BorderSize = 0;
                                TableButton1.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 119, 195);
                                TableButton1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 119, 195);
                                TableButton1.FlatStyle = FlatStyle.Flat;
                                TableButton1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
                                TableButton1.ForeColor = Color.White;
                                TableButton1.Location = new Point(10, 10);
                                TableButton1.Margin = new Padding(10, 10, 3, 3);
                                TableButton1.Size = new Size(150, 50);
                                TableButton1.TabIndex = 0;
                                TableButton1.Text = "In Use";
                                TableButton1.UseVisualStyleBackColor = false;
                                SelectTableFlowLayoutPanel.Controls.Add(TableButton1);
                            }
                            else
                            {
                                Button TableButton = new Button();
                                TableButton.BackColor = Color.FromArgb(0, 119, 195);
                                TableButton.FlatAppearance.BorderSize = 0;
                                TableButton.FlatStyle = FlatStyle.Flat;
                                TableButton.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
                                TableButton.ForeColor = Color.White;
                                TableButton.Location = new Point(10, 10);
                                TableButton.Margin = new Padding(10, 10, 3, 3);
                                TableButton.Size = new Size(150, 50);
                                TableButton.TabIndex = 0;
                                TableButton.Click += TableButton_Click;
                                TableButton.Text = row["table_name"].ToString();
                                TableButton.UseVisualStyleBackColor = false;
                                SelectTableFlowLayoutPanel.Controls.Add(TableButton);
                            }
                        }
                    }
                }
            }

            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                connection.Close();
               
            }
        }

        private void TableButton_Click(object? sender, EventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into bill_list(items,table_name,date,type,status,total_amount) values(@Items,@Table,@Date,@Type,@Status,@Total)", connection);
                command.Parameters.AddWithValue("@Items",json);
                command.Parameters.AddWithValue("@Table", button.Text);
                command.Parameters.AddWithValue("@Date",DateTime.Now);
                command.Parameters.AddWithValue("@Type","Dine In");
                command.Parameters.AddWithValue("@Status","In Complete");
                command.Parameters.AddWithValue("@Total",total_amount);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Saved Successfully");
                    updatedString = "Added";
                }
                else {
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










    }
}
