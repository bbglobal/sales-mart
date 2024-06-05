namespace POS
{
    partial class POSIngredientScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSIngredientScreen));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            label3 = new Label();
            panel4 = new Panel();
            cancel_button = new Button();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            IngredientsListBox = new ListBox();
            pictureBox1 = new PictureBox();
            ProductName_Label = new Label();
            ProductPrice_Label = new Label();
            Category_Label = new Label();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            Title_label.Location = new Point(103, 33);
            Title_label.Name = "Title_label";
            Title_label.Size = new Size(218, 40);
            Title_label.TabIndex = 1;
            Title_label.Text = "Product Details";
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(26, 26);
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
            panel1.Size = new Size(502, 564);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(Category_Label);
            panel2.Controls.Add(ProductPrice_Label);
            panel2.Controls.Add(ProductName_Label);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(IngredientsListBox);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(498, 561);
            panel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(26, 128);
            label3.Name = "label3";
            label3.Size = new Size(111, 20);
            label3.TabIndex = 2;
            label3.Text = "Product Name :";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 491);
            panel4.Name = "panel4";
            panel4.Size = new Size(498, 70);
            panel4.TabIndex = 1;
            // 
            // cancel_button
            // 
            cancel_button.BackColor = Color.FromArgb(0, 119, 194);
            cancel_button.FlatAppearance.BorderSize = 0;
            cancel_button.FlatStyle = FlatStyle.Flat;
            cancel_button.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cancel_button.ForeColor = Color.White;
            cancel_button.Location = new Point(190, 15);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(130, 39);
            cancel_button.TabIndex = 0;
            cancel_button.Text = "Close";
            cancel_button.UseVisualStyleBackColor = false;
            cancel_button.Click += cancel_button_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(26, 161);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 3;
            label2.Text = "Category :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(26, 193);
            label4.Name = "label4";
            label4.Size = new Size(103, 20);
            label4.TabIndex = 4;
            label4.Text = "Product Price :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(26, 226);
            label5.Name = "label5";
            label5.Size = new Size(90, 20);
            label5.TabIndex = 5;
            label5.Text = "Ingredients :";
            // 
            // IngredientsListBox
            // 
            IngredientsListBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IngredientsListBox.FormattingEnabled = true;
            IngredientsListBox.ItemHeight = 17;
            IngredientsListBox.Location = new Point(31, 258);
            IngredientsListBox.Name = "IngredientsListBox";
            IngredientsListBox.Size = new Size(243, 208);
            IngredientsListBox.TabIndex = 6;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(336, 137);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 125);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // ProductName_Label
            // 
            ProductName_Label.AutoSize = true;
            ProductName_Label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProductName_Label.Location = new Point(143, 128);
            ProductName_Label.Name = "ProductName_Label";
            ProductName_Label.Size = new Size(0, 20);
            ProductName_Label.TabIndex = 10;
            // 
            // ProductPrice_Label
            // 
            ProductPrice_Label.AutoSize = true;
            ProductPrice_Label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProductPrice_Label.Location = new Point(135, 193);
            ProductPrice_Label.Name = "ProductPrice_Label";
            ProductPrice_Label.Size = new Size(0, 20);
            ProductPrice_Label.TabIndex = 11;
            // 
            // Category_Label
            // 
            Category_Label.AutoSize = true;
            Category_Label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Category_Label.Location = new Point(108, 161);
            Category_Label.Name = "Category_Label";
            Category_Label.Size = new Size(0, 20);
            Category_Label.TabIndex = 12;
            // 
            // POSIngredientScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 564);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "POSIngredientScreen";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StaffCategoryForm";
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private ListBox IngredientsListBox;
        private Label label5;
        private Label label4;
        private Label label2;
        private PictureBox pictureBox1;
        private Label Category_Label;
        private Label ProductPrice_Label;
        private Label ProductName_Label;
    }
}