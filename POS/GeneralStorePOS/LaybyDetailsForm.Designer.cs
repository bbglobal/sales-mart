namespace POS
{
    partial class LaybyDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaybyDetailsForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            DueTextBox = new TextBox();
            label6 = new Label();
            PaidTextBox = new TextBox();
            label3 = new Label();
            LayByDetailsDataGrid = new DataGridView();
            TotalTextBox = new TextBox();
            ClientTextBox = new TextBox();
            LaybyNoTextBox = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            panel4 = new Panel();
            cancel_button = new Button();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LayByDetailsDataGrid).BeginInit();
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
            panel3.Size = new Size(729, 104);
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
            Title_label.Size = new Size(230, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Layby Details";
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
            panel1.Size = new Size(733, 567);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(DueTextBox);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(PaidTextBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(LayByDetailsDataGrid);
            panel2.Controls.Add(TotalTextBox);
            panel2.Controls.Add(ClientTextBox);
            panel2.Controls.Add(LaybyNoTextBox);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(729, 564);
            panel2.TabIndex = 1;
            // 
            // DueTextBox
            // 
            DueTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DueTextBox.Location = new Point(547, 443);
            DueTextBox.Multiline = true;
            DueTextBox.Name = "DueTextBox";
            DueTextBox.ReadOnly = true;
            DueTextBox.Size = new Size(129, 30);
            DueTextBox.TabIndex = 24;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(388, 446);
            label6.Name = "label6";
            label6.Size = new Size(147, 20);
            label6.TabIndex = 23;
            label6.Text = "Outstanding Amount";
            // 
            // PaidTextBox
            // 
            PaidTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PaidTextBox.Location = new Point(547, 402);
            PaidTextBox.Multiline = true;
            PaidTextBox.Name = "PaidTextBox";
            PaidTextBox.ReadOnly = true;
            PaidTextBox.Size = new Size(129, 30);
            PaidTextBox.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(441, 405);
            label3.Name = "label3";
            label3.Size = new Size(94, 20);
            label3.TabIndex = 21;
            label3.Text = "Paid Amount";
            // 
            // LayByDetailsDataGrid
            // 
            LayByDetailsDataGrid.AllowUserToAddRows = false;
            LayByDetailsDataGrid.AllowUserToDeleteRows = false;
            LayByDetailsDataGrid.AllowUserToResizeColumns = false;
            LayByDetailsDataGrid.AllowUserToResizeRows = false;
            LayByDetailsDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LayByDetailsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LayByDetailsDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            LayByDetailsDataGrid.BackgroundColor = Color.White;
            LayByDetailsDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            LayByDetailsDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(246, 247, 252);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            LayByDetailsDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            LayByDetailsDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            LayByDetailsDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            LayByDetailsDataGrid.EnableHeadersVisualStyles = false;
            LayByDetailsDataGrid.Location = new Point(57, 223);
            LayByDetailsDataGrid.Margin = new Padding(0);
            LayByDetailsDataGrid.Name = "LayByDetailsDataGrid";
            LayByDetailsDataGrid.ReadOnly = true;
            LayByDetailsDataGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            LayByDetailsDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            LayByDetailsDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.Padding = new Padding(3);
            LayByDetailsDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            LayByDetailsDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            LayByDetailsDataGrid.Size = new Size(619, 166);
            LayByDetailsDataGrid.TabIndex = 20;
            LayByDetailsDataGrid.CellContentClick += LayByDetailsDataGrid_CellContentClick;
            // 
            // TotalTextBox
            // 
            TotalTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TotalTextBox.Location = new Point(495, 164);
            TotalTextBox.Multiline = true;
            TotalTextBox.Name = "TotalTextBox";
            TotalTextBox.ReadOnly = true;
            TotalTextBox.Size = new Size(181, 30);
            TotalTextBox.TabIndex = 19;
            // 
            // ClientTextBox
            // 
            ClientTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ClientTextBox.Location = new Point(278, 164);
            ClientTextBox.Multiline = true;
            ClientTextBox.Name = "ClientTextBox";
            ClientTextBox.ReadOnly = true;
            ClientTextBox.Size = new Size(181, 30);
            ClientTextBox.TabIndex = 19;
            // 
            // LaybyNoTextBox
            // 
            LaybyNoTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LaybyNoTextBox.Location = new Point(61, 164);
            LaybyNoTextBox.Multiline = true;
            LaybyNoTextBox.Name = "LaybyNoTextBox";
            LaybyNoTextBox.ReadOnly = true;
            LaybyNoTextBox.Size = new Size(181, 30);
            LaybyNoTextBox.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(491, 137);
            label5.Name = "label5";
            label5.Size = new Size(99, 20);
            label5.TabIndex = 18;
            label5.Text = "Total Amount";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(274, 137);
            label4.Name = "label4";
            label4.Size = new Size(91, 20);
            label4.TabIndex = 18;
            label4.Text = "Client Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(57, 137);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 18;
            label2.Text = "Layby No.";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 494);
            panel4.Name = "panel4";
            panel4.Size = new Size(729, 70);
            panel4.TabIndex = 1;
            // 
            // cancel_button
            // 
            cancel_button.BackColor = Color.FromArgb(0, 119, 194);
            cancel_button.FlatAppearance.BorderSize = 0;
            cancel_button.FlatStyle = FlatStyle.Flat;
            cancel_button.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cancel_button.ForeColor = Color.White;
            cancel_button.Location = new Point(313, 16);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(130, 39);
            cancel_button.TabIndex = 0;
            cancel_button.Text = "Close";
            cancel_button.UseVisualStyleBackColor = false;
            cancel_button.Click += cancel_button_Click;
            // 
            // LaybyDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancel_button;
            ClientSize = new Size(733, 567);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LaybyDetailsForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StaffForm";
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LayByDetailsDataGrid).EndInit();
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
        private TextBox LaybyNoTextBox;
        private Label label2;
        private TextBox TotalTextBox;
        private TextBox ClientTextBox;
        private Label label5;
        private Label label4;
        private TextBox DueTextBox;
        private Label label6;
        private TextBox PaidTextBox;
        private Label label3;
        private DataGridView LayByDetailsDataGrid;
    }
}