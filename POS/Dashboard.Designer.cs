namespace POS
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Main_panel = new Panel();
            Sidebar_panel = new Panel();
            LogoText = new Label();
            Logo = new Label();
            Menu_Settings_label = new Label();
            Menu_Reports_label = new Label();
            Menu_Kitchen_label = new Label();
            Menu_POS_label = new Label();
            Menu_Staff_label = new Label();
            Menu_Tables_label = new Label();
            Menu_Products_label = new Label();
            Menu_Dashboard_label = new Label();
            ScreenContainer_panel = new Panel();
            CurrentUser_label = new Label();
            Current_ScreenName_label = new Label();
            Splitter_label = new Label();
            ProductPanel = new Panel();
            ProductButtonsLayout = new TableLayoutPanel();
            button2 = new Button();
            comboBox2 = new ComboBox();
            panel1 = new Panel();
            textBox1 = new TextBox();
            button3 = new Button();
            ContentContainer_panel = new Panel();
            comboBox1 = new ComboBox();
            button1 = new Button();
            ChartContainer = new TableLayoutPanel();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tableLayoutPanel2 = new TableLayoutPanel();
            Total_Pay_CardBox = new Panel();
            T_Pay_Amount_label = new Label();
            TPay_line = new Label();
            Total_Pay_label = new Label();
            Total_Tax_CardBox = new Panel();
            T_Tax_Amout_label = new Label();
            TT_line = new Label();
            Total_Tax_label = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            Total_Profit_CardBox = new Panel();
            T_Profit_Amount_label = new Label();
            TP_line = new Label();
            Total_Profit_label = new Label();
            Total_Disc_CardBox = new Panel();
            T_Disc_Amount_label = new Label();
            TD_line = new Label();
            Total_Disc_label = new Label();
            Total_Cost_CardBox = new Panel();
            T_Cost_Amount_label = new Label();
            TC_line = new Label();
            Total_Cost_label = new Label();
            Total_Sale_CardBox = new Panel();
            T_Sale_Amount_label = new Label();
            TS_line = new Label();
            Total_Sale_label = new Label();
            Main_panel.SuspendLayout();
            Sidebar_panel.SuspendLayout();
            ScreenContainer_panel.SuspendLayout();
            ProductPanel.SuspendLayout();
            ProductButtonsLayout.SuspendLayout();
            panel1.SuspendLayout();
            ContentContainer_panel.SuspendLayout();
            ChartContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            Total_Pay_CardBox.SuspendLayout();
            Total_Tax_CardBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            Total_Profit_CardBox.SuspendLayout();
            Total_Disc_CardBox.SuspendLayout();
            Total_Cost_CardBox.SuspendLayout();
            Total_Sale_CardBox.SuspendLayout();
            SuspendLayout();
            // 
            // Main_panel
            // 
            Main_panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Main_panel.BackColor = Color.FromArgb(246, 247, 252);
            Main_panel.Controls.Add(Sidebar_panel);
            Main_panel.Controls.Add(ScreenContainer_panel);
            Main_panel.Location = new Point(2, 0);
            Main_panel.Name = "Main_panel";
            Main_panel.Size = new Size(1284, 815);
            Main_panel.TabIndex = 2;
            // 
            // Sidebar_panel
            // 
            Sidebar_panel.BackColor = Color.Transparent;
            Sidebar_panel.BackgroundImage = (Image)resources.GetObject("Sidebar_panel.BackgroundImage");
            Sidebar_panel.BackgroundImageLayout = ImageLayout.Stretch;
            Sidebar_panel.Controls.Add(LogoText);
            Sidebar_panel.Controls.Add(Logo);
            Sidebar_panel.Controls.Add(Menu_Settings_label);
            Sidebar_panel.Controls.Add(Menu_Reports_label);
            Sidebar_panel.Controls.Add(Menu_Kitchen_label);
            Sidebar_panel.Controls.Add(Menu_POS_label);
            Sidebar_panel.Controls.Add(Menu_Staff_label);
            Sidebar_panel.Controls.Add(Menu_Tables_label);
            Sidebar_panel.Controls.Add(Menu_Products_label);
            Sidebar_panel.Controls.Add(Menu_Dashboard_label);
            Sidebar_panel.Dock = DockStyle.Left;
            Sidebar_panel.Location = new Point(0, 0);
            Sidebar_panel.Name = "Sidebar_panel";
            Sidebar_panel.Size = new Size(266, 815);
            Sidebar_panel.TabIndex = 0;
            // 
            // LogoText
            // 
            LogoText.Image = (Image)resources.GetObject("LogoText.Image");
            LogoText.Location = new Point(85, 36);
            LogoText.Margin = new Padding(0);
            LogoText.Name = "LogoText";
            LogoText.Size = new Size(150, 40);
            LogoText.TabIndex = 9;
            LogoText.Click += LogoText_Click;
            // 
            // Logo
            // 
            Logo.Image = (Image)resources.GetObject("Logo.Image");
            Logo.Location = new Point(36, 30);
            Logo.Margin = new Padding(0);
            Logo.Name = "Logo";
            Logo.Size = new Size(40, 50);
            Logo.TabIndex = 8;
            Logo.Click += Logo_Click;
            // 
            // Menu_Settings_label
            // 
            Menu_Settings_label.Anchor = AnchorStyles.None;
            Menu_Settings_label.Font = new Font("Arial", 12F);
            Menu_Settings_label.ForeColor = SystemColors.HighlightText;
            Menu_Settings_label.Image = (Image)resources.GetObject("Menu_Settings_label.Image");
            Menu_Settings_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Settings_label.Location = new Point(55, 606);
            Menu_Settings_label.Margin = new Padding(0);
            Menu_Settings_label.Name = "Menu_Settings_label";
            Menu_Settings_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Settings_label.Size = new Size(211, 39);
            Menu_Settings_label.TabIndex = 7;
            Menu_Settings_label.Text = "           Settings";
            Menu_Settings_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_Settings_label.Click += Menu_Settings_label_Click;
            // 
            // Menu_Reports_label
            // 
            Menu_Reports_label.Anchor = AnchorStyles.None;
            Menu_Reports_label.Font = new Font("Arial", 12F);
            Menu_Reports_label.ForeColor = SystemColors.HighlightText;
            Menu_Reports_label.Image = (Image)resources.GetObject("Menu_Reports_label.Image");
            Menu_Reports_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Reports_label.Location = new Point(55, 546);
            Menu_Reports_label.Margin = new Padding(0);
            Menu_Reports_label.Name = "Menu_Reports_label";
            Menu_Reports_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Reports_label.Size = new Size(211, 39);
            Menu_Reports_label.TabIndex = 6;
            Menu_Reports_label.Text = "           Reports";
            Menu_Reports_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_Reports_label.Click += Menu_Reports_label_Click;
            // 
            // Menu_Kitchen_label
            // 
            Menu_Kitchen_label.Anchor = AnchorStyles.None;
            Menu_Kitchen_label.Font = new Font("Arial", 12F);
            Menu_Kitchen_label.ForeColor = SystemColors.HighlightText;
            Menu_Kitchen_label.Image = (Image)resources.GetObject("Menu_Kitchen_label.Image");
            Menu_Kitchen_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Kitchen_label.Location = new Point(55, 486);
            Menu_Kitchen_label.Margin = new Padding(0);
            Menu_Kitchen_label.Name = "Menu_Kitchen_label";
            Menu_Kitchen_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Kitchen_label.Size = new Size(211, 39);
            Menu_Kitchen_label.TabIndex = 5;
            Menu_Kitchen_label.Text = "           Kitchen";
            Menu_Kitchen_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_Kitchen_label.Click += Menu_Kitchen_label_Click;
            // 
            // Menu_POS_label
            // 
            Menu_POS_label.Anchor = AnchorStyles.None;
            Menu_POS_label.Font = new Font("Arial", 12F);
            Menu_POS_label.ForeColor = SystemColors.HighlightText;
            Menu_POS_label.Image = (Image)resources.GetObject("Menu_POS_label.Image");
            Menu_POS_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_POS_label.Location = new Point(55, 426);
            Menu_POS_label.Margin = new Padding(0);
            Menu_POS_label.Name = "Menu_POS_label";
            Menu_POS_label.Padding = new Padding(10, 0, 0, 0);
            Menu_POS_label.Size = new Size(211, 39);
            Menu_POS_label.TabIndex = 4;
            Menu_POS_label.Text = "           POS";
            Menu_POS_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_POS_label.Click += Menu_POS_label_Click;
            // 
            // Menu_Staff_label
            // 
            Menu_Staff_label.Anchor = AnchorStyles.None;
            Menu_Staff_label.Font = new Font("Arial", 12F);
            Menu_Staff_label.ForeColor = SystemColors.HighlightText;
            Menu_Staff_label.Image = (Image)resources.GetObject("Menu_Staff_label.Image");
            Menu_Staff_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Staff_label.Location = new Point(55, 366);
            Menu_Staff_label.Margin = new Padding(0);
            Menu_Staff_label.Name = "Menu_Staff_label";
            Menu_Staff_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Staff_label.Size = new Size(211, 39);
            Menu_Staff_label.TabIndex = 3;
            Menu_Staff_label.Text = "           Staff";
            Menu_Staff_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_Staff_label.Click += Menu_Staff_label_Click;
            // 
            // Menu_Tables_label
            // 
            Menu_Tables_label.Anchor = AnchorStyles.None;
            Menu_Tables_label.Font = new Font("Arial", 12F);
            Menu_Tables_label.ForeColor = SystemColors.HighlightText;
            Menu_Tables_label.Image = (Image)resources.GetObject("Menu_Tables_label.Image");
            Menu_Tables_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Tables_label.Location = new Point(55, 306);
            Menu_Tables_label.Margin = new Padding(0);
            Menu_Tables_label.Name = "Menu_Tables_label";
            Menu_Tables_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Tables_label.Size = new Size(211, 39);
            Menu_Tables_label.TabIndex = 2;
            Menu_Tables_label.Text = "           Tables";
            Menu_Tables_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_Tables_label.Click += Menu_Tables_label_Click;
            // 
            // Menu_Products_label
            // 
            Menu_Products_label.Anchor = AnchorStyles.None;
            Menu_Products_label.Font = new Font("Arial", 12F);
            Menu_Products_label.ForeColor = SystemColors.HighlightText;
            Menu_Products_label.Image = (Image)resources.GetObject("Menu_Products_label.Image");
            Menu_Products_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Products_label.Location = new Point(55, 246);
            Menu_Products_label.Margin = new Padding(0);
            Menu_Products_label.Name = "Menu_Products_label";
            Menu_Products_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Products_label.Size = new Size(211, 39);
            Menu_Products_label.TabIndex = 1;
            Menu_Products_label.Text = "           Products";
            Menu_Products_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_Products_label.Click += Menu_Products_label_Click;
            // 
            // Menu_Dashboard_label
            // 
            Menu_Dashboard_label.Anchor = AnchorStyles.None;
            Menu_Dashboard_label.Font = new Font("Arial", 12F);
            Menu_Dashboard_label.ForeColor = SystemColors.HighlightText;
            Menu_Dashboard_label.Image = (Image)resources.GetObject("Menu_Dashboard_label.Image");
            Menu_Dashboard_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Dashboard_label.Location = new Point(55, 186);
            Menu_Dashboard_label.Margin = new Padding(0);
            Menu_Dashboard_label.Name = "Menu_Dashboard_label";
            Menu_Dashboard_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Dashboard_label.Size = new Size(211, 39);
            Menu_Dashboard_label.TabIndex = 0;
            Menu_Dashboard_label.Text = "           Dashboard";
            Menu_Dashboard_label.TextAlign = ContentAlignment.MiddleLeft;
            Menu_Dashboard_label.Click += Menu_Dashboard_label_Click_1;
            // 
            // ScreenContainer_panel
            // 
            ScreenContainer_panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ScreenContainer_panel.Controls.Add(CurrentUser_label);
            ScreenContainer_panel.Controls.Add(Current_ScreenName_label);
            ScreenContainer_panel.Controls.Add(Splitter_label);
            ScreenContainer_panel.Controls.Add(ProductPanel);
            ScreenContainer_panel.Controls.Add(ContentContainer_panel);
            ScreenContainer_panel.Location = new Point(264, 3);
            ScreenContainer_panel.Name = "ScreenContainer_panel";
            ScreenContainer_panel.Size = new Size(1020, 812);
            ScreenContainer_panel.TabIndex = 0;
            // 
            // CurrentUser_label
            // 
            CurrentUser_label.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CurrentUser_label.AutoSize = true;
            CurrentUser_label.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CurrentUser_label.ForeColor = Color.FromArgb(110, 111, 115);
            CurrentUser_label.Location = new Point(914, 27);
            CurrentUser_label.Margin = new Padding(0);
            CurrentUser_label.Name = "CurrentUser_label";
            CurrentUser_label.RightToLeft = RightToLeft.No;
            CurrentUser_label.Size = new Size(94, 21);
            CurrentUser_label.TabIndex = 2;
            CurrentUser_label.Text = "User Name:";
            CurrentUser_label.TextAlign = ContentAlignment.TopRight;
            // 
            // Current_ScreenName_label
            // 
            Current_ScreenName_label.AutoSize = true;
            Current_ScreenName_label.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Current_ScreenName_label.ForeColor = Color.FromArgb(0, 119, 195);
            Current_ScreenName_label.Location = new Point(21, 21);
            Current_ScreenName_label.Name = "Current_ScreenName_label";
            Current_ScreenName_label.Size = new Size(118, 30);
            Current_ScreenName_label.TabIndex = 1;
            Current_ScreenName_label.Text = "Dashboard";
            // 
            // Splitter_label
            // 
            Splitter_label.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Splitter_label.BackColor = Color.Black;
            Splitter_label.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Splitter_label.ForeColor = Color.Transparent;
            Splitter_label.Location = new Point(2, 71);
            Splitter_label.Name = "Splitter_label";
            Splitter_label.Size = new Size(1017, 1);
            Splitter_label.TabIndex = 5;
            // 
            // ProductPanel
            // 
            ProductPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProductPanel.Controls.Add(ProductButtonsLayout);
            ProductPanel.Location = new Point(21, 95);
            ProductPanel.Name = "ProductPanel";
            ProductPanel.Size = new Size(978, 701);
            ProductPanel.TabIndex = 6;
            ProductPanel.Visible = false;
            // 
            // ProductButtonsLayout
            // 
            ProductButtonsLayout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProductButtonsLayout.ColumnCount = 4;
            ProductButtonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ProductButtonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ProductButtonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ProductButtonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            ProductButtonsLayout.Controls.Add(button2, 0, 0);
            ProductButtonsLayout.Controls.Add(comboBox2, 2, 0);
            ProductButtonsLayout.Controls.Add(panel1, 3, 0);
            ProductButtonsLayout.Location = new Point(3, 75);
            ProductButtonsLayout.Name = "ProductButtonsLayout";
            ProductButtonsLayout.RowCount = 1;
            ProductButtonsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ProductButtonsLayout.Size = new Size(972, 100);
            ProductButtonsLayout.TabIndex = 0;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(37, 150, 190);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(15, 15);
            button2.Margin = new Padding(15, 15, 3, 3);
            button2.Name = "button2";
            button2.Size = new Size(40, 40);
            button2.TabIndex = 0;
            button2.UseVisualStyleBackColor = false;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "25", "50", "75", "100" });
            comboBox2.Location = new Point(598, 25);
            comboBox2.Margin = new Padding(15, 25, 3, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(79, 29);
            comboBox2.TabIndex = 1;
            comboBox2.Text = "100";
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button3);
            panel1.Location = new Point(680, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 100);
            panel1.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(22, 25);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Search..";
            textBox1.Size = new Size(150, 29);
            textBox1.TabIndex = 0;
            textBox1.WordWrap = false;
            // 
            // button3
            // 
            button3.BackColor = Color.White;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(169, 25);
            button3.Name = "button3";
            button3.Size = new Size(29, 29);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = false;
            // 
            // ContentContainer_panel
            // 
            ContentContainer_panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ContentContainer_panel.BackColor = Color.White;
            ContentContainer_panel.Controls.Add(comboBox1);
            ContentContainer_panel.Controls.Add(button1);
            ContentContainer_panel.Controls.Add(ChartContainer);
            ContentContainer_panel.Controls.Add(tableLayoutPanel2);
            ContentContainer_panel.Controls.Add(tableLayoutPanel1);
            ContentContainer_panel.Location = new Point(21, 95);
            ContentContainer_panel.Name = "ContentContainer_panel";
            ContentContainer_panel.Size = new Size(978, 701);
            ContentContainer_panel.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownWidth = 150;
            comboBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox1.IntegralHeight = false;
            comboBox1.ItemHeight = 20;
            comboBox1.Items.AddRange(new object[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            comboBox1.Location = new Point(441, 278);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(150, 28);
            comboBox1.TabIndex = 5;
            comboBox1.Text = " Month";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderColor = Color.FromArgb(209, 211, 212);
            button1.FlatAppearance.BorderSize = 3;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(800, 24);
            button1.Name = "button1";
            button1.Size = new Size(150, 34);
            button1.TabIndex = 4;
            button1.Text = "Print Sales Report";
            button1.UseVisualStyleBackColor = false;
            // 
            // ChartContainer
            // 
            ChartContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ChartContainer.ColumnCount = 1;
            ChartContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ChartContainer.Controls.Add(chart1, 0, 0);
            ChartContainer.Location = new Point(3, 340);
            ChartContainer.Name = "ChartContainer";
            ChartContainer.RowCount = 1;
            ChartContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ChartContainer.Size = new Size(972, 350);
            ChartContainer.TabIndex = 3;
            // 
            // chart1
            // 
            chart1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chart1.BorderlineWidth = 0;
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Location = new Point(3, 28);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(966, 294);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(Total_Pay_CardBox, 2, 0);
            tableLayoutPanel2.Controls.Add(Total_Tax_CardBox, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 160);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(972, 80);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // Total_Pay_CardBox
            // 
            Total_Pay_CardBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Total_Pay_CardBox.AutoSize = true;
            Total_Pay_CardBox.BackColor = Color.FromArgb(0, 148, 68);
            Total_Pay_CardBox.Controls.Add(T_Pay_Amount_label);
            Total_Pay_CardBox.Controls.Add(TPay_line);
            Total_Pay_CardBox.Controls.Add(Total_Pay_label);
            Total_Pay_CardBox.Location = new Point(491, 5);
            Total_Pay_CardBox.Margin = new Padding(5, 5, 5, 0);
            Total_Pay_CardBox.Name = "Total_Pay_CardBox";
            Total_Pay_CardBox.Size = new Size(233, 75);
            Total_Pay_CardBox.TabIndex = 4;
            // 
            // T_Pay_Amount_label
            // 
            T_Pay_Amount_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            T_Pay_Amount_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            T_Pay_Amount_label.ForeColor = Color.White;
            T_Pay_Amount_label.Location = new Point(3, 42);
            T_Pay_Amount_label.Margin = new Padding(0);
            T_Pay_Amount_label.Name = "T_Pay_Amount_label";
            T_Pay_Amount_label.Size = new Size(225, 30);
            T_Pay_Amount_label.TabIndex = 2;
            T_Pay_Amount_label.Text = "47.00$";
            T_Pay_Amount_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // TPay_line
            // 
            TPay_line.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TPay_line.BackColor = Color.White;
            TPay_line.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TPay_line.ForeColor = Color.White;
            TPay_line.Location = new Point(10, 37);
            TPay_line.Name = "TPay_line";
            TPay_line.Size = new Size(210, 1);
            TPay_line.TabIndex = 1;
            TPay_line.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Pay_label
            // 
            Total_Pay_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Total_Pay_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Total_Pay_label.ForeColor = Color.White;
            Total_Pay_label.Location = new Point(0, 2);
            Total_Pay_label.Margin = new Padding(0);
            Total_Pay_label.Name = "Total_Pay_label";
            Total_Pay_label.Size = new Size(225, 30);
            Total_Pay_label.TabIndex = 0;
            Total_Pay_label.Text = "Total Payable Amount";
            Total_Pay_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Tax_CardBox
            // 
            Total_Tax_CardBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Total_Tax_CardBox.AutoSize = true;
            Total_Tax_CardBox.BackColor = Color.FromArgb(102, 45, 145);
            Total_Tax_CardBox.Controls.Add(T_Tax_Amout_label);
            Total_Tax_CardBox.Controls.Add(TT_line);
            Total_Tax_CardBox.Controls.Add(Total_Tax_label);
            Total_Tax_CardBox.Location = new Point(248, 5);
            Total_Tax_CardBox.Margin = new Padding(5, 5, 5, 0);
            Total_Tax_CardBox.Name = "Total_Tax_CardBox";
            Total_Tax_CardBox.Size = new Size(233, 75);
            Total_Tax_CardBox.TabIndex = 3;
            // 
            // T_Tax_Amout_label
            // 
            T_Tax_Amout_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            T_Tax_Amout_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            T_Tax_Amout_label.ForeColor = Color.White;
            T_Tax_Amout_label.Location = new Point(3, 46);
            T_Tax_Amout_label.Margin = new Padding(0);
            T_Tax_Amout_label.Name = "T_Tax_Amout_label";
            T_Tax_Amout_label.Size = new Size(225, 30);
            T_Tax_Amout_label.TabIndex = 3;
            T_Tax_Amout_label.Text = "47.00$";
            T_Tax_Amout_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // TT_line
            // 
            TT_line.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TT_line.BackColor = Color.White;
            TT_line.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TT_line.ForeColor = Color.White;
            TT_line.Location = new Point(10, 40);
            TT_line.Name = "TT_line";
            TT_line.Size = new Size(210, 1);
            TT_line.TabIndex = 1;
            TT_line.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Tax_label
            // 
            Total_Tax_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Total_Tax_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Total_Tax_label.ForeColor = Color.White;
            Total_Tax_label.Location = new Point(0, 4);
            Total_Tax_label.Margin = new Padding(0);
            Total_Tax_label.Name = "Total_Tax_label";
            Total_Tax_label.Size = new Size(225, 30);
            Total_Tax_label.TabIndex = 0;
            Total_Tax_label.Text = "Total Tax Amount";
            Total_Tax_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(Total_Profit_CardBox, 3, 0);
            tableLayoutPanel1.Controls.Add(Total_Disc_CardBox, 2, 0);
            tableLayoutPanel1.Controls.Add(Total_Cost_CardBox, 1, 0);
            tableLayoutPanel1.Controls.Add(Total_Sale_CardBox, 0, 0);
            tableLayoutPanel1.Location = new Point(3, 75);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(972, 80);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // Total_Profit_CardBox
            // 
            Total_Profit_CardBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Total_Profit_CardBox.AutoSize = true;
            Total_Profit_CardBox.BackColor = Color.FromArgb(243, 156, 18);
            Total_Profit_CardBox.Controls.Add(T_Profit_Amount_label);
            Total_Profit_CardBox.Controls.Add(TP_line);
            Total_Profit_CardBox.Controls.Add(Total_Profit_label);
            Total_Profit_CardBox.Location = new Point(734, 0);
            Total_Profit_CardBox.Margin = new Padding(5, 0, 5, 0);
            Total_Profit_CardBox.Name = "Total_Profit_CardBox";
            Total_Profit_CardBox.Size = new Size(233, 80);
            Total_Profit_CardBox.TabIndex = 3;
            // 
            // T_Profit_Amount_label
            // 
            T_Profit_Amount_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            T_Profit_Amount_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            T_Profit_Amount_label.ForeColor = Color.White;
            T_Profit_Amount_label.Location = new Point(3, 45);
            T_Profit_Amount_label.Margin = new Padding(0);
            T_Profit_Amount_label.Name = "T_Profit_Amount_label";
            T_Profit_Amount_label.Size = new Size(225, 30);
            T_Profit_Amount_label.TabIndex = 4;
            T_Profit_Amount_label.Text = "47.00$";
            T_Profit_Amount_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // TP_line
            // 
            TP_line.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TP_line.BackColor = Color.White;
            TP_line.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TP_line.ForeColor = Color.White;
            TP_line.Location = new Point(10, 40);
            TP_line.Name = "TP_line";
            TP_line.Size = new Size(210, 1);
            TP_line.TabIndex = 1;
            TP_line.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Profit_label
            // 
            Total_Profit_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Total_Profit_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Total_Profit_label.ForeColor = Color.White;
            Total_Profit_label.Location = new Point(0, 5);
            Total_Profit_label.Name = "Total_Profit_label";
            Total_Profit_label.Size = new Size(225, 30);
            Total_Profit_label.TabIndex = 0;
            Total_Profit_label.Text = "Total Profit Amount";
            Total_Profit_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Disc_CardBox
            // 
            Total_Disc_CardBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Total_Disc_CardBox.AutoSize = true;
            Total_Disc_CardBox.BackColor = Color.FromArgb(231, 76, 60);
            Total_Disc_CardBox.Controls.Add(T_Disc_Amount_label);
            Total_Disc_CardBox.Controls.Add(TD_line);
            Total_Disc_CardBox.Controls.Add(Total_Disc_label);
            Total_Disc_CardBox.Location = new Point(491, 0);
            Total_Disc_CardBox.Margin = new Padding(5, 0, 5, 0);
            Total_Disc_CardBox.Name = "Total_Disc_CardBox";
            Total_Disc_CardBox.Size = new Size(233, 80);
            Total_Disc_CardBox.TabIndex = 2;
            // 
            // T_Disc_Amount_label
            // 
            T_Disc_Amount_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            T_Disc_Amount_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            T_Disc_Amount_label.ForeColor = Color.White;
            T_Disc_Amount_label.Location = new Point(3, 45);
            T_Disc_Amount_label.Margin = new Padding(0);
            T_Disc_Amount_label.Name = "T_Disc_Amount_label";
            T_Disc_Amount_label.Size = new Size(225, 30);
            T_Disc_Amount_label.TabIndex = 5;
            T_Disc_Amount_label.Text = "47.00$";
            T_Disc_Amount_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // TD_line
            // 
            TD_line.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TD_line.BackColor = Color.White;
            TD_line.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TD_line.ForeColor = Color.White;
            TD_line.Location = new Point(10, 40);
            TD_line.Name = "TD_line";
            TD_line.Size = new Size(210, 1);
            TD_line.TabIndex = 1;
            TD_line.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Disc_label
            // 
            Total_Disc_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Total_Disc_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Total_Disc_label.ForeColor = Color.White;
            Total_Disc_label.Location = new Point(0, 5);
            Total_Disc_label.Margin = new Padding(0);
            Total_Disc_label.Name = "Total_Disc_label";
            Total_Disc_label.Size = new Size(225, 30);
            Total_Disc_label.TabIndex = 0;
            Total_Disc_label.Text = "Total Discount Amount";
            Total_Disc_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Cost_CardBox
            // 
            Total_Cost_CardBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Total_Cost_CardBox.AutoSize = true;
            Total_Cost_CardBox.BackColor = Color.FromArgb(3, 169, 244);
            Total_Cost_CardBox.Controls.Add(T_Cost_Amount_label);
            Total_Cost_CardBox.Controls.Add(TC_line);
            Total_Cost_CardBox.Controls.Add(Total_Cost_label);
            Total_Cost_CardBox.ForeColor = Color.White;
            Total_Cost_CardBox.Location = new Point(248, 0);
            Total_Cost_CardBox.Margin = new Padding(5, 0, 5, 0);
            Total_Cost_CardBox.Name = "Total_Cost_CardBox";
            Total_Cost_CardBox.Size = new Size(233, 80);
            Total_Cost_CardBox.TabIndex = 1;
            // 
            // T_Cost_Amount_label
            // 
            T_Cost_Amount_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            T_Cost_Amount_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            T_Cost_Amount_label.ForeColor = Color.White;
            T_Cost_Amount_label.Location = new Point(3, 45);
            T_Cost_Amount_label.Margin = new Padding(0);
            T_Cost_Amount_label.Name = "T_Cost_Amount_label";
            T_Cost_Amount_label.Size = new Size(225, 30);
            T_Cost_Amount_label.TabIndex = 6;
            T_Cost_Amount_label.Text = "47.00$";
            T_Cost_Amount_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // TC_line
            // 
            TC_line.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TC_line.BackColor = Color.White;
            TC_line.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TC_line.ForeColor = Color.White;
            TC_line.Location = new Point(10, 40);
            TC_line.Name = "TC_line";
            TC_line.Size = new Size(210, 1);
            TC_line.TabIndex = 1;
            TC_line.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Cost_label
            // 
            Total_Cost_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Total_Cost_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Total_Cost_label.ForeColor = Color.White;
            Total_Cost_label.Location = new Point(0, 5);
            Total_Cost_label.Name = "Total_Cost_label";
            Total_Cost_label.Size = new Size(225, 30);
            Total_Cost_label.TabIndex = 0;
            Total_Cost_label.Text = "Total Cost Amount";
            Total_Cost_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Sale_CardBox
            // 
            Total_Sale_CardBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Total_Sale_CardBox.BackColor = Color.FromArgb(46, 52, 80);
            Total_Sale_CardBox.Controls.Add(T_Sale_Amount_label);
            Total_Sale_CardBox.Controls.Add(TS_line);
            Total_Sale_CardBox.Controls.Add(Total_Sale_label);
            Total_Sale_CardBox.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            Total_Sale_CardBox.ForeColor = Color.White;
            Total_Sale_CardBox.Location = new Point(10, 0);
            Total_Sale_CardBox.Margin = new Padding(10, 0, 5, 0);
            Total_Sale_CardBox.Name = "Total_Sale_CardBox";
            Total_Sale_CardBox.Size = new Size(228, 80);
            Total_Sale_CardBox.TabIndex = 0;
            // 
            // T_Sale_Amount_label
            // 
            T_Sale_Amount_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            T_Sale_Amount_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            T_Sale_Amount_label.ForeColor = Color.White;
            T_Sale_Amount_label.Location = new Point(3, 45);
            T_Sale_Amount_label.Margin = new Padding(0);
            T_Sale_Amount_label.Name = "T_Sale_Amount_label";
            T_Sale_Amount_label.Size = new Size(225, 30);
            T_Sale_Amount_label.TabIndex = 7;
            T_Sale_Amount_label.Text = "47.00$";
            T_Sale_Amount_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // TS_line
            // 
            TS_line.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TS_line.BackColor = Color.White;
            TS_line.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TS_line.ForeColor = Color.White;
            TS_line.Location = new Point(10, 40);
            TS_line.Name = "TS_line";
            TS_line.Size = new Size(210, 1);
            TS_line.TabIndex = 1;
            TS_line.TextAlign = ContentAlignment.TopCenter;
            // 
            // Total_Sale_label
            // 
            Total_Sale_label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Total_Sale_label.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Total_Sale_label.Location = new Point(0, 5);
            Total_Sale_label.Name = "Total_Sale_label";
            Total_Sale_label.Size = new Size(225, 30);
            Total_Sale_label.TabIndex = 0;
            Total_Sale_label.Text = "Total Sale Amount";
            Total_Sale_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 811);
            Controls.Add(Main_panel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "Dashboard";
            Text = "POS";
            WindowState = FormWindowState.Maximized;
            Load += Dashboard_Load;
            Main_panel.ResumeLayout(false);
            Sidebar_panel.ResumeLayout(false);
            ScreenContainer_panel.ResumeLayout(false);
            ScreenContainer_panel.PerformLayout();
            ProductPanel.ResumeLayout(false);
            ProductButtonsLayout.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ContentContainer_panel.ResumeLayout(false);
            ChartContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Total_Pay_CardBox.ResumeLayout(false);
            Total_Tax_CardBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            Total_Profit_CardBox.ResumeLayout(false);
            Total_Disc_CardBox.ResumeLayout(false);
            Total_Cost_CardBox.ResumeLayout(false);
            Total_Sale_CardBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel Main_panel;
        private Panel Sidebar_panel;
        private Label Menu_Dashboard_label;
        private Label Menu_Products_label;
        private Label Menu_Settings_label;
        private Label Menu_Reports_label;
        private Label Menu_Kitchen_label;
        private Label Menu_POS_label;
        private Label Menu_Staff_label;
        private Label Menu_Tables_label;
        private Label Current_ScreenName_label;
        private Label CurrentUser_label;
        private Panel ContentContainer_panel;
        private Label Splitter_label;
        private Panel ScreenContainer_panel;
        private Label Logo;
        private Label LogoText;
        private Panel Total_Sale_CardBox;
        private Label Total_Sale_label;
        private Panel Total_Disc_CardBox;
        private Label Total_Disc_label;
        private Panel Total_Cost_CardBox;
        private Label Total_Cost_label;
        private Panel Total_Profit_CardBox;
        private Label Total_Profit_label;
        private Label TP_line;
        private Label TC_line;
        private Label TD_line;
        private Label TS_line;
        private Panel Total_Tax_CardBox;
        private Panel Total_Pay_CardBox;
        private Label TPay_line;
        private Label Total_Pay_label;
        private Label TT_line;
        private Label Total_Tax_label;
        private Label T_Pay_Amount_label;
        private Label T_Tax_Amout_label;
        private Label T_Sale_Amount_label;
        private Label T_Cost_Amount_label;
        private Label T_Disc_Amount_label;
        private Label T_Profit_Amount_label;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel ChartContainer;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button button1;
        private ComboBox comboBox1;
        private Panel ProductPanel;
        private TableLayoutPanel ProductButtonsLayout;
        private Button button2;
        private ComboBox comboBox2;
        private Button button3;
        private Panel panel1;
        private TextBox textBox1;
    }
}