using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class TableCustomerInfo : Form
    {
        public string CustomerName { get; private set; }
        public string PhoneNumber { get; private set; }

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));

        public TableCustomerInfo()
        {
            InitializeComponent();
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);

            // Restrict Phone_TextBox to numeric input only
            Phone_TextBox.KeyPress += Phone_TextBox_KeyPress;
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

        private void SaveData()
        {
            // Check for empty fields and confirm if user wants to proceed
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) || string.IsNullOrWhiteSpace(Phone_TextBox.Text))
            {
                string missingField = string.IsNullOrWhiteSpace(Name_TextBox.Text) ? "Name" : "Phone Number";
                var result = MessageBox.Show($"{missingField} is not provided. Do you want to proceed without it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return; // Stop if the user chooses not to proceed
                }
            }

            // Set CustomerName and PhoneNumber properties
            CustomerName = Name_TextBox.Text;
            PhoneNumber = Phone_TextBox.Text;

            // Set dialog result to OK and close the form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Phone_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
