namespace POS
{
    partial class BillList
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillList));
            panel1 = new Panel();
            BillListDataGrid = new DataGridView();
            DineIn_label = new Label();
            TakeAway_label = new Label();
            Delivery_label = new Label();
            All_label = new Label();
            panel2 = new Panel();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BillListDataGrid).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(BillListDataGrid);
            panel1.Controls.Add(DineIn_label);
            panel1.Controls.Add(TakeAway_label);
            panel1.Controls.Add(Delivery_label);
            panel1.Controls.Add(All_label);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(961, 572);
            panel1.TabIndex = 0;
            // 
            // BillListDataGrid
            // 
            BillListDataGrid.AllowUserToAddRows = false;
            BillListDataGrid.AllowUserToDeleteRows = false;
            BillListDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BillListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BillListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            BillListDataGrid.BackgroundColor = SystemColors.Control;
            BillListDataGrid.BorderStyle = BorderStyle.None;
            BillListDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            BillListDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(0, 119, 195);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            BillListDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            BillListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.GrayText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.GrayText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            BillListDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            BillListDataGrid.EnableHeadersVisualStyles = false;
            BillListDataGrid.Location = new Point(12, 143);
            BillListDataGrid.Name = "BillListDataGrid";
            BillListDataGrid.ReadOnly = true;
            BillListDataGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            BillListDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            BillListDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            BillListDataGrid.Size = new Size(938, 417);
            BillListDataGrid.TabIndex = 5;
            // 
            // DineIn_label
            // 
            DineIn_label.BackColor = Color.Transparent;
            DineIn_label.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DineIn_label.ForeColor = SystemColors.GrayText;
            DineIn_label.Image = (Image)resources.GetObject("DineIn_label.Image");
            DineIn_label.ImageAlign = ContentAlignment.MiddleLeft;
            DineIn_label.Location = new Point(143, 101);
            DineIn_label.Name = "DineIn_label";
            DineIn_label.Size = new Size(101, 27);
            DineIn_label.TabIndex = 4;
            DineIn_label.Text = "Dine In";
            DineIn_label.TextAlign = ContentAlignment.MiddleRight;
            DineIn_label.Click += DineIn_label_Click;
            // 
            // TakeAway_label
            // 
            TakeAway_label.BackColor = Color.Transparent;
            TakeAway_label.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TakeAway_label.ForeColor = SystemColors.GrayText;
            TakeAway_label.Image = (Image)resources.GetObject("TakeAway_label.Image");
            TakeAway_label.ImageAlign = ContentAlignment.MiddleLeft;
            TakeAway_label.Location = new Point(264, 101);
            TakeAway_label.Name = "TakeAway_label";
            TakeAway_label.Size = new Size(131, 27);
            TakeAway_label.TabIndex = 3;
            TakeAway_label.Text = "Take Away";
            TakeAway_label.TextAlign = ContentAlignment.MiddleRight;
            TakeAway_label.Click += TakeAway_label_Click;
            // 
            // Delivery_label
            // 
            Delivery_label.BackColor = Color.Transparent;
            Delivery_label.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Delivery_label.ForeColor = SystemColors.GrayText;
            Delivery_label.Image = (Image)resources.GetObject("Delivery_label.Image");
            Delivery_label.ImageAlign = ContentAlignment.MiddleLeft;
            Delivery_label.Location = new Point(413, 101);
            Delivery_label.Name = "Delivery_label";
            Delivery_label.Size = new Size(111, 27);
            Delivery_label.TabIndex = 2;
            Delivery_label.Text = "Delivery";
            Delivery_label.TextAlign = ContentAlignment.MiddleRight;
            Delivery_label.Click += Delivery_label_Click;
            // 
            // All_label
            // 
            All_label.BackColor = Color.Transparent;
            All_label.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            All_label.ForeColor = SystemColors.GrayText;
            All_label.Image = (Image)resources.GetObject("All_label.Image");
            All_label.ImageAlign = ContentAlignment.MiddleLeft;
            All_label.Location = new Point(63, 101);
            All_label.Name = "All_label";
            All_label.Size = new Size(61, 27);
            All_label.TabIndex = 1;
            All_label.Text = "All";
            All_label.TextAlign = ContentAlignment.MiddleRight;
            All_label.Click += All_label_Click;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.Sidebar_panel;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(961, 89);
            panel2.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGray;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(891, 30);
            button1.Name = "button1";
            button1.Size = new Size(35, 35);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(146, 19);
            label1.Name = "label1";
            label1.Size = new Size(141, 50);
            label1.TabIndex = 0;
            label1.Text = "Bill List";
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Image = Properties.Resources.LOGO;
            label2.Location = new Point(87, 15);
            label2.Name = "label2";
            label2.Size = new Size(50, 60);
            label2.TabIndex = 1;
            // 
            // BillList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 119, 195);
            ClientSize = new Size(963, 574);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BillList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BillList";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)BillListDataGrid).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Button button1;
        private Label All_label;
        private Label DineIn_label;
        private Label TakeAway_label;
        private Label Delivery_label;
        private DataGridView BillListDataGrid;
    }
}