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
            ContentContainer_panel = new Panel();
            Main_panel.SuspendLayout();
            Sidebar_panel.SuspendLayout();
            ScreenContainer_panel.SuspendLayout();
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
            Main_panel.Size = new Size(800, 666);
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
            Sidebar_panel.Size = new Size(266, 666);
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
            Menu_Settings_label.Location = new Point(55, 532);
            Menu_Settings_label.Margin = new Padding(0);
            Menu_Settings_label.Name = "Menu_Settings_label";
            Menu_Settings_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Settings_label.Size = new Size(211, 39);
            Menu_Settings_label.TabIndex = 7;
            Menu_Settings_label.Text = "           Settings";
            Menu_Settings_label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Menu_Reports_label
            // 
            Menu_Reports_label.Anchor = AnchorStyles.None;
            Menu_Reports_label.Font = new Font("Arial", 12F);
            Menu_Reports_label.ForeColor = SystemColors.HighlightText;
            Menu_Reports_label.Image = (Image)resources.GetObject("Menu_Reports_label.Image");
            Menu_Reports_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Reports_label.Location = new Point(55, 472);
            Menu_Reports_label.Margin = new Padding(0);
            Menu_Reports_label.Name = "Menu_Reports_label";
            Menu_Reports_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Reports_label.Size = new Size(211, 39);
            Menu_Reports_label.TabIndex = 6;
            Menu_Reports_label.Text = "           Reports";
            Menu_Reports_label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Menu_Kitchen_label
            // 
            Menu_Kitchen_label.Anchor = AnchorStyles.None;
            Menu_Kitchen_label.Font = new Font("Arial", 12F);
            Menu_Kitchen_label.ForeColor = SystemColors.HighlightText;
            Menu_Kitchen_label.Image = (Image)resources.GetObject("Menu_Kitchen_label.Image");
            Menu_Kitchen_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Kitchen_label.Location = new Point(55, 412);
            Menu_Kitchen_label.Margin = new Padding(0);
            Menu_Kitchen_label.Name = "Menu_Kitchen_label";
            Menu_Kitchen_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Kitchen_label.Size = new Size(211, 39);
            Menu_Kitchen_label.TabIndex = 5;
            Menu_Kitchen_label.Text = "           Kitchen";
            Menu_Kitchen_label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Menu_POS_label
            // 
            Menu_POS_label.Anchor = AnchorStyles.None;
            Menu_POS_label.Font = new Font("Arial", 12F);
            Menu_POS_label.ForeColor = SystemColors.HighlightText;
            Menu_POS_label.Image = (Image)resources.GetObject("Menu_POS_label.Image");
            Menu_POS_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_POS_label.Location = new Point(55, 352);
            Menu_POS_label.Margin = new Padding(0);
            Menu_POS_label.Name = "Menu_POS_label";
            Menu_POS_label.Padding = new Padding(10, 0, 0, 0);
            Menu_POS_label.Size = new Size(211, 39);
            Menu_POS_label.TabIndex = 4;
            Menu_POS_label.Text = "           POS";
            Menu_POS_label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Menu_Staff_label
            // 
            Menu_Staff_label.Anchor = AnchorStyles.None;
            Menu_Staff_label.Font = new Font("Arial", 12F);
            Menu_Staff_label.ForeColor = SystemColors.HighlightText;
            Menu_Staff_label.Image = (Image)resources.GetObject("Menu_Staff_label.Image");
            Menu_Staff_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Staff_label.Location = new Point(55, 292);
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
            Menu_Tables_label.Location = new Point(55, 232);
            Menu_Tables_label.Margin = new Padding(0);
            Menu_Tables_label.Name = "Menu_Tables_label";
            Menu_Tables_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Tables_label.Size = new Size(211, 39);
            Menu_Tables_label.TabIndex = 2;
            Menu_Tables_label.Text = "           Tables";
            Menu_Tables_label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Menu_Products_label
            // 
            Menu_Products_label.Anchor = AnchorStyles.None;
            Menu_Products_label.Font = new Font("Arial", 12F);
            Menu_Products_label.ForeColor = SystemColors.HighlightText;
            Menu_Products_label.Image = (Image)resources.GetObject("Menu_Products_label.Image");
            Menu_Products_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Products_label.Location = new Point(55, 172);
            Menu_Products_label.Margin = new Padding(0);
            Menu_Products_label.Name = "Menu_Products_label";
            Menu_Products_label.Padding = new Padding(10, 0, 0, 0);
            Menu_Products_label.Size = new Size(211, 39);
            Menu_Products_label.TabIndex = 1;
            Menu_Products_label.Text = "           Products";
            Menu_Products_label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Menu_Dashboard_label
            // 
            Menu_Dashboard_label.Anchor = AnchorStyles.None;
            Menu_Dashboard_label.Font = new Font("Arial", 12F);
            Menu_Dashboard_label.ForeColor = SystemColors.HighlightText;
            Menu_Dashboard_label.Image = (Image)resources.GetObject("Menu_Dashboard_label.Image");
            Menu_Dashboard_label.ImageAlign = ContentAlignment.MiddleLeft;
            Menu_Dashboard_label.Location = new Point(55, 112);
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
            ScreenContainer_panel.Controls.Add(ContentContainer_panel);
            ScreenContainer_panel.Location = new Point(264, 3);
            ScreenContainer_panel.Name = "ScreenContainer_panel";
            ScreenContainer_panel.Size = new Size(536, 663);
            ScreenContainer_panel.TabIndex = 0;
            // 
            // CurrentUser_label
            // 
            CurrentUser_label.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CurrentUser_label.AutoSize = true;
            CurrentUser_label.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CurrentUser_label.ForeColor = Color.FromArgb(110, 111, 115);
            CurrentUser_label.Location = new Point(430, 27);
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
            Splitter_label.Size = new Size(533, 1);
            Splitter_label.TabIndex = 5;
            // 
            // ContentContainer_panel
            // 
            ContentContainer_panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ContentContainer_panel.BackColor = Color.White;
            ContentContainer_panel.Location = new Point(21, 95);
            ContentContainer_panel.Name = "ContentContainer_panel";
            ContentContainer_panel.Size = new Size(494, 552);
            ContentContainer_panel.TabIndex = 4;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 662);
            Controls.Add(Main_panel);
            Name = "Dashboard";
            Text = "S";
            WindowState = FormWindowState.Maximized;
            Load += Dashboard_Load;
            Main_panel.ResumeLayout(false);
            Sidebar_panel.ResumeLayout(false);
            ScreenContainer_panel.ResumeLayout(false);
            ScreenContainer_panel.PerformLayout();
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
    }
}