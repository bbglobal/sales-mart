﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS
{
    public partial class PaymentMethodScreen : Form
    {
        private string statusUpdated = "";
        private int rowIndex;
        SqlConnection connection;
        SqlCommand command;
        System.Windows.Forms.TextBox targetTextBox;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        private int total_amount;
        public PaymentMethodScreen(int total_amount, int rowIndex)
        {

            InitializeComponent();
            InitializeDatabaseConnection();
            this.total_amount = total_amount;
            this.rowIndex = rowIndex;
            SetFields(this.total_amount);
            InitializeLabel(label1, (Image)resources.GetObject("label1.Image"), 45, 60);
            foreach (Control control in panel2.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.Enter += TextBox_Enter;
                }
            }

        }

        public string StatusUpdated
        { 
            get { return statusUpdated; }
        }



        private void TextBox_Enter(object? sender, EventArgs e)
        {
            targetTextBox = (System.Windows.Forms.TextBox)sender;
            targetTextBox.SelectionStart = targetTextBox.Text.Length;
            if (Nk.Visible == false)
            {
                Nk.Show();
            }
            //else
            //{
            //    Nk.BringToFront();
            //}
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }


        private void SaveData()
        {
            //if (BillAmount_TextBox.Text == "")
            //{
            //    MessageBox.Show("Please fill the field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            try
            {
                connection.Open();
                string query = "UPDATE bill_list SET status=@Status WHERE bill_id=@Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", "Paid");
                    command.Parameters.AddWithValue("@Id", rowIndex);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        statusUpdated = "Updated";
                        this.Close();
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
            BillAmount_TextBox.Text = rowNo.ToString();
            NetAmount_TextBox.Text = BillAmount_TextBox.Text;
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
            SaveData();
        }

        private void BillAmount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Discount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void NetAmount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void CashReceived_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Change_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }


        NumberKeypad Nk;
        private void PaymentMethodScreen_Load(object sender, EventArgs e)
        {
            int x = this.Location.X + cancel_button.Location.X;
            int y = this.Location.Y + Change_TextBox.Location.Y + Change_TextBox.Height + 20;
            Nk = new NumberKeypad(x, y);
            Nk.NumberButtonPressed += Nk_NumberButtonPressed;
        }

        private void Nk_NumberButtonPressed(object sender, int number)
        {
            if (number == -1)
            {
                if (targetTextBox.Text.Length > 0)
                {
                    targetTextBox.Text = targetTextBox.Text.Substring(0, targetTextBox.Text.Length - 1);
                    targetTextBox.SelectionStart = targetTextBox.Text.Length;
                }
            }
            else if (number == -2)
            {
                targetTextBox.Text = "";
                targetTextBox.SelectionStart = targetTextBox.Text.Length;
            }
            else if (number == -3)
            {
                targetTextBox.Text += ".";
                targetTextBox.SelectionStart = targetTextBox.Text.Length;
            }
            else
            {
                targetTextBox.Text += number.ToString();
                targetTextBox.SelectionStart = targetTextBox.Text.Length;
            }
        }

        private void Discount_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (Discount_TextBox.Text != "")
            {
                int bill = Convert.ToInt32(BillAmount_TextBox.Text);
                int disc = Convert.ToInt32(Discount_TextBox.Text);
                if (disc <= bill)
                {
                    NetAmount_TextBox.Text = (bill - disc).ToString();
                }
                else 
                {
                    MessageBox.Show("Discount can't be greater than the Bill Amount","Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    Discount_TextBox.Text = "";
                    NetAmount_TextBox.Text = BillAmount_TextBox.Text;
                }

            }
            else
            {
                NetAmount_TextBox.Text = BillAmount_TextBox.Text;
            }
            if (CashReceived_TextBox.Text != "" && Change_TextBox.Text != "")
            {
                UpdateCashAndChangeTextBox();
            }
            
        }

            private void CashReceived_TextBox_TextChanged(object sender, EventArgs e)
            {
                UpdateCashAndChangeTextBox();
            }

        private void UpdateCashAndChangeTextBox() 
        {
            if (CashReceived_TextBox.Text != "")
            {
                int net = Convert.ToInt32(NetAmount_TextBox.Text);
                int cash = Convert.ToInt32(CashReceived_TextBox.Text);
                if (cash > net)
                {
                    Change_TextBox.Text = (cash - net).ToString();
                }
                else if (cash == net)
                {
                    Change_TextBox.Text = "0";
                }
                else
                {
                    Change_TextBox.Text = "";
                }
            }
        }
    }
}