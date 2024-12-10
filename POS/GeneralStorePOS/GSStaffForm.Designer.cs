namespace POS
{
    partial class GSStaffForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GSStaffForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            Shift_ComboBox = new ComboBox();
            label5 = new Label();
            Phone_TextBox = new TextBox();
            Status_ComboBox = new ComboBox();
            label2 = new Label();
            Address_TextBox = new TextBox();
            label4 = new Label();
            status_label = new Label();
            Type_ComboBox = new ComboBox();
            category_label = new Label();
            StaffName_TextBox = new TextBox();
            label3 = new Label();
            panel4 = new Panel();
            save_button = new Button();
            cancel_button = new Button();
            label7 = new Label();
            emailTB = new TextBox();
            label6 = new Label();
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
            Title_label.Size = new Size(287, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Staff Details";
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
            panel2.Controls.Add(label7);
            panel2.Controls.Add(Shift_ComboBox);
            panel2.Controls.Add(emailTB);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(Phone_TextBox);
            panel2.Controls.Add(Status_ComboBox);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(Address_TextBox);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(status_label);
            panel2.Controls.Add(Type_ComboBox);
            panel2.Controls.Add(category_label);
            panel2.Controls.Add(StaffName_TextBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(655, 518);
            panel2.TabIndex = 1;
            // 
            // Shift_ComboBox
            // 
            Shift_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Shift_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Shift_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Shift_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Shift_ComboBox.FormattingEnabled = true;
            Shift_ComboBox.IntegralHeight = false;
            Shift_ComboBox.Items.AddRange(new object[] { "Morning", "Afternoon", "Evening" });
            Shift_ComboBox.Location = new Point(104, 394);
            Shift_ComboBox.Name = "Shift_ComboBox";
            Shift_ComboBox.Size = new Size(181, 28);
            Shift_ComboBox.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(102, 367);
            label5.Name = "label5";
            label5.Size = new Size(39, 20);
            label5.TabIndex = 18;
            label5.Text = "Shift";
            // 
            // Phone_TextBox
            // 
            Phone_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Phone_TextBox.Location = new Point(104, 315);
            Phone_TextBox.Multiline = true;
            Phone_TextBox.Name = "Phone_TextBox";
            Phone_TextBox.Size = new Size(181, 30);
            Phone_TextBox.TabIndex = 17;
            // 
            // Status_ComboBox
            // 
            Status_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Status_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Status_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Status_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Status_ComboBox.FormattingEnabled = true;
            Status_ComboBox.IntegralHeight = false;
            Status_ComboBox.Items.AddRange(new object[] { "Active", "Inactive" });
            Status_ComboBox.Location = new Point(375, 280);
            Status_ComboBox.Name = "Status_ComboBox";
            Status_ComboBox.Size = new Size(181, 28);
            Status_ComboBox.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(371, 253);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 15;
            label2.Text = "Status";
            // 
            // Address_TextBox
            // 
            Address_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Address_TextBox.Location = new Point(375, 164);
            Address_TextBox.Multiline = true;
            Address_TextBox.Name = "Address_TextBox";
            Address_TextBox.Size = new Size(181, 69);
            Address_TextBox.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(371, 137);
            label4.Name = "label4";
            label4.Size = new Size(62, 20);
            label4.TabIndex = 13;
            label4.Text = "Address";
            // 
            // status_label
            // 
            status_label.AutoSize = true;
            status_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            status_label.Location = new Point(100, 288);
            status_label.Name = "status_label";
            status_label.Size = new Size(50, 20);
            status_label.TabIndex = 11;
            status_label.Text = "Phone";
            // 
            // Type_ComboBox
            // 
            Type_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Type_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Type_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Type_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Type_ComboBox.FormattingEnabled = true;
            Type_ComboBox.IntegralHeight = false;
            Type_ComboBox.Location = new Point(104, 236);
            Type_ComboBox.Name = "Type_ComboBox";
            Type_ComboBox.Size = new Size(181, 28);
            Type_ComboBox.TabIndex = 10;
            // 
            // category_label
            // 
            category_label.AutoSize = true;
            category_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            category_label.Location = new Point(100, 209);
            category_label.Name = "category_label";
            category_label.Size = new Size(40, 20);
            category_label.TabIndex = 4;
            category_label.Text = "Type";
            // 
            // StaffName_TextBox
            // 
            StaffName_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StaffName_TextBox.Location = new Point(104, 157);
            StaffName_TextBox.Multiline = true;
            StaffName_TextBox.Name = "StaffName_TextBox";
            StaffName_TextBox.Size = new Size(181, 30);
            StaffName_TextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(100, 130);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 2;
            label3.Text = "Staff Name";
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
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.25F);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(325, 402);
            label7.Name = "label7";
            label7.Size = new Size(309, 17);
            label7.TabIndex = 25;
            label7.Text = "Your email will be used to create your user account.";
            // 
            // emailTB
            // 
            emailTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            emailTB.Location = new Point(375, 357);
            emailTB.Multiline = true;
            emailTB.Name = "emailTB";
            emailTB.Size = new Size(181, 30);
            emailTB.TabIndex = 24;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(375, 325);
            label6.Name = "label6";
            label6.Size = new Size(46, 20);
            label6.TabIndex = 23;
            label6.Text = "Email";
            // 
            // GSStaffForm
            // 
            AcceptButton = save_button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancel_button;
            ClientSize = new Size(659, 521);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GSStaffForm";
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
        private TextBox StaffName_TextBox;
        private Label category_label;
        private ComboBox Type_ComboBox;
        private Label status_label;
        private TextBox Phone_TextBox;
        private ComboBox Status_ComboBox;
        private Label label2;
        private TextBox Address_TextBox;
        private Label label4;
        private ComboBox Shift_ComboBox;
        private Label label5;
        private Label label7;
        private TextBox emailTB;
        private Label label6;
    }
}