namespace POS
{
    partial class AccountsAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsAddForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            label2 = new Label();
            Access_ComboBox = new ComboBox();
            passwordTB = new TextBox();
            emailTB = new TextBox();
            usernameTB = new TextBox();
            AAFemail = new Label();
            AAFaccess = new Label();
            AAFpassword = new Label();
            AAFusername = new Label();
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
            panel3.Size = new Size(498, 104);
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
            Title_label.Size = new Size(137, 40);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add User";
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
            panel1.Size = new Size(502, 425);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(Access_ComboBox);
            panel2.Controls.Add(passwordTB);
            panel2.Controls.Add(emailTB);
            panel2.Controls.Add(usernameTB);
            panel2.Controls.Add(AAFemail);
            panel2.Controls.Add(AAFaccess);
            panel2.Controls.Add(AAFpassword);
            panel2.Controls.Add(AAFusername);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(498, 422);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(200, 176);
            label2.Name = "label2";
            label2.Size = new Size(265, 15);
            label2.TabIndex = 18;
            label2.Text = "Use the email submitted during staff registration.";
            // 
            // Access_ComboBox
            // 
            Access_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Access_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Access_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Access_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Access_ComboBox.FormattingEnabled = true;
            Access_ComboBox.IntegralHeight = false;
            Access_ComboBox.Location = new Point(242, 281);
            Access_ComboBox.Name = "Access_ComboBox";
            Access_ComboBox.Size = new Size(181, 28);
            Access_ComboBox.TabIndex = 17;
            // 
            // passwordTB
            // 
            passwordTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordTB.Location = new Point(242, 243);
            passwordTB.Multiline = true;
            passwordTB.Name = "passwordTB";
            passwordTB.Size = new Size(181, 30);
            passwordTB.TabIndex = 9;
            // 
            // emailTB
            // 
            emailTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            emailTB.Location = new Point(242, 207);
            emailTB.Multiline = true;
            emailTB.Name = "emailTB";
            emailTB.Size = new Size(181, 30);
            emailTB.TabIndex = 8;
            // 
            // usernameTB
            // 
            usernameTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameTB.Location = new Point(242, 143);
            usernameTB.Multiline = true;
            usernameTB.Name = "usernameTB";
            usernameTB.Size = new Size(181, 30);
            usernameTB.TabIndex = 7;
            // 
            // AAFemail
            // 
            AAFemail.AutoSize = true;
            AAFemail.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AAFemail.Location = new Point(69, 217);
            AAFemail.Name = "AAFemail";
            AAFemail.Size = new Size(75, 20);
            AAFemail.TabIndex = 6;
            AAFemail.Text = "Username";
            // 
            // AAFaccess
            // 
            AAFaccess.AutoSize = true;
            AAFaccess.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AAFaccess.Location = new Point(69, 289);
            AAFaccess.Name = "AAFaccess";
            AAFaccess.Size = new Size(128, 20);
            AAFaccess.TabIndex = 4;
            AAFaccess.Text = "Allowed access to";
            // 
            // AAFpassword
            // 
            AAFpassword.AutoSize = true;
            AAFpassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AAFpassword.Location = new Point(69, 253);
            AAFpassword.Name = "AAFpassword";
            AAFpassword.Size = new Size(70, 20);
            AAFpassword.TabIndex = 3;
            AAFpassword.Text = "Password";
            // 
            // AAFusername
            // 
            AAFusername.AutoSize = true;
            AAFusername.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AAFusername.Location = new Point(69, 153);
            AAFusername.Name = "AAFusername";
            AAFusername.RightToLeft = RightToLeft.No;
            AAFusername.Size = new Size(46, 20);
            AAFusername.TabIndex = 2;
            AAFusername.Text = "Email";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 352);
            panel4.Name = "panel4";
            panel4.Size = new Size(498, 70);
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
            save_button.Location = new Point(252, 15);
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
            cancel_button.Location = new Point(116, 15);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(130, 39);
            cancel_button.TabIndex = 0;
            cancel_button.Text = "Cancel";
            cancel_button.UseVisualStyleBackColor = false;
            cancel_button.Click += cancel_button_Click;
            // 
            // AccountsAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 425);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AccountsAddForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StaffCategoryForm";
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
        private Label AAFaccess;
        private Label AAFpassword;
        private Label AAFusername;
        private Label AAFemail;
        private TextBox emailTB;
        private TextBox usernameTB;
        private TextBox passwordTB;
        private ComboBox Access_ComboBox;
        private Label label2;
    }
}