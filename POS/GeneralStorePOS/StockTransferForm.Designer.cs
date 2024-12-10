namespace POS
{
    partial class StockTransferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockTransferForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panelimistakenlychanged = new Panel();
            SourceBranchComboBox = new TextBox();
            ProductComboBox = new ComboBox();
            label4 = new Label();
            DestinationBranchComboBox = new ComboBox();
            label3 = new Label();
            category_label = new Label();
            CurrentStockTextBox = new TextBox();
            label2 = new Label();
            TransferAmountTB = new TextBox();
            status_label = new Label();
            panel4 = new Panel();
            save_button = new Button();
            cancel_button = new Button();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panelimistakenlychanged.SuspendLayout();
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
            panel3.Size = new Size(609, 104);
            panel3.TabIndex = 0;
            // 
            // Title_label
            // 
            Title_label.AutoSize = true;
            Title_label.BackColor = Color.Transparent;
            Title_label.Font = new Font("Segoe UI Semibold", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Title_label.ForeColor = Color.White;
            Title_label.Location = new Point(130, 33);
            Title_label.Name = "Title_label";
            Title_label.Size = new Size(436, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Stock Transfer Details";
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(53, 26);
            label1.Name = "label1";
            label1.Size = new Size(60, 60);
            label1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(37, 150, 190);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(panelimistakenlychanged);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(1, 0, 1, 1);
            panel1.Size = new Size(613, 573);
            panel1.TabIndex = 1;
            // 
            // panelimistakenlychanged
            // 
            panelimistakenlychanged.BackColor = Color.White;
            panelimistakenlychanged.Controls.Add(SourceBranchComboBox);
            panelimistakenlychanged.Controls.Add(ProductComboBox);
            panelimistakenlychanged.Controls.Add(label4);
            panelimistakenlychanged.Controls.Add(DestinationBranchComboBox);
            panelimistakenlychanged.Controls.Add(label3);
            panelimistakenlychanged.Controls.Add(category_label);
            panelimistakenlychanged.Controls.Add(CurrentStockTextBox);
            panelimistakenlychanged.Controls.Add(label2);
            panelimistakenlychanged.Controls.Add(TransferAmountTB);
            panelimistakenlychanged.Controls.Add(status_label);
            panelimistakenlychanged.Controls.Add(panel4);
            panelimistakenlychanged.Controls.Add(panel3);
            panelimistakenlychanged.Dock = DockStyle.Fill;
            panelimistakenlychanged.Location = new Point(1, 0);
            panelimistakenlychanged.Name = "panelimistakenlychanged";
            panelimistakenlychanged.Size = new Size(609, 570);
            panelimistakenlychanged.TabIndex = 1;
            // 
            // SourceBranchComboBox
            // 
            SourceBranchComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SourceBranchComboBox.Location = new Point(199, 158);
            SourceBranchComboBox.Multiline = true;
            SourceBranchComboBox.Name = "SourceBranchComboBox";
            SourceBranchComboBox.ReadOnly = true;
            SourceBranchComboBox.Size = new Size(181, 30);
            SourceBranchComboBox.TabIndex = 26;
            // 
            // ProductComboBox
            // 
            ProductComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            ProductComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            ProductComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ProductComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProductComboBox.FormattingEnabled = true;
            ProductComboBox.IntegralHeight = false;
            ProductComboBox.Location = new Point(199, 303);
            ProductComboBox.Name = "ProductComboBox";
            ProductComboBox.Size = new Size(181, 28);
            ProductComboBox.TabIndex = 25;
            ProductComboBox.SelectedIndexChanged += ProductComboBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(195, 276);
            label4.Name = "label4";
            label4.Size = new Size(60, 20);
            label4.TabIndex = 24;
            label4.Text = "Product";
            // 
            // DestinationBranchComboBox
            // 
            DestinationBranchComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            DestinationBranchComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            DestinationBranchComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DestinationBranchComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DestinationBranchComboBox.FormattingEnabled = true;
            DestinationBranchComboBox.IntegralHeight = false;
            DestinationBranchComboBox.Location = new Point(199, 229);
            DestinationBranchComboBox.Name = "DestinationBranchComboBox";
            DestinationBranchComboBox.Size = new Size(181, 28);
            DestinationBranchComboBox.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(195, 202);
            label3.Name = "label3";
            label3.Size = new Size(134, 20);
            label3.TabIndex = 22;
            label3.Text = "Destination Branch";
            // 
            // category_label
            // 
            category_label.AutoSize = true;
            category_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            category_label.Location = new Point(195, 125);
            category_label.Name = "category_label";
            category_label.Size = new Size(103, 20);
            category_label.TabIndex = 20;
            category_label.Text = "Source Branch";
            // 
            // CurrentStockTextBox
            // 
            CurrentStockTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CurrentStockTextBox.Location = new Point(199, 375);
            CurrentStockTextBox.Multiline = true;
            CurrentStockTextBox.Name = "CurrentStockTextBox";
            CurrentStockTextBox.ReadOnly = true;
            CurrentStockTextBox.Size = new Size(181, 30);
            CurrentStockTextBox.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(195, 348);
            label2.Name = "label2";
            label2.Size = new Size(97, 20);
            label2.TabIndex = 18;
            label2.Text = "Current Stock";
            // 
            // TransferAmountTB
            // 
            TransferAmountTB.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TransferAmountTB.Location = new Point(199, 449);
            TransferAmountTB.Multiline = true;
            TransferAmountTB.Name = "TransferAmountTB";
            TransferAmountTB.Size = new Size(181, 30);
            TransferAmountTB.TabIndex = 17;
            TransferAmountTB.TextChanged += TransferAmountTB_TextChanged;
            // 
            // status_label
            // 
            status_label.AutoSize = true;
            status_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            status_label.Location = new Point(195, 422);
            status_label.Name = "status_label";
            status_label.Size = new Size(158, 20);
            status_label.TabIndex = 11;
            status_label.Text = "Stock Transfer Amount";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 500);
            panel4.Name = "panel4";
            panel4.Size = new Size(609, 70);
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
            // StockTransferForm
            // 
            AcceptButton = save_button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancel_button;
            ClientSize = new Size(613, 573);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "StockTransferForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StaffForm";
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panelimistakenlychanged.ResumeLayout(false);
            panelimistakenlychanged.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Label label1;
        private Panel panel1;
        private Panel panelimistakenlychanged;
        private Label Title_label;
        private Panel panel4;
        private Button cancel_button;
        private Button save_button;
        private Label status_label;
        private TextBox TransferAmountTB;
        private TextBox CurrentStockTextBox;
        private Label label2;
        private ComboBox ProductComboBox;
        private Label label4;
        private ComboBox DestinationBranchComboBox;
        private Label label3;
        private Label category_label;
        private TextBox SourceBranchComboBox;
    }
}