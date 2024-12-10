namespace POS
{
    partial class EditOrderForm
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditOrderForm));
            panel4 = new Panel();
            OrderCancelButton = new Button();
            save_button = new Button();
            cancel_button = new Button();
            panel2 = new Panel();
            OrderItemsDataGrid = new DataGridView();
            NetAmountLabel = new Label();
            label6 = new Label();
            TotalAmountLabel = new Label();
            label5 = new Label();
            CustomerNameLabel = new Label();
            label2 = new Label();
            BillIDLabel = new Label();
            label3 = new Label();
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OrderItemsDataGrid).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(OrderCancelButton);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 479);
            panel4.Name = "panel4";
            panel4.Size = new Size(727, 70);
            panel4.TabIndex = 1;
            // 
            // OrderCancelButton
            // 
            OrderCancelButton.BackColor = Color.Red;
            OrderCancelButton.FlatAppearance.BorderSize = 0;
            OrderCancelButton.FlatStyle = FlatStyle.Flat;
            OrderCancelButton.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OrderCancelButton.ForeColor = Color.White;
            OrderCancelButton.Location = new Point(298, 16);
            OrderCancelButton.Name = "OrderCancelButton";
            OrderCancelButton.Size = new Size(130, 39);
            OrderCancelButton.TabIndex = 2;
            OrderCancelButton.Text = "Cancel Order";
            OrderCancelButton.UseVisualStyleBackColor = false;
            OrderCancelButton.Click += OrderCancelButton_Click;
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
            save_button.Location = new Point(434, 16);
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
            cancel_button.Location = new Point(162, 16);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(130, 39);
            cancel_button.TabIndex = 0;
            cancel_button.Text = "Close";
            cancel_button.UseVisualStyleBackColor = false;
            cancel_button.Click += cancel_button_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(OrderItemsDataGrid);
            panel2.Controls.Add(NetAmountLabel);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(TotalAmountLabel);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(CustomerNameLabel);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(BillIDLabel);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(727, 549);
            panel2.TabIndex = 2;
            // 
            // OrderItemsDataGrid
            // 
            OrderItemsDataGrid.AllowUserToAddRows = false;
            OrderItemsDataGrid.AllowUserToDeleteRows = false;
            OrderItemsDataGrid.AllowUserToResizeColumns = false;
            OrderItemsDataGrid.AllowUserToResizeRows = false;
            OrderItemsDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OrderItemsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            OrderItemsDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            OrderItemsDataGrid.BackgroundColor = Color.White;
            OrderItemsDataGrid.BorderStyle = BorderStyle.None;
            OrderItemsDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            OrderItemsDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(246, 247, 252);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            OrderItemsDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            OrderItemsDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            OrderItemsDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            OrderItemsDataGrid.EnableHeadersVisualStyles = false;
            OrderItemsDataGrid.Location = new Point(0, 152);
            OrderItemsDataGrid.Margin = new Padding(0);
            OrderItemsDataGrid.Name = "OrderItemsDataGrid";
            OrderItemsDataGrid.ReadOnly = true;
            OrderItemsDataGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            OrderItemsDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            OrderItemsDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.Padding = new Padding(3);
            OrderItemsDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            OrderItemsDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            OrderItemsDataGrid.Size = new Size(727, 324);
            OrderItemsDataGrid.TabIndex = 12;
            OrderItemsDataGrid.CellContentClick += OrderItemsDataGrid_CellContentClick;
            // 
            // NetAmountLabel
            // 
            NetAmountLabel.AutoSize = true;
            NetAmountLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NetAmountLabel.Location = new Point(622, 120);
            NetAmountLabel.Name = "NetAmountLabel";
            NetAmountLabel.Size = new Size(93, 20);
            NetAmountLabel.TabIndex = 11;
            NetAmountLabel.Text = "Net Amount:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(532, 120);
            label6.Name = "label6";
            label6.Size = new Size(93, 20);
            label6.TabIndex = 10;
            label6.Text = "Net Amount:";
            // 
            // TotalAmountLabel
            // 
            TotalAmountLabel.AutoSize = true;
            TotalAmountLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TotalAmountLabel.Location = new Point(446, 120);
            TotalAmountLabel.Name = "TotalAmountLabel";
            TotalAmountLabel.Size = new Size(49, 20);
            TotalAmountLabel.TabIndex = 9;
            TotalAmountLabel.Text = "Total: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(406, 120);
            label5.Name = "label5";
            label5.Size = new Size(49, 20);
            label5.TabIndex = 8;
            label5.Text = "Total: ";
            // 
            // CustomerNameLabel
            // 
            CustomerNameLabel.AutoSize = true;
            CustomerNameLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CustomerNameLabel.Location = new Point(251, 120);
            CustomerNameLabel.Name = "CustomerNameLabel";
            CustomerNameLabel.Size = new Size(119, 20);
            CustomerNameLabel.TabIndex = 7;
            CustomerNameLabel.Text = "Customer Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(136, 120);
            label2.Name = "label2";
            label2.Size = new Size(119, 20);
            label2.TabIndex = 6;
            label2.Text = "Customer Name:";
            // 
            // BillIDLabel
            // 
            BillIDLabel.AutoSize = true;
            BillIDLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BillIDLabel.Location = new Point(74, 120);
            BillIDLabel.Name = "BillIDLabel";
            BillIDLabel.Size = new Size(56, 20);
            BillIDLabel.TabIndex = 5;
            BillIDLabel.Text = "Bill ID: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(24, 120);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 2;
            label3.Text = "Bill ID: ";
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
            panel3.Size = new Size(727, 104);
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
            Title_label.Text = "Order Details";
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
            // EditOrderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(727, 549);
            Controls.Add(panel2);
            Name = "EditOrderForm";
            Text = "EditOrderForm";
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OrderItemsDataGrid).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel4;
        private Button save_button;
        private Button cancel_button;
        private Panel panel2;
        private Label label3;
        private Panel panel3;
        private Label Title_label;
        private Label label1;
        private Label BillIDLabel;
        private Label NetAmountLabel;
        private Label label6;
        private Label TotalAmountLabel;
        private Label label5;
        private Label CustomerNameLabel;
        private Label label2;
        private Button OrderCancelButton;
        private DataGridView OrderItemsDataGrid;
    }
}