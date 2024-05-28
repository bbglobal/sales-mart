namespace POS
{
    partial class PaymentMethodScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentMethodScreen));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            Change_TextBox = new TextBox();
            label6 = new Label();
            CashReceived_TextBox = new TextBox();
            label5 = new Label();
            NetAmount_TextBox = new TextBox();
            label4 = new Label();
            Discount_TextBox = new TextBox();
            label2 = new Label();
            BillAmount_TextBox = new TextBox();
            label3 = new Label();
            panel4 = new Panel();
            save_button = new Button();
            cancel_button = new Button();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackgroundImage = Properties.Resources.Sidebar_panel;
            panel3.BackgroundImageLayout = ImageLayout.Stretch;
            panel3.Controls.Add(Title_label);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(510, 104);
            panel3.TabIndex = 0;
            // 
            // Title_label
            // 
            Title_label.AutoSize = true;
            Title_label.BackColor = Color.Transparent;
            Title_label.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Title_label.ForeColor = Color.White;
            Title_label.Location = new Point(132, 33);
            Title_label.Name = "Title_label";
            Title_label.Size = new Size(229, 40);
            Title_label.TabIndex = 1;
            Title_label.Text = "Payment Details";
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(55, 26);
            label1.Name = "label1";
            label1.Size = new Size(60, 60);
            label1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(37, 150, 190);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(1, 0, 1, 1);
            panel1.Size = new Size(514, 424);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(Change_TextBox);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(CashReceived_TextBox);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(NetAmount_TextBox);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(Discount_TextBox);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(BillAmount_TextBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(510, 421);
            panel2.TabIndex = 1;
            // 
            // Change_TextBox
            // 
            Change_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Change_TextBox.Location = new Point(189, 259);
            Change_TextBox.Multiline = true;
            Change_TextBox.Name = "Change_TextBox";
            Change_TextBox.ReadOnly = true;
            Change_TextBox.Size = new Size(133, 30);
            Change_TextBox.TabIndex = 11;
            Change_TextBox.KeyPress += Change_TextBox_KeyPress;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(185, 232);
            label6.Name = "label6";
            label6.Size = new Size(59, 20);
            label6.TabIndex = 10;
            label6.Text = "Change";
            // 
            // CashReceived_TextBox
            // 
            CashReceived_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CashReceived_TextBox.Location = new Point(31, 259);
            CashReceived_TextBox.Multiline = true;
            CashReceived_TextBox.Name = "CashReceived_TextBox";
            CashReceived_TextBox.Size = new Size(133, 30);
            CashReceived_TextBox.TabIndex = 9;
            CashReceived_TextBox.TextChanged += CashReceived_TextBox_TextChanged;
            CashReceived_TextBox.KeyPress += CashReceived_TextBox_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(27, 232);
            label5.Name = "label5";
            label5.Size = new Size(104, 20);
            label5.TabIndex = 8;
            label5.Text = "Cash Received";
            // 
            // NetAmount_TextBox
            // 
            NetAmount_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NetAmount_TextBox.Location = new Point(349, 170);
            NetAmount_TextBox.Multiline = true;
            NetAmount_TextBox.Name = "NetAmount_TextBox";
            NetAmount_TextBox.ReadOnly = true;
            NetAmount_TextBox.Size = new Size(133, 30);
            NetAmount_TextBox.TabIndex = 7;
            NetAmount_TextBox.KeyPress += NetAmount_TextBox_KeyPress;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(345, 143);
            label4.Name = "label4";
            label4.Size = new Size(90, 20);
            label4.TabIndex = 6;
            label4.Text = "Net Amount";
            // 
            // Discount_TextBox
            // 
            Discount_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Discount_TextBox.Location = new Point(189, 170);
            Discount_TextBox.Multiline = true;
            Discount_TextBox.Name = "Discount_TextBox";
            Discount_TextBox.Size = new Size(133, 30);
            Discount_TextBox.TabIndex = 5;
            Discount_TextBox.TextChanged += Discount_TextBox_TextChanged;
            Discount_TextBox.KeyPress += Discount_TextBox_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(185, 143);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 4;
            label2.Text = "Discount(%)";
            // 
            // BillAmount_TextBox
            // 
            BillAmount_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BillAmount_TextBox.Location = new Point(31, 170);
            BillAmount_TextBox.Multiline = true;
            BillAmount_TextBox.Name = "BillAmount_TextBox";
            BillAmount_TextBox.ReadOnly = true;
            BillAmount_TextBox.Size = new Size(133, 30);
            BillAmount_TextBox.TabIndex = 3;
            BillAmount_TextBox.KeyPress += BillAmount_TextBox_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(27, 143);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 2;
            label3.Text = "Bill Amount";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 351);
            panel4.Name = "panel4";
            panel4.Size = new Size(510, 70);
            panel4.TabIndex = 1;
            // 
            // save_button
            // 
            save_button.BackColor = Color.White;
            save_button.FlatAppearance.BorderColor = Color.FromArgb(0, 119, 194);
            save_button.FlatAppearance.BorderSize = 3;
            save_button.FlatAppearance.MouseDownBackColor = Color.White;
            save_button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            save_button.FlatStyle = FlatStyle.Flat;
            save_button.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            save_button.ForeColor = Color.Black;
            save_button.Location = new Point(252, 15);
            save_button.Name = "save_button";
            save_button.Size = new Size(130, 39);
            save_button.TabIndex = 1;
            save_button.Text = "Save";
            save_button.UseVisualStyleBackColor = false;
            save_button.Click += save_button_Click;
            // 
            // cancel_button
            // 
            cancel_button.BackColor = Color.FromArgb(0, 119, 194);
            cancel_button.FlatAppearance.BorderSize = 0;
            cancel_button.FlatStyle = FlatStyle.Flat;
            cancel_button.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cancel_button.ForeColor = Color.White;
            cancel_button.Location = new Point(116, 15);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(130, 39);
            cancel_button.TabIndex = 0;
            cancel_button.Text = "Cancel";
            cancel_button.UseVisualStyleBackColor = false;
            cancel_button.Click += cancel_button_Click;
            // 
            // PaymentMethodScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 424);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PaymentMethodScreen";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StaffCategoryForm";
            Load += PaymentMethodScreen_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Label Title_label;
        private Panel panel4;
        private Button cancel_button;
        private Button save_button;
        private Label label3;
        private TextBox BillAmount_TextBox;
        private TextBox Change_TextBox;
        private Label label6;
        private TextBox CashReceived_TextBox;
        private Label label5;
        private TextBox NetAmount_TextBox;
        private Label label4;
        private TextBox Discount_TextBox;
        private Label label2;
    }
}