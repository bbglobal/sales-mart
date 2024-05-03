namespace POS
{
    partial class Reports
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
            reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            fromDate = new DateTimePicker();
            toDate = new DateTimePicker();
            button1 = new Button();
            SuspendLayout();
            // 
            // reportViewer1
            // 
            reportViewer1.Location = new Point(0, 83);
            reportViewer1.Name = "ReportViewer";
            reportViewer1.ServerReport.BearerToken = null;
            reportViewer1.Size = new Size(670, 700);
            reportViewer1.TabIndex = 0;
            // 
            // fromDate
            // 
            fromDate.Format = DateTimePickerFormat.Custom;
            fromDate.Location = new Point(24, 21);
            fromDate.Name = "fromDate";
            fromDate.Size = new Size(114, 23);
            fromDate.TabIndex = 1;
            // 
            // toDate
            // 
            toDate.Format = DateTimePickerFormat.Custom;
            toDate.Location = new Point(158, 21);
            toDate.Name = "toDate";
            toDate.Size = new Size(104, 23);
            toDate.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(294, 21);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Reports
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(675, 778);
            Controls.Add(button1);
            Controls.Add(toDate);
            Controls.Add(fromDate);
            Controls.Add(reportViewer1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Reports";
            Text = "Reports";
            Load += Reports_Load;
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DateTimePicker fromDate;
        private DateTimePicker toDate;
        private Button button1;
    }
}