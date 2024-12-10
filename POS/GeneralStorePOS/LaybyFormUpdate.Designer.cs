namespace POS
{
    partial class LaybyFormUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaybyFormUpdate));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            label3 = new Label();
            CurrentDepositTB = new TextBox();
            ClientNameTB = new TextBox();
            category_label = new Label();
            OutstandingAmountTB = new TextBox();
            TotalAmountTB = new TextBox();
            DepositTB = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
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
            Title_label.Size = new Size(260, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Layby Payment";
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
            panel1.Size = new Size(659, 459);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(CurrentDepositTB);
            panel2.Controls.Add(ClientNameTB);
            panel2.Controls.Add(category_label);
            panel2.Controls.Add(OutstandingAmountTB);
            panel2.Controls.Add(TotalAmountTB);
            panel2.Controls.Add(DepositTB);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(655, 456);
            panel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(373, 216);
            label3.Name = "label3";
            label3.Size = new Size(113, 20);
            label3.TabIndex = 30;
            label3.Text = "Current Deposit";
            // 
            // CurrentDepositTB
            // 
            CurrentDepositTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CurrentDepositTB.Location = new Point(373, 243);
            CurrentDepositTB.Multiline = true;
            CurrentDepositTB.Name = "CurrentDepositTB";
            CurrentDepositTB.Size = new Size(181, 30);
            CurrentDepositTB.TabIndex = 29;
            // 
            // ClientNameTB
            // 
            ClientNameTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ClientNameTB.Location = new Point(100, 164);
            ClientNameTB.Multiline = true;
            ClientNameTB.Name = "ClientNameTB";
            ClientNameTB.ReadOnly = true;
            ClientNameTB.Size = new Size(181, 30);
            ClientNameTB.TabIndex = 28;
            // 
            // category_label
            // 
            category_label.AutoSize = true;
            category_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            category_label.Location = new Point(100, 137);
            category_label.Name = "category_label";
            category_label.Size = new Size(91, 20);
            category_label.TabIndex = 22;
            category_label.Text = "Client Name";
            // 
            // OutstandingAmountTB
            // 
            OutstandingAmountTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            OutstandingAmountTB.Location = new Point(242, 328);
            OutstandingAmountTB.Multiline = true;
            OutstandingAmountTB.Name = "OutstandingAmountTB";
            OutstandingAmountTB.ReadOnly = true;
            OutstandingAmountTB.Size = new Size(181, 30);
            OutstandingAmountTB.TabIndex = 19;
            // 
            // TotalAmountTB
            // 
            TotalAmountTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TotalAmountTB.Location = new Point(373, 164);
            TotalAmountTB.Multiline = true;
            TotalAmountTB.Name = "TotalAmountTB";
            TotalAmountTB.ReadOnly = true;
            TotalAmountTB.Size = new Size(181, 30);
            TotalAmountTB.TabIndex = 19;
            // 
            // DepositTB
            // 
            DepositTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DepositTB.Location = new Point(100, 243);
            DepositTB.Multiline = true;
            DepositTB.Name = "DepositTB";
            DepositTB.ReadOnly = true;
            DepositTB.Size = new Size(181, 30);
            DepositTB.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(259, 305);
            label5.Name = "label5";
            label5.Size = new Size(147, 20);
            label5.TabIndex = 18;
            label5.Text = "Outstanding Amount";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(373, 137);
            label4.Name = "label4";
            label4.Size = new Size(99, 20);
            label4.TabIndex = 18;
            label4.Text = "Total Amount";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(100, 216);
            label2.Name = "label2";
            label2.Size = new Size(135, 20);
            label2.TabIndex = 18;
            label2.Text = "Deposited Amount";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 386);
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
            // LaybyFormUpdate
            // 
            AcceptButton = save_button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancel_button;
            ClientSize = new Size(659, 459);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LaybyFormUpdate";
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
        private TextBox DepositTB;
        private Label label2;
        private Label category_label;
        private TextBox OutstandingAmountTB;
        private TextBox TotalAmountTB;
        private Label label5;
        private Label label4;
        private TextBox ClientNameTB;
        private Label label3;
        private TextBox CurrentDepositTB;
    }
}