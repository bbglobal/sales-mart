namespace POS
{
    partial class IngredientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IngredientsForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            MinimumQuantity_TextBox = new TextBox();
            label5 = new Label();
            label4 = new Label();
            STUnit_ComboBox = new ComboBox();
            STQuantity_TextBox = new TextBox();
            ProductPrice_TextBox = new TextBox();
            label2 = new Label();
            IngName_TextBox = new TextBox();
            label3 = new Label();
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
            panel3.Size = new Size(623, 104);
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
            Title_label.Size = new Size(276, 47);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Ingredients";
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
            panel1.Size = new Size(627, 521);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(MinimumQuantity_TextBox);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(STUnit_ComboBox);
            panel2.Controls.Add(STQuantity_TextBox);
            panel2.Controls.Add(ProductPrice_TextBox);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(IngName_TextBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(623, 518);
            panel2.TabIndex = 1;
            // 
            // MinimumQuantity_TextBox
            // 
            MinimumQuantity_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MinimumQuantity_TextBox.Location = new Point(190, 314);
            MinimumQuantity_TextBox.Multiline = true;
            MinimumQuantity_TextBox.Name = "MinimumQuantity_TextBox";
            MinimumQuantity_TextBox.Size = new Size(181, 30);
            MinimumQuantity_TextBox.TabIndex = 20;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(186, 287);
            label5.Name = "label5";
            label5.Size = new Size(218, 20);
            label5.TabIndex = 19;
            label5.Text = "Minimum Quantity (acc. to unit)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(187, 209);
            label4.Name = "label4";
            label4.Size = new Size(133, 20);
            label4.TabIndex = 18;
            label4.Text = "Qty(Standard Unit)";
            // 
            // STUnit_ComboBox
            // 
            STUnit_ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            STUnit_ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            STUnit_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            STUnit_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            STUnit_ComboBox.FormattingEnabled = true;
            STUnit_ComboBox.IntegralHeight = false;
            STUnit_ComboBox.Items.AddRange(new object[] { "kg", "litres", "item" });
            STUnit_ComboBox.Location = new Point(259, 237);
            STUnit_ComboBox.Name = "STUnit_ComboBox";
            STUnit_ComboBox.Size = new Size(70, 28);
            STUnit_ComboBox.TabIndex = 17;
            // 
            // STQuantity_TextBox
            // 
            STQuantity_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            STQuantity_TextBox.Location = new Point(191, 236);
            STQuantity_TextBox.Multiline = true;
            STQuantity_TextBox.Name = "STQuantity_TextBox";
            STQuantity_TextBox.Size = new Size(66, 30);
            STQuantity_TextBox.TabIndex = 15;
            STQuantity_TextBox.KeyPress += STQuantity_TextBox_KeyPress;
            // 
            // ProductPrice_TextBox
            // 
            ProductPrice_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProductPrice_TextBox.Location = new Point(192, 389);
            ProductPrice_TextBox.Multiline = true;
            ProductPrice_TextBox.Name = "ProductPrice_TextBox";
            ProductPrice_TextBox.Size = new Size(181, 30);
            ProductPrice_TextBox.TabIndex = 14;
            ProductPrice_TextBox.KeyPress += STQuantity_TextBox_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(187, 362);
            label2.Name = "label2";
            label2.Size = new Size(93, 20);
            label2.TabIndex = 13;
            label2.Text = "Cost Per Unit";
            // 
            // IngName_TextBox
            // 
            IngName_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IngName_TextBox.Location = new Point(191, 159);
            IngName_TextBox.Multiline = true;
            IngName_TextBox.Name = "IngName_TextBox";
            IngName_TextBox.Size = new Size(181, 30);
            IngName_TextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(187, 132);
            label3.Name = "label3";
            label3.Size = new Size(121, 20);
            label3.TabIndex = 2;
            label3.Text = "Ingredient Name";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 448);
            panel4.Name = "panel4";
            panel4.Size = new Size(623, 70);
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
            // IngredientsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(627, 521);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "IngredientsForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ProductsForm";
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
        private TextBox IngName_TextBox;
        private TextBox ProductPrice_TextBox;
        private Label label2;
        private Label label4;
        private ComboBox STUnit_ComboBox;
        private TextBox STQuantity_TextBox;
        private TextBox MinimumQuantity_TextBox;
        private Label label5;
    }
}