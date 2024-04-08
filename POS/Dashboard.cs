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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
        private System.Windows.Forms.Timer LabelTimer;
        private System.Windows.Forms.Timer PanelTimer;
        private Point Menu_Logo_initialPosition;
        private Point Menu_Logo_targetPosition;
        private Point Menu_Dashboard_label_initialPosition;
        private Point Menu_Products_label_initialPosition;
        private Point Menu_Tables_label_initialPosition;
        private Point Menu_Staff_label_initialPosition;
        private Point Menu_POS_label_initialPosition;
        private Point Menu_Kitchen_label_initialPosition;
        private Point Menu_Reports_label_initialPosition;
        private Point Menu_Settings_label_initialPosition;
        private Point Menu_Dashboard_label_targetPosition;
        private Point Menu_Products_label_targetPosition;
        private Point Menu_Tables_label_targetPosition;
        private Point Menu_Staff_label_targetPosition;
        private Point Menu_POS_label_targetPosition;
        private Point Menu_Kitchen_label_targetPosition;
        private Point Menu_Reports_label_targetPosition;
        private Point Menu_Settings_label_targetPosition;
        private int targetWidth;
        private int initialWidth;
        private int ScreenContainer_panelinitialWidth;
        private int ScreenContainer_paneltargetWidth;
        private int ScreenContainer_panelinitialLeft;
        private int ScreenContainer_paneltargetLeft;
        private const int AnimationDuration = 600; // Duration of the animation in milliseconds
        private const int PanelAnimationDuration = 100; // Duration of the animation in milliseconds
        private DateTime animationStartTime;

        #endregion
        public Dashboard()
        {
            InitializeComponent();

            #region Calling Image Resize and Rounded Corner,Timer & Font Functions 
            InitializeLabel(Menu_Dashboard_label, (Image)resources.GetObject("Menu_Dashboard_label.Image"), 25, 25);
            InitializeLabel(Menu_Products_label, (Image)resources.GetObject("Menu_Products_label.Image"), 25, 25);
            InitializeLabel(Menu_Tables_label, (Image)resources.GetObject("Menu_Tables_label.Image"), 25, 27);
            InitializeLabel(Menu_Staff_label, (Image)resources.GetObject("Menu_Staff_label.Image"), 25, 25);
            InitializeLabel(Menu_POS_label, (Image)resources.GetObject("Menu_POS_label.Image"), 25, 25);
            InitializeLabel(Menu_Kitchen_label, (Image)resources.GetObject("Menu_Kitchen_label.Image"), 25, 25);
            InitializeLabel(Menu_Reports_label, (Image)resources.GetObject("Menu_Reports_label.Image"), 25, 25);
            InitializeLabel(Menu_Settings_label, (Image)resources.GetObject("Menu_Settings_label.Image"), 30, 30);
            InitializeLabel(Logo, (Image)resources.GetObject("Logo.Image"), 30, 45);
            InitializeLabel(LogoText, (Image)resources.GetObject("LogoText.Image"), 150, 25);
            RoundCorners(Menu_Dashboard_label, 20);
            RoundCorners(Menu_Products_label, 20);
            RoundCorners(Menu_Tables_label, 20);
            RoundCorners(Menu_Staff_label, 20);
            RoundCorners(Menu_POS_label, 20);
            RoundCorners(Menu_Kitchen_label, 20);
            RoundCorners(Menu_Reports_label, 20);
            RoundCorners(Menu_Settings_label, 20);
            InitializeTimer();
            LoadCustomFont("POS.MyriadProSemibold.ttf");
            #endregion
        }

        #region Setting Label Fonts, Locations, Colors & Rounding Corners Functions

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

        #endregion

        #region Resizing Sidebar Icon Functions
        private void InitializeLabel(Label label, Image image, int newWidth, int newHeight)
        {
            // Load the image
            //string fullPath = Path.Combine(GetPro, RelativePath);
            //Image image = Image.FromFile(path);
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


        #region Timer Initialization and Transition Functions

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

                Menu_Dashboard_label_initialPosition = Menu_Dashboard_label.Location;
                Menu_Dashboard_label_targetPosition = new Point(10, 114);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(10, 184);

                Menu_Tables_label_initialPosition = Menu_Tables_label.Location;
                Menu_Tables_label_targetPosition = new Point(10, 254);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(10, 324);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(10, 394);

                Menu_Kitchen_label_initialPosition = Menu_Kitchen_label.Location;
                Menu_Kitchen_label_targetPosition = new Point(10, 464);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(10, 534);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(10, 604);

                Menu_Logo_initialPosition = Logo.Location;
                Menu_Logo_targetPosition = new Point(10, 30);

                initialWidth = Sidebar_panel.Width;
                targetWidth = newWidth;

                ScreenContainer_panelinitialLeft = ScreenContainer_panel.Left;
                ScreenContainer_panelinitialWidth = ScreenContainer_panel.Width;
                ScreenContainer_paneltargetWidth = ScreenContainer_panelinitialWidth + (initialWidth - targetWidth);
                ScreenContainer_paneltargetLeft = initialWidth - targetWidth;

                animationStartTime = DateTime.Now;
                PanelTimer.Start();
                LabelTimer.Start();

            }
            else
            {
                Menu_Dashboard_label_initialPosition = Menu_Dashboard_label.Location;
                Menu_Dashboard_label_targetPosition = new Point(55, 114);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(55, 184);

                Menu_Tables_label_initialPosition = Menu_Tables_label.Location;
                Menu_Tables_label_targetPosition = new Point(55, 254);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(55, 324);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(55, 394);

                Menu_Kitchen_label_initialPosition = Menu_Kitchen_label.Location;
                Menu_Kitchen_label_targetPosition = new Point(55, 464);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(55, 534);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(55, 604);

                Menu_Logo_initialPosition = Logo.Location;
                Menu_Logo_targetPosition = new Point(36, 30);

                initialWidth = Sidebar_panel.Width;
                targetWidth = newWidth;

                ScreenContainer_panelinitialLeft = ScreenContainer_panel.Left;
                ScreenContainer_panelinitialWidth = ScreenContainer_panel.Width;
                ScreenContainer_paneltargetWidth = ScreenContainer_panelinitialWidth + (initialWidth - targetWidth);
                ScreenContainer_paneltargetLeft = initialWidth - targetWidth;

                animationStartTime = DateTime.Now;
                PanelTimer.Start();
                LabelTimer.Start();
            }

        }

        #endregion

        #region Label and Panel Timer Tick Event Functions 

        private void LabelTimer_Tick(object sender, EventArgs e)
        {
            double progress = (DateTime.Now - animationStartTime).TotalMilliseconds / AnimationDuration;
            if (progress >= 1.0)
            {
                LabelTimer.Stop();
                Menu_Dashboard_label.Location = Menu_Dashboard_label_targetPosition;
                Menu_Products_label.Location = Menu_Products_label_targetPosition;
                Menu_Tables_label.Location = Menu_Tables_label_targetPosition;
                Menu_Staff_label.Location = Menu_Staff_label_targetPosition;
                Menu_POS_label.Location = Menu_POS_label_targetPosition;
                Menu_Kitchen_label.Location = Menu_Kitchen_label_targetPosition;
                Menu_Reports_label.Location = Menu_Reports_label_targetPosition;
                Menu_Settings_label.Location = Menu_Settings_label_targetPosition;
                Logo.Location = Menu_Logo_targetPosition;

            }
            else
            {
                int Menu_Dashboard_label_newX = (int)(Menu_Dashboard_label_initialPosition.X + (Menu_Dashboard_label_targetPosition.X - Menu_Dashboard_label_initialPosition.X) * progress);
                int Menu_Dashboard_label_newY = (int)Menu_Dashboard_label_targetPosition.Y;
                Menu_Dashboard_label.Location = new Point(Menu_Dashboard_label_newX, Menu_Dashboard_label_newY);

                int Menu_Products_label_newX = (int)(Menu_Products_label_initialPosition.X + (Menu_Products_label_targetPosition.X - Menu_Products_label_initialPosition.X) * progress);
                int Menu_Products_label_newY = (int)Menu_Products_label_targetPosition.Y;
                Menu_Products_label.Location = new Point(Menu_Products_label_newX, Menu_Products_label_newY);

                int Menu_Tables_label_newX = (int)(Menu_Tables_label_initialPosition.X + (Menu_Tables_label_targetPosition.X - Menu_Tables_label_initialPosition.X) * progress);
                int Menu_Tables_label_newY = (int)Menu_Tables_label_targetPosition.Y;
                Menu_Tables_label.Location = new Point(Menu_Tables_label_newX, Menu_Tables_label_newY);

                int Menu_Staff_label_newX = (int)(Menu_Staff_label_initialPosition.X + (Menu_Staff_label_targetPosition.X - Menu_Staff_label_initialPosition.X) * progress);
                int Menu_Staff_label_newY = (int)Menu_Staff_label_targetPosition.Y;
                Menu_Staff_label.Location = new Point(Menu_Staff_label_newX, Menu_Staff_label_newY);

                int Menu_POS_label_newX = (int)(Menu_POS_label_initialPosition.X + (Menu_POS_label_targetPosition.X - Menu_POS_label_initialPosition.X) * progress);
                int Menu_POS_label_newY = (int)Menu_POS_label_targetPosition.Y;
                Menu_POS_label.Location = new Point(Menu_POS_label_newX, Menu_POS_label_newY);

                int Menu_Kitchen_label_newX = (int)(Menu_Kitchen_label_initialPosition.X + (Menu_Kitchen_label_targetPosition.X - Menu_Kitchen_label_initialPosition.X) * progress);
                int Menu_Kitchen_label_newY = (int)Menu_Kitchen_label_targetPosition.Y;
                Menu_Kitchen_label.Location = new Point(Menu_Kitchen_label_newX, Menu_Kitchen_label_newY);

                int Menu_Reports_label_newX = (int)(Menu_Reports_label_initialPosition.X + (Menu_Reports_label_targetPosition.X - Menu_Reports_label_initialPosition.X) * progress);
                int Menu_Reports_label_newY = (int)Menu_Reports_label_targetPosition.Y;
                Menu_Reports_label.Location = new Point(Menu_Reports_label_newX, Menu_Reports_label_newY);

                int Menu_Settings_label_newX = (int)(Menu_Settings_label_initialPosition.X + (Menu_Settings_label_targetPosition.X - Menu_Settings_label_initialPosition.X) * progress);
                int Menu_Settings_label_newY = (int)Menu_Settings_label_targetPosition.Y;
                Menu_Settings_label.Location = new Point(Menu_Settings_label_newX, Menu_Settings_label_newY);

                int Menu_Logo_newX = (int)(Menu_Logo_initialPosition.X + (Menu_Logo_targetPosition.X - Menu_Logo_initialPosition.X) * progress);
                int Menu_Logo_newY = (int)Menu_Logo_targetPosition.Y;
                Logo.Location = new Point(Menu_Logo_newX, Menu_Logo_newY);


            }
        }

        private void PanelTimer_Tick(object sender, EventArgs e)
        {
            double progress1 = (DateTime.Now - animationStartTime).TotalMilliseconds / PanelAnimationDuration;
            if (progress1 >= 1.0)
            {
                PanelTimer.Stop();
                Sidebar_panel.Width = targetWidth;
                ScreenContainer_panel.Width = ScreenContainer_paneltargetWidth;
                ScreenContainer_panel.Left = ScreenContainer_panelinitialLeft - ScreenContainer_paneltargetLeft;

            }
            else
            {
                int newWidth = (int)(initialWidth + (targetWidth - initialWidth) * progress1);
                Sidebar_panel.Width = newWidth;

                int Main_panelnewWidth = (int)(ScreenContainer_panelinitialWidth + (ScreenContainer_paneltargetWidth - ScreenContainer_panelinitialWidth) * progress1);
                ScreenContainer_panel.Width = Main_panelnewWidth;

                int Main_panelnewLeft = (int)(ScreenContainer_panelinitialLeft + (ScreenContainer_paneltargetLeft - ScreenContainer_panelinitialLeft) * progress1);
                ScreenContainer_panel.Left = Main_panelnewLeft;


            }
        }

        #endregion


        #region Dashboard Load Event
        private void Dashboard_Load(object sender, EventArgs e)
        {
            int CurrentUserInitialWidth = CurrentUser_label.Width;
            CurrentUser_label.Text = $"User Name:  {Session.Username}";
            CurrentUser_label.Location = new Point(CurrentUser_label.Location.X - (CurrentUser_label.Width - CurrentUserInitialWidth), CurrentUser_label.Location.Y);
            Set_CardBox_Positions();
            SetLabelLocations(Menu_Dashboard_label, new Point(55, 112));
            SetLabelLocations(Menu_Products_label, new Point(55, 182));
            SetLabelLocations(Menu_Tables_label, new Point(55, 252));
            SetLabelLocations(Menu_Staff_label, new Point(55, 324));
            SetLabelLocations(Menu_POS_label, new Point(55, 394));
            SetLabelLocations(Menu_Kitchen_label, new Point(55, 464));
            SetLabelLocations(Menu_Reports_label, new Point(55, 534));
            SetLabelLocations(Menu_Settings_label, new Point(55, 604));
            SetLabelColor(Menu_Dashboard_label, "#0077C3");
            Menu_Dashboard_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            Menu_Products_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            Menu_Tables_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            Menu_Staff_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            Menu_POS_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            Menu_Kitchen_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            Menu_Reports_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            Menu_Settings_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
        }

        #endregion

        #region Label Click Event Functions
        private void Logo_Click(object sender, EventArgs e)
        {
            if (Sidebar_panel.Width == 266)
            {
                StartTransition(60, "Hide");
            }
            else
            {
                StartTransition(266, "Show");
            }


        }

        private void LogoText_Click(object sender, EventArgs e)
        {
            if (Sidebar_panel.Width == 266)
            {
                StartTransition(60, "Hide");
            }
            else
            {
                StartTransition(266, "Show");
            }
        }
        private void Menu_Dashboard_label_Click_1(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Dashboard_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;

        }
        private void Menu_Staff_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Staff_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
        }
        #endregion

        #region Card Box Positions Functions
        private void Set_CardBox_Positions()
        {
            Total_Cost_CardBox.Location = new Point(Total_Sale_CardBox.Location.X + 240, Total_Cost_CardBox.Location.Y);
            Total_Disc_CardBox.Location = new Point(Total_Cost_CardBox.Location.X + 240, Total_Disc_CardBox.Location.Y);
            Total_Profit_CardBox.Location = new Point(Total_Disc_CardBox.Location.X + 240, Total_Profit_CardBox.Location.Y);
            Total_Tax_CardBox.Location = new Point(Total_Cost_CardBox.Location.X, Total_Cost_CardBox.Location.Y + 100);
            Total_Pay_CardBox.Location = new Point(Total_Disc_CardBox.Location.X, Total_Disc_CardBox.Location.Y + 100);
        }
        #endregion

        #region Sidebar Labels Click Event Functions
        private void Menu_Products_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Products_label, "#0077C3");
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
        }

        private void Menu_Tables_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Tables_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
        }

        private void Menu_POS_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_POS_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
        }

        private void Menu_Kitchen_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Kitchen_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
        }

        private void Menu_Reports_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Reports_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
        }

        private void Menu_Settings_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Settings_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
        }
        #endregion
    }
}
