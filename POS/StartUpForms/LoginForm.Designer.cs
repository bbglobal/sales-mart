﻿namespace POS
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            Category_ComboBox = new ComboBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(203, 284);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Email";
            textBox1.Size = new Size(389, 37);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(203, 347);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.PlaceholderText = "Password";
            textBox2.Size = new Size(389, 37);
            textBox2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.BackColor = Color.Red;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(203, 426);
            button1.Name = "button1";
            button1.Size = new Size(389, 37);
            button1.TabIndex = 2;
            button1.Text = "Sign In";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.None;
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.Transparent;
            linkLabel1.Font = new Font("MS Reference Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.ForeColor = Color.White;
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(203, 478);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(124, 16);
            linkLabel1.TabIndex = 3;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Forgot Password?";
            linkLabel1.Visible = false;
            // 
            // linkLabel2
            // 
            linkLabel2.Anchor = AnchorStyles.None;
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.Transparent;
            linkLabel2.Font = new Font("MS Reference Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel2.ForeColor = Color.White;
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkColor = Color.White;
            linkLabel2.Location = new Point(461, 478);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(131, 16);
            linkLabel2.TabIndex = 4;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Create an account";
            linkLabel2.Visible = false;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // Category_ComboBox
            // 
            Category_ComboBox.Anchor = AnchorStyles.None;
            Category_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            Category_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Category_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Category_ComboBox.FormattingEnabled = true;
            Category_ComboBox.IntegralHeight = false;
            Category_ComboBox.Items.AddRange(new object[] { "Restaurant POS", "General Store", "Retail Store", "Hotel Management" });
            Category_ComboBox.Location = new Point(203, 229);
            Category_ComboBox.Name = "Category_ComboBox";
            Category_ComboBox.Size = new Size(389, 28);
            Category_ComboBox.TabIndex = 11;
            Category_ComboBox.Text = "Module Select...";
            // 
            // LoginForm
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(798, 569);
            Controls.Add(Category_ComboBox);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            DoubleBuffered = true;
            MaximizeBox = false;
            Name = "LoginForm";
            Text = "Login Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private ComboBox Category_ComboBox;
    }
}