namespace POS
{
    partial class SupplierForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupplierForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            Email_TextBox = new TextBox();
            label2 = new Label();
            status_label = new Label();
            browse_button = new Button();
            pictureBox1 = new PictureBox();
            category_label = new Label();
            Name_TextBox = new TextBox();
            label3 = new Label();
            panel4 = new Panel();
            save_button = new Button();
            cancel_button = new Button();
            Phone_TextBox = new TextBox();
            Address_TextBox = new TextBox();
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
            Title_label.Size = new Size(227, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Supplier";
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
            panel1.Size = new Size(659, 548);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(Address_TextBox);
            panel2.Controls.Add(Phone_TextBox);
            panel2.Controls.Add(Email_TextBox);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(status_label);
            panel2.Controls.Add(browse_button);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(category_label);
            panel2.Controls.Add(Name_TextBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(655, 545);
            panel2.TabIndex = 1;
            // 
            // Email_TextBox
            // 
            Email_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Email_TextBox.Location = new Point(104, 228);
            Email_TextBox.Multiline = true;
            Email_TextBox.Name = "Email_TextBox";
            Email_TextBox.Size = new Size(181, 30);
            Email_TextBox.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(100, 201);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 13;
            label2.Text = "Email";
            // 
            // status_label
            // 
            status_label.AutoSize = true;
            status_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            status_label.Location = new Point(100, 348);
            status_label.Name = "status_label";
            status_label.Size = new Size(62, 20);
            status_label.TabIndex = 11;
            status_label.Text = "Address";
            // 
            // browse_button
            // 
            browse_button.BackColor = Color.FromArgb(0, 119, 194);
            browse_button.FlatAppearance.BorderSize = 0;
            browse_button.FlatStyle = FlatStyle.Flat;
            browse_button.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            browse_button.ForeColor = Color.White;
            browse_button.Location = new Point(411, 330);
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
            pictureBox1.Location = new Point(401, 179);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 125);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // category_label
            // 
            category_label.AutoSize = true;
            category_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            category_label.Location = new Point(100, 275);
            category_label.Name = "category_label";
            category_label.Size = new Size(50, 20);
            category_label.TabIndex = 4;
            category_label.Text = "Phone";
            // 
            // Name_TextBox
            // 
            Name_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name_TextBox.Location = new Point(104, 151);
            Name_TextBox.Multiline = true;
            Name_TextBox.Name = "Name_TextBox";
            Name_TextBox.Size = new Size(181, 30);
            Name_TextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(100, 124);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 2;
            label3.Text = "Name";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 475);
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
            // Phone_TextBox
            // 
            Phone_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Phone_TextBox.Location = new Point(104, 298);
            Phone_TextBox.Multiline = true;
            Phone_TextBox.Name = "Phone_TextBox";
            Phone_TextBox.Size = new Size(181, 30);
            Phone_TextBox.TabIndex = 15;
            // 
            // Address_TextBox
            // 
            Address_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Address_TextBox.Location = new Point(104, 371);
            Address_TextBox.Multiline = true;
            Address_TextBox.Name = "Address_TextBox";
            Address_TextBox.Size = new Size(181, 82);
            Address_TextBox.TabIndex = 16;
            // 
            // SupplierForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 548);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SupplierForm";
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
        private TextBox Name_TextBox;
        private Label category_label;
        private Button browse_button;
        private PictureBox pictureBox1;
        private Label status_label;
        private TextBox Email_TextBox;
        private Label label2;
        private TextBox Address_TextBox;
        private TextBox Phone_TextBox;
    }
}