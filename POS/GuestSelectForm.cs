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
    public partial class GuestSelectForm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        public int? SelectedGuestId { get; private set; }

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));

        public GuestSelectForm()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

            LoadGuests();

            // Select the first row if rows exist
            if (GuestSelectDataGrid.Rows.Count > 0)
            {
                GuestSelectDataGrid.Rows[0].Selected = true;
            }
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
            if (label2.Text.StartsWith("Selected Guest ID: "))
            {
                string guestIdText = label2.Text.Replace("Selected Guest ID: ", "").Trim();

                if (int.TryParse(guestIdText, out int guestId))
                {
                    SelectedGuestId = guestId;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Guest ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a guest before saving.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void LoadGuests()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
            string query = "SELECT GuestID, GuestName, RoomNumber FROM Guests";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    GuestSelectDataGrid.DataSource = dataTable;
                    GuestSelectDataGrid.Columns["GuestID"].HeaderText = "Guest ID";
                    GuestSelectDataGrid.Columns["GuestName"].HeaderText = "Name";
                    GuestSelectDataGrid.Columns["RoomNumber"].HeaderText = "Room";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GuestSelectDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && GuestSelectDataGrid.Rows[e.RowIndex].Cells["GuestID"].Value != null)
            {
                int selectedGuestId = Convert.ToInt32(GuestSelectDataGrid.Rows[e.RowIndex].Cells["GuestID"].Value);
                label2.Text = $"Selected Guest ID: {selectedGuestId}";
            }
        }
    }
}
