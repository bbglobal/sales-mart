﻿namespace POS
{
    partial class ItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            ExpiryDate_TextBox = new DateTimePicker();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            Unit_ComboBox = new ComboBox();
            Quantity_TextBox = new TextBox();
            ProductPrice_TextBox = new TextBox();
            label2 = new Label();
            Status_ComboBox = new ComboBox();
            status_label = new Label();
            Category_ComboBox = new ComboBox();
            browse_button = new Button();
            pictureBox1 = new PictureBox();
            category_label = new Label();
            ProductName_TextBox = new TextBox();
            label3 = new Label();
            panel4 = new Panel();
            save_button = new Button();
            cancel_button = new Button();
            label7 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            comboBox1 = new ComboBox();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            textBox3 = new TextBox();
            label11 = new Label();
            textBox4 = new TextBox();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            Title_label.Size = new Size(169, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Item";
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
            panel1.Size = new Size(659, 646);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(ExpiryDate_TextBox);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(Unit_ComboBox);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(Quantity_TextBox);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(ProductPrice_TextBox);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(Status_ComboBox);
            panel2.Controls.Add(status_label);
            panel2.Controls.Add(Category_ComboBox);
            panel2.Controls.Add(browse_button);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(category_label);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(ProductName_TextBox);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(655, 643);
            panel2.TabIndex = 1;
            // 
            // ExpiryDate_TextBox
            // 
            ExpiryDate_TextBox.Format = DateTimePickerFormat.Short;
            ExpiryDate_TextBox.Location = new Point(102, 523);
            ExpiryDate_TextBox.Name = "ExpiryDate_TextBox";
            ExpiryDate_TextBox.Size = new Size(181, 23);
            ExpiryDate_TextBox.TabIndex = 21;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(98, 494);
            label6.Name = "label6";
            label6.Size = new Size(85, 20);
            label6.TabIndex = 20;
            label6.Text = "Expiry Date";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(136, 202);
            label5.Name = "label5";
            label5.Size = new Size(36, 20);
            label5.TabIndex = 19;
            label5.Text = "Unit";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(98, 201);
            label4.Name = "label4";
            label4.Size = new Size(32, 20);
            label4.TabIndex = 18;
            label4.Text = "Qty";
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
            Unit_ComboBox.Location = new Point(134, 229);
            Unit_ComboBox.Name = "Unit_ComboBox";
            Unit_ComboBox.Size = new Size(70, 28);
            Unit_ComboBox.TabIndex = 17;
            // 
            // Quantity_TextBox
            // 
            Quantity_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Quantity_TextBox.Location = new Point(104, 228);
            Quantity_TextBox.Multiline = true;
            Quantity_TextBox.Name = "Quantity_TextBox";
            Quantity_TextBox.Size = new Size(28, 30);
            Quantity_TextBox.TabIndex = 15;
            Quantity_TextBox.Text = "1";
            // 
            // ProductPrice_TextBox
            // 
            ProductPrice_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProductPrice_TextBox.Location = new Point(206, 228);
            ProductPrice_TextBox.Multiline = true;
            ProductPrice_TextBox.Name = "ProductPrice_TextBox";
            ProductPrice_TextBox.Size = new Size(79, 30);
            ProductPrice_TextBox.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(201, 201);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 13;
            label2.Text = "Cost Price";
            // 
            // Status_ComboBox
            // 
            Status_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Status_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Status_ComboBox.FormattingEnabled = true;
            Status_ComboBox.IntegralHeight = false;
            Status_ComboBox.Items.AddRange(new object[] { "Active", "Inactive" });
            Status_ComboBox.Location = new Point(381, 227);
            Status_ComboBox.Name = "Status_ComboBox";
            Status_ComboBox.Size = new Size(181, 28);
            Status_ComboBox.TabIndex = 12;
            // 
            // status_label
            // 
            status_label.AutoSize = true;
            status_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            status_label.Location = new Point(377, 200);
            status_label.Name = "status_label";
            status_label.Size = new Size(49, 20);
            status_label.TabIndex = 11;
            status_label.Text = "Status";
            // 
            // Category_ComboBox
            // 
            Category_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Category_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Category_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Category_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Category_ComboBox.FormattingEnabled = true;
            Category_ComboBox.IntegralHeight = false;
            Category_ComboBox.Location = new Point(381, 151);
            Category_ComboBox.Name = "Category_ComboBox";
            Category_ComboBox.Size = new Size(181, 28);
            Category_ComboBox.TabIndex = 10;
            // 
            // browse_button
            // 
            browse_button.BackColor = Color.FromArgb(0, 119, 194);
            browse_button.FlatAppearance.BorderSize = 0;
            browse_button.FlatStyle = FlatStyle.Flat;
            browse_button.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            browse_button.ForeColor = Color.White;
            browse_button.Location = new Point(411, 470);
            browse_button.Name = "browse_button";
            browse_button.Size = new Size(103, 38);
            browse_button.TabIndex = 9;
            browse_button.Text = "Browse";
            browse_button.UseVisualStyleBackColor = false;
            browse_button.Click += browse_button_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(401, 319);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 125);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // category_label
            // 
            category_label.AutoSize = true;
            category_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            category_label.Location = new Point(377, 124);
            category_label.Name = "category_label";
            category_label.Size = new Size(69, 20);
            category_label.TabIndex = 4;
            category_label.Text = "Category";
            // 
            // ProductName_TextBox
            // 
            ProductName_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProductName_TextBox.Location = new Point(104, 151);
            ProductName_TextBox.Multiline = true;
            ProductName_TextBox.Name = "ProductName_TextBox";
            ProductName_TextBox.Size = new Size(181, 30);
            ProductName_TextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(100, 124);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 2;
            label3.Text = "Item Name";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 573);
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
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(199, 277);
            label7.Name = "label7";
            label7.Size = new Size(90, 20);
            label7.TabIndex = 13;
            label7.Text = "Selling Price";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(204, 304);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(79, 30);
            textBox1.TabIndex = 14;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(102, 304);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(28, 30);
            textBox2.TabIndex = 15;
            textBox2.Text = "1";
            // 
            // comboBox1
            // 
            comboBox1.AutoCompleteMode = AutoCompleteMode.Append;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Items.AddRange(new object[] { "kg", "litres", "piece", "dozen" });
            comboBox1.Location = new Point(132, 305);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(70, 28);
            comboBox1.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(96, 277);
            label8.Name = "label8";
            label8.Size = new Size(32, 20);
            label8.TabIndex = 18;
            label8.Text = "Qty";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(134, 278);
            label9.Name = "label9";
            label9.Size = new Size(36, 20);
            label9.TabIndex = 19;
            label9.Text = "Unit";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(98, 351);
            label10.Name = "label10";
            label10.Size = new Size(154, 20);
            label10.TabIndex = 2;
            label10.Text = "Selling Price (Incl. tax)";
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(102, 378);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(181, 30);
            textBox3.TabIndex = 3;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(96, 420);
            label11.Name = "label11";
            label11.Size = new Size(64, 20);
            label11.TabIndex = 2;
            label11.Text = "Barcode";
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox4.Location = new Point(100, 447);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(181, 30);
            textBox4.TabIndex = 3;
            // 
            // ItemsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 646);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ItemsForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ProductsForm";
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private TextBox ProductName_TextBox;
        private Label category_label;
        private Button browse_button;
        private PictureBox pictureBox1;
        private ComboBox Category_ComboBox;
        private ComboBox Status_ComboBox;
        private Label status_label;
        private TextBox ProductPrice_TextBox;
        private Label label2;
        private Label label5;
        private Label label4;
        private ComboBox Unit_ComboBox;
        private TextBox Quantity_TextBox;
        private DateTimePicker ExpiryDate_TextBox;
        private Label label6;
        private Label label9;
        private Label label8;
        private ComboBox comboBox1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label7;
        private TextBox textBox4;
        private TextBox textBox3;
        private Label label11;
        private Label label10;
    }
}