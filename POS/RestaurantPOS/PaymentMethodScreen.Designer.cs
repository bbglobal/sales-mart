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
            label11 = new Label();
            label7 = new Label();
            UseCredit = new TextBox();
            AvailableCreditCB = new CheckBox();
            CustomerPointsCB = new CheckBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            CustomerNameLabel = new Label();
            CustomerIDTB = new TextBox();
            CustomerCreditTB = new TextBox();
            CustomerNameTB = new TextBox();
            CustomerPointsTB = new TextBox();
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
            panel3.Size = new Size(543, 104);
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
            panel1.Size = new Size(547, 518);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(UseCredit);
            panel2.Controls.Add(AvailableCreditCB);
            panel2.Controls.Add(CustomerPointsCB);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(CustomerNameLabel);
            panel2.Controls.Add(CustomerIDTB);
            panel2.Controls.Add(CustomerCreditTB);
            panel2.Controls.Add(CustomerNameTB);
            panel2.Controls.Add(CustomerPointsTB);
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
            panel2.Size = new Size(543, 515);
            panel2.TabIndex = 1;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(422, 250);
            label11.Name = "label11";
            label11.Size = new Size(62, 20);
            label11.TabIndex = 24;
            label11.Text = "Useable";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(345, 230);
            label7.Name = "label7";
            label7.Size = new Size(71, 20);
            label7.TabIndex = 23;
            label7.Text = "Available";
            // 
            // UseCredit
            // 
            UseCredit.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UseCredit.Location = new Point(422, 273);
            UseCredit.Multiline = true;
            UseCredit.Name = "UseCredit";
            UseCredit.ReadOnly = true;
            UseCredit.Size = new Size(60, 30);
            UseCredit.TabIndex = 22;
            UseCredit.Text = "0";
            // 
            // AvailableCreditCB
            // 
            AvailableCreditCB.AutoSize = true;
            AvailableCreditCB.Location = new Point(437, 309);
            AvailableCreditCB.Name = "AvailableCreditCB";
            AvailableCreditCB.Size = new Size(45, 19);
            AvailableCreditCB.TabIndex = 21;
            AvailableCreditCB.Text = "Use";
            AvailableCreditCB.UseVisualStyleBackColor = true;
            AvailableCreditCB.CheckedChanged += AvailableCreditCB_CheckedChanged;
            // 
            // CustomerPointsCB
            // 
            CustomerPointsCB.AutoSize = true;
            CustomerPointsCB.Location = new Point(437, 403);
            CustomerPointsCB.Name = "CustomerPointsCB";
            CustomerPointsCB.Size = new Size(45, 19);
            CustomerPointsCB.TabIndex = 20;
            CustomerPointsCB.Text = "Use";
            CustomerPointsCB.UseVisualStyleBackColor = true;
            CustomerPointsCB.CheckedChanged += CustomerPointsCB_CheckedChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(349, 344);
            label10.Name = "label10";
            label10.Size = new Size(48, 20);
            label10.TabIndex = 19;
            label10.Text = "Points";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(349, 250);
            label9.Name = "label9";
            label9.Size = new Size(49, 20);
            label9.TabIndex = 18;
            label9.Text = "Credit";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(189, 250);
            label8.Name = "label8";
            label8.Size = new Size(91, 20);
            label8.TabIndex = 17;
            label8.Text = "Customer ID";
            // 
            // CustomerNameLabel
            // 
            CustomerNameLabel.AutoSize = true;
            CustomerNameLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CustomerNameLabel.Location = new Point(31, 250);
            CustomerNameLabel.Name = "CustomerNameLabel";
            CustomerNameLabel.Size = new Size(116, 20);
            CustomerNameLabel.TabIndex = 16;
            CustomerNameLabel.Text = "Customer Name";
            // 
            // CustomerIDTB
            // 
            CustomerIDTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CustomerIDTB.Location = new Point(189, 273);
            CustomerIDTB.Multiline = true;
            CustomerIDTB.Name = "CustomerIDTB";
            CustomerIDTB.ReadOnly = true;
            CustomerIDTB.Size = new Size(133, 30);
            CustomerIDTB.TabIndex = 15;
            // 
            // CustomerCreditTB
            // 
            CustomerCreditTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CustomerCreditTB.Location = new Point(349, 273);
            CustomerCreditTB.Multiline = true;
            CustomerCreditTB.Name = "CustomerCreditTB";
            CustomerCreditTB.ReadOnly = true;
            CustomerCreditTB.Size = new Size(60, 30);
            CustomerCreditTB.TabIndex = 14;
            CustomerCreditTB.Text = "0";
            // 
            // CustomerNameTB
            // 
            CustomerNameTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CustomerNameTB.Location = new Point(31, 273);
            CustomerNameTB.Multiline = true;
            CustomerNameTB.Name = "CustomerNameTB";
            CustomerNameTB.ReadOnly = true;
            CustomerNameTB.Size = new Size(133, 30);
            CustomerNameTB.TabIndex = 13;
            // 
            // CustomerPointsTB
            // 
            CustomerPointsTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CustomerPointsTB.Location = new Point(349, 367);
            CustomerPointsTB.Multiline = true;
            CustomerPointsTB.Name = "CustomerPointsTB";
            CustomerPointsTB.ReadOnly = true;
            CustomerPointsTB.Size = new Size(133, 30);
            CustomerPointsTB.TabIndex = 12;
            CustomerPointsTB.Text = "0";
            // 
            // Change_TextBox
            // 
            Change_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Change_TextBox.Location = new Point(189, 367);
            Change_TextBox.Multiline = true;
            Change_TextBox.Name = "Change_TextBox";
            Change_TextBox.ReadOnly = true;
            Change_TextBox.Size = new Size(133, 30);
            Change_TextBox.TabIndex = 11;
            Change_TextBox.Text = "0";
            Change_TextBox.KeyPress += Change_TextBox_KeyPress;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(185, 344);
            label6.Name = "label6";
            label6.Size = new Size(59, 20);
            label6.TabIndex = 10;
            label6.Text = "Change";
            // 
            // CashReceived_TextBox
            // 
            CashReceived_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CashReceived_TextBox.Location = new Point(27, 367);
            CashReceived_TextBox.Multiline = true;
            CashReceived_TextBox.Name = "CashReceived_TextBox";
            CashReceived_TextBox.Size = new Size(133, 30);
            CashReceived_TextBox.TabIndex = 9;
            CashReceived_TextBox.Text = "0";
            CashReceived_TextBox.TextChanged += CashReceived_TextBox_TextChanged;
            CashReceived_TextBox.KeyPress += CashReceived_TextBox_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(27, 344);
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
            Discount_TextBox.Text = "0";
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
            panel4.Location = new Point(0, 445);
            panel4.Name = "panel4";
            panel4.Size = new Size(543, 70);
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
            ClientSize = new Size(547, 518);
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
        private TextBox CustomerPointsTB;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label CustomerNameLabel;
        private TextBox CustomerIDTB;
        private TextBox CustomerCreditTB;
        private TextBox CustomerNameTB;
        private CheckBox AvailableCreditCB;
        private CheckBox CustomerPointsCB;
        private TextBox UseCredit;
        private Label label11;
        private Label label7;
    }
}