namespace POS
{
    partial class PurchaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            label6 = new Label();
            label7 = new Label();
            Unit_ComboBox = new ComboBox();
            Quantity_TextBox = new TextBox();
            PurchaseStatus_ComboBox = new ComboBox();
            label5 = new Label();
            PaymentStatus_ComboBox = new ComboBox();
            label2 = new Label();
            Supplier_ComboBox = new ComboBox();
            DueAmount_label = new Label();
            DueAmount_TextBox = new TextBox();
            label9 = new Label();
            Product_TextBox = new TextBox();
            PaidAmount_label = new Label();
            PaidAmount_TextBox = new TextBox();
            category_label = new Label();
            TotalAmount_TextBox = new TextBox();
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
            panel3.Size = new Size(655, 104);
            panel3.TabIndex = 0;
            // 
            // Title_label
            // 
            Title_label.AutoSize = true;
            Title_label.BackColor = Color.Transparent;
            Title_label.Font = new Font("Segoe UI Semibold", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Title_label.ForeColor = Color.White;
            Title_label.Location = new Point(163, 33);
            Title_label.Name = "Title_label";
            Title_label.Size = new Size(355, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Purchase Details";
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(86, 26);
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
            panel1.Size = new Size(659, 521);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(Unit_ComboBox);
            panel2.Controls.Add(Quantity_TextBox);
            panel2.Controls.Add(PurchaseStatus_ComboBox);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(PaymentStatus_ComboBox);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(Supplier_ComboBox);
            panel2.Controls.Add(DueAmount_label);
            panel2.Controls.Add(DueAmount_TextBox);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(Product_TextBox);
            panel2.Controls.Add(PaidAmount_label);
            panel2.Controls.Add(PaidAmount_TextBox);
            panel2.Controls.Add(category_label);
            panel2.Controls.Add(TotalAmount_TextBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(655, 518);
            panel2.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(217, 290);
            label6.Name = "label6";
            label6.Size = new Size(36, 20);
            label6.TabIndex = 25;
            label6.Text = "Unit";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(100, 288);
            label7.Name = "label7";
            label7.Size = new Size(65, 20);
            label7.TabIndex = 24;
            label7.Text = "Quantity";
            // 
            // Unit_ComboBox
            // 
            Unit_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Unit_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Unit_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Unit_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Unit_ComboBox.FormattingEnabled = true;
            Unit_ComboBox.IntegralHeight = false;
            Unit_ComboBox.Items.AddRange(new object[] { "kg", "litres", "piece", "dozen" });
            Unit_ComboBox.Location = new Point(215, 317);
            Unit_ComboBox.Name = "Unit_ComboBox";
            Unit_ComboBox.Size = new Size(70, 28);
            Unit_ComboBox.TabIndex = 23;
            // 
            // Quantity_TextBox
            // 
            Quantity_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Quantity_TextBox.Location = new Point(106, 315);
            Quantity_TextBox.Multiline = true;
            Quantity_TextBox.Name = "Quantity_TextBox";
            Quantity_TextBox.Size = new Size(103, 30);
            Quantity_TextBox.TabIndex = 22;
            Quantity_TextBox.Text = "1";
            Quantity_TextBox.KeyPress += Quantity_TextBox_KeyPress;
            // 
            // PurchaseStatus_ComboBox
            // 
            PurchaseStatus_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            PurchaseStatus_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            PurchaseStatus_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PurchaseStatus_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PurchaseStatus_ComboBox.FormattingEnabled = true;
            PurchaseStatus_ComboBox.IntegralHeight = false;
            PurchaseStatus_ComboBox.Items.AddRange(new object[] { "Ordered", "Received" });
            PurchaseStatus_ComboBox.Location = new Point(104, 394);
            PurchaseStatus_ComboBox.Name = "PurchaseStatus_ComboBox";
            PurchaseStatus_ComboBox.Size = new Size(181, 28);
            PurchaseStatus_ComboBox.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(102, 367);
            label5.Name = "label5";
            label5.Size = new Size(111, 20);
            label5.TabIndex = 18;
            label5.Text = "Purchase Status";
            // 
            // PaymentStatus_ComboBox
            // 
            PaymentStatus_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            PaymentStatus_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            PaymentStatus_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PaymentStatus_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PaymentStatus_ComboBox.FormattingEnabled = true;
            PaymentStatus_ComboBox.IntegralHeight = false;
            PaymentStatus_ComboBox.Items.AddRange(new object[] { "Pending", "Paid" });
            PaymentStatus_ComboBox.Location = new Point(372, 157);
            PaymentStatus_ComboBox.Name = "PaymentStatus_ComboBox";
            PaymentStatus_ComboBox.Size = new Size(181, 28);
            PaymentStatus_ComboBox.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(368, 130);
            label2.Name = "label2";
            label2.Size = new Size(109, 20);
            label2.TabIndex = 15;
            label2.Text = "Payment Status";
            // 
            // Supplier_ComboBox
            // 
            Supplier_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Supplier_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Supplier_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Supplier_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Supplier_ComboBox.FormattingEnabled = true;
            Supplier_ComboBox.IntegralHeight = false;
            Supplier_ComboBox.Location = new Point(104, 157);
            Supplier_ComboBox.Name = "Supplier_ComboBox";
            Supplier_ComboBox.Size = new Size(181, 28);
            Supplier_ComboBox.TabIndex = 10;
            // 
            // DueAmount_label
            // 
            DueAmount_label.AutoSize = true;
            DueAmount_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DueAmount_label.Location = new Point(368, 368);
            DueAmount_label.Name = "DueAmount_label";
            DueAmount_label.Size = new Size(93, 20);
            DueAmount_label.TabIndex = 4;
            DueAmount_label.Text = "Due Amount";
            DueAmount_label.Visible = false;
            // 
            // DueAmount_TextBox
            // 
            DueAmount_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DueAmount_TextBox.Location = new Point(371, 394);
            DueAmount_TextBox.Multiline = true;
            DueAmount_TextBox.Name = "DueAmount_TextBox";
            DueAmount_TextBox.ReadOnly = true;
            DueAmount_TextBox.Size = new Size(181, 30);
            DueAmount_TextBox.TabIndex = 3;
            DueAmount_TextBox.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(100, 210);
            label9.Name = "label9";
            label9.Size = new Size(60, 20);
            label9.TabIndex = 4;
            label9.Text = "Product";
            // 
            // Product_TextBox
            // 
            Product_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Product_TextBox.Location = new Point(103, 236);
            Product_TextBox.Multiline = true;
            Product_TextBox.Name = "Product_TextBox";
            Product_TextBox.Size = new Size(181, 30);
            Product_TextBox.TabIndex = 3;
            // 
            // PaidAmount_label
            // 
            PaidAmount_label.AutoSize = true;
            PaidAmount_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PaidAmount_label.Location = new Point(368, 290);
            PaidAmount_label.Name = "PaidAmount_label";
            PaidAmount_label.Size = new Size(94, 20);
            PaidAmount_label.TabIndex = 4;
            PaidAmount_label.Text = "Paid Amount";
            PaidAmount_label.Visible = false;
            // 
            // PaidAmount_TextBox
            // 
            PaidAmount_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PaidAmount_TextBox.Location = new Point(371, 316);
            PaidAmount_TextBox.Multiline = true;
            PaidAmount_TextBox.Name = "PaidAmount_TextBox";
            PaidAmount_TextBox.Size = new Size(181, 30);
            PaidAmount_TextBox.TabIndex = 3;
            PaidAmount_TextBox.Visible = false;
            PaidAmount_TextBox.TextChanged += PaidAmount_TextBox_TextChanged;
            PaidAmount_TextBox.KeyPress += PaidAmount_TextBox_KeyPress;
            // 
            // category_label
            // 
            category_label.AutoSize = true;
            category_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            category_label.Location = new Point(368, 210);
            category_label.Name = "category_label";
            category_label.Size = new Size(99, 20);
            category_label.TabIndex = 4;
            category_label.Text = "Total Amount";
            // 
            // TotalAmount_TextBox
            // 
            TotalAmount_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TotalAmount_TextBox.Location = new Point(371, 236);
            TotalAmount_TextBox.Multiline = true;
            TotalAmount_TextBox.Name = "TotalAmount_TextBox";
            TotalAmount_TextBox.Size = new Size(181, 30);
            TotalAmount_TextBox.TabIndex = 3;
            TotalAmount_TextBox.KeyPress += TotalAmount_TextBox_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(100, 130);
            label3.Name = "label3";
            label3.Size = new Size(64, 20);
            label3.TabIndex = 2;
            label3.Text = "Supplier";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 448);
            panel4.Name = "panel4";
            panel4.Size = new Size(655, 70);
            panel4.TabIndex = 1;
            // 
            // save_button
            // 
            save_button.BackColor = Color.Transparent;
            save_button.FlatAppearance.BorderColor = Color.FromArgb(0, 119, 194);
            save_button.FlatAppearance.BorderSize = 3;
            save_button.FlatAppearance.MouseDownBackColor = Color.White;
            save_button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            save_button.FlatStyle = FlatStyle.Flat;
            save_button.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            save_button.ForeColor = Color.Black;
            save_button.Location = new Point(325, 15);
            save_button.Name = "save_button";
            save_button.Size = new Size(130, 39);
            save_button.TabIndex = 1;
            save_button.Text = "Add";
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
            cancel_button.Location = new Point(189, 15);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(130, 39);
            cancel_button.TabIndex = 0;
            cancel_button.Text = "Cancel";
            cancel_button.UseVisualStyleBackColor = false;
            cancel_button.Click += cancel_button_Click;
            // 
            // PurchaseForm
            // 
            AcceptButton = save_button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancel_button;
            ClientSize = new Size(659, 521);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PurchaseForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StaffForm";
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
        private TextBox TotalAmount_TextBox;
        private Label category_label;
        private ComboBox Supplier_ComboBox;
        private ComboBox PaymentStatus_ComboBox;
        private Label label2;
        private ComboBox PurchaseStatus_ComboBox;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox Unit_ComboBox;
        private TextBox Quantity_TextBox;
        private Label PaidAmount_label;
        private TextBox PaidAmount_TextBox;
        private Label DueAmount_label;
        private TextBox DueAmount_TextBox;
        private Label label9;
        private TextBox Product_TextBox;
    }
}