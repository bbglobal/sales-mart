namespace POS
{
    partial class RoomCard
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomCard));
			panel1 = new Panel();
			pictureBox1 = new PictureBox();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = Color.FromArgb(37, 150, 190);
			panel1.Controls.Add(pictureBox1);
			panel1.Location = new Point(0, 0);
			panel1.Margin = new Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new Size(198, 159);
			panel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pictureBox1.BackColor = Color.White;
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(0, 0);
			pictureBox1.Margin = new Padding(0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(198, 159);
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			pictureBox1.Click += pictureBox1_Click;
			pictureBox1.MouseDown += pictureBox1_MouseDown;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			label1.BackColor = Color.Transparent;
			label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.GrayText;
			label1.Location = new Point(0, 158);
			label1.Name = "label1";
			label1.Size = new Size(101, 28);
			label1.TabIndex = 1;
			label1.Text = "A/C Standard";
			label1.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			label2.BackColor = Color.Transparent;
			label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.ForeColor = SystemColors.GrayText;
			label2.Location = new Point(128, 159);
			label2.Name = "label2";
			label2.Size = new Size(67, 27);
			label2.TabIndex = 2;
			label2.Text = "Rent";
			label2.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			label3.BackColor = Color.Transparent;
			label3.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.ForeColor = SystemColors.GrayText;
			label3.Location = new Point(128, 186);
			label3.Name = "label3";
			label3.Size = new Size(67, 27);
			label3.TabIndex = 3;
			label3.Text = "125";
			label3.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			label4.BackColor = Color.Transparent;
			label4.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label4.ForeColor = SystemColors.GrayText;
			label4.Location = new Point(3, 185);
			label4.Name = "label4";
			label4.Size = new Size(107, 28);
			label4.TabIndex = 4;
			label4.Text = "Room View: 101";
			label4.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// RoomCard
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Transparent;
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(panel1);
			Margin = new Padding(3, 3, 0, 2);
			Name = "RoomCard";
			Size = new Size(198, 218);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
	}
}
