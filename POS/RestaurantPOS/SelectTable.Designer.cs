namespace POS
{
    partial class SelectTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectTable));
            panel1 = new Panel();
            SelectTableFlowLayoutPanel = new FlowLayoutPanel();
            panel2 = new Panel();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(SelectTableFlowLayoutPanel);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(896, 527);
            panel1.TabIndex = 0;
            // 
            // SelectTableFlowLayoutPanel
            // 
            SelectTableFlowLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SelectTableFlowLayoutPanel.AutoScroll = true;
            SelectTableFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SelectTableFlowLayoutPanel.Location = new Point(34, 108);
            SelectTableFlowLayoutPanel.MaximumSize = new Size(939, 428);
            SelectTableFlowLayoutPanel.Name = "SelectTableFlowLayoutPanel";
            SelectTableFlowLayoutPanel.Size = new Size(824, 383);
            SelectTableFlowLayoutPanel.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.BackgroundImage = Properties.Resources.Sidebar_panel;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(896, 89);
            panel2.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGray;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(851, 30);
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
            label1.Size = new Size(219, 50);
            label1.TabIndex = 0;
            label1.Text = "Select Table";
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
            // SelectTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 119, 195);
            ClientSize = new Size(898, 529);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SelectTable";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BillList";
            panel1.ResumeLayout(false);
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
        private FlowLayoutPanel SelectTableFlowLayoutPanel;
    }
}