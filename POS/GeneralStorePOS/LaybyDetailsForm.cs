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
    public partial class LaybyDetailsForm : Form
    {
        private int rowIndex;
        private string client;
        private decimal total;
        private decimal paid;
        private decimal due;
        SqlConnection connection;
        SqlCommand command;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
        public LaybyDetailsForm(string client, decimal total, decimal paid, decimal due, int rowIndex = -1)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            //SetTypeComboBox();
            this.rowIndex = rowIndex;
            this.client = client;
            this.total = total;
            this.paid = paid;
            this.due = due;
            LaybyNoTextBox.Text = rowIndex.ToString();
            ClientTextBox.Text = client;
            TotalTextBox.Text = total.ToString();
            PaidTextBox.Text = paid.ToString();
            DueTextBox.Text = due.ToString();
            SampleData();
            if (this.rowIndex != -1)
            {
                Title_label.Text = "Layby Details";
                //save_button.Text = "Save";
                //SetFields(this.rowIndex);

            }

            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

        }

        private void SampleData()
        {
            DataTable laybyTable = new DataTable();

            // Define columns
            laybyTable.Columns.Add("S No.", typeof(int));
            laybyTable.Columns.Add("Payment Date", typeof(DateTime));
            laybyTable.Columns.Add("Deposit", typeof(decimal));

            try
            {
                // Define the query to fetch payment details based on Layby No. from the 'layby' table
                string query = @"
            SELECT 
                ROW_NUMBER() OVER (ORDER BY payment_date) AS [S No.],
                payment_date AS [Payment Date],
                deposit AS [Deposit]
            FROM layby
            WHERE layby_no = @LaybyNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LaybyNo", rowIndex);  // Pass the LaybyNo parameter

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(laybyTable); // Fill the DataTable with query results
                }

                // Bind the data to the DataGridView
                LayByDetailsDataGrid.DataSource = laybyTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching payment details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDatabaseConnection()
        {
            if (Session.BranchCode == "PK728")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            else if (Session.BranchCode == "BR001")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;
                connection = new SqlConnection(connectionString);
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



        private void save_button_Click(object sender, EventArgs e)
        {
            //SaveData();
        }

        private void LayByDetailsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
