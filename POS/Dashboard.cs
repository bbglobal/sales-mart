using POS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Dashboard : Form
    {
        #region All Declared Variables
        private PrivateFontCollection privateFonts = new PrivateFontCollection();
        private System.Windows.Forms.Timer LabelTimer;
        private System.Windows.Forms.Timer PanelTimer;
        private Point Label1_initialPosition;
        private Point Label2_initialPosition;
        private Point Label3_initialPosition;
        private Point Label4_initialPosition;
        private Point Label5_initialPosition;
        private Point Label6_initialPosition;
        private Point Label7_initialPosition;
        private Point Label8_initialPosition;
        private Point Label1_targetPosition;
        private Point Label2_targetPosition;
        private Point Label3_targetPosition;
        private Point Label4_targetPosition;
        private Point Label5_targetPosition;
        private Point Label6_targetPosition;
        private Point Label7_targetPosition;
        private Point Label8_targetPosition;
        private int targetWidth;
        private int initialWidth;
        private int panel4initialWidth;
        private int panel4targetWidth;
        private int panel4initialLeft;
        private int panel4targetLeft;
        private const int AnimationDuration = 600; // Duration of the animation in milliseconds
        private const int PanelAnimationDuration = 100; // Duration of the animation in milliseconds
        private DateTime animationStartTime;

        #endregion
        public Dashboard()
        {
            InitializeComponent();

            InitializeLabel(label1, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\Home.png", 25, 25);
            InitializeLabel(label2, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\Products.png", 25, 25);
            InitializeLabel(label3, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\Tables.png", 25, 27);
            InitializeLabel(label4, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\Staff.png", 25, 25);
            InitializeLabel(label5, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\POS.png", 25, 25);
            InitializeLabel(label6, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\Kitchen.png", 25, 25);
            InitializeLabel(label7, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\Reports.png", 25, 25);
            InitializeLabel(label8, "C:\\Users\\etechs\\source\\repos\\POS\\POS\\images\\sidebar_icons\\Settings1.png", 30, 30);
            RoundCorners(label1, 20);
            InitializeTimer();

            LoadCustomFont("POS.MyriadProSemibold.ttf");

            // Use the custom font for your controls


        }

        private void LoadCustomFont(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    MessageBox.Show("Font resource not found");
                    return;
                }
                // Load the font from the stream
                byte[] fontData = new byte[stream.Length];
                stream.Read(fontData, 0, (int)stream.Length);
                IntPtr data = Marshal.AllocCoTaskMem(fontData.Length);
                Marshal.Copy(fontData, 0, data, fontData.Length);
                privateFonts.AddMemoryFont(data, fontData.Length);
                Marshal.FreeCoTaskMem(data);
            }
        }



        void SetLabelLocations(Label labelSelector, Point points)
        {
            labelSelector.Location = points;
        }
        private void RoundCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(control.ClientRectangle.Left, control.ClientRectangle.Top, radius * 2, radius * 2, 180, 90); // Top left corner
            path.AddArc(control.ClientRectangle.Right - 0 * 2, control.ClientRectangle.Top, radius * 2, radius * 2, 270, 90); // Top right corner
            path.AddArc(control.ClientRectangle.Right - 0 * 2, control.ClientRectangle.Bottom - radius * 2, radius * 2, radius * 2, 0, 90); // Bottom right corner
            path.AddArc(control.ClientRectangle.Left, control.ClientRectangle.Bottom - radius * 2, radius * 2, radius * 2, 90, 90); // Bottom left corner
            path.CloseFigure();
            control.Region = new Region(path);
        }


        private void SetLabelColor(Label label, string hexColor)
        {
            Color color;
            try
            {
                // Convert the hexadecimal color code to a Color object
                color = ColorTranslator.FromHtml(hexColor);
                // Set the label's ForeColor or BackColor property
                label.BackColor = color;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid hexadecimal color code: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Resizing Sidebar Icons
        private void InitializeLabel(Label label, string path, int newWidth, int newHeight)
        {
            // Load the image
            Image image = Image.FromFile(path);
            Image resizedImage = ResizeImage(image, newWidth, newHeight);

            label.Image = resizedImage;
        }

        private Image ResizeImage(Image img, int newWidth, int newHeight)
        {
            Bitmap resizedImg = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, newWidth, newHeight);
            }
            return resizedImg;
        }

        #endregion

        private void label1_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show(label1.Location.X.ToString() + "," + label1.Location.Y.ToString());
            SetLabelColor(label1, "#0077C3");
            StartTransition(60, "Hide");


        }


        private void InitializeTimer()
        {
            PanelTimer = new System.Windows.Forms.Timer();
            PanelTimer.Interval = 10; // Update every 10 milliseconds (adjust as needed for smoother or faster animation)
            PanelTimer.Tick += PanelTimer_Tick;
            LabelTimer = new System.Windows.Forms.Timer();
            LabelTimer.Interval = 100; // Update every 10 milliseconds (adjust as needed for smoother or faster animation)
            LabelTimer.Tick += LabelTimer_Tick;

        }

        private void StartTransition(int newWidth, string status)
        {
            if (status == "Hide")
            {

                Label1_initialPosition = label1.Location;
                Label1_targetPosition = new Point(10, 114);

                Label2_initialPosition = label2.Location;
                Label2_targetPosition = new Point(10, 184);

                Label3_initialPosition = label3.Location;
                Label3_targetPosition = new Point(10, 254);

                Label4_initialPosition = label4.Location;
                Label4_targetPosition = new Point(10, 324);

                Label5_initialPosition = label5.Location;
                Label5_targetPosition = new Point(10, 394);

                Label6_initialPosition = label6.Location;
                Label6_targetPosition = new Point(10, 464);

                Label7_initialPosition = label7.Location;
                Label7_targetPosition = new Point(10, 534);

                Label8_initialPosition = label8.Location;
                Label8_targetPosition = new Point(10, 604);

                initialWidth = panel2.Width;
                targetWidth = newWidth;

                panel4initialLeft = panel4.Left;
                panel4initialWidth = panel4.Width;
                panel4targetWidth = panel4initialWidth + (initialWidth - targetWidth);
                panel4targetLeft = initialWidth - targetWidth;

                animationStartTime = DateTime.Now;
                PanelTimer.Start();
                LabelTimer.Start();

            }
            else
            {
                Label1_initialPosition = label1.Location;
                Label1_targetPosition = new Point(55, 114);

                Label2_initialPosition = label2.Location;
                Label2_targetPosition = new Point(55, 184);

                Label3_initialPosition = label3.Location;
                Label3_targetPosition = new Point(55, 254);

                Label4_initialPosition = label4.Location;
                Label4_targetPosition = new Point(55, 324);

                Label5_initialPosition = label5.Location;
                Label5_targetPosition = new Point(55, 394);

                Label6_initialPosition = label6.Location;
                Label6_targetPosition = new Point(55, 464);

                Label7_initialPosition = label7.Location;
                Label7_targetPosition = new Point(55, 534);

                Label8_initialPosition = label8.Location;
                Label8_targetPosition = new Point(55, 604);

                initialWidth = panel2.Width;
                targetWidth = newWidth;

                panel4initialLeft = panel4.Left;
                panel4initialWidth = panel4.Width;
                panel4targetWidth = panel4initialWidth + (initialWidth - targetWidth);
                panel4targetLeft = initialWidth - targetWidth;

                animationStartTime = DateTime.Now;
                PanelTimer.Start();
                LabelTimer.Start();
            }

        }

        #region Label and Panel Timer Tick Event Functions 

        private void LabelTimer_Tick(object sender, EventArgs e)
        {
            double progress = (DateTime.Now - animationStartTime).TotalMilliseconds / AnimationDuration;
            if (progress >= 1.0)
            {
                LabelTimer.Stop();
                label1.Location = Label1_targetPosition;
                label2.Location = Label2_targetPosition;
                label3.Location = Label3_targetPosition;
                label4.Location = Label4_targetPosition;
                label5.Location = Label5_targetPosition;
                label6.Location = Label6_targetPosition;
                label7.Location = Label7_targetPosition;
                label8.Location = Label8_targetPosition;

            }
            else
            {
                int label1_newX = (int)(Label1_initialPosition.X + (Label1_targetPosition.X - Label1_initialPosition.X) * progress);
                int label1_newY = (int)Label1_targetPosition.Y;
                label1.Location = new Point(label1_newX, label1_newY);

                int label2_newX = (int)(Label2_initialPosition.X + (Label2_targetPosition.X - Label2_initialPosition.X) * progress);
                int label2_newY = (int)Label2_targetPosition.Y;
                label2.Location = new Point(label2_newX, label2_newY);

                int label3_newX = (int)(Label3_initialPosition.X + (Label3_targetPosition.X - Label3_initialPosition.X) * progress);
                int label3_newY = (int)Label3_targetPosition.Y;
                label3.Location = new Point(label3_newX, label3_newY);

                int label4_newX = (int)(Label4_initialPosition.X + (Label4_targetPosition.X - Label4_initialPosition.X) * progress);
                int label4_newY = (int)Label4_targetPosition.Y;
                label4.Location = new Point(label4_newX, label4_newY);

                int label5_newX = (int)(Label5_initialPosition.X + (Label5_targetPosition.X - Label5_initialPosition.X) * progress);
                int label5_newY = (int)Label5_targetPosition.Y;
                label5.Location = new Point(label5_newX, label5_newY);

                int label6_newX = (int)(Label6_initialPosition.X + (Label6_targetPosition.X - Label6_initialPosition.X) * progress);
                int label6_newY = (int)Label6_targetPosition.Y;
                label6.Location = new Point(label6_newX, label6_newY);

                int label7_newX = (int)(Label7_initialPosition.X + (Label7_targetPosition.X - Label7_initialPosition.X) * progress);
                int label7_newY = (int)Label7_targetPosition.Y;
                label7.Location = new Point(label7_newX, label7_newY);

                int label8_newX = (int)(Label8_initialPosition.X + (Label8_targetPosition.X - Label8_initialPosition.X) * progress);
                int label8_newY = (int)Label8_targetPosition.Y;
                label8.Location = new Point(label8_newX, label8_newY);


            }
        }

        private void PanelTimer_Tick(object sender, EventArgs e)
        {
            double progress1 = (DateTime.Now - animationStartTime).TotalMilliseconds / PanelAnimationDuration;
            if (progress1 >= 1.0)
            {
                PanelTimer.Stop();
                panel2.Width = targetWidth;
                panel4.Width = panel4targetWidth;
                panel4.Left = panel4initialLeft - panel4targetLeft;

            }
            else
            {
                int newWidth = (int)(initialWidth + (targetWidth - initialWidth) * progress1);
                panel2.Width = newWidth;

                int panel1newWidth = (int)(panel4initialWidth + (panel4targetWidth - panel4initialWidth) * progress1);
                panel4.Width = panel1newWidth;

                int panel1newLeft = (int)(panel4initialLeft + (panel4targetLeft - panel4initialLeft) * progress1);
                panel4.Left = panel1newLeft;


            }
        }

        #endregion



        private void Dashboard_Load(object sender, EventArgs e)
        {
            SetLabelLocations(label1, new Point(55, 112));
            SetLabelLocations(label2, new Point(55, 182));
            SetLabelLocations(label3, new Point(55, 252));
            SetLabelLocations(label4, new Point(55, 324));
            SetLabelLocations(label5, new Point(55, 394));
            SetLabelLocations(label6, new Point(55, 464));
            SetLabelLocations(label7, new Point(55, 534));
            SetLabelLocations(label8, new Point(55, 604));
            label1.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            label2.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            label3.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            label4.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            label5.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            label6.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            label7.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            label8.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            StartTransition(266, "Show");
        }


    }
}
