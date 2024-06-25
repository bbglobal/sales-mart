namespace POS
{
    partial class ProductIngredientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductIngredientsForm));
            panel3 = new Panel();
            Title_label = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            AddIngredientButton = new Button();
            IngredientsListBox = new ListBox();
            Product_ComboBox = new ComboBox();
            status_label = new Label();
            Ingredient_TextBox = new TextBox();
            label3 = new Label();
            panel4 = new Panel();
            save_button = new Button();
            cancel_button = new Button();
            RemoveButton = new Button();
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
            panel3.Size = new Size(447, 104);
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
            Title_label.Size = new Size(226, 40);
            Title_label.TabIndex = 1;
            Title_label.Text = "Add Ingredients";
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
            panel1.Size = new Size(451, 581);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(RemoveButton);
            panel2.Controls.Add(AddIngredientButton);
            panel2.Controls.Add(IngredientsListBox);
            panel2.Controls.Add(Product_ComboBox);
            panel2.Controls.Add(status_label);
            panel2.Controls.Add(Ingredient_TextBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(1, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(447, 578);
            panel2.TabIndex = 1;
            // 
            // AddIngredientButton
            // 
            AddIngredientButton.BackColor = Color.FromArgb(0, 119, 194);
            AddIngredientButton.FlatStyle = FlatStyle.Flat;
            AddIngredientButton.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddIngredientButton.ForeColor = Color.White;
            AddIngredientButton.Location = new Point(295, 252);
            AddIngredientButton.Name = "AddIngredientButton";
            AddIngredientButton.Size = new Size(60, 30);
            AddIngredientButton.TabIndex = 16;
            AddIngredientButton.Text = "Add";
            AddIngredientButton.UseVisualStyleBackColor = false;
            AddIngredientButton.Click += AddIngredientButton_Click;
            // 
            // IngredientsListBox
            // 
            IngredientsListBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IngredientsListBox.FormattingEnabled = true;
            IngredientsListBox.ItemHeight = 20;
            IngredientsListBox.Location = new Point(107, 306);
            IngredientsListBox.Name = "IngredientsListBox";
            IngredientsListBox.Size = new Size(181, 164);
            IngredientsListBox.TabIndex = 15;
            // 
            // Product_ComboBox
            // 
            Product_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Product_ComboBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Product_ComboBox.FormattingEnabled = true;
            Product_ComboBox.IntegralHeight = false;
            Product_ComboBox.Location = new Point(107, 170);
            Product_ComboBox.Name = "Product_ComboBox";
            Product_ComboBox.Size = new Size(181, 28);
            Product_ComboBox.TabIndex = 14;
            // 
            // status_label
            // 
            status_label.AutoSize = true;
            status_label.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            status_label.Location = new Point(103, 143);
            status_label.Name = "status_label";
            status_label.Size = new Size(60, 20);
            status_label.TabIndex = 13;
            status_label.Text = "Product";
            // 
            // Ingredient_TextBox
            // 
            Ingredient_TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Ingredient_TextBox.Location = new Point(108, 252);
            Ingredient_TextBox.Multiline = true;
            Ingredient_TextBox.Name = "Ingredient_TextBox";
            Ingredient_TextBox.Size = new Size(181, 30);
            Ingredient_TextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(103, 225);
            label3.Name = "label3";
            label3.Size = new Size(109, 20);
            label3.TabIndex = 2;
            label3.Text = "Add Ingredient";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 231, 232);
            panel4.Controls.Add(save_button);
            panel4.Controls.Add(cancel_button);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 508);
            panel4.Name = "panel4";
            panel4.Size = new Size(447, 70);
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
            save_button.Location = new Point(225, 15);
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
            cancel_button.Location = new Point(89, 15);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(130, 39);
            cancel_button.TabIndex = 0;
            cancel_button.Text = "Cancel";
            cancel_button.UseVisualStyleBackColor = false;
            cancel_button.Click += cancel_button_Click;
            // 
            // RemoveButton
            // 
            RemoveButton.BackColor = Color.FromArgb(0, 119, 194);
            RemoveButton.FlatStyle = FlatStyle.Flat;
            RemoveButton.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RemoveButton.ForeColor = Color.White;
            RemoveButton.Location = new Point(295, 306);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(68, 30);
            RemoveButton.TabIndex = 17;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = false;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // IngredientsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(451, 581);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "IngredientsForm";
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
        private Label label3;
        private TextBox Ingredient_TextBox;
        private ComboBox Product_ComboBox;
        private Label status_label;
        private Button AddIngredientButton;
        private ListBox IngredientsListBox;
        private Button RemoveButton;
    }
}