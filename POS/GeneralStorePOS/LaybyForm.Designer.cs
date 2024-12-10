namespace POS
{
    partial class LaybyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaybyForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            ExpiryDateTB = new DateTimePicker();
            label6 = new Label();
            PaymentScheduleComboBox = new ComboBox();
            label3 = new Label();
            ClientNameComboBox = new ComboBox();
            category_label = new Label();
            OutstandingAmountTB = new TextBox();
            TotalAmountTB = new TextBox();
            DepositTB = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            DurationTB = new TextBox();
            status_label = new Label();
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
            Title_label.Size = new Size(188, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Layby";
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
            panel2.Controls.Add(ExpiryDateTB);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(PaymentScheduleComboBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(ClientNameComboBox);
            panel2.Controls.Add(category_label);
            panel2.Controls.Add(OutstandingAmountTB);
            panel2.Controls.Add(TotalAmountTB);
            panel2.Controls.Add(DepositTB);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(DurationTB);
            panel2.Controls.Add(status_label);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(655, 456);
            panel2.TabIndex = 1;
            // 
            // ExpiryDateTB
            // 
            ExpiryDateTB.Format = DateTimePickerFormat.Short;
            ExpiryDateTB.Location = new Point(381, 323);
            ExpiryDateTB.Name = "ExpiryDateTB";
            ExpiryDateTB.Size = new Size(181, 23);
            ExpiryDateTB.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(377, 294);
            label6.Name = "label6";
            label6.Size = new Size(85, 20);
            label6.TabIndex = 26;
            label6.Text = "Expiry Date";
            // 
            // PaymentScheduleComboBox
            // 
            PaymentScheduleComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            PaymentScheduleComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            PaymentScheduleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PaymentScheduleComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PaymentScheduleComboBox.FormattingEnabled = true;
            PaymentScheduleComboBox.IntegralHeight = false;
            PaymentScheduleComboBox.Items.AddRange(new object[] { "Daily", "Weekly", "Monthly" });
            PaymentScheduleComboBox.Location = new Point(106, 322);
            PaymentScheduleComboBox.Name = "PaymentScheduleComboBox";
            PaymentScheduleComboBox.Size = new Size(119, 28);
            PaymentScheduleComboBox.TabIndex = 25;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(100, 294);
            label3.Name = "label3";
            label3.Size = new Size(129, 20);
            label3.TabIndex = 24;
            label3.Text = "Payment Schedule";
            // 
            // ClientNameComboBox
            // 
            ClientNameComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            ClientNameComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            ClientNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ClientNameComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ClientNameComboBox.FormattingEnabled = true;
            ClientNameComboBox.IntegralHeight = false;
            ClientNameComboBox.Location = new Point(104, 164);
            ClientNameComboBox.Name = "ClientNameComboBox";
            ClientNameComboBox.Size = new Size(181, 28);
            ClientNameComboBox.TabIndex = 23;
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
            OutstandingAmountTB.Location = new Point(377, 243);
            OutstandingAmountTB.Multiline = true;
            OutstandingAmountTB.Name = "OutstandingAmountTB";
            OutstandingAmountTB.Size = new Size(181, 30);
            OutstandingAmountTB.TabIndex = 19;
            // 
            // TotalAmountTB
            // 
            TotalAmountTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TotalAmountTB.Location = new Point(377, 164);
            TotalAmountTB.Multiline = true;
            TotalAmountTB.Name = "TotalAmountTB";
            TotalAmountTB.Size = new Size(181, 30);
            TotalAmountTB.TabIndex = 19;
            // 
            // DepositTB
            // 
            DepositTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DepositTB.Location = new Point(104, 243);
            DepositTB.Multiline = true;
            DepositTB.Name = "DepositTB";
            DepositTB.Size = new Size(181, 30);
            DepositTB.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(373, 216);
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
            label2.Size = new Size(61, 20);
            label2.TabIndex = 18;
            label2.Text = "Deposit";
            // 
            // DurationTB
            // 
            DurationTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DurationTB.Location = new Point(236, 321);
            DurationTB.Multiline = true;
            DurationTB.Name = "DurationTB";
            DurationTB.Size = new Size(54, 30);
            DurationTB.TabIndex = 17;
            // 
            // status_label
            // 
            status_label.AutoSize = true;
            status_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            status_label.Location = new Point(229, 294);
            status_label.Name = "status_label";
            status_label.Size = new Size(67, 20);
            status_label.TabIndex = 11;
            status_label.Text = "Duration";
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
            // LaybyForm
            // 
            AcceptButton = save_button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancel_button;
            ClientSize = new Size(659, 459);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LaybyForm";
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
        private Label status_label;
        private TextBox DurationTB;
        private TextBox DepositTB;
        private Label label2;
        private ComboBox ClientNameComboBox;
        private Label category_label;
        private ComboBox PaymentScheduleComboBox;
        private Label label3;
        private DateTimePicker ExpiryDateTB;
        private Label label6;
        private TextBox OutstandingAmountTB;
        private TextBox TotalAmountTB;
        private Label label5;
        private Label label4;
    }
}