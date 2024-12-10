using POS.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;

namespace POS
{
    public partial class GSDashboard : Form
    {
        #region All Declared Variables
        private PrivateFontCollection privateFonts = new PrivateFontCollection();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GSDashboard));
        private System.Windows.Forms.Timer LabelTimer;
        private System.Windows.Forms.Timer PanelTimer;
        private Point Menu_Logo_initialPosition;
        private Point Menu_Logo_targetPosition;
        private Point Menu_Dashboard_label_initialPosition;
        private Point Menu_Products_label_initialPosition;
        private Point Menu_Staff_label_initialPosition;
        private Point Menu_Purchase_label_initialPosition;
        private Point Menu_Supplier_label_initialPosition;
        private Point Menu_Client_label_initialPosition;
        private Point Menu_Layby_label_initialPosition;
        private Point Menu_POS_label_initialPosition;
        private Point Menu_Reports_label_initialPosition;
        private Point Menu_Branch_label_initialPosition;
        private Point Menu_Settings_label_initialPosition;
        private Point Menu_Admin_label_initialPosition;
        private Point Menu_Attendance_label_initialPosition;
        private Point LogOutLabel_initialPosition;


        private Point Menu_Dashboard_label_targetPosition;
        private Point Menu_Products_label_targetPosition;
        private Point Menu_Staff_label_targetPosition;
        private Point Menu_Purchase_label_targetPosition;
        private Point Menu_Supplier_label_targetPosition;
        private Point Menu_Client_label_targetPosition;
        private Point Menu_Layby_label_targetPosition;
        private Point Menu_POS_label_targetPosition;
        private Point Menu_Reports_label_targetPosition;
        private Point Menu_Branch_label_targetPosition;
        private Point Menu_Settings_label_targetPosition;
        private Point Menu_Admin_label_targetPosition;
        private Point Menu_Attendance_label_targetPosition;
        private Point LogOutLabel_targetPosition;

        private string Unionquery = @"
        SELECT username, password, email, Role, Access FROM POS.dbo.users
        UNION ALL
        SELECT username, password, email, role, Access FROM GeneralStorePOS.dbo.users
        UNION ALL
        SELECT username, password, email, role, Access FROM HotelManagementPOS.dbo.users;";

        private int targetWidth;
        private int initialWidth;
        private int ScreenContainer_panelinitialWidth;
        private int ScreenContainer_paneltargetWidth;
        private int ScreenContainer_panelinitialLeft;
        private int ScreenContainer_paneltargetLeft;
        private const int AnimationDuration = 600; // Duration of the animation in milliseconds
        private const int PanelAnimationDuration = 100; // Duration of the animation in milliseconds
        private DateTime animationStartTime;
        private SqlConnection connection;
        private SqlCommand command;
        private DataGridView WorkingDataGridView;
        Image EditImage;
        Image DeleteImage;
        Image DetailsImage; 
        Image PayImage;
        string connectionString;

        #endregion
        public GSDashboard()
        {
            InitializeComponent();
            AdjustFormSize();
            InitializeDatabaseConnection();
            ImageEditDelLoad();
            ImageDetailsPayLoad();
            SupplierDashboardData();

            #region Calling Image Resize and Rounded Corner,Timer & Font Functions 

            InitializeLabel(Menu_Dashboard_label, (Image)resources.GetObject("Menu_Dashboard_label.Image"), 25, 25);
            InitializeLabel(Menu_Products_label, (Image)resources.GetObject("Menu_Products_label.Image"), 25, 25);
            InitializeLabel(Menu_Transactions_label, (Image)resources.GetObject("Menu_Transactions_label.Image"), 25, 27);
            InitializeLabel(Menu_Supplier_label, (Image)resources.GetObject("Menu_Supplier_label.Image"), 25, 27);
            InitializeLabel(Menu_Client_label, (Image)resources.GetObject("Menu_Client_label.Image"), 25, 27);
            InitializeLabel(Menu_Staff_label, (Image)resources.GetObject("Menu_Staff_label.Image"), 25, 25);
            InitializeLabel(Menu_POS_label, (Image)resources.GetObject("Menu_POS_label.Image"), 25, 25);
            InitializeLabel(Menu_Reports_label, (Image)resources.GetObject("Menu_Reports_label.Image"), 25, 25);
            InitializeLabel(Logo, (Image)resources.GetObject("Logo.Image"), 30, 45);
            InitializeLabel(LogoText, (Image)resources.GetObject("LogoText.Image"), 150, 25);
            InitializeLabel(Menu_Admin_Label, (Image)resources.GetObject("Menu_Admin_Label.Image"), 25, 25);
            InitializeLabel(LogOutLabel, (Image)resources.GetObject("LogOutLabel.Image"), 25, 25);
            InitializeLabel(Menu_Settings_label, (Image)resources.GetObject("Menu_Settings_label.Image"), 25, 25);
            InitializeLabel(Menu_Attendance_Label, (Image)resources.GetObject("Menu_Attendance_Label.Image"), 25, 25);
            RoundCorners(Menu_Dashboard_label, 20);
            RoundCorners(Menu_Products_label, 20);
            RoundCorners(Menu_Transactions_label, 20);
            RoundCorners(Menu_Staff_label, 20);
            RoundCorners(Menu_POS_label, 20);
            RoundCorners(Menu_Transactions_label, 20);
            RoundCorners(Menu_Supplier_label, 20);
            RoundCorners(Menu_Client_label, 20);
            RoundCorners(Menu_Reports_label, 20);
            RoundCorners(Menu_Branch_label, 20);
            RoundCorners(Menu_Layby_label, 20);
            RoundCorners(Menu_Attendance_Label, 20);
            RoundCorners(Menu_Admin_Label, 20);
            RoundCorners(Menu_Settings_label, 20);
            RoundCorners(LogOutLabel, 20);
            InitializeTimer();
            #endregion

        }


        #region Adjust Form Size Function
        private void AdjustFormSize()
        {
            var screenBounds = Screen.PrimaryScreen.Bounds;

            // Get the working area (excluding taskbars)
            var workingArea = Screen.PrimaryScreen.WorkingArea;

            // Calculate the taskbar height
            int taskbarHeight = screenBounds.Height - workingArea.Height;

            // Set form size to match the screen size excluding taskbar
            this.Width = screenBounds.Width;
            this.Height = screenBounds.Height - taskbarHeight;

            // Set form location to top-left corner
            //this.Location = new Point(0, 0);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #endregion


        #region All Necessary Functions(Sidebar Labels,Icons,Animations and ByteToImage)

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

        #region Byte Array to Image Function
        private Image ByteArraytoImage(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                Image image = new Bitmap(Image.FromStream(ms));
                return image;
            }
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
                Menu_Dashboard_label_targetPosition = new Point(10, 112);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(10, 172);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(10, 232);

                Menu_Purchase_label_initialPosition = Menu_Transactions_label.Location;
                Menu_Purchase_label_targetPosition = new Point(10, 292);

                Menu_Supplier_label_initialPosition = Menu_Supplier_label.Location;
                Menu_Supplier_label_targetPosition = new Point(10, 352);

                Menu_Client_label_initialPosition = Menu_Client_label.Location;
                Menu_Client_label_targetPosition = new Point(10, 412);

                Menu_Layby_label_initialPosition = Menu_Layby_label.Location;
                Menu_Layby_label_targetPosition = new Point(10, 472);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(10, 532);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(10, 592);

                Menu_Branch_label_initialPosition = Menu_Branch_label.Location;
                Menu_Branch_label_targetPosition = new Point(10, 652);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(10, 712);

                Menu_Admin_label_initialPosition = Menu_Admin_Label.Location;
                Menu_Admin_label_targetPosition = new Point(10, 772);

                Menu_Attendance_label_initialPosition = Menu_Attendance_Label.Location;
                Menu_Attendance_label_targetPosition = new Point(10, 832);

                LogOutLabel_initialPosition = LogOutLabel.Location;
                LogOutLabel_targetPosition = new Point(13, 892);

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
                Menu_Dashboard_label_targetPosition = new Point(55, 112);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(55, 172);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(55, 232);

                Menu_Purchase_label_initialPosition = Menu_Transactions_label.Location;
                Menu_Purchase_label_targetPosition = new Point(55, 292);

                Menu_Supplier_label_initialPosition = Menu_Supplier_label.Location;
                Menu_Supplier_label_targetPosition = new Point(55, 352);

                Menu_Client_label_initialPosition = Menu_Client_label.Location;
                Menu_Client_label_targetPosition = new Point(55, 412);

                Menu_Layby_label_initialPosition = Menu_Layby_label.Location;
                Menu_Layby_label_targetPosition = new Point(55, 472);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(55, 532);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(55, 592);

                Menu_Branch_label_initialPosition = Menu_Branch_label.Location;
                Menu_Branch_label_targetPosition = new Point(55, 652);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(55, 712);

                Menu_Admin_label_initialPosition = Menu_Admin_Label.Location;
                Menu_Admin_label_targetPosition = new Point(55, 772);

                Menu_Attendance_label_initialPosition = Menu_Attendance_Label.Location;
                Menu_Attendance_label_targetPosition = new Point(55, 832);

                LogOutLabel_initialPosition = LogOutLabel.Location;
                LogOutLabel_targetPosition = new Point(58, 892);

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
                Menu_Staff_label.Location = Menu_Staff_label_targetPosition;
                Menu_Transactions_label.Location = Menu_Purchase_label_targetPosition;
                Menu_Supplier_label.Location = Menu_Supplier_label_targetPosition;
                Menu_Client_label.Location = Menu_Client_label_targetPosition;
                Menu_Layby_label.Location = Menu_Layby_label_targetPosition;
                Menu_POS_label.Location = Menu_POS_label_targetPosition;
                Menu_Reports_label.Location = Menu_Reports_label_targetPosition;
                Menu_Branch_label.Location = Menu_Branch_label_targetPosition;
                Logo.Location = Menu_Logo_targetPosition;
                Menu_Attendance_Label.Location = Menu_Attendance_label_targetPosition;
                Menu_Admin_Label.Location = Menu_Admin_label_targetPosition;
                Menu_Settings_label.Location = Menu_Settings_label_targetPosition;
                LogOutLabel.Location = LogOutLabel_targetPosition;

            }
            else
            {
                int Menu_Dashboard_label_newX = (int)(Menu_Dashboard_label_initialPosition.X + (Menu_Dashboard_label_targetPosition.X - Menu_Dashboard_label_initialPosition.X) * progress);
                int Menu_Dashboard_label_newY = (int)Menu_Dashboard_label_targetPosition.Y;
                Menu_Dashboard_label.Location = new Point(Menu_Dashboard_label_newX, Menu_Dashboard_label_newY);

                int Menu_Products_label_newX = (int)(Menu_Products_label_initialPosition.X + (Menu_Products_label_targetPosition.X - Menu_Products_label_initialPosition.X) * progress);
                int Menu_Products_label_newY = (int)Menu_Products_label_targetPosition.Y;
                Menu_Products_label.Location = new Point(Menu_Products_label_newX, Menu_Products_label_newY);

                int Menu_Purchase_label_newX = (int)(Menu_Purchase_label_initialPosition.X + (Menu_Purchase_label_targetPosition.X - Menu_Purchase_label_initialPosition.X) * progress);
                int Menu_Purchase_label_newY = (int)Menu_Purchase_label_targetPosition.Y;
                Menu_Transactions_label.Location = new Point(Menu_Purchase_label_newX, Menu_Purchase_label_newY);


                int Menu_Supplier_label_newX = (int)(Menu_Supplier_label_initialPosition.X + (Menu_Supplier_label_targetPosition.X - Menu_Supplier_label_initialPosition.X) * progress);
                int Menu_Supplier_label_newY = (int)Menu_Supplier_label_targetPosition.Y;
                Menu_Supplier_label.Location = new Point(Menu_Supplier_label_newX, Menu_Supplier_label_newY);


                int Menu_Client_label_newX = (int)(Menu_Client_label_initialPosition.X + (Menu_Client_label_targetPosition.X - Menu_Client_label_initialPosition.X) * progress);
                int Menu_Client_label_newY = (int)Menu_Client_label_targetPosition.Y;
                Menu_Client_label.Location = new Point(Menu_Client_label_newX, Menu_Client_label_newY);


                int Menu_Layby_label_newX = (int)(Menu_Layby_label_initialPosition.X + (Menu_Layby_label_targetPosition.X - Menu_Layby_label_initialPosition.X) * progress);
                int Menu_Layby_label_newY = (int)Menu_Layby_label_targetPosition.Y;
                Menu_Layby_label.Location = new Point(Menu_Layby_label_newX, Menu_Layby_label_newY);


                int Menu_Staff_label_newX = (int)(Menu_Staff_label_initialPosition.X + (Menu_Staff_label_targetPosition.X - Menu_Staff_label_initialPosition.X) * progress);
                int Menu_Staff_label_newY = (int)Menu_Staff_label_targetPosition.Y;
                Menu_Staff_label.Location = new Point(Menu_Staff_label_newX, Menu_Staff_label_newY);

                int Menu_POS_label_newX = (int)(Menu_POS_label_initialPosition.X + (Menu_POS_label_targetPosition.X - Menu_POS_label_initialPosition.X) * progress);
                int Menu_POS_label_newY = (int)Menu_POS_label_targetPosition.Y;
                Menu_POS_label.Location = new Point(Menu_POS_label_newX, Menu_POS_label_newY);

                int Menu_Reports_label_newX = (int)(Menu_Reports_label_initialPosition.X + (Menu_Reports_label_targetPosition.X - Menu_Reports_label_initialPosition.X) * progress);
                int Menu_Reports_label_newY = (int)Menu_Reports_label_targetPosition.Y;
                Menu_Reports_label.Location = new Point(Menu_Reports_label_newX, Menu_Reports_label_newY);

                int Menu_Branch_label_newX = (int)(Menu_Branch_label_initialPosition.X + (Menu_Branch_label_targetPosition.X - Menu_Branch_label_initialPosition.X) * progress);
                int Menu_Branch_label_newY = (int)Menu_Branch_label_targetPosition.Y;
                Menu_Branch_label.Location = new Point(Menu_Branch_label_newX, Menu_Branch_label_newY);

                int Menu_Logo_newX = (int)(Menu_Logo_initialPosition.X + (Menu_Logo_targetPosition.X - Menu_Logo_initialPosition.X) * progress);
                int Menu_Logo_newY = (int)Menu_Logo_targetPosition.Y;
                Logo.Location = new Point(Menu_Logo_newX, Menu_Logo_newY);

                int Menu_Admin_newX = (int)(Menu_Admin_label_initialPosition.X + (Menu_Admin_label_targetPosition.X - Menu_Admin_label_initialPosition.X) * progress);
                int Menu_Admin_newY = (int)Menu_Admin_label_targetPosition.Y;
                Menu_Admin_Label.Location = new Point(Menu_Admin_newX, Menu_Admin_newY);

                int Menu_Settings_label_newX = (int)(Menu_Settings_label_initialPosition.X + (Menu_Settings_label_targetPosition.X - Menu_Settings_label_initialPosition.X) * progress);
                int Menu_Settings_label_newY = (int)Menu_Settings_label_targetPosition.Y;
                Menu_Settings_label.Location = new Point(Menu_Settings_label_newX, Menu_Settings_label_newY);

                int Menu_Attendance_label_newX = (int)(Menu_Attendance_label_initialPosition.X + (Menu_Attendance_label_targetPosition.X - Menu_Attendance_label_initialPosition.X) * progress);
                int Menu_Attendance_label_newY = (int)Menu_Attendance_label_targetPosition.Y;
                Menu_Attendance_Label.Location = new Point(Menu_Attendance_label_newX, Menu_Attendance_label_newY);

                int LogOutLabel_newX = (int)(LogOutLabel_initialPosition.X + (LogOutLabel_targetPosition.X - LogOutLabel_initialPosition.X) * progress);
                int LogOutLabel_newY = (int)LogOutLabel_targetPosition.Y;
                LogOutLabel.Location = new Point(LogOutLabel_newX, LogOutLabel_newY);

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

        #region Sidebar Labels Click Event Functions

        private void Logo_Click(object sender, EventArgs e)
        {
            if (POSPanel.Visible != true)
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

        }

        private void LogoText_Click(object sender, EventArgs e)
        {
            if (POSPanel.Visible != true)
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
        }
        private void Menu_Dashboard_label_Click_1(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Dashboard_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Dashboard";
            if (ContentContainer_panel.Visible == false)
            {
                ProductPanel.Visible = false;
                StaffPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = false;
                //ProductPanel.Visible = false;
                ContentContainer_panel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;

            }
        }

        private void Menu_Products_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Products_label, "#0077C3");
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Items";
            if (ProductPanel.Visible == false)
            {
                ContentContainer_panel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = false;
                //ProductPanel.Visible = false;
                ProductPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;


            }
        }



        private int ControlsCount = 0;
        private async void Menu_POS_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_POS_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "POS";
            if (POSPanel.Visible == false)
            {
                POSPanel.Visible = true;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                StaffPanel.Visible = false;
                SupplierPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = false;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;

                StartTransition(60, "Hide");
                AddPOSCategory();
                await Task.Delay(300);
                LoadPOSProducts();


                if (CategoriesFlowLayoutPanel.Controls.Count > ControlsCount + 5)
                {
                    int position = CategoriesFlowLayoutPanel.Location.Y + CategoriesFlowLayoutPanel.Height;
                    int height = ProductsFlowLayoutPanel.Location.Y;
                    ProductsFlowLayoutPanel.Location = new Point(ProductsFlowLayoutPanel.Location.X, position);
                    ProductsFlowLayoutPanel.Height = ProductsFlowLayoutPanel.Height - (position - height);
                    ControlsCount += 5;
                }
            }
            //await Task.Delay(500);
            ProductDataGrid_panel.Visible = true;
        }



        private void Menu_Staff_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Staff_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Staff";
            if (StaffPanel.Visible == false)
            {
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = false;
                //ProductPanel.Visible = false;
                StaffPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;

            }
        }

        private void Menu_Purchase_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Transactions_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Transactions";
            if (PurchasePanel.Visible == false)
            {
                ProductPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = false;
                ProductPanel.Visible = false;
                PurchasePanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;
                button4.Visible = true;
                PurchaseAddButton.Visible = true;
                PurchaseDataGrid.Visible = true;
                Purchase_SearchTextBox.Visible = true;
                PurchasesButton.BackColor = Color.FromArgb(37, 150, 190);
                PurchasesButton.ForeColor = Color.White;
                ManageOrdersButton.BackColor = Color.Transparent;
                ManageOrdersButton.ForeColor = SystemColors.GrayText;

            }
        }


        private void Menu_Supplier_label_Click(object sender, EventArgs e)
        {


            SetLabelColor(Menu_Supplier_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Suppliers";
            if (SupplierPanel.Visible == false)
            {
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = false;
                //ProductPanel.Visible = false;
                SupplierPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;

            }


        }

        private void Menu_Client_label_Click(object sender, EventArgs e)
        {


            SetLabelColor(Menu_Client_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Clients";
            if (ClientsPanel.Visible == false)
            {
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                PurchasePanel.Visible = false;
                StaffPanel.Visible = false;
                LaybyPanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = false;
                //ProductPanel.Visible = false;
                ClientsPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;

            }


        }

        private void Menu_Layby_label_Click(object sender, EventArgs e)
        {


            SetLabelColor(Menu_Layby_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Layby";
            if (LaybyPanel.Visible == false)
            {
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                PurchasePanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                ClientsPanel.Visible = false;
                BranchPanel.Visible = false;
                //ProductPanel.Visible = false;
                LaybyPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;

            }


        }


        private void Menu_Reports_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Reports_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Reports";
            if (ReportsPanel.Visible == false)
            {
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                StaffPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                BranchPanel.Visible = false;
                //ProductPanel.Visible = false;
                ReportsPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;

            }
        }
        private void Menu_Branch_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Branch_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Branch Management";
            if (BranchPanel.Visible == false)
            {
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                StaffPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                //ProductPanel.Visible = false;
                ReportsPanel.Visible = false;
                BranchPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;
            }
        }

        private void Menu_Attendance_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Attendance_Label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;

            ProductPanel.Visible = false;
            BranchPanel.Visible = false;
            ContentContainer_panel.Visible = false;
            POSPanel.Visible = false;
            SupplierPanel.Visible = false;
            StaffPanel.Visible = false;
            ClientsPanel.Visible = false;
            LaybyPanel.Visible = false;
            PurchasePanel.Visible = false;
            ReportsPanel.Visible = false;
            AttendancePanel.Visible = true;
            TimesheetPanel.Visible = true;
            AdminPanel.Visible = false;
            CustomerPanel.Visible = false;

            LoadTimesheetData();
            TimesheetSearchBox.TextChanged += TimesheetSearchBox_TextChanged;
            SetColumnHeaderText(TimesheetGridView);
            Current_ScreenName_label.Text = "Tracking";
        }

        private void Menu_Admin_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Admin_Label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Admin";
            if (AdminPanel.Visible == false)
            {
                ProductPanel.Visible = false;
                BranchPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                SupplierPanel.Visible = false;
                StaffPanel.Visible = false;
                ClientsPanel.Visible = false;
                LaybyPanel.Visible = false;
                PurchasePanel.Visible = false;
                ReportsPanel.Visible = false;
                AdminPanel.Visible = true;
                AttendancePanel.Visible = false;
                TimesheetPanel.Visible = false;
                CustomerPanel.Visible = false;
                LoadDataAsync(adminDataGrid, Unionquery, "Sync");
                AdminSearchTB.TextChanged += AdminSearchTB_TextChanged;

            }
        }

        private void Menu_Settings_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Settings_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Transactions_label.BackColor = Color.Transparent;
            Menu_Supplier_label.BackColor = Color.Transparent;
            Menu_Client_label.BackColor = Color.Transparent;
            Menu_Layby_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Branch_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Customer";

            ProductPanel.Visible = false;
            BranchPanel.Visible = false;
            ContentContainer_panel.Visible = false;
            POSPanel.Visible = false;
            SupplierPanel.Visible = false;
            StaffPanel.Visible = false;
            ClientsPanel.Visible = false;
            LaybyPanel.Visible = false;
            PurchasePanel.Visible = false;
            ReportsPanel.Visible = false;
            AttendancePanel.Visible = false;
            TimesheetPanel.Visible = false;
            AdminPanel.Visible = false;
            CustomerPanel.Visible = true;

            string query = "SELECT * FROM CUSTOMERS";
            LoadDataAsync(CustomersGridView, query, "Sync");
            SetColumnHeaderText(CustomersGridView);
            LoadDataAsync(PurchaseHistoryGridView, "SELECT * FROM purchase_history", "Sync");
            SetColumnHeaderText(PurchaseHistoryGridView);
            if (PurchaseHistoryButton.ForeColor == Color.FromArgb(37, 150, 190))
            {
                CustomerAddButton.Visible = false;
            }

        }
        //niggers
        #endregion

        #endregion


        #region All Common Database Functions

        #region DatabaseInitialization

        private void InitializeDatabaseConnection()
        {
            if (Session.BranchCode == "PK728")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            else if (Session.BranchCode == "BR001")
            {
                connectionString = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }

            else
            {
                MessageBox.Show("Database for the selected branch is not found. The application will now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        #endregion

        #region LoadDataGridViews Functions

        private void LoadDataAsync2(DataGridView myDataGrid, string query, string method)
        {
            command = new SqlCommand(query, connection);
            WorkingDataGridView = myDataGrid;
            WorkingDataGridView.DataSource = null;
            WorkingDataGridView.Columns.Clear();
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                if (method == "Async")
                {
                    command.BeginExecuteReader(OnReaderComplete, null);
                }
                else
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            DataGridViewTextBoxColumn SR = new DataGridViewTextBoxColumn
                            {
                                HeaderText = "SR#",
                                ValueType = typeof(string),
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                            };
                            WorkingDataGridView.Columns.Insert(0, SR);


                            WorkingDataGridView.DataSource = dataTable;
                            SetColumnHeaderText(WorkingDataGridView);
                            DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                            {
                                HeaderText = "Edit",
                                Image = ResizeImage((Image)EditImage, 15, 15),
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            };
                            WorkingDataGridView.Columns.Add(EditBtn);
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                WorkingDataGridView.Rows[i].Cells[0].Value = (i + 1).ToString();
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                    finally
                    {
                        connection.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadDataAsync(DataGridView myDataGrid, string query, string method)
        {
            command = new SqlCommand(query, connection);
            WorkingDataGridView = myDataGrid;
            WorkingDataGridView.DataSource = null;
            WorkingDataGridView.Columns.Clear();
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                if (method == "Async")
                {
                    command.BeginExecuteReader(OnReaderComplete, null);
                }
                else
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            DataGridViewTextBoxColumn SR = new DataGridViewTextBoxColumn
                            {
                                Name = "sr",
                                HeaderText = "SR#",
                                ValueType = typeof(string),
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            };
                            WorkingDataGridView.Columns.Insert(0, SR);


                            WorkingDataGridView.DataSource = dataTable;
                            SetColumnHeaderText(WorkingDataGridView);
                            DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                            {
                                HeaderText = "Edit",
                                Image = ResizeImage((Image)EditImage, 15, 15),
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            };
                            WorkingDataGridView.Columns.Add(EditBtn);

                            DataGridViewImageColumn DelBtn = new DataGridViewImageColumn
                            {
                                HeaderText = "Delete",
                                Image = ResizeImage((Image)DeleteImage, 15, 15),
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            };
                            WorkingDataGridView.Columns.Add(DelBtn);
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                WorkingDataGridView.Rows[i].Cells[0].Value = (i + 1).ToString();
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void OnReaderComplete(IAsyncResult result)
        {
            try
            {
                using (SqlDataReader reader = command.EndExecuteReader(result))
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    BeginInvoke(new Action(() =>
                    {
                        DataGridViewTextBoxColumn SR = new DataGridViewTextBoxColumn
                        {
                            Name = "sr",
                            HeaderText = "SR#",
                            ValueType = typeof(string),
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        };
                        WorkingDataGridView.Columns.Insert(0, SR);
                        WorkingDataGridView.DataSource = dataTable;
                        SetColumnHeaderText(WorkingDataGridView);
                        DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                        {
                            HeaderText = "Edit",
                            Image = ResizeImage((Image)EditImage, 15, 15),
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        };
                        WorkingDataGridView.Columns.Add(EditBtn);

                        DataGridViewImageColumn DelBtn = new DataGridViewImageColumn
                        {
                            HeaderText = "Delete",
                            Image = ResizeImage((Image)DeleteImage, 15, 15),
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        };
                        WorkingDataGridView.Columns.Add(DelBtn);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            WorkingDataGridView.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Set Column Headers for Data Grid Views


        private void SetColumnHeaderText(DataGridView dataGridView)
        {
            if (dataGridView == ProductsDataGrid)
            {
                if (dataGridView.Columns["item_name"] != null)
                {
                    dataGridView.Columns["or_image"].Visible = false;
                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    dataGridView.Columns["item_name"].HeaderText = "Name";
                    dataGridView.Columns["cost_price"].HeaderText = "Cost";
                    dataGridView.Columns["selling_price"].HeaderText = "Selling";
                    dataGridView.Columns["selling_price_tax"].HeaderText = "Selling(tax)";
                    dataGridView.Columns["category"].HeaderText = "Category";
                    dataGridView.Columns["quantity"].HeaderText = "Qty";
                    dataGridView.Columns["unit"].HeaderText = "Unit";
                    dataGridView.Columns["status"].HeaderText = "Status";
                    dataGridView.Columns["image"].HeaderText = "Image";
                }
                else
                {
                    dataGridView.Columns["id"].Visible = false;

                    dataGridView.Columns["types"].HeaderText = "Category Types";
                }
            }

            else if (dataGridView == StaffDataGrid)
            {

                if (dataGridView.Columns["staff_name"] != null)
                {

                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["staff_name"].HeaderText = "Staff Name";
                    dataGridView.Columns["type"].HeaderText = "Type";
                    dataGridView.Columns["phone_number"].HeaderText = "Phone";
                    dataGridView.Columns["address"].HeaderText = "Address";
                    dataGridView.Columns["status"].HeaderText = "Status";
                    dataGridView.Columns["shifts"].HeaderText = "Shifts";
                }
                else
                {
                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["types"].HeaderText = "Category Types";
                }
            }

            else if (dataGridView == SupplierDataGrid)
            {

                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["or_image"].Visible = false;
                dataGridView.Columns["image"].HeaderText = "Image";
                dataGridView.Columns["supplier_name"].HeaderText = "Supplier Name";
                dataGridView.Columns["email"].HeaderText = "Email";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["address"].HeaderText = "Address";


            }

            else if (dataGridView == AdminOrdersDataGrid)
            {
                dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                dataGridView.Columns["customer"].HeaderText = "Customer";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["date"].HeaderText = "Date";
                dataGridView.Columns["type"].HeaderText = "Type";
                dataGridView.Columns["status"].HeaderText = "Status";
                dataGridView.Columns["total_amount"].HeaderText = "Total";
                dataGridView.Columns["discount"].HeaderText = "Discount";
                dataGridView.Columns["net_total_amount"].HeaderText = "Net";
            }

            else if (dataGridView == StockTransferDataGrid)
            {
                dataGridView.Columns["transfer_id"].HeaderText = "ID";
                dataGridView.Columns["source"].HeaderText = "Source";
                dataGridView.Columns["destination"].HeaderText = "Destination";
                dataGridView.Columns["product"].HeaderText = "Product";
                dataGridView.Columns["current_stock"].HeaderText = "Current Stock";
                dataGridView.Columns["transferred"].HeaderText = "Transferred Stock";
                dataGridView.Columns["transfer_date"].HeaderText = "Date";
            }

            else if (dataGridView == BranchDataGrid)
            {
                dataGridView.Columns["branch_code"].HeaderText = "Branch Code";
                dataGridView.Columns["branch_name"].HeaderText = "Name";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["address"].HeaderText = "Address";
            }


            else if (dataGridView == ClientsDataGrid)
            {

                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["or_image"].Visible = false;
                dataGridView.Columns["image"].HeaderText = "Image";
                dataGridView.Columns["client_name"].HeaderText = "Client Name";
                dataGridView.Columns["email"].HeaderText = "Email";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["address"].HeaderText = "Address";


            }

            else if (dataGridView == PurchaseDataGrid)
            {

                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["sr"].Visible = false;
                dataGridView.Columns["quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                dataGridView.Columns["date"].HeaderText = "Date";
                dataGridView.Columns["supplier"].HeaderText = "Supplier Name";
                dataGridView.Columns["product"].HeaderText = "Product";
                dataGridView.Columns["quantity"].HeaderText = "Qty";
                dataGridView.Columns["unit"].HeaderText = "Unit";
                dataGridView.Columns["total_amount"].HeaderText = "Total Amount";
                dataGridView.Columns["paid_amount"].HeaderText = "Paid Amount";
                dataGridView.Columns["due_amount"].HeaderText = "Due Amount";
                dataGridView.Columns["purchase_status"].HeaderText = "Purchase Status";
                dataGridView.Columns["payment_status"].HeaderText = "Payment Status";

            }
            else if (dataGridView == CustomersGridView)
            {

                dataGridView.Columns["customer_id"].HeaderText = "ID";
                dataGridView.Columns["customer_id"].Width = 50;
                dataGridView.Columns["customer_name"].HeaderText = "Name";
                dataGridView.Columns["phone_number"].HeaderText = "Phone";
                dataGridView.Columns["email"].HeaderText = "Email";
                dataGridView.Columns["address"].HeaderText = "Address";
                dataGridView.Columns["email"].Width = 280;
                dataGridView.Columns["last_purchase_date"].HeaderText = "Last Purchase Date";
                dataGridView.Columns["credit"].HeaderText = "Credit";
                dataGridView.Columns["credit"].Width = 140;
                dataGridView.Columns["points"].HeaderText = "Points";

            }

            else if (dataGridView == PurchaseHistoryGridView)
            {

                dataGridView.Columns["purchase_id"].HeaderText = "Purchase ID";
                dataGridView.Columns["customer_id"].HeaderText = " Customer ID";
                dataGridView.Columns["customer_name"].HeaderText = "Name";
                dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                dataGridView.Columns["purchase_date"].HeaderText = "Date";
                dataGridView.Columns["purchase_time"].HeaderText = "Time";

            }


            else if (dataGridView == TimesheetGridView)
            {
                dataGridView.Columns["name"].HeaderText = "Name";
                dataGridView.Columns["email"].HeaderText = "Email";
                dataGridView.Columns["clock_in_time"].HeaderText = "Clock In Time";
                dataGridView.Columns["clock_out_time"].HeaderText = "Clock Out Time";
                dataGridView.Columns["worked_hours"].HeaderText = "Worked Hours(Day)";
                dataGridView.Columns["monthly_total_hours"].HeaderText = "Work Hours(Month)";

            }
            else if (dataGridView == ActivityGridView)
            {
                dataGridView.Columns["time"].HeaderText = "Time";
                dataGridView.Columns["action"].HeaderText = "Action";
                dataGridView.Columns["description"].HeaderText = "Description";
                dataGridView.Columns["username"].HeaderText = "Action By";

            }

            else if (dataGridView == adminDataGrid)
            {
                if (dataGridView.Columns["id"] != null)
                {
                    dataGridView.Columns["id"].Visible = false;
                }

                if (dataGridView.Columns["username"] != null)
                {
                    dataGridView.Columns["username"].HeaderText = "Username";
                }

                if (dataGridView.Columns["email"] != null)
                {
                    dataGridView.Columns["email"].HeaderText = "Email";
                }

                if (dataGridView.Columns["password"] != null)
                {
                    dataGridView.Columns["password"].HeaderText = "Password";
                    dataGridView.Columns["password"].Visible = true;
                }

                if (dataGridView.Columns["role"] != null)
                {
                    dataGridView.Columns["role"].HeaderText = "Role";
                }

                if (dataGridView.Columns["Access"] != null)
                {
                    dataGridView.Columns["Access"].HeaderText = "Access";
                }
            }

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Loading Edit and Delete Icons
        private void ImageEditDelLoad()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "POS.Resources.edit.png";
            string resourceName1 = "POS.Resources.delete.png";

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    EditImage = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName1))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    DeleteImage = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }
        }
        private void ImageDetailsPayLoad()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "POS.Resources.details.png";
            string resourceName1 = "POS.Resources.pay.png";

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    DetailsImage = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName1))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    PayImage = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }
        }

        #endregion

        #region Delete Function
        private void DeleteRowFromDatabase(int primaryKeyValue, string TableName, DataGridView dataGridView, int rowIndex)
        {
            string query = $"DELETE FROM {TableName} WHERE id = @PrimaryKeyValue";
            using (SqlCommand delcommand = new SqlCommand(query, connection))
            {
                delcommand.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue);

                try
                {
                    connection.Open();
                    int rowsAffected = delcommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //dataGridView.Rows.RemoveAt(rowIndex);
                        LoadDataAsync(dataGridView, $"select * from {TableName}", "Sync");
                        MessageBox.Show("Deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting row from database: " + ex.Message);
                }
                finally
                {

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion

        #endregion 


        #region All Dashboard Screen Funtions

        #region Dashboard Load Event And Dashboard Supplier Datagrid Function
        private async void Dashboard_Load(object sender, EventArgs e)
        {

            int CurrentUserInitialWidth = CurrentUser_label.Width;
            CurrentUser_label.Text = $"User Name:  {Session.Username}";
            BranchSession.Text = $"Branch Code: {Session.BranchCode}";
            CurrentUser_label.Location = new Point(CurrentUser_label.Location.X - (CurrentUser_label.Width - CurrentUserInitialWidth), CurrentUser_label.Location.Y);
            Set_CardBox_Positions();
            AllLabelLocations(55, 112, 60);
            SetLabelColor(Menu_Dashboard_label, "#0077C3");
            InitiateChart();
            await Task.Delay(10);
            ContentContainer_panel.Visible = true;


        }


        private void AllLabelLocations(int X, int start, int increment)
        {
            SetLabelLocations(Menu_Dashboard_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Products_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Staff_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Transactions_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Supplier_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Client_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Layby_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_POS_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Reports_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Branch_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Settings_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Admin_Label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Attendance_Label, new Point(X, start));
            start += increment;
            SetLabelLocations(LogOutLabel, new Point(X, start));
            start += increment;
        }



        private async void SupplierDashboardData()
        {
            try
            {
                connection.Open();
                command = new SqlCommand("select top 10 supplier,total_amount,paid_amount,due_amount from purchases", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    SupplierDashboardDataGrid.DataSource = dataTable;
                    SupplierDashboardDataGrid.Columns["supplier"].HeaderText = "Supplier";
                    SupplierDashboardDataGrid.Columns["total_amount"].HeaderText = "Payable";
                    SupplierDashboardDataGrid.Columns["paid_amount"].HeaderText = "Paid";
                    SupplierDashboardDataGrid.Columns["due_amount"].HeaderText = "Due";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Card Box Positions Functions
        private void Set_CardBox_Positions()
        {
            Profit_CardBox.Location = new Point(Sale_CardBox.Location.X + 240, Profit_CardBox.Location.Y);
            Discount_Cardbox.Location = new Point(Profit_CardBox.Location.X + 240, Discount_Cardbox.Location.Y);
            Expense_CardBox.Location = new Point(Discount_Cardbox.Location.X + 240, Expense_CardBox.Location.Y);
            Total_Tax_CardBox.Location = new Point(Profit_CardBox.Location.X, Profit_CardBox.Location.Y + 100);
            Total_Pay_CardBox.Location = new Point(Discount_Cardbox.Location.X, Discount_Cardbox.Location.Y + 100);
        }

        private async Task updateDashboardValues()
        {
            try
            {
                await connection.OpenAsync(); // Open connection asynchronously

                decimal totalSales = 0;
                decimal discount = 0;
                decimal totalTax = 0;
                decimal totalDueAmount = 0;
                decimal totalExpense = 0; // To hold the sum of paid_amount from purchases

                // Retrieve sales and discount information from non-cancelled orders
                string querySalesDiscount = "SELECT SUM(total_amount) AS total_sales, SUM((discount/100) * total_amount) AS discount FROM bill_list WHERE status != 'Cancelled'";
                using (SqlCommand command = new SqlCommand(querySalesDiscount, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        totalSales = reader["total_sales"] != DBNull.Value ? (decimal)reader["total_sales"] : 0;
                        discount = reader["discount"] != DBNull.Value ? (decimal)reader["discount"] : 0;
                    }
                }

                // Retrieve all items from non-cancelled orders for tax calculation
                string queryItems = "SELECT items FROM bill_list WHERE status != 'Cancelled'";
                List<string> itemsList = new List<string>();
                using (SqlCommand cmd = new SqlCommand(queryItems, connection))
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        itemsList.Add(rdr["items"].ToString());
                    }
                }

                // Process each item's JSON data for tax calculation
                foreach (string json in itemsList)
                {
                    var JSONLIST = JsonConvert.DeserializeObject<List<string>>(json);

                    foreach (var item in JSONLIST)
                    {
                        // Validate item format (split by '-' and check if quantity is decimal)
                        var parts = item.Split('-');
                        if (parts.Length != 2 || !decimal.TryParse(parts[1], out decimal quantity))
                        {
                            MessageBox.Show($"Invalid item format or quantity: {item}");
                            continue;
                        }
                        string itemName = parts[0];

                        // Fetch selling_price and selling_price_tax for the item
                        string priceQuery = "SELECT selling_price, selling_price_tax FROM items WHERE item_name = @productName";
                        using (SqlCommand priceCmd = new SqlCommand(priceQuery, connection))
                        {
                            priceCmd.Parameters.AddWithValue("@productName", itemName);

                            using (SqlDataReader priceReader = await priceCmd.ExecuteReaderAsync())
                            {
                                if (await priceReader.ReadAsync())
                                {
                                    decimal sellingPrice = priceReader["selling_price"] != DBNull.Value ? (decimal)priceReader["selling_price"] : 0;
                                    decimal sellingPriceTax = priceReader["selling_price_tax"] != DBNull.Value ? (decimal)priceReader["selling_price_tax"] : 0;

                                    // Calculate tax for this item (selling_price - selling_price_tax)
                                    decimal tax = sellingPrice - sellingPriceTax;

                                    // Add the tax for this item (multiplied by the quantity) to the totalTax
                                    totalTax += tax * quantity;
                                }
                            }
                        }
                    }
                }

                // Retrieve total expense (paid_amount) from purchases where status is NOT 'Cancelled'
                string queryExpense = "SELECT SUM(paid_amount) AS total_expense FROM purchases WHERE purchase_status != 'Cancelled'";
                using (SqlCommand expenseCmd = new SqlCommand(queryExpense, connection))
                using (SqlDataReader expenseReader = await expenseCmd.ExecuteReaderAsync())
                {
                    if (await expenseReader.ReadAsync())
                    {
                        totalExpense = expenseReader["total_expense"] != DBNull.Value ? (decimal)expenseReader["total_expense"] : 0;
                    }
                }

                // Retrieve sum of due amounts from purchases where status is NOT 'Cancelled'
                string queryDueAmount = "SELECT SUM(due_amount) AS total_due_amount FROM purchases WHERE purchase_status != 'Cancelled'";
                using (SqlCommand dueAmountCmd = new SqlCommand(queryDueAmount, connection))
                using (SqlDataReader dueAmountReader = await dueAmountCmd.ExecuteReaderAsync())
                {
                    if (await dueAmountReader.ReadAsync())
                    {
                        totalDueAmount = dueAmountReader["total_due_amount"] != DBNull.Value ? (decimal)dueAmountReader["total_due_amount"] : 0;
                    }
                }

                // Update the dashboard labels
                Sale_Amount_label.Text = totalSales.ToString("C"); // Total sales
                Discount_Amount_label.Text = discount.ToString("C"); // Discount
                Expense_Amount_label.Text = totalExpense.ToString("C"); // Total expense
                T_Pay_Amount_label.Text = totalDueAmount.ToString("C"); // Total due
                T_Tax_Amout_label.Text = totalTax.ToString("C"); // Total tax

                // Calculate and display profit (Total Sales - Discount - Total Expense)
                decimal profit = totalSales - discount - totalExpense;
                Profit_Amount_label.Text = profit.ToString("C"); // Profit
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        #endregion

        #region Chart Create Function

        private void InitiateChart()
        {

            //string[] monthLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //double[] xValues = { 0, 2, 4, 6, 8, 9 };
            //double[] originalYValues = { 2, 4, 2, 6, 2, 10 };

            //var series = new Series();
            //series.ChartType = SeriesChartType.Spline;
            //series.Color = Color.FromArgb(161, 74, 222);

            //for (int i = 0; i < xValues.Length; i++)
            //{
            //    series.Points.AddXY(xValues[i], originalYValues[i]);
            //    series.Points[i].MarkerSize = 5;
            //    series.Points[i].MarkerColor = Color.White;
            //    series.Points[i].Tag = i;
            //}
            //chart1.Series.Add(series);


            ////chart1.ChartAreas[0].AxisY.Title = "Y-axis Label";
            //chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisY.Minimum = 0;

            //chart1.ChartAreas[0].AxisX.Maximum = xValues[xValues.Length - 1];
            //chart1.ChartAreas[0].AxisY.Maximum = originalYValues.Max() + 1;
            //chart1.ChartAreas[0].AxisY.Interval = 1;
            //chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            ////chart1.ChartAreas[0].AxisX.Title = "Month";
            //chart1.ChartAreas[0].AxisX.CustomLabels.Clear();


            //var areaSeries = new Series();
            //areaSeries.ChartType = SeriesChartType.SplineArea;
            //areaSeries.Points.DataBindXY(xValues, originalYValues);
            //areaSeries.BackGradientStyle = GradientStyle.TopBottom;
            //areaSeries.Color = Color.FromArgb(73, 162, 215);
            //chart1.Series.Insert(0, areaSeries);


            //for (int i = 0; i < monthLabels.Length; i++)
            //{
            //    chart1.ChartAreas[0].AxisX.CustomLabels.Add((double)(i + 1), (double)i, monthLabels[((int)i)]);
            //}


            //for (int i = 0; i < chart1.ChartAreas[0].AxisX.CustomLabels.Count - 1; i++)
            //{
            //    if (i == 0)
            //    {
            //        double intervalStart = i == 0 ? chart1.ChartAreas[0].AxisX.Minimum : chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;
            //        double intervalEnd = chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;

            //        StripLine stripLine = new StripLine();
            //        stripLine.IntervalOffset = intervalStart;
            //        stripLine.StripWidth = intervalEnd - intervalStart;
            //        stripLine.BackColor = Color.FromArgb(249, 249, 249);
            //        chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine);
            //    }

            //    else
            //    {
            //        StripLine stripLine = new StripLine();
            //        stripLine.IntervalOffset = chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;
            //        stripLine.StripWidth = chart1.ChartAreas[0].AxisX.CustomLabels[i + 1].FromPosition - chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;
            //        stripLine.BackColor = (i % 2 != 0) ? Color.FromArgb(249, 249, 249) : Color.White; // Set color based on index
            //        chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine);
            //    }
            //}


        }



        #endregion


        #endregion


        #region All Items Screen Functions

        #region Products Tab Button Functions

        private void ProductsTabButton_Click(object sender, EventArgs e)
        {
            if (ProductsTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {

                ProductsTabButton.BackColor = Color.FromArgb(37, 150, 190);
                ProductsTabButton.ForeColor = Color.White;
                ProductsCategoryTabButton.BackColor = Color.Transparent;
                ProductsCategoryTabButton.ForeColor = SystemColors.GrayText;
                string query = "select * from items";
                LoadDataAsync(ProductsDataGrid, query, "Async");
            }
        }

        private void ProductsCategoryTabButton_Click(object sender, EventArgs e)
        {
            if (ProductsCategoryTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {

                ProductsCategoryTabButton.BackColor = Color.FromArgb(37, 150, 190);
                ProductsCategoryTabButton.ForeColor = Color.White;
                ProductsTabButton.BackColor = Color.Transparent;
                ProductsTabButton.ForeColor = SystemColors.GrayText;
                string query = "select * from item_category";
                LoadDataAsync(ProductsDataGrid, query, "Async");
            }
        }


        #endregion

        #region Products Data Grid All Event Listeners
        private void ProductsDataGrid_VisibleChanged(object sender, EventArgs e)
        {

            if (ProductsDataGrid.Visible == true)
            {
                if (ProductsTabButton.BackColor == Color.FromArgb(37, 150, 190))
                {
                    string query = "select * from items";
                    LoadDataAsync(ProductsDataGrid, query, "Async");
                }

                else
                {
                    string query = "select * from item_category";
                    LoadDataAsync(ProductsDataGrid, query, "Async");
                }
            }
        }
        private void ProductsDataGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < ProductsDataGrid.Rows.Count)
            {
                ProductsDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(247, 247, 247);
            }
        }

        private void ProductsDataGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < ProductsDataGrid.Rows.Count)
            {
                ProductsDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = ProductsDataGrid.DefaultCellStyle.BackColor;
            }
        }

        private void ProductsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (ProductsDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    if (ProductsDataGrid.Columns["item_name"] != null)
                    {
                        ItemsForm productsForm = new ItemsForm((int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        productsForm.ShowDialog();
                        LoadDataAsync(ProductsDataGrid, "select * from items", "Sync");
                    }
                    else
                    {
                        ItemsCategoryForm productsCatForm = new ItemsCategoryForm((int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        productsCatForm.ShowDialog();
                        LoadDataAsync(ProductsDataGrid, "select * from item_category", "Sync");
                    }
                }
                else if (ProductsDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    if (ProductsDataGrid.Columns["item_name"] != null)
                    {
                        if (MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            // Fetch item details before deletion
                            int itemId = Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                            string itemName = ProductsDataGrid.Rows[e.RowIndex].Cells["item_name"].Value.ToString();
                            string itemQuantity = ProductsDataGrid.Rows[e.RowIndex].Cells["quantity"].Value.ToString();

                            // Delete the row
                            DeleteRowFromDatabase(itemId, "items", ProductsDataGrid, e.RowIndex);

                            // Log the deletion
                            LogActivity("Delete", $"Deleted item '{itemName}' with quantity {itemQuantity}");
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure you want to delete this item category?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            // Fetch category details before deletion
                            int categoryId = Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                            string categoryName = ProductsDataGrid.Rows[e.RowIndex].Cells["category_name"].Value.ToString();

                            // Delete the row
                            DeleteRowFromDatabase(categoryId, "item_category", ProductsDataGrid, e.RowIndex);

                            // Log the deletion
                            LogActivity("Delete", $"Deleted item category '{categoryName}'");
                        }
                    }
                }
            }
        }

        #endregion

        #region Products Add Button and Search TextBox Events Functions

        //Add Product Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (ProductsTabButton.BackColor == Color.FromArgb(37, 150, 190))
            {
                ItemsForm productsForm = new ItemsForm();
                productsForm.ShowDialog();
                LoadDataAsync(ProductsDataGrid, "select * from items", "Sync");
            }

            else
            {
                ItemsCategoryForm productCategory = new ItemsCategoryForm();
                productCategory.ShowDialog();
                string query = "select * from item_category";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
        }


        // Products Search TextBox
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (ProductsDataGrid.Columns["item_name"] != null)
            {
                string query = $"select * from items where item_name like '%{textBox1.Text}%' OR status like '%{textBox1.Text}%' ";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
            else
            {
                string query = $"select * from item_category where types like '%{textBox1.Text}%'";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
        }

        #endregion

        #endregion


        #region All Staff Screen Functions

        #region Staff Tabs Click Functions and StaffPanel VisibleChange Functions
        private void StaffCategoryTab_Click(object sender, EventArgs e)
        {
            if (StaffCategoryTab.BackColor != Color.FromArgb(37, 150, 190))
            {

                StaffCategoryTab.BackColor = Color.FromArgb(37, 150, 190);
                StaffCategoryTab.ForeColor = Color.White;
                StaffTab.BackColor = Color.Transparent;
                StaffTab.ForeColor = SystemColors.GrayText;
                string query = "select * from staff_category";
                LoadDataAsync(StaffDataGrid, query, "Async");
            }
        }

        private void StaffTab_Click(object sender, EventArgs e)
        {
            if (StaffTab.BackColor != Color.FromArgb(37, 150, 190))
            {

                StaffTab.BackColor = Color.FromArgb(37, 150, 190);
                StaffTab.ForeColor = Color.White;
                StaffCategoryTab.BackColor = Color.Transparent;
                StaffCategoryTab.ForeColor = SystemColors.GrayText;
                string query = "select * from staff_details";
                LoadDataAsync(StaffDataGrid, query, "Async");
            }
        }



        private void StaffPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (StaffPanel.Visible == true)
            {
                if (StaffTab.BackColor == Color.FromArgb(37, 150, 190))
                {
                    string query = "select * from staff_details";
                    LoadDataAsync(StaffDataGrid, query, "Async");
                }

                else
                {
                    string query = "select * from staff_category";
                    LoadDataAsync(StaffDataGrid, query, "Async");
                }
            }
        }
        #endregion

        #region Staff Add Button And Search TextBox Functions
        private void AddStaffButton_Click(object sender, EventArgs e)
        {
            if (StaffTab.BackColor == Color.FromArgb(37, 150, 190))
            {
                GSStaffForm staffForm = new GSStaffForm();
                staffForm.ShowDialog();
                LoadDataAsync(StaffDataGrid, "select * from staff_details", "Sync");
            }

            else
            {
                GSStaffCategoryForm staffCategory = new GSStaffCategoryForm();
                staffCategory.ShowDialog();
                string query = "select * from staff_category";
                LoadDataAsync(StaffDataGrid, query, "Sync");
            }
        }

        private void SearchStaff_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (StaffDataGrid.Columns["staff_name"] != null)
            {
                string query = $"select * from staff_details where staff_name like '%{SearchStaff_TextBox.Text}%' OR status like '%{SearchStaff_TextBox.Text}%' ";
                LoadDataAsync(StaffDataGrid, query, "Sync");
            }
            else
            {
                string query = $"select * from staff_category where types like '%{SearchStaff_TextBox.Text}%' ";
                LoadDataAsync(StaffDataGrid, query, "Sync");
            }
        }

        #endregion

        #region Staff Data Grid Event Functions
        private void StaffDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (StaffDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    if (StaffDataGrid.Columns["staff_name"] != null)
                    {
                        GSStaffForm staffForm = new GSStaffForm((int)StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        staffForm.ShowDialog();
                        LoadDataAsync(StaffDataGrid, "select * from staff_details", "Sync");
                    }
                    else
                    {
                        GSStaffCategoryForm staffcategory = new GSStaffCategoryForm((int)StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        staffcategory.ShowDialog();
                        LoadDataAsync(StaffDataGrid, "select * from staff_category", "Sync");
                    }
                }

                else if (StaffDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {

                    if (StaffDataGrid.Columns["staff_name"] != null)
                    {
                        if (MessageBox.Show("Are you sure you want to delete this staff detail?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            DeleteRowFromDatabase(Convert.ToInt32(StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value), "staff_details", StaffDataGrid, e.RowIndex);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure you want to delete this staff category?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            DeleteRowFromDatabase(Convert.ToInt32(StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value), "staff_category", StaffDataGrid, e.RowIndex);
                        }
                    }


                }

            }
        }

        private void StaffDataGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < StaffDataGrid.Rows.Count)
            {
                StaffDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(247, 247, 247);
            }
        }

        private void StaffDataGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < StaffDataGrid.Rows.Count)
            {
                StaffDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = ProductsDataGrid.DefaultCellStyle.BackColor;
            }
        }

        #endregion

        #endregion


        #region All Purchase Screen Functions


        #region PurchasePanel Visibility and DataGrid Event Functions


        private void PurchasePanel_VisibleChanged(object sender, EventArgs e)
        {
            if (PurchasePanel.Visible == true)
            {
                LoadDataAsync(PurchaseDataGrid, "select * from purchases", "Async");
            }
        }

        private void PurchaseDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (PurchaseDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    PurchaseForm purchaseForm = new PurchaseForm((int)PurchaseDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                    purchaseForm.ShowDialog();
                    LoadDataAsync(PurchaseDataGrid, "select * from purchases", "Sync");
                }
                else if (PurchaseDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    if (MessageBox.Show("Are you sure you want to delete this purchase?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        // Fetch purchase details before deletion
                        int purchaseId = Convert.ToInt32(PurchaseDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        string supplierName = PurchaseDataGrid.Rows[e.RowIndex].Cells["supplier"].Value.ToString();
                        string productName = PurchaseDataGrid.Rows[e.RowIndex].Cells["product"].Value.ToString();
                        string productQuantity = PurchaseDataGrid.Rows[e.RowIndex].Cells["quantity"].Value.ToString();

                        // Delete the row
                        DeleteRowFromDatabase(purchaseId, "purchases", PurchaseDataGrid, e.RowIndex);

                        // Log the deletion
                        LogActivity("Delete", $"Deleted purchase by supplier '{supplierName}' for product '{productName}' with quantity {productQuantity}");
                    }
                }
            }
        }


        #endregion

        #region Purchase Add Button and Search TextBox Event Functions

        private void PurchaseAddButton_Click(object sender, EventArgs e)
        {
            PurchaseForm purchaseForm = new PurchaseForm();
            purchaseForm.ShowDialog();
            LoadDataAsync(PurchaseDataGrid, "select * from purchases", "Sync");
        }

        private void Purchase_SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string query = $"select * from purchases where supplier like '%{Purchase_SearchTextBox.Text}%'";
            LoadDataAsync(PurchaseDataGrid, query, "Sync");
        }

        #endregion

        #endregion


        #region All Supplier Screen Functions

        #region Supplier Panel VisibleChanged and Supplier Data Grid Functions

        private void SupplierPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (SupplierPanel.Visible == true)
            {
                LoadDataAsync(SupplierDataGrid, "select * from suppliers", "Async");
            }
        }

        private void SupplierDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (SupplierDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    SupplierForm supplierForm = new SupplierForm((int)SupplierDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                    supplierForm.ShowDialog();
                    LoadDataAsync(SupplierDataGrid, "select * from suppliers", "Sync");
                }

                else if (SupplierDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {

                    if (MessageBox.Show("Are you sure you want to delete this supplier?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DeleteRowFromDatabase(Convert.ToInt32(SupplierDataGrid.Rows[e.RowIndex].Cells["id"].Value), "suppliers", SupplierDataGrid, e.RowIndex);
                    }

                }

            }
        }


        #endregion

        #region Supplier Add Button and Search TextBox Functions

        private void SupplierAddButton_Click(object sender, EventArgs e)
        {
            SupplierForm supplierForm = new SupplierForm();
            supplierForm.ShowDialog();
            LoadDataAsync(SupplierDataGrid, "select * from suppliers", "Sync");
        }

        private void SupplierSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string query = $"select * from suppliers where supplier_name like '%{SupplierSearchTextBox.Text}%'";
            LoadDataAsync(SupplierDataGrid, query, "Sync");
        }


        #endregion

        #endregion


        #region All Client Screen Functions

        #region Client Panel VisibleChanged and Client Data Grid Functions


        private void ClientsPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (ClientsPanel.Visible == true)
            {
                LoadDataAsync(ClientsDataGrid, "select * from clients", "Async");
            }
        }

        private void ClientsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (ClientsDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    ClientForm clientForm = new ClientForm((int)ClientsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                    clientForm.ShowDialog();
                    LoadDataAsync(ClientsDataGrid, "select * from clients", "Sync");
                }

                else if (SupplierDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {

                    if (MessageBox.Show("Are you sure you want to delete this client?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DeleteRowFromDatabase(Convert.ToInt32(ClientsDataGrid.Rows[e.RowIndex].Cells["id"].Value), "clients", ClientsDataGrid, e.RowIndex);
                    }

                }

            }
        }



        #endregion

        #region Client Add Button and Search TextBox Functions

        private void AddClientsButton_Click(object sender, EventArgs e)
        {
            ClientForm clientForm = new ClientForm();
            clientForm.ShowDialog();
            LoadDataAsync(ClientsDataGrid, "select * from clients", "Sync");
        }

        private void Clients_SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string query = $"select * from clients where client_name like '%{Clients_SearchTextBox.Text}%'";
            LoadDataAsync(ClientsDataGrid, query, "Sync");
        }



        #endregion

        #endregion


        #region All POS Screen Functions


        #region POS Add Category and Event Listener Functions 

        private void AddPOSCategory()
        {
            DataTable table = new DataTable();
            string qry = "select * from item_category";
            try
            {
                connection.Open();
                SqlDataAdapter sqladapter = new SqlDataAdapter(qry, connection);
                sqladapter.Fill(table);
                CategoriesFlowLayoutPanel.Controls.Clear();
                if (table.Rows.Count > 0)
                {
                    Button AllCategoriesButton = new Button();
                    AllCategoriesButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    AllCategoriesButton.BackColor = Color.FromArgb(37, 150, 190);
                    AllCategoriesButton.FlatAppearance.BorderColor = Color.FromArgb(37, 150, 190);
                    AllCategoriesButton.FlatAppearance.BorderSize = 2;
                    AllCategoriesButton.FlatStyle = FlatStyle.Flat;
                    AllCategoriesButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
                    AllCategoriesButton.ForeColor = Color.White;
                    AllCategoriesButton.Margin = new Padding(3, 3, 0, 3);
                    AllCategoriesButton.Size = new Size(150, 35);
                    AllCategoriesButton.TabIndex = 1;
                    AllCategoriesButton.Text = "All Categories";
                    AllCategoriesButton.Click += new EventHandler(Category_Click);
                    AllCategoriesButton.UseVisualStyleBackColor = false;
                    CategoriesFlowLayoutPanel.Controls.Add(AllCategoriesButton);
                    foreach (DataRow row in table.Rows)
                    {
                        Button button2 = new Button();
                        button2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        button2.BackColor = Color.Transparent;
                        button2.FlatAppearance.BorderColor = Color.FromArgb(37, 150, 190);
                        button2.FlatAppearance.BorderSize = 2;
                        button2.FlatStyle = FlatStyle.Flat;
                        button2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
                        button2.Location = new Point(156, 3);
                        button2.Margin = new Padding(3, 3, 0, 3);
                        button2.Size = new Size(150, 35);
                        button2.TabIndex = 3;
                        button2.Text = row["types"].ToString();
                        button2.Click += new EventHandler(Category_Click);
                        button2.UseVisualStyleBackColor = false;
                        CategoriesFlowLayoutPanel.Controls.Add(button2);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Category_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.BackColor = Color.FromArgb(37, 150, 190);
            b.ForeColor = Color.White;
            foreach (Button item in CategoriesFlowLayoutPanel.Controls)
            {
                if (item.Text != b.Text)
                {
                    item.BackColor = Color.Transparent;
                    item.ForeColor = SystemColors.ControlText;
                }
            }
            if (b.Text == "All Categories")
            {
                foreach (var item in ProductsFlowLayoutPanel.Controls)
                {
                    var pro = (ProductCard)item;
                    pro.Visible = true;
                }
            }
            else
            {
                foreach (var item in ProductsFlowLayoutPanel.Controls)
                {
                    var pro = (ProductCard)item;
                    pro.Visible = pro.product_category.ToLower().Contains(b.Text.Trim().ToLower());
                }
            }
        }

        #endregion


        #region Load POS Products and Add Products Functions

        private void LoadPOSProducts()
        {
            DataTable table = new DataTable();
            string qry = "select * from items";
            try
            {
                connection.Open();
                SqlDataAdapter sqladapter = new SqlDataAdapter(qry, connection);
                sqladapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    POSProductsDataGrid.Rows.Clear();
                    ProductsFlowLayoutPanel.Controls.Clear();
                    foreach (DataRow row in table.Rows)
                    {
                        Byte[] imageArray = (byte[])row["or_image"];
                        byte[] imageByteArray = imageArray;
                        AddPOSProducts(Convert.ToInt32(row["id"]), Convert.ToDecimal(row["selling_price_tax"]), row["unit"].ToString(), row["category"].ToString(), row["item_name"].ToString(), ByteArraytoImage(imageByteArray));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void AddPOSProducts(int pid, decimal product_price, string unit, string category, string name, Image image)
        {
            var w = new ProductCard()
            {
                id = Convert.ToInt32(pid),
                product_name = name,
                product_category = category,
                product_price = product_price,
                unit = unit,
                product_image = image
            };
            ProductsFlowLayoutPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                if (BillID == -1)
                {
                    var wdg = (ProductCard)ss;
                    foreach (DataGridViewRow item in POSProductsDataGrid.Rows)
                    {
                        if (Convert.ToInt32(item.Cells["hidden_id"].Value) == wdg.id)
                        {
                            item.Cells["quantity"].Value = decimal.Parse(item.Cells["quantity"].Value.ToString()) + 1;
                            item.Cells["total_amount"].Value = decimal.Parse(item.Cells["quantity"].Value.ToString()) *
                                                               decimal.Parse(item.Cells["product_price"].Value.ToString());
                            setTotalAmount();
                            return;
                        }

                    }

                    POSProductsDataGrid.Rows.Add(new object[] { 0, wdg.id, wdg.product_name, 1, wdg.product_price, wdg.unit, wdg.product_price });
                    setTotalAmount();
                }
                else
                {
                    MessageBox.Show("Complete the Selected Bill Payement First", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }


            };
        }

        private void setTotalAmount()
        {
            decimal total = 0;
            if (POSProductsDataGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in POSProductsDataGrid.Rows)
                {
                    total = total + (Convert.ToDecimal(item.Cells["product_price"].Value) * Convert.ToDecimal(item.Cells["quantity"].Value));
                }
                TotalAmountLabel.Text = "Total Amount : " + total;
                TotalAmountLabel.Visible = true;
            }
            else
            {
                TotalAmountLabel.Visible = false;
            }

        }


        #endregion


        #region POSPanel Visibilty,POSProductDataGrid CellFormat & SearchBox Event Functions

        private void POSPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (POSPanel.Visible == true)
            {
                Point location = ProductDataGrid_panel.PointToScreen(new Point(0, POSProductsDataGrid.Height));
                nk = new NumberKeypad(location.X + 40, location.Y);
                nk.NumberButtonPressed += Nk_NumberButtonPressed;
                NewScreen();
            }
        }

        private void POSProductsDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in POSProductsDataGrid.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        //POS Screen Search Box
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Button selectedCategoryButton = CategoriesFlowLayoutPanel.Controls.Cast<Button>().FirstOrDefault(b => b.BackColor == Color.FromArgb(37, 150, 190));

            if (selectedCategoryButton.Text != "All Categories")
            {
                foreach (var item in ProductsFlowLayoutPanel.Controls)
                {

                    var pro = (ProductCard)item;
                    bool product = pro.product_name.ToLower().Contains(textBox2.Text.Trim().ToLower());
                    bool category = pro.product_category.ToLower().Contains(selectedCategoryButton.Text.Trim().ToLower());
                    if (product && category)
                    {
                        pro.Visible = true;
                    }
                    else
                    {
                        pro.Visible = false;
                    }

                }
            }
            else
            {
                foreach (var item in ProductsFlowLayoutPanel.Controls)
                {

                    var pro = (ProductCard)item;
                    pro.Visible = pro.product_name.ToLower().Contains(textBox2.Text.Trim().ToLower());
                }
            }

        }

        #endregion


        #region NewScreen Function & Add Data to POSProduct DataGrid Function

        private void NewScreen()
        {
            if (POSProductsDataGrid.Rows.Count > 0)
            {
                POSProductsDataGrid.Rows.Clear();
                BillID = -1;
                JSONItems = "";
                TotalItemsAmount = 0;
                FastCashButton.Visible = false;
                CheckOutButton.Visible = false;
                TotalAmountLabel.Text = "Total Amount : ";
                TotalAmountLabel.Visible = false;
            }
        }

        private decimal TotalItemsAmount = 0;
        private void AddDataToPOSDataGrid(string json, string status)
        {
            List<string> jsonData = JsonConvert.DeserializeObject<List<string>>(json);
            try
            {
                connection.Open();
                POSProductsDataGrid.Rows.Clear();
                decimal total = 0;
                foreach (string item in jsonData)
                {
                    string[] parts = item.Split("-");
                    SqlCommand cmd = new SqlCommand($"select * from items where item_name='{parts[0]}'", connection);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                POSProductsDataGrid.Rows.Add(new object[] { 0, (int)reader["id"], reader["item_name"].ToString(), Convert.ToDecimal(parts[1]), Convert.ToDecimal(reader["selling_price_tax"]), reader["unit"].ToString(), Convert.ToDecimal(parts[1]) * Convert.ToDecimal(reader["selling_price_tax"]) });
                                total += Convert.ToDecimal(parts[1]) * Convert.ToDecimal(reader["selling_price_tax"]);

                            }
                        }
                    }
                }
                if (status == "Paid")
                {
                    FastCashButton.Visible = false;
                    CheckOutButton.Visible = false;
                    TotalAmountLabel.Text = "Paid!";
                    TotalAmountLabel.Visible = true;
                }
                else
                {
                    FastCashButton.Visible = true;
                    CheckOutButton.Visible = true;
                    TotalAmountLabel.Text = "Total Amount : " + total;
                    TotalItemsAmount = total;
                    TotalAmountLabel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        #endregion


        #region All POS Menu Buttons Event Functions

        private void NewButton_Click(object sender, EventArgs e)
        {
            NewScreen();
        }




        private int BillID = -1;
        private string JSONItems = "";

        private void BillListButton_Click(object sender, EventArgs e)
        {

            using (GSBillList billList = new GSBillList())
            {
                if (billList.ShowDialog() != DialogResult.OK)
                {
                    if (billList.Status != "" && billList.BillID != -1 && billList.JSONData != "")
                    {
                        BillID = billList.BillID;
                        JSONItems = billList.JSONData;
                        AddDataToPOSDataGrid(JSONItems, billList.BillStatus);

                    }
                }
            }
        }

        private void TakeAwayButton_Click(object sender, EventArgs e)
        {
            if (BillID != -1)
            {
                MessageBox.Show("Complete the Selected Bill Payment First", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (POSProductsDataGrid.Rows.Count > 0)
            {
                List<string> columnValues = new List<string>();
                decimal total_amount = 0;

                foreach (DataGridViewRow row in POSProductsDataGrid.Rows)
                {
                    string ItemsValue = row.Cells["product_name"].Value.ToString();
                    string QtyValue = row.Cells["quantity"].Value.ToString();
                    columnValues.Add(ItemsValue + "-" + QtyValue);
                    total_amount += Convert.ToDecimal(row.Cells["total_amount"].Value.ToString());
                }

                string json = JsonConvert.SerializeObject(columnValues);
                AddCustomerInfoGS addCustomerInfoForm = new AddCustomerInfoGS(json, total_amount);
                DialogResult result = addCustomerInfoForm.ShowDialog();

                if (result == DialogResult.Cancel)
                {
                    NewScreen();
                    return;
                }
                POSProductsDataGrid.Rows.Clear();
                NewScreen();
                TotalAmountLabel.Text = "";
            }
            else
            {
                MessageBox.Show("Please select something first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }



        private void LaybyButton_Click(object sender, EventArgs e)
        {
            if (BillID != -1)
            {
                MessageBox.Show("Complete the Selected Bill Payement First", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (POSProductsDataGrid.Rows.Count > 0)
            {
                LaybyForm layby = new LaybyForm();
                layby.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select something first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }



        private void DeliveryButton_Click(object sender, EventArgs e)
        {
            if (BillID != -1)
            {
                MessageBox.Show("Complete the Selected Bill Payement First", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (POSProductsDataGrid.Rows.Count > 0)
            {
                List<string> columnValues = new List<string>();
                decimal total_amount = 0;

                foreach (DataGridViewRow row in POSProductsDataGrid.Rows)
                {
                    string ItemsValue = row.Cells["product_name"].Value.ToString();
                    string QtyValue = row.Cells["quantity"].Value.ToString();
                    columnValues.Add(ItemsValue + "-" + QtyValue);
                    total_amount += Convert.ToDecimal(row.Cells["total_amount"].Value.ToString());
                }
                string json = JsonConvert.SerializeObject(columnValues);
                string Added = "";
                using (GSDeliveryForm DF = new GSDeliveryForm(json, total_amount))
                {
                    if (DF.ShowDialog() != DialogResult.OK)
                    {
                        Added = DF.InsertStatus;
                    }
                }

                if (Added != "")
                {
                    POSProductsDataGrid.Rows.Clear();
                }

            }
            else
            {
                MessageBox.Show("Please select something first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        #endregion


        #region POS DataGrid All Event Listener for Edit Functionality


        private NumberKeypad nk;
        private TextBox SelectedCell;
        private void POSProductsDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BillID == -1)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && POSProductsDataGrid.Columns[e.ColumnIndex].HeaderText == "Qty")
                {
                    POSProductsDataGrid.ReadOnly = false;
                    POSProductsDataGrid.BeginEdit(true);

                    nk.Show();
                    SelectedCell.Focus();

                }
            }
        }

        // Nk_NumberButtonPressed added to nk form which is initialized in the POSPanel Visible Function

        private void Nk_NumberButtonPressed(object sender, int number)
        {
            if (number == -1)
            {
                if (SelectedCell.Text.Length > 0)
                {
                    SelectedCell.Text = SelectedCell.Text.Substring(0, SelectedCell.Text.Length - 1);
                    SelectedCell.SelectionStart = SelectedCell.Text.Length;
                }
            }
            else if (number == -2)
            {
                SelectedCell.Text = "";
                SelectedCell.SelectionStart = SelectedCell.Text.Length;
            }
            else if (number == -3)
            {
                SelectedCell.Text += ".";
                SelectedCell.SelectionStart = SelectedCell.Text.Length;
            }
            else if (number == -5)
            {
                POSProductsDataGrid.ReadOnly = true;
                POSProductsDataGrid.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                POSProductsDataGrid.AllowUserToDeleteRows = false;
            }
            else
            {
                SelectedCell.Text += number.ToString();
                SelectedCell.SelectionStart = SelectedCell.Text.Length;
            }

        }

        private void POSProductsDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (BillID == -1)
            {
                TextBox tb = e.Control as TextBox;
                tb.Text = "";
                SelectedCell = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                    tb.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_PreviewKeyPress);
                }
            }
        }

        private void tb_PreviewKeyPress(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (nk.Visible == true)
                {
                    nk.Hide();
                }
                POSProductsDataGrid.ReadOnly = true;
                POSProductsDataGrid.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                POSProductsDataGrid.AllowUserToDeleteRows = false;
            }

        }

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void POSProductsDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BillID == -1)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    POSProductsDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    POSProductsDataGrid.Rows[e.RowIndex].Selected = true;
                    POSProductsDataGrid.RowsDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                    POSProductsDataGrid.AllowUserToDeleteRows = true;
                }
            }
        }

        private void POSProductsDataGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            POSProductsDataGrid.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            POSProductsDataGrid.AllowUserToDeleteRows = false;
            setTotalAmount();
        }

        private void POSProductsDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            POSProductsDataGrid.ReadOnly = true;
            POSProductsDataGrid.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            POSProductsDataGrid.AllowUserToDeleteRows = false;
            if (POSProductsDataGrid.Columns[e.ColumnIndex].HeaderText == "Qty")
            {
                nk.Hide();
                if (POSProductsDataGrid.Rows[e.RowIndex].Cells["quantity"].Value == null)
                {
                    POSProductsDataGrid.Rows[e.RowIndex].Cells["quantity"].Value = 1;
                    POSProductsDataGrid.Rows[e.RowIndex].Cells["total_amount"].Value = Convert.ToDecimal(POSProductsDataGrid.Rows[e.RowIndex].Cells["product_price"].Value) * Convert.ToDecimal(POSProductsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    setTotalAmount();
                    return;
                }
                decimal qty = Convert.ToDecimal(POSProductsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (qty > 0)
                {
                    POSProductsDataGrid.Rows[e.RowIndex].Cells["total_amount"].Value = Convert.ToDecimal(POSProductsDataGrid.Rows[e.RowIndex].Cells["product_price"].Value) * qty;
                    setTotalAmount();
                }
                else
                {
                    POSProductsDataGrid.Rows.RemoveAt(e.RowIndex);
                    setTotalAmount();
                }

            }
        }

        #endregion


        #region Fast Cash and Checkout Buttons Event Functions

        private void FastCashButton_Click(object sender, EventArgs e)
        {
            if (BillID != -2)
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE bill_list SET status=@Status WHERE bill_id=@Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", "Paid");
                        command.Parameters.AddWithValue("@Id", BillID);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NewScreen();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Message : " + ex.Message);

                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("insert into bill_list(items,date,type,status,total_amount,net_total_amount) values(@Items,@Date,@Type,@Status,@Total,@NetTotal)", connection);

                    command.Parameters.AddWithValue("@Items", JSONItems);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.Parameters.AddWithValue("@Type", "Take Away");
                    command.Parameters.AddWithValue("@Status", "Paid");
                    command.Parameters.AddWithValue("@Total", TotalItemsAmount);
                    command.Parameters.AddWithValue("@NetTotal", TotalItemsAmount);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NewScreen();

                    }
                    else
                    {
                        MessageBox.Show("There was a problem saving");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        private void CheckOutButton_Click(object sender, EventArgs e)
        {

            string updateStatus = "";
            using (PaymentMethodScreenGS pms = new PaymentMethodScreenGS(TotalItemsAmount, BillID))
            {
                if (pms.ShowDialog() != DialogResult.OK)
                {
                    updateStatus = pms.StatusUpdated;
                    NewScreen();

                }
            }
            if (updateStatus == "Updated")
            {
                NewScreen();
            }
        }

        #endregion


        #endregion


        #region All Reports Screen Functions

        private void SalesReportsByCatButton_Click(object sender, EventArgs e)
        {
            SalesReportsByCatButton.BackColor = Color.FromArgb(0, 119, 195);
            StaffListButton.BackColor = Color.White;
            SalesReportItemWiseButton.BackColor = Color.White;
            MenuListButton.BackColor = Color.White;

            SalesReportsByCatButton.ForeColor = Color.White;
            StaffListButton.ForeColor = SystemColors.GrayText;
            SalesReportItemWiseButton.ForeColor = SystemColors.GrayText;
            MenuListButton.ForeColor = SystemColors.GrayText;

        }

        private void StaffListButton_Click(object sender, EventArgs e)
        {
            StaffListButton.BackColor = Color.FromArgb(0, 119, 195);
            SalesReportsByCatButton.BackColor = Color.White;
            SalesReportItemWiseButton.BackColor = Color.White;
            MenuListButton.BackColor = Color.White;

            StaffListButton.ForeColor = Color.White;
            SalesReportsByCatButton.ForeColor = SystemColors.GrayText;
            SalesReportItemWiseButton.ForeColor = SystemColors.GrayText;
            MenuListButton.ForeColor = SystemColors.GrayText;
        }

        private void SalesReportItemWiseButton_Click(object sender, EventArgs e)
        {
            SalesReportItemWiseButton.BackColor = Color.FromArgb(0, 119, 195);
            SalesReportsByCatButton.BackColor = Color.White;
            StaffListButton.BackColor = Color.White;
            MenuListButton.BackColor = Color.White;

            SalesReportItemWiseButton.ForeColor = Color.White;
            SalesReportsByCatButton.ForeColor = SystemColors.GrayText;
            StaffListButton.ForeColor = SystemColors.GrayText;
            MenuListButton.ForeColor = SystemColors.GrayText;
        }

        private void MenuListButton_Click(object sender, EventArgs e)
        {
            MenuListButton.BackColor = Color.FromArgb(0, 119, 195);
            SalesReportsByCatButton.BackColor = Color.White;
            StaffListButton.BackColor = Color.White;
            SalesReportItemWiseButton.BackColor = Color.White;

            MenuListButton.ForeColor = Color.White;
            SalesReportsByCatButton.ForeColor = SystemColors.GrayText;
            StaffListButton.ForeColor = SystemColors.GrayText;
            SalesReportItemWiseButton.ForeColor = SystemColors.GrayText;
        }



        #endregion


        #region KeyBoard Shortcuts Logic Functions
        private void Dashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (ProductPanel.Visible)
            {
                ProductsPanel_KeyDown(sender, e);
            }
        }
        private void ProductsPanel_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts specific to products panel
            if (e.Control && e.KeyCode == Keys.P)
            {
                AddProductsButton.PerformClick();
            }
        }
        #endregion


        #region Notfication Logic (if necessary)
        //private void button2_Click_1(object sender, EventArgs e)
        //{
        //    ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        //    contextMenuStrip.Items.Add("Exit", null, ExitMenuItem_Click);
        //    notifyIcon1.ContextMenuStrip = contextMenuStrip;
        //    notifyIcon1.ShowBalloonTip(3000, "Task Completed", "The task has been completed successfully.", ToolTipIcon.Info);
        //}

        //private void ExitMenuItem_Click(object sender, EventArgs e)
        //{
        //    notifyIcon1.Visible = false;

        //}
        #endregion



        private void ReportsPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (ReportsPanel.Visible)
            {
                try
                {
                    connection.Open();
                    command = new SqlCommand("select types from product_category", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ReportsCategoryComboBox.Items.Add(reader["types"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void GenerateReportByCatButton_Click(object sender, EventArgs e)
        {
            if (ReportsCatFromDateTextBox.Text != "" && ReportsCatToDateTextBox.Text != "" && ReportsCategoryComboBox.Text != "")
            {
                Reports reports = new Reports(ReportsCatFromDateTextBox.Value, ReportsCatToDateTextBox.Value, ReportsCategoryComboBox.Text);
                reports.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Fill All Fields");
            }
        }


        private void BranchTabButton_Click(object sender, EventArgs e)
        {
            if (BranchTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {

                BranchTabButton.BackColor = Color.FromArgb(37, 150, 190);
                BranchTabButton.ForeColor = Color.White;
                StockTransferTabButton.BackColor = Color.Transparent;
                StockTransferTabButton.ForeColor = SystemColors.GrayText;
                LoadDataAsync(BranchDataGrid,"select * from branches","Sync");
                BranchDataGrid.Visible = true;
                StockTransferDataGrid.Visible = false;
            }
        }

        private void StockTransferTabButton_Click(object sender, EventArgs e)
        {
            if (StockTransferTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {

                StockTransferTabButton.BackColor = Color.FromArgb(37, 150, 190);
                StockTransferTabButton.ForeColor = Color.White;
                BranchTabButton.BackColor = Color.Transparent;
                BranchTabButton.ForeColor = SystemColors.GrayText;
                BranchDataGrid.Visible = false;
                StockTransferDataGrid.Visible = true;
                LoadDataAsync(StockTransferDataGrid, "select * from transfers", "Sync");
            }
        }

        private void BranchPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (BranchPanel.Visible == true)
            {
                if (BranchTabButton.BackColor == Color.FromArgb(37, 150, 190))
                {
                    LoadDataAsync(BranchDataGrid, "select * from branches" , "Sync");
                }

                else
                {
                    LoadDataAsync(StockTransferDataGrid, "Select * from transfers" , "Sync");
                }
            }
        }

        private void AddBranchButton_Click(object sender, EventArgs e)
        {
            if (BranchTabButton.BackColor == Color.FromArgb(37, 150, 190))
            {
                BranchForm BForm = new BranchForm();
                BForm.FormClosed += (s, args) =>
                {
                    LoadDataAsync(BranchDataGrid, "SELECT * FROM BRANCHES", "Sync");
                };

                BForm.ShowDialog();
                LoadDataAsync(BranchDataGrid, "Select * from branches", "Sync");
            }
            else
            {
                StockTransferForm stockTransfer = new StockTransferForm();
                stockTransfer.ShowDialog();
                LoadDataAsync(StockTransferDataGrid, "Select * from transfers", "Sync");
            }
        }

        private void ReloadLaybyDataGrid()
        {
            DataTable laybyTable = new DataTable();

            // Fetch data from the database
            string query = "SELECT layby_no, client_name, total_amount, deposit, total_amount - deposit AS due_amount, payment_schedule, duration, expiry_date, status FROM layby";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    dataAdapter.Fill(laybyTable);
                }

                // Bind the DataTable to the DataGridView
                LaybyDataGrid.DataSource = laybyTable;

                // Set proper column headers
                LaybyDataGrid.Columns["layby_no"].HeaderText = "Layby No.";
                LaybyDataGrid.Columns["client_name"].HeaderText = "Client Name";
                LaybyDataGrid.Columns["total_amount"].HeaderText = "Total Amount";
                LaybyDataGrid.Columns["deposit"].HeaderText = "Deposit";
                LaybyDataGrid.Columns["due_amount"].HeaderText = "Due Amount";
                LaybyDataGrid.Columns["payment_schedule"].HeaderText = "Payment Schedule";
                LaybyDataGrid.Columns["duration"].HeaderText = "Duration";
                LaybyDataGrid.Columns["expiry_date"].HeaderText = "Expiry Date";
                LaybyDataGrid.Columns["status"].HeaderText = "Status";

                // Check if "Details" column already exists
                if (!LaybyDataGrid.Columns.Contains("Details"))
                {
                    DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                    {
                        HeaderText = "Details",
                        Name = "Details", // Set a unique Name for the column
                        Image = ResizeImage((Image)DetailsImage, 20, 20),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    LaybyDataGrid.Columns.Add(EditBtn);
                }

                // Check if "Pay" column already exists
                if (!LaybyDataGrid.Columns.Contains("Pay"))
                {
                    DataGridViewImageColumn DelBtn = new DataGridViewImageColumn
                    {
                        HeaderText = "Pay",
                        Name = "Pay", // Set a unique Name for the column
                        Image = ResizeImage((Image)PayImage, 20, 20),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    LaybyDataGrid.Columns.Add(DelBtn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching layby data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LaybyPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (LaybyPanel.Visible == true)
            {
                DataTable laybyTable = new DataTable();

                // Fetch data from the database
                string query = "SELECT layby_no, client_name, total_amount, deposit, total_amount - deposit AS due_amount, payment_schedule, duration, expiry_date, status FROM layby";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                        dataAdapter.Fill(laybyTable);
                    }

                    // Bind the DataTable to the DataGridView
                    LaybyDataGrid.DataSource = laybyTable;

                    // Set proper column headers
                    LaybyDataGrid.Columns["layby_no"].HeaderText = "Layby No.";
                    LaybyDataGrid.Columns["client_name"].HeaderText = "Client Name";
                    LaybyDataGrid.Columns["total_amount"].HeaderText = "Total Amount";
                    LaybyDataGrid.Columns["deposit"].HeaderText = "Deposit";
                    LaybyDataGrid.Columns["due_amount"].HeaderText = "Due Amount";
                    LaybyDataGrid.Columns["payment_schedule"].HeaderText = "Payment Schedule";
                    LaybyDataGrid.Columns["duration"].HeaderText = "Duration";
                    LaybyDataGrid.Columns["expiry_date"].HeaderText = "Expiry Date";
                    LaybyDataGrid.Columns["status"].HeaderText = "Status";

                    // Check if "Details" column already exists
                    if (!LaybyDataGrid.Columns.Contains("Details"))
                    {
                        DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                        {
                            HeaderText = "Details",
                            Name = "Details", // Set a unique Name for the column
                            Image = ResizeImage((Image)DetailsImage, 20, 20),
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        };
                        LaybyDataGrid.Columns.Add(EditBtn);
                    }

                    // Check if "Pay" column already exists
                    if (!LaybyDataGrid.Columns.Contains("Pay"))
                    {
                        DataGridViewImageColumn DelBtn = new DataGridViewImageColumn
                        {
                            HeaderText = "Pay",
                            Name = "Pay", // Set a unique Name for the column
                            Image = ResizeImage((Image)PayImage, 20, 20),
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        };
                        LaybyDataGrid.Columns.Add(DelBtn);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching layby data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LaybyDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Check if the "Details" button is clicked
                if (LaybyDataGrid.Columns[e.ColumnIndex].HeaderText == "Details")
                {
                    LaybyDetailsForm layby = new LaybyDetailsForm(
                        LaybyDataGrid.Rows[e.RowIndex].Cells["client_name"].Value.ToString(),
                        (decimal)LaybyDataGrid.Rows[e.RowIndex].Cells["total_amount"].Value,
                        (decimal)LaybyDataGrid.Rows[e.RowIndex].Cells["deposit"].Value,
                        (decimal)LaybyDataGrid.Rows[e.RowIndex].Cells["due_amount"].Value,
                        (int)LaybyDataGrid.Rows[e.RowIndex].Cells["layby_no"].Value
                    );
                    layby.ShowDialog();
                }
                // Check if the "Pay" button is clicked
                else if (LaybyDataGrid.Columns[e.ColumnIndex].HeaderText == "Pay")
                {
                    // Fetch the Layby No. from the clicked row
                    int laybyNo = (int)LaybyDataGrid.Rows[e.RowIndex].Cells["layby_no"].Value;

                    // Pass the Layby No. to the LaybyFormUpdate constructor
                    LaybyFormUpdate laybyUpdateForm = new LaybyFormUpdate(laybyNo);
                    laybyUpdateForm.ShowDialog();
                    ReloadLaybyDataGrid();
                }
            }
        }



        private void ScreenContainer_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Admin Panel

        private void adminDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    if (adminDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                    {
                        string username = adminDataGrid.Rows[e.RowIndex].Cells["username"].Value.ToString();
                        string email = adminDataGrid.Rows[e.RowIndex].Cells["email"].Value.ToString();
                        string password = adminDataGrid.Rows[e.RowIndex].Cells["password"].Value.ToString(); // Make sure you have the password column
                        UserEditForm editForm = new UserEditForm(email, username, password, e.RowIndex);
                        editForm.ShowDialog();

                        LoadDataAsync(adminDataGrid, Unionquery, "Sync");
                    }
                    else if (adminDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                    {
                        if (MessageBox.Show("Are you sure you want to delete this user?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            string userEmail = adminDataGrid.Rows[e.RowIndex].Cells["email"].Value.ToString();
                            string deleteQuery = "DELETE FROM users WHERE email = @UserEmail";
                            List<string> connectionStrings = new List<string> { "myconn", "myconnGS", "myconnHM" };

                            // Loop through each database connection string and delete the user
                            foreach (string connStringName in connectionStrings)
                            {
                                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
                                {
                                    conn.Open(); // Use synchronous open
                                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);
                                        int rowsAffected = cmd.ExecuteNonQuery(); // Execute the command
                                        if (rowsAffected > 0)
                                        {
                                            // Optionally, you can show how many rows were affected for debugging
                                            // Console.WriteLine($"{rowsAffected} row(s) deleted from {connStringName}.");
                                        }
                                    }
                                }
                            }

                            // Remove the user from the DataGridView
                            adminDataGrid.Rows.RemoveAt(e.RowIndex);
                            MessageBox.Show("User deleted successfully from all databases.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //yet to be added to activity log
                            ; LoadDataAsync(adminDataGrid, Unionquery, "Sync");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AccountsAddForm AAForm = new AccountsAddForm();
            AAForm.ShowDialog();
            LoadDataAsync(adminDataGrid, Unionquery, "Sync");
        }

        private void AdminSearchTB_TextChanged(object sender, EventArgs e)
        {
            string searchText = AdminSearchTB.Text;

            string query = $@"
        SELECT username, password, email, Role, Access 
        FROM POS.dbo.users 
        WHERE username LIKE '%{searchText}%' 
        OR Access LIKE '%{searchText}%' 
        OR email LIKE '%{searchText}%' 
        OR Role LIKE '%{searchText}%'
        UNION ALL
        SELECT username, password, email, Role, Access 
        FROM GeneralStorePOS.dbo.users 
        WHERE username LIKE '%{searchText}%' 
        OR Access LIKE '%{searchText}%' 
        OR email LIKE '%{searchText}%' 
        OR Role LIKE '%{searchText}%'
        UNION ALL
        SELECT username, password, email, Role, Access 
        FROM HotelManagementPOS.dbo.users 
        WHERE username LIKE '%{searchText}%' 
        OR Access LIKE '%{searchText}%' 
        OR email LIKE '%{searchText}%' 
        OR Role LIKE '%{searchText}%';";

            LoadDataAsync(adminDataGrid, query, "Sync");
        }

        #endregion

        #region Timesheet Panel

        private void LoadTimesheetData()
        {
            string query = "SELECT name, email, clock_in_time, clock_out_time, worked_hours, monthly_total_hours FROM timesheet";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Create a DataTable to hold the fetched data
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            // Add serial number column to the DataTable
                            dt.Columns.Add("SR#", typeof(int));
                            int serialNumber = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                row["SR#"] = serialNumber++;
                            }

                            dt.Columns["SR#"].SetOrdinal(0);
                            TimesheetGridView.DataSource = dt;
                            TimesheetGridView.Columns["SR#"].Width = 55;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading timesheet data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Ensure the connection is closed
                    connection.Close();
                }
            }
        }

        private void TimesheetSearchBox_TextChanged(object sender, EventArgs e)
        {
            // Get the search term
            string searchTerm = TimesheetSearchBox.Text.ToLower();

            // Filter the rows in the DataGridView based on the search term
            if (TimesheetGridView.DataSource is DataTable dt)
            {
                // Apply filter
                dt.DefaultView.RowFilter = string.Format("name LIKE '%{0}%' OR email LIKE '%{0}%'",
                                                         searchTerm);
            }
        }
        private void TimesheetButton_Click(object sender, EventArgs e)
        {
            if (TimesheetButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                TimesheetButton.BackColor = Color.FromArgb(37, 150, 190);
                TimesheetButton.ForeColor = Color.White;
                ActivityLogButton.BackColor = Color.Transparent;
                ActivityLogButton.ForeColor = SystemColors.GrayText;
                ActivityLogPanel.Visible = false;
            }
            if (TimesheetPanel.Visible == false)
            {
                TimesheetPanel.Visible = true;
                TimesheetSearchBox.TextChanged += TimesheetSearchBox_TextChanged;
            }
        }


        #endregion

        #region Activity Log

        private void LoadActivityLogData()
        {
            string query = "SELECT time, action, description, username FROM activity_log";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            // Add serial number column (SR#) at the start
                            dt.Columns.Add("SR#", typeof(int));
                            int serialNumber = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                row["SR#"] = serialNumber++;
                            }

                            // Move "SR#" column to the first position
                            dt.Columns["SR#"].SetOrdinal(0);

                            // Swap the positions of the "description" and "username" columns
                            dt.Columns["description"].SetOrdinal(3);  // Move to position 3 (index 3, right before the last column)
                            dt.Columns["username"].SetOrdinal(3);    // Move to position 2 (right after action)

                            // Bind the DataTable to the ActivityGridView
                            ActivityGridView.DataSource = dt;

                            // Set width of all columns to 100, except "description"
                            foreach (DataGridViewColumn column in ActivityGridView.Columns)
                            {
                                if (column.Name != "description")
                                {
                                    column.Width = 240;
                                }
                            }

                            ActivityGridView.Columns["SR#"].Width = 55;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading activity log data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ActivitySearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = ActivityLogSearchBox.Text.ToLower();
            if (ActivityGridView.DataSource is DataTable dt)
            {
                // Apply filter
                dt.DefaultView.RowFilter = string.Format("action LIKE '%{0}%' OR description LIKE '%{0}%' OR username LIKE '%{0}%'", searchTerm);
            }
        }

        private void ActivityLogButton_Click(object sender, EventArgs e)
        {
            if (ActivityLogButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                ActivityLogButton.BackColor = Color.FromArgb(37, 150, 190);
                ActivityLogButton.ForeColor = Color.White;
                TimesheetButton.BackColor = Color.Transparent;
                TimesheetButton.ForeColor = SystemColors.GrayText;
                TimesheetPanel.Visible = false;
            }
            if (ActivityLogPanel.Visible == false)
            {
                ActivityLogPanel.Visible = true;
                ActivityLogSearchBox.TextChanged += ActivitySearchBox_TextChanged;
                LoadActivityLogData();
                SetColumnHeaderText(ActivityGridView);

            }
        }

        #endregion

        #region CheckOut
        private void CheckOut_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to check out?",
                "Check Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string username = Session.Username; // Assumes you have a session variable for username
                DateTime currentDate = DateTime.Now.Date;
                int currentMonth = currentDate.Month;
                int currentYear = currentDate.Year;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Check if there is a check-in for today without a checkout
                        string checkQuery = @"SELECT clock_in_time FROM timesheet 
                                      WHERE name = @username 
                                      AND CAST(clock_in_time AS DATE) = @currentDate 
                                      AND clock_out_time IS NULL";

                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@username", username);
                            checkCmd.Parameters.AddWithValue("@currentDate", currentDate);

                            var clockInTime = checkCmd.ExecuteScalar();

                            if (clockInTime != null)
                            {
                                DateTime clockIn = (DateTime)clockInTime;
                                DateTime clockOut = DateTime.Now;

                                // Calculate worked hours
                                TimeSpan workedHours = clockOut - clockIn;
                                double workedHoursDecimal = Math.Round(workedHours.TotalHours, 2); // Rounded to 2 decimal places

                                // Update the timesheet with clock-out time and worked hours
                                string updateQuery = @"UPDATE timesheet 
                                               SET clock_out_time = @clockOutTime, 
                                                   worked_hours = @workedHours 
                                               WHERE name = @username 
                                               AND CAST(clock_in_time AS DATE) = @currentDate 
                                               AND clock_out_time IS NULL";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                                {
                                    updateCmd.Parameters.AddWithValue("@clockOutTime", clockOut);
                                    updateCmd.Parameters.AddWithValue("@workedHours", workedHoursDecimal);
                                    updateCmd.Parameters.AddWithValue("@username", username);
                                    updateCmd.Parameters.AddWithValue("@currentDate", currentDate);

                                    updateCmd.ExecuteNonQuery();
                                }

                                // Calculate the monthly total hours worked
                                string monthlyTotalQuery = @"SELECT SUM(worked_hours) FROM timesheet 
                                                     WHERE name = @username 
                                                     AND MONTH(clock_in_time) = @currentMonth 
                                                     AND YEAR(clock_in_time) = @currentYear";

                                using (SqlCommand monthlyTotalCmd = new SqlCommand(monthlyTotalQuery, connection))
                                {
                                    monthlyTotalCmd.Parameters.AddWithValue("@username", username);
                                    monthlyTotalCmd.Parameters.AddWithValue("@currentMonth", currentMonth);
                                    monthlyTotalCmd.Parameters.AddWithValue("@currentYear", currentYear);

                                    var monthlyTotal = monthlyTotalCmd.ExecuteScalar();
                                    double monthlyTotalHours = monthlyTotal != DBNull.Value ? Convert.ToDouble(monthlyTotal) : 0;

                                    // Optionally, you could update this in the timesheet table
                                    string updateMonthlyTotalQuery = @"UPDATE timesheet 
                                                               SET monthly_total_hours = @monthlyTotalHours 
                                                               WHERE name = @username 
                                                               AND MONTH(clock_in_time) = @currentMonth 
                                                               AND YEAR(clock_in_time) = @currentYear";

                                    using (SqlCommand updateMonthlyTotalCmd = new SqlCommand(updateMonthlyTotalQuery, connection))
                                    {
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@monthlyTotalHours", monthlyTotalHours);
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@username", username);
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@currentMonth", currentMonth);
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@currentYear", currentYear);

                                        updateMonthlyTotalCmd.ExecuteNonQuery();
                                    }

                                    // Insert the action into the activity log
                                    string logQuery = @"INSERT INTO activity_log (time, action, description, username) 
                                                VALUES (@timestamp, @action, @description, @username)";

                                    using (SqlCommand logCmd = new SqlCommand(logQuery, connection))
                                    {
                                        logCmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                                        logCmd.Parameters.AddWithValue("@action", "Checkout");
                                        logCmd.Parameters.AddWithValue("@description", $"User checked out. Worked hours: {workedHoursDecimal}, Monthly total hours: {monthlyTotalHours}");
                                        logCmd.Parameters.AddWithValue("@username", username);

                                        logCmd.ExecuteNonQuery();
                                    }

                                    MessageBox.Show($"You have successfully checked out. Worked hours: {workedHoursDecimal}\n" +
                                                    $"Monthly total hours: {monthlyTotalHours}",
                                                    "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No check-in found for today or already checked out.", "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred during checkout: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion


        #region Customer 

        // Method to log activity
        private void LogActivity(string actionType, string description)
        {
            try
            {
                {
                    connection.Open();
                    string logQuery = "INSERT INTO activity_log (action, description, time, username) VALUES (@Action, @Description, @Time, @Username)";

                    using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                    {
                        logCommand.Parameters.AddWithValue("@Action", actionType);
                        logCommand.Parameters.AddWithValue("@Description", description);
                        logCommand.Parameters.AddWithValue("@Time", DateTime.Now);
                        logCommand.Parameters.AddWithValue("@Username", Session.Username); // Replace with your current session user retrieval

                        logCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while logging activity: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomerAddButton_Click(object sender, EventArgs e)
        {
            CustomerAddFormGS customerAddForm = new CustomerAddFormGS();
            customerAddForm.ShowDialog();
            LoadDataAsync(CustomersGridView, "Select * from customers", "Sync");
        }

        private void CustomersSearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = CustomersSearchBox.Text.ToLower();  // Get the search text

            try
            {
                if (CustomersGridView.Visible)  // Check if the CustomersGridView is visible
                {
                    // Filtering the CustomersGridView based on the search text
                    foreach (DataGridViewRow row in CustomersGridView.Rows)
                    {
                        bool isVisible = row.Cells["customer_name"].Value.ToString().ToLower().Contains(searchText) ||
                                         row.Cells["phone_number"].Value.ToString().ToLower().Contains(searchText) ||
                                         row.Cells["email"].Value.ToString().ToLower().Contains(searchText) ||
                                         row.Cells["address"].Value.ToString().ToLower().Contains(searchText);

                        row.Visible = isVisible;  // Hide or show row based on the condition
                    }
                }
                else if (PurchaseHistoryGridView.Visible)  // Check if the PurchaseHistoryGridView is visible
                {
                    // Filtering the PurchaseHistoryGridView based on the search text
                    foreach (DataGridViewRow row in PurchaseHistoryGridView.Rows)
                    {
                        bool isVisible = row.Cells["customer_name"].Value.ToString().ToLower().Contains(searchText) ||
                                         row.Cells["bill_id"].Value.ToString().ToLower().Contains(searchText) ||
                                         row.Cells["purchase_date"].Value.ToString().ToLower().Contains(searchText) ||
                                         row.Cells["purchase_time"].Value.ToString().ToLower().Contains(searchText);

                        row.Visible = isVisible;  // Hide or show row based on the condition
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    // Handle Edit action
                    if (CustomersGridView.Columns[e.ColumnIndex].HeaderText == "Edit")
                    {
                        string customerId = CustomersGridView.Rows[e.RowIndex].Cells["customer_id"].Value.ToString();
                        string customerName = CustomersGridView.Rows[e.RowIndex].Cells["customer_name"].Value.ToString();
                        string phoneNumber = CustomersGridView.Rows[e.RowIndex].Cells["phone_number"].Value.ToString();
                        string email = CustomersGridView.Rows[e.RowIndex].Cells["email"].Value.ToString();
                        string address = CustomersGridView.Rows[e.RowIndex].Cells["address"].Value.ToString();
                        DateTime lastPurchaseDate = CustomersGridView.Rows[e.RowIndex].Cells["last_purchase_date"].Value is DateTime dateValue
                            ? dateValue
                            : DateTime.MinValue; // Handle the case if MinValue is not desired
                        string credit = CustomersGridView.Rows[e.RowIndex].Cells["credit"].Value.ToString();
                        string points = CustomersGridView.Rows[e.RowIndex].Cells["points"].Value.ToString();

                        // Pass the data to the CustomerUpdateForm
                        CustomerUpdateFormGS updateForm = new CustomerUpdateFormGS(customerId, customerName, phoneNumber, email, address, lastPurchaseDate, credit, points, e.RowIndex);
                        updateForm.ShowDialog();

                        // Optionally, reload data after the update
                        LoadDataAsync(CustomersGridView, "SELECT * FROM customers", "Sync");

                        // Log the activity of editing the customer
                        LogActivity("Edit Customer", $"Customer edited: {customerName} (ID: {customerId})");

                    }
                    // Handle Delete action
                    else if (CustomersGridView.Columns[e.ColumnIndex].HeaderText == "Delete")
                    {
                        if (MessageBox.Show("Are you sure you want to delete this customer?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            string customerId = CustomersGridView.Rows[e.RowIndex].Cells["customer_id"].Value.ToString();
                            string customerName = CustomersGridView.Rows[e.RowIndex].Cells["customer_name"].Value.ToString();

                            string deleteQuery = "DELETE FROM customers WHERE customer_id = @CustomerId";

                            {
                                connection.Open(); // Use synchronous open
                                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                                {
                                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                                    int rowsAffected = cmd.ExecuteNonQuery(); // Execute the command
                                    if (rowsAffected > 0)
                                    {
                                        // Optionally, you can show how many rows were affected for debugging
                                        // Console.WriteLine($"{rowsAffected} row(s) deleted.");
                                    }
                                }
                            }

                            // Remove the customer from the DataGridView
                            CustomersGridView.Rows.RemoveAt(e.RowIndex);
                            MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reload the data after deletion
                            LoadDataAsync(CustomersGridView, "SELECT * FROM customers", "Sync");

                            // Log the activity of deleting the customer
                            LogActivity("Delete Customer", $"Customer deleted: {customerName} (ID: {customerId})");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PurchaseHistoryButton_Click(object sender, EventArgs e)
        {
            if (PurchaseHistoryButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                PurchaseHistoryButton.BackColor = Color.FromArgb(37, 150, 190);
                PurchaseHistoryButton.ForeColor = Color.White;
                CustomerDetailsButton.BackColor = Color.Transparent;
                CustomerDetailsButton.ForeColor = SystemColors.GrayText;
                LoadDataAsync(PurchaseHistoryGridView, "SELECT * FROM purchase_history", "Sync");
                SetColumnHeaderText(PurchaseHistoryGridView);
                CustomersGridView.Visible = false;
                PurchaseHistoryGridView.Visible = true;
                CustomerAddButton.Visible = false;

            }
        }

        private void CustomerDetailsButton_Click_1(object sender, EventArgs e)
        {
            if (CustomerDetailsButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                CustomerDetailsButton.BackColor = Color.FromArgb(37, 150, 190);
                CustomerDetailsButton.ForeColor = Color.White;
                PurchaseHistoryButton.BackColor = Color.Transparent;
                PurchaseHistoryButton.ForeColor = SystemColors.GrayText;
                LoadDataAsync(CustomersGridView, "SELECT * FROM CUSTOMERS", "Sync");
                PurchaseHistoryGridView.Visible = false;
                CustomersGridView.Visible = true;
                CustomerAddButton.Visible = true;
            }

        }
        #endregion

        private void LogOutLabel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the user confirms to log out
            if (result == DialogResult.Yes)
            {
                string username = Session.Username;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string logQuery = @"INSERT INTO activity_log (time, action, description, username) 
                                    VALUES (@timestamp, @action, @description, @username)";

                        using (SqlCommand logCmd = new SqlCommand(logQuery, connection))
                        {
                            logCmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                            logCmd.Parameters.AddWithValue("@action", "Logout");
                            logCmd.Parameters.AddWithValue("@description", "User logged out successfully.");
                            logCmd.Parameters.AddWithValue("@username", username);

                            logCmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while logging the logout action: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                Session.ClearSession();
                Application.Exit();
                LoginForm loginform = new LoginForm();
                loginform.Show();
            }
        }

        private void CheckOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Are you sure you want to check out?",
               "Check Out",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string username = Session.Username; // Assumes you have a session variable for username
                DateTime currentDate = DateTime.Now.Date;
                int currentMonth = currentDate.Month;
                int currentYear = currentDate.Year;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Check if there is a check-in for today without a checkout
                        string checkQuery = @"SELECT clock_in_time FROM timesheet 
                                      WHERE name = @username 
                                      AND CAST(clock_in_time AS DATE) = @currentDate 
                                      AND clock_out_time IS NULL";

                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@username", username);
                            checkCmd.Parameters.AddWithValue("@currentDate", currentDate);

                            var clockInTime = checkCmd.ExecuteScalar();

                            if (clockInTime != null)
                            {
                                DateTime clockIn = (DateTime)clockInTime;
                                DateTime clockOut = DateTime.Now;

                                // Calculate worked hours
                                TimeSpan workedHours = clockOut - clockIn;
                                double workedHoursDecimal = Math.Round(workedHours.TotalHours, 2); // Rounded to 2 decimal places

                                // Update the timesheet with clock-out time and worked hours
                                string updateQuery = @"UPDATE timesheet 
                                               SET clock_out_time = @clockOutTime, 
                                                   worked_hours = @workedHours 
                                               WHERE name = @username 
                                               AND CAST(clock_in_time AS DATE) = @currentDate 
                                               AND clock_out_time IS NULL";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                                {
                                    updateCmd.Parameters.AddWithValue("@clockOutTime", clockOut);
                                    updateCmd.Parameters.AddWithValue("@workedHours", workedHoursDecimal);
                                    updateCmd.Parameters.AddWithValue("@username", username);
                                    updateCmd.Parameters.AddWithValue("@currentDate", currentDate);

                                    updateCmd.ExecuteNonQuery();
                                }

                                // Calculate the monthly total hours worked
                                string monthlyTotalQuery = @"SELECT SUM(worked_hours) FROM timesheet 
                                                     WHERE name = @username 
                                                     AND MONTH(clock_in_time) = @currentMonth 
                                                     AND YEAR(clock_in_time) = @currentYear";

                                using (SqlCommand monthlyTotalCmd = new SqlCommand(monthlyTotalQuery, connection))
                                {
                                    monthlyTotalCmd.Parameters.AddWithValue("@username", username);
                                    monthlyTotalCmd.Parameters.AddWithValue("@currentMonth", currentMonth);
                                    monthlyTotalCmd.Parameters.AddWithValue("@currentYear", currentYear);

                                    var monthlyTotal = monthlyTotalCmd.ExecuteScalar();
                                    double monthlyTotalHours = monthlyTotal != DBNull.Value ? Convert.ToDouble(monthlyTotal) : 0;

                                    // Optionally, you could update this in the timesheet table
                                    string updateMonthlyTotalQuery = @"UPDATE timesheet 
                                                               SET monthly_total_hours = @monthlyTotalHours 
                                                               WHERE name = @username 
                                                               AND MONTH(clock_in_time) = @currentMonth 
                                                               AND YEAR(clock_in_time) = @currentYear";

                                    using (SqlCommand updateMonthlyTotalCmd = new SqlCommand(updateMonthlyTotalQuery, connection))
                                    {
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@monthlyTotalHours", monthlyTotalHours);
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@username", username);
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@currentMonth", currentMonth);
                                        updateMonthlyTotalCmd.Parameters.AddWithValue("@currentYear", currentYear);
                                        
                                        updateMonthlyTotalCmd.ExecuteNonQuery();
                                    }

                                    // Insert the action into the activity log
                                    string logQuery = @"INSERT INTO activity_log (time, action, description, username) 
                                                VALUES (@timestamp, @action, @description, @username)";

                                    using (SqlCommand logCmd = new SqlCommand(logQuery, connection))
                                    {
                                        logCmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                                        logCmd.Parameters.AddWithValue("@action", "Checkout");
                                        logCmd.Parameters.AddWithValue("@description", $"User checked out. Worked hours: {workedHoursDecimal}, Monthly total hours: {monthlyTotalHours}");
                                        logCmd.Parameters.AddWithValue("@username", username);

                                        logCmd.ExecuteNonQuery();
                                    }

                                    MessageBox.Show($"You have successfully checked out. Worked hours: {workedHoursDecimal}\n" +
                                                    $"Monthly total hours: {monthlyTotalHours}",
                                                    "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No check-in found for today or already checked out.", "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred during checkout: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            NewScreen();
        }

        private void PurchasesButton_Click(object sender, EventArgs e)
        {
            if (PurchasesButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                PurchasesButton.BackColor = Color.FromArgb(37, 150, 190);
                PurchasesButton.ForeColor = Color.White;
                ManageOrdersButton.BackColor = Color.Transparent;
                ManageOrdersButton.ForeColor = SystemColors.GrayText;

            }
            if (AdminOrdersDataGrid.Visible == true)
            {
                AdminOrdersDataGrid.Visible = false;
                PurchaseDataGrid.Visible = true;
                button4.Visible = true;
                PurchaseAddButton.Visible = true;
                Purchase_SearchTextBox.Visible = true;
                LoadDataAsync(PurchaseDataGrid, "select * from purchases", "Async");
            }
        }

        private void ManageOrdersButton_Click(object sender, EventArgs e)
        {
            if (ManageOrdersButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                ManageOrdersButton.BackColor = Color.FromArgb(37, 150, 190);
                ManageOrdersButton.ForeColor = Color.White;
                PurchasesButton.BackColor = Color.Transparent;
                PurchasesButton.ForeColor = SystemColors.GrayText;

            }
            if (PurchaseDataGrid.Visible == true)
            {
                PurchaseDataGrid.Visible = false;
                AdminOrdersDataGrid.Visible = true;
                button4.Visible = false;
                PurchaseAddButton.Visible = false;
                Purchase_SearchTextBox.Visible = false;
                LoadDataAsync2(AdminOrdersDataGrid, "select bill_id, customer, phone, date, type , status , total_amount , discount , net_total_amount from bill_list", "Sync");
                LoadDataAsync(PurchaseDataGrid, "select * from purchases", "Async");
            }
        }

        private void ContentContainer_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (ContentContainer_panel.Visible == true)
            {
                updateDashboardValues();
            }
        }

        private void AdminOrdersDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string headerText = AdminOrdersDataGrid.Columns[e.ColumnIndex].HeaderText;
            if (headerText == "Edit")
            {
                string billId = AdminOrdersDataGrid.Rows[e.RowIndex].Cells["bill_id"].Value?.ToString();
                string orderStatus = AdminOrdersDataGrid.Rows[e.RowIndex].Cells["status"].Value?.ToString();

                if (!string.IsNullOrEmpty(billId))
                {
                    // Check if the order status is "Cancelled"
                    if (orderStatus == "Cancelled")
                    {
                        MessageBox.Show("Cancelled orders cannot be edited.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; 
                    }
                    EditOrderFormGS editOrderForm = new EditOrderFormGS(billId);
                    editOrderForm.ShowDialog();
                    LoadDataAsync2(AdminOrdersDataGrid, "select bill_id, customer, phone, date, type , status , total_amount , discount , net_total_amount from bill_list", "Sync");
                }
            }
        }
    }
}

