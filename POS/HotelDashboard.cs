using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;
using System.Numerics;
using System.Windows.Forms;
using static POS.RoomCard;

namespace POS
{
    public partial class HotelDashboard : Form
    {
        #region All Declared Variables
        private PrivateFontCollection privateFonts = new PrivateFontCollection();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotelDashboard));
        private System.Windows.Forms.Timer LabelTimer;
        private System.Windows.Forms.Timer PanelTimer;
        private Point Menu_Logo_initialPosition;
        private Point Menu_Logo_targetPosition;
        private Point Menu_Dashboard_label_initialPosition;
        private Point Menu_GuestInfo_Label_initialPosition;
        private Point Menu_Rooms_Label_initialPosition;
        private Point Menu_Billing_Label_initialPosition;
        private Point Menu_Check_Label_initialPosition;
        private Point Menu_Ingredients_label_initialPosition;
        private Point Menu_Products_label_initialPosition;
        private Point Menu_Tables_label_initialPosition;
        private Point Menu_Staff_label_initialPosition;
        private Point Menu_POS_label_initialPosition;
        private Point Menu_Kitchen_label_initialPosition;
        private Point Menu_Reports_label_initialPosition;
        private Point Menu_Settings_label_initialPosition;
        private Point Menu_Dashboard_label_targetPosition;
        private Point Menu_GuestInfo_Label_targetPosition;
        private Point Menu_Rooms_Label_targetPosition;
        private Point Menu_Billing_Label_targetPosition;
        private Point Menu_Check_Label_targetPosition;
        private Point Menu_Ingredients_label_targetPosition;
        private Point Menu_Products_label_targetPosition;
        private Point Menu_Tables_label_targetPosition;
        private Point Menu_Staff_label_targetPosition;
        private Point Menu_POS_label_targetPosition;
        private Point Menu_Kitchen_label_targetPosition;
        private Point Menu_Reports_label_targetPosition;
        private Point Menu_Settings_label_targetPosition;
        private Point Menu_Attendance_Label_InitialPosition;
        private Point Menu_Attendance_Label_targetPosition;
        private Point LogOutLabel_initialPosition;
        private Point LogOutLabel_targetPosition;
        private Point Menu_Admin_Label_targetPosition;
        private Point Menu_Admin_Label_initialPosition;
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
        string connectionString;
        private string Unionquery = @"
        SELECT username, password, email, Role, Access FROM POS.dbo.users
        UNION ALL
        SELECT username, password, email, role, Access FROM GeneralStorePOS.dbo.users
        UNION ALL
        SELECT username, password, email, role, Access FROM HotelManagementPOS.dbo.users;";
        private int? selectedRecordId = null;
        #endregion
        public HotelDashboard()
        {
            InitializeComponent();
            AdjustFormSize();
            InitializeDatabaseConnection();
            ImageEditDelLoad();
            AllowOnlyNumbers();
            AddEnterKeyPreventionToTextBoxes(BillTaxTB, BillAdditionalChargesTB, BillDiscountTB, BillCashReceivedTB);
            #region Calling Image Resize and Rounded Corner,Timer & Font Functions 
            InitializeLabel(Menu_Dashboard_label, (Image)resources.GetObject("Menu_Dashboard_label.Image"), 25, 25);
            InitializeLabel(Menu_Billing_Label, (Image)resources.GetObject("Menu_Billing_Label.Image"), 35, 35);
            InitializeLabel(Menu_Ingredients_label, (Image)resources.GetObject("Menu_Ingredients_label.Image"), 25, 25);
            InitializeLabel(Menu_Products_label, (Image)resources.GetObject("Menu_Products_label.Image"), 25, 25);
            InitializeLabel(Menu_Tables_label, (Image)resources.GetObject("Menu_Tables_label.Image"), 25, 27);
            InitializeLabel(Menu_Staff_label, (Image)resources.GetObject("Menu_Staff_label.Image"), 25, 25);
            InitializeLabel(Menu_POS_label, (Image)resources.GetObject("Menu_POS_label.Image"), 25, 25);
            InitializeLabel(Menu_Kitchen_label, (Image)resources.GetObject("Menu_Kitchen_label.Image"), 25, 25);
            InitializeLabel(Menu_Reports_label, (Image)resources.GetObject("Menu_Reports_label.Image"), 25, 25);
            InitializeLabel(Menu_Settings_label, (Image)resources.GetObject("Menu_Settings_label.Image"), 25, 25);
            InitializeLabel(Menu_Attendance_Label, (Image)resources.GetObject("Menu_Attendance_Label.Image"), 25, 25);
            InitializeLabel(Menu_Admin_Label, (Image)resources.GetObject("Menu_Admin_Label.Image"), 25, 25);
            InitializeLabel(LogOutLabel, (Image)resources.GetObject("LogOutLabel.Image"), 25, 25);
            InitializeLabel(Logo, (Image)resources.GetObject("Logo.Image"), 30, 45);
            InitializeLabel(LogoText, (Image)resources.GetObject("LogoText.Image"), 150, 25);
            RoundCorners(Menu_Dashboard_label, 20);
            RoundCorners(Menu_GuestInfo_Label, 20);
            RoundCorners(Menu_Rooms_Label, 20);
            RoundCorners(Menu_Billing_Label, 20);
            RoundCorners(Menu_Check_Label, 20);
            RoundCorners(Menu_Ingredients_label, 20);
            RoundCorners(Menu_Products_label, 20);
            RoundCorners(Menu_Tables_label, 20);
            RoundCorners(Menu_Staff_label, 20);
            RoundCorners(Menu_POS_label, 20);
            RoundCorners(Menu_Kitchen_label, 20);
            RoundCorners(Menu_Reports_label, 20);
            RoundCorners(Menu_Settings_label, 20);
            RoundCorners(Menu_Attendance_Label, 20);
            RoundCorners(Menu_Admin_Label, 20);
            RoundCorners(LogOutLabel, 20);
            InitializeTimer();
            Menu_Admin_Label.Location = new Point(55, 735); //ADMIN LABEL 
            Menu_Attendance_Label.Location = new Point(55, 785);
            LogOutLabel.Location = new Point(59, 835); //new label mazdoori

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


        #region All Necessary Functions  (Sidebar Labels,Icons,Animations and ByteToImage)

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
                Menu_Dashboard_label_targetPosition = new Point(10, 85);

                Menu_GuestInfo_Label_initialPosition = Menu_GuestInfo_Label.Location;
                Menu_GuestInfo_Label_targetPosition = new Point(10, 135);

                Menu_Rooms_Label_initialPosition = Menu_Rooms_Label.Location;
                Menu_Rooms_Label_targetPosition = new Point(10, 185);

                Menu_Billing_Label_initialPosition = Menu_Billing_Label.Location;
                Menu_Billing_Label_targetPosition = new Point(10, 235);

                Menu_Check_Label_initialPosition = Menu_Check_Label.Location;
                Menu_Check_Label_targetPosition = new Point(10, 285);

                Menu_Ingredients_label_initialPosition = Menu_Ingredients_label.Location;
                Menu_Ingredients_label_targetPosition = new Point(10, 335);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(10, 385);

                Menu_Tables_label_initialPosition = Menu_Tables_label.Location;
                Menu_Tables_label_targetPosition = new Point(10, 435);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(10, 485);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(10, 535);

                Menu_Kitchen_label_initialPosition = Menu_Kitchen_label.Location;
                Menu_Kitchen_label_targetPosition = new Point(10, 585);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(10, 635);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(10, 685);

                Menu_Admin_Label_initialPosition = Menu_Admin_Label.Location;
                Menu_Admin_Label_targetPosition = new Point(10, 735); //ADMIN LABEL 

                Menu_Attendance_Label_InitialPosition = Menu_Attendance_Label.Location;
                Menu_Attendance_Label_targetPosition = new Point(10, 785);

                LogOutLabel_initialPosition = LogOutLabel.Location;
                LogOutLabel_targetPosition = new Point(14, 835); //new label mazdoori


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
                Menu_Dashboard_label_targetPosition = new Point(55, 85);

                Menu_GuestInfo_Label_initialPosition = Menu_GuestInfo_Label.Location;
                Menu_GuestInfo_Label_targetPosition = new Point(55, 135);

                Menu_Rooms_Label_initialPosition = Menu_Rooms_Label.Location;
                Menu_Rooms_Label_targetPosition = new Point(55, 185);

                Menu_Billing_Label_initialPosition = Menu_Billing_Label.Location;
                Menu_Billing_Label_targetPosition = new Point(55, 235);

                Menu_Check_Label_initialPosition = Menu_Check_Label.Location;
                Menu_Check_Label_targetPosition = new Point(55, 285);

                Menu_Ingredients_label_initialPosition = Menu_Ingredients_label.Location;
                Menu_Ingredients_label_targetPosition = new Point(55, 335);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(55, 385);

                Menu_Tables_label_initialPosition = Menu_Tables_label.Location;
                Menu_Tables_label_targetPosition = new Point(55, 435);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(55, 485);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(55, 535);

                Menu_Kitchen_label_initialPosition = Menu_Kitchen_label.Location;
                Menu_Kitchen_label_targetPosition = new Point(55, 585);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(55, 635);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(55, 685);

                Menu_Admin_Label_initialPosition = Menu_Admin_Label.Location;
                Menu_Admin_Label_targetPosition = new Point(55, 735); //ADMIN LABEL 

                Menu_Attendance_Label_InitialPosition = Menu_Attendance_Label.Location;
                Menu_Attendance_Label_targetPosition = new Point(55, 785);

                LogOutLabel_initialPosition = LogOutLabel.Location;
                LogOutLabel_targetPosition = new Point(59, 835); //new label mazdoori

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
                Menu_GuestInfo_Label.Location = Menu_GuestInfo_Label_targetPosition;
                Menu_Rooms_Label.Location = Menu_Rooms_Label_targetPosition;
                Menu_Billing_Label.Location = Menu_Billing_Label_targetPosition;
                Menu_Check_Label.Location = Menu_Check_Label_targetPosition;
                Menu_Ingredients_label.Location = Menu_Ingredients_label_targetPosition;
                Menu_Products_label.Location = Menu_Products_label_targetPosition;
                Menu_Tables_label.Location = Menu_Tables_label_targetPosition;
                Menu_Staff_label.Location = Menu_Staff_label_targetPosition;
                Menu_POS_label.Location = Menu_POS_label_targetPosition;
                Menu_Kitchen_label.Location = Menu_Kitchen_label_targetPosition;
                Menu_Reports_label.Location = Menu_Reports_label_targetPosition;
                Menu_Settings_label.Location = Menu_Settings_label_targetPosition;
                Menu_Admin_Label.Location = Menu_Admin_Label_targetPosition; //ADMIN LABEL
                Menu_Attendance_Label.Location = Menu_Attendance_Label_targetPosition;
                LogOutLabel.Location = LogOutLabel_targetPosition;
                Logo.Location = Menu_Logo_targetPosition;

            }
            else
            {
                int Menu_Dashboard_label_newX = (int)(Menu_Dashboard_label_initialPosition.X + (Menu_Dashboard_label_targetPosition.X - Menu_Dashboard_label_initialPosition.X) * progress);
                int Menu_Dashboard_label_newY = (int)Menu_Dashboard_label_targetPosition.Y;
                Menu_Dashboard_label.Location = new Point(Menu_Dashboard_label_newX, Menu_Dashboard_label_newY);

                int Menu_GuestInfo_Label_newX = (int)(Menu_GuestInfo_Label_initialPosition.X + (Menu_GuestInfo_Label_targetPosition.X - Menu_GuestInfo_Label_initialPosition.X) * progress);
                int Menu_GuestInfo_Label_newY = (int)Menu_GuestInfo_Label_targetPosition.Y;
                Menu_GuestInfo_Label.Location = new Point(Menu_GuestInfo_Label_newX, Menu_GuestInfo_Label_newY);

                int Menu_Rooms_Label_newX = (int)(Menu_Rooms_Label_initialPosition.X + (Menu_Rooms_Label_targetPosition.X - Menu_Rooms_Label_initialPosition.X) * progress);
                int Menu_Rooms_Label_newY = (int)Menu_Rooms_Label_targetPosition.Y;
                Menu_Rooms_Label.Location = new Point(Menu_Rooms_Label_newX, Menu_Rooms_Label_newY);

                int Menu_Billing_Label_newX = (int)(Menu_Billing_Label_initialPosition.X + (Menu_Billing_Label_targetPosition.X - Menu_Billing_Label_initialPosition.X) * progress);
                int Menu_Billing_Label_newY = (int)Menu_Billing_Label_targetPosition.Y;
                Menu_Billing_Label.Location = new Point(Menu_Billing_Label_newX, Menu_Billing_Label_newY);

                int Menu_Check_Label_newX = (int)(Menu_Check_Label_initialPosition.X + (Menu_Check_Label_targetPosition.X - Menu_Check_Label_initialPosition.X) * progress);
                int Menu_Check_Label_newY = (int)Menu_Check_Label_targetPosition.Y;
                Menu_Check_Label.Location = new Point(Menu_Check_Label_newX, Menu_Check_Label_newY);

                int Menu_Ingredients_label_newX = (int)(Menu_Ingredients_label_initialPosition.X + (Menu_Ingredients_label_targetPosition.X - Menu_Ingredients_label_initialPosition.X) * progress);
                int Menu_Ingredients_label_newY = (int)Menu_Ingredients_label_targetPosition.Y;
                Menu_Ingredients_label.Location = new Point(Menu_Ingredients_label_newX, Menu_Ingredients_label_newY);

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

                int Menu_Admin_Label_newX = (int)(Menu_Admin_Label_initialPosition.X + (Menu_Admin_Label_targetPosition.X - Menu_Admin_Label_initialPosition.X) * progress);
                int Menu_Admin_Label_newY = (int)Menu_Admin_Label_targetPosition.Y;
                Menu_Admin_Label.Location = new Point(Menu_Admin_Label_newX, Menu_Admin_Label_newY); //ADMIN LABEL

                int Menu_Attendance_label_newX = (int)(Menu_Attendance_Label_InitialPosition.X + (Menu_Attendance_Label_targetPosition.X - Menu_Attendance_Label_InitialPosition.X) * progress);
                int Menu_Attendance_label_newY = (int)Menu_Attendance_Label_targetPosition.Y;
                Menu_Attendance_Label.Location = new Point(Menu_Attendance_label_newX, Menu_Attendance_label_newY);

                int LogOut_label_newX = (int)(LogOutLabel_initialPosition.X + (LogOutLabel_targetPosition.X - LogOutLabel_initialPosition.X) * progress);
                int LogOut_label_newY = (int)LogOutLabel_targetPosition.Y;
                LogOutLabel.Location = new Point(LogOut_label_newX, LogOut_label_newY);

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
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Dashboard";
            if (ContentContainer_panel.Visible == false)
            {
                ContentContainer_panel.Visible = true;
                GuestInfoPanel.Visible = false;
                CheckPanel.Visible = false;
                RoomsPanel.Visible = false;
                BillingPanel.Visible = false;
                ProductPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }


        private void Menu_GuestInfo_Label_Click(object sender, EventArgs e) //guest mazdoori
        {
            SetLabelColor(Menu_GuestInfo_Label, "#0077C3");
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Guest Information";
            if (GuestInfoPanel.Visible == false)
            {
                GuestInfoPanel.Visible = true;

                ClearGuestInfo();
                PopulateRoomNumberComboBox();
                AllowOnlyNumbers();

                CheckPanel.Visible = false;
                BillingPanel.Visible = false;
                RoomsPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                ProductPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Rooms_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Rooms_Label, "#0077C3");
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Rooms";
            if (RoomsPanel.Visible == false)
            {
                RoomsPanel.Visible = true;
                GuestInfoPanel.Visible = false;
                CheckPanel.Visible = false;
                BillingPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                ProductPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Billing_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Billing_Label, "#0077C3");
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Billing Details";
            if (BillingPanel.Visible == false)
            {
                BillingPanel.Visible = true;
                GuestInfoPanel.Visible = false;
                RoomsPanel.Visible = false;
                CheckPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                ProductPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                ClearBillFields();
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Check_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Check_Label, "#0077C3");
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Check In/Check Out";
            if (CheckPanel.Visible == false)
            {
                CheckPanel.Visible = true;
                GuestInfoPanel.Visible = false;
                RoomsPanel.Visible = false;
                BillingPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                ProductPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                LoadDataAsync(CheckDataGrid, "select * from guestcheck", "Sync");
                CheckCheckOUTButton.Enabled = false;
                CheckCCheckINButton.Enabled = false;
                CheckUpdateButton.Enabled = false;
            }
        }


        private void Menu_Ingredients_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Ingredients_label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Ingredients";
            if (IngredientsPanel.Visible == false)
            {
                IngredientsPanel.Visible = true;
                ContentContainer_panel.Visible = false;
                CheckPanel.Visible = false;
                RoomsPanel.Visible = false;
                BillingPanel.Visible = false;
                GuestInfoPanel.Visible = false;
                ProductPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Products_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Products_label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Products/Restaurant";
            if (ProductPanel.Visible == false)
            {
                ProductPanel.Visible = true;
                ContentContainer_panel.Visible = false;
                GuestInfoPanel.Visible = false;
                RoomsPanel.Visible = false;
                CheckPanel.Visible = false;
                BillingPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Tables_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Tables_label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Tables";
            if (TablesPanel.Visible == false)
            {
                TablesPanel.Visible = true;
                ContentContainer_panel.Visible = false;
                GuestInfoPanel.Visible = false;
                BillingPanel.Visible = false;
                RoomsPanel.Visible = false;
                CheckPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                ProductPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private int ControlsCount = 0;
        private async void Menu_POS_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_POS_label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "POS";
            if (POSPanel.Visible == false)
            {
                POSPanel.Visible = true;
                ProductPanel.Visible = false;
                GuestInfoPanel.Visible = false;
                CheckPanel.Visible = false;
                RoomsPanel.Visible = false;
                BillingPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
                StartTransition(60, "Hide");
                AddPOSCategory();
                await Task.Delay(100);
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
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Staff";
            if (StaffPanel.Visible == false)
            {
                StaffPanel.Visible = true;
                ProductPanel.Visible = false;
                RoomsPanel.Visible = false;
                CheckPanel.Visible = false;
                BillingPanel.Visible = false;
                GuestInfoPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Kitchen_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Kitchen_label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Kitchen";
            if (KitchenPanel.Visible == false)
            {
                KitchenPanel.Visible = true;
                ProductPanel.Visible = false;
                RoomsPanel.Visible = false;
                BillingPanel.Visible = false;
                GuestInfoPanel.Visible = false;
                CheckPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Reports_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Reports_label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Reports";
            if (ReportsPanel.Visible == false)
            {
                ReportsPanel.Visible = true;
                ProductPanel.Visible = false;
                GuestInfoPanel.Visible = false;
                RoomsPanel.Visible = false;
                BillingPanel.Visible = false;
                CheckPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                KitchenPanel.Visible = false;
                CustomerPanel.Visible = false;
                AttendancePanel.Visible = false;
                AdminPanel.Visible = false;
                AdminOrdersDataGrid.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Settings_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Settings_label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;

            GuestInfoPanel.Visible = false;
            RoomsPanel.Visible = false;
            CheckPanel.Visible = false;
            BillingPanel.Visible = false;
            AttendancePanel.Visible = false;
            AdminPanel.Visible = false;
            ContentContainer_panel.Visible = false;
            ProductPanel.Visible = false;
            IngredientsPanel.Visible = false;
            StaffPanel.Visible = false;
            POSPanel.Visible = false;
            TablesPanel.Visible = false;
            KitchenPanel.Visible = false;
            ReportsPanel.Visible = false;
            CustomerPanel.Visible = true;
            AdminOrdersDataGrid.Visible = false;
            Current_ScreenName_label.Text = "Customers";
            LoadDataAsync(CustomersGridView, "select * from customers", "Sync");
            SetColumnHeaderText(CustomersGridView);
            LoadDataAsync(PurchaseHistoryGridView, "SELECT * FROM purchase_history", "Sync");
            SetColumnHeaderText(PurchaseHistoryGridView);
            if (PurchaseHistoryButton.ForeColor == Color.FromArgb(37, 150, 190))
            {
                CustomerAddButton.Visible = false;
            }

        }
        #endregion

        #endregion


        #region All Common Database Functions

        #region DatabaseInitialization

        private void InitializeDatabaseConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        #endregion

        #region LoadDataGridViews Functions
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
                if (dataGridView.Columns["product_name"] != null)
                {
                    dataGridView.Columns["or_image"].Visible = false;
                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["product_name"].HeaderText = "Name";
                    dataGridView.Columns["product_price"].HeaderText = "Price";
                    dataGridView.Columns["category"].HeaderText = "Category";
                    dataGridView.Columns["status"].HeaderText = "Status";
                    dataGridView.Columns["image"].HeaderText = "Image";
                }
                else if (dataGridView.Columns["types"] != null)
                {
                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["types"].HeaderText = "Category Types";
                }
                else
                {
                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["product"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView.Columns["product"].HeaderText = "Product";
                    dataGridView.Columns["ingredients"].HeaderText = "Ingredients";
                }
            }

            else if (dataGridView == StaffDataGrid)
            {

                if (dataGridView.Columns["staff_name"] != null)
                {
                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["staff_name"].HeaderText = "Name";
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

            else if (dataGridView == TablesDataGrid)
            {
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["table_name"].HeaderText = "Table Name";
            }

            else if (dataGridView == Ingredients_DataGrid)
            {
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["standard_quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; ;
                dataGridView.Columns["precise_quantity"].Visible = false;
                dataGridView.Columns["precise_unit"].Visible = false;
                dataGridView.Columns["ingredient_name"].HeaderText = "Ingredient";
                dataGridView.Columns["standard_quantity"].HeaderText = "Qty";
                dataGridView.Columns["standard_unit"].HeaderText = "Unit";
                dataGridView.Columns["cost_per_unit"].HeaderText = "Cost(per unit)";
                dataGridView.Columns["min_quantity"].HeaderText = "Min Qty";
            }

            else if (dataGridView == GuestInfoListDataGrid)
            {
                dataGridView.Columns["GuestID"].HeaderText = "ID";
                dataGridView.Columns["GuestName"].HeaderText = "Name";
                dataGridView.Columns["Email"].HeaderText = "Email";
                dataGridView.Columns["RoomNumber"].HeaderText = "Room";
                dataGridView.Columns["RoomType"].HeaderText = "Type";
                dataGridView.Columns["RentPerDay"].HeaderText = "Rent/DAY";
                dataGridView.Columns["NumberOfAdults"].HeaderText = "Adults";
                dataGridView.Columns["NumberOfChildren"].HeaderText = "Children";
                dataGridView.Columns["ExtraBeds"].HeaderText = "Extra Bed";
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

            else if (dataGridView == CheckDataGrid)
            {
                dataGridView.Columns["id"].HeaderText = "ID";
                dataGridView.Columns["guestid"].HeaderText = "Guest ID";
                dataGridView.Columns["checkintime"].HeaderText = "Check in time";
                dataGridView.Columns["checkouttime"].HeaderText = "Check out time";
                dataGridView.Columns["checkoutdate"].HeaderText = "Check out date";
                dataGridView.Columns["checkindate"].HeaderText = "Check out date";
                dataGridView.Columns["days"].HeaderText = "Days";
            }

            else if (dataGridView == BillCheckGridView1)
            {
                dataGridView.Columns["id"].HeaderText = "ID";
                dataGridView.Columns["guestid"].HeaderText = "Guest ID";
                dataGridView.Columns["checkintime"].HeaderText = "Check in time";
                dataGridView.Columns["checkouttime"].HeaderText = "Check out time";
                dataGridView.Columns["checkoutdate"].HeaderText = "Check out date";
                dataGridView.Columns["checkindate"].HeaderText = "Check out date";
                dataGridView.Columns["days"].HeaderText = "Days";
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
            else if (dataGridView == RoomHistoryDataGrid)
            {
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["image"].Visible = false;
                dataGridView.Columns["room_no"].HeaderText = "Room";
                dataGridView.Columns["room_type"].HeaderText = "Type";
                dataGridView.Columns["rent_day"].HeaderText = "Rent";
                dataGridView.Columns["occupied"].HeaderText = "Occupied";

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
            else if (dataGridView == AdminOrdersDataGrid)
            {
                dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                dataGridView.Columns["table_name"].HeaderText = "Table";
                dataGridView.Columns["customer"].HeaderText = "Customer";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["date"].HeaderText = "Date";
                dataGridView.Columns["type"].HeaderText = "Type";
                dataGridView.Columns["status"].HeaderText = "Status";
                dataGridView.Columns["total_amount"].HeaderText = "Total";
                dataGridView.Columns["discount"].HeaderText = "Discount";
                dataGridView.Columns["net_total_amount"].HeaderText = "Net";

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

        #region Dashboard Load Event
        private async void Dashboard_Load(object sender, EventArgs e)
        {

            int CurrentUserInitialWidth = CurrentUser_label.Width;
            CurrentUser_label.Text = $"User Name:  {Session.Username}";
            CurrentUser_label.Location = new Point(CurrentUser_label.Location.X - (CurrentUser_label.Width - CurrentUserInitialWidth), CurrentUser_label.Location.Y);
            Set_CardBox_Positions();
            AllLabelLocations(55, 85, 50);
            //SetLabelLocations(Menu_Dashboard_label, new Point(55, 112));
            //SetLabelLocations(Menu_GuestInfo_Label, new Point(55, 157));
            //SetLabelLocations(Menu_Rooms_Label, new Point(55, 202));
            //SetLabelLocations(Menu_Billing_Label, new Point(55, 247));
            //SetLabelLocations(Menu_Check_Label, new Point(55, 292));
            //SetLabelLocations(Menu_Ingredients_label, new Point(55, 337));
            //SetLabelLocations(Menu_Products_label, new Point(55, 382));
            //SetLabelLocations(Menu_Tables_label, new Point(55, 427));
            //SetLabelLocations(Menu_Staff_label, new Point(55, 472));
            //SetLabelLocations(Menu_POS_label, new Point(55, 517));
            //SetLabelLocations(Menu_Kitchen_label, new Point(55, 562));
            //SetLabelLocations(Menu_Reports_label, new Point(55, 607));
            //SetLabelLocations(Menu_Settings_label, new Point(55, 652));
            SetLabelColor(Menu_Dashboard_label, "#0077C3");

            //Menu_Dashboard_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Menu_Products_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Menu_Tables_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Menu_Staff_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Menu_POS_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Menu_Kitchen_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Menu_Reports_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Menu_Settings_label.Font = new Font(privateFonts.Families[0], 12f, FontStyle.Regular);
            //Thread.Sleep(10000);
            InitiateChart();
            await Task.Delay(10);
            ContentContainer_panel.Visible = true;
        }

        private void AllLabelLocations(int X, int start, int increment)
        {
            SetLabelLocations(Menu_Dashboard_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_GuestInfo_Label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Rooms_Label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Billing_Label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Check_Label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Ingredients_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Products_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Tables_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Staff_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_POS_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Kitchen_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Reports_label, new Point(X, start));
            start += increment;
            SetLabelLocations(Menu_Settings_label, new Point(X, start));
        }

        #endregion

        #region Card Box Positions Functions
        private void Set_CardBox_Positions()
        {
            Check_Out_CardBox.Location = new Point(Check_In_CardBox.Location.X + 240, Check_Out_CardBox.Location.Y);
            Reservation_CardBox.Location = new Point(Check_Out_CardBox.Location.X + 240, Reservation_CardBox.Location.Y);
            Total_Profit_CardBox.Location = new Point(Reservation_CardBox.Location.X + 240, Total_Profit_CardBox.Location.Y);
            Total_Tax_CardBox.Location = new Point(Check_Out_CardBox.Location.X, Check_Out_CardBox.Location.Y + 100);
            Total_Pay_CardBox.Location = new Point(Reservation_CardBox.Location.X, Reservation_CardBox.Location.Y + 100);
        }
        #endregion

        #region Chart Create Function

        private void InitiateChart()
        {

            string[] monthLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            double[] xValues = { 0, 2, 4, 6, 8, 9 };
            double[] originalYValues = { 2, 4, 2, 6, 2, 10 };

            var series = new Series();
            series.ChartType = SeriesChartType.Spline;
            series.Color = Color.FromArgb(161, 74, 222);

            for (int i = 0; i < xValues.Length; i++)
            {
                series.Points.AddXY(xValues[i], originalYValues[i]);
                series.Points[i].MarkerSize = 5;
                series.Points[i].MarkerColor = Color.White;
                series.Points[i].Tag = i;
            }
            chart1.Series.Add(series);


            //chart1.ChartAreas[0].AxisY.Title = "Y-axis Label";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            chart1.ChartAreas[0].AxisX.Maximum = xValues[xValues.Length - 1];
            chart1.ChartAreas[0].AxisY.Maximum = originalYValues.Max() + 1;
            chart1.ChartAreas[0].AxisY.Interval = 1;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            //chart1.ChartAreas[0].AxisX.Title = "Month";
            chart1.ChartAreas[0].AxisX.CustomLabels.Clear();


            var areaSeries = new Series();
            areaSeries.ChartType = SeriesChartType.SplineArea;
            areaSeries.Points.DataBindXY(xValues, originalYValues);
            areaSeries.BackGradientStyle = GradientStyle.TopBottom;
            areaSeries.Color = Color.FromArgb(73, 162, 215);
            chart1.Series.Insert(0, areaSeries);


            for (int i = 0; i < monthLabels.Length; i++)
            {
                chart1.ChartAreas[0].AxisX.CustomLabels.Add((double)(i + 1), (double)i, monthLabels[((int)i)]);
            }


            for (int i = 0; i < chart1.ChartAreas[0].AxisX.CustomLabels.Count - 1; i++)
            {
                if (i == 0)
                {
                    double intervalStart = i == 0 ? chart1.ChartAreas[0].AxisX.Minimum : chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;
                    double intervalEnd = chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;

                    StripLine stripLine = new StripLine();
                    stripLine.IntervalOffset = intervalStart;
                    stripLine.StripWidth = intervalEnd - intervalStart;
                    stripLine.BackColor = Color.FromArgb(249, 249, 249);
                    chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine);
                }

                else
                {
                    StripLine stripLine = new StripLine();
                    stripLine.IntervalOffset = chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;
                    stripLine.StripWidth = chart1.ChartAreas[0].AxisX.CustomLabels[i + 1].FromPosition - chart1.ChartAreas[0].AxisX.CustomLabels[i].FromPosition;
                    stripLine.BackColor = (i % 2 != 0) ? Color.FromArgb(249, 249, 249) : Color.White; // Set color based on index
                    chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine);
                }
            }


        }



        #endregion


        #endregion


        #region All Products Screen Functions

        #region Products Tab Button Functions

        private void ProductsTabButton_Click(object sender, EventArgs e)
        {
            if (ProductsTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {

                ProductsTabButton.BackColor = Color.FromArgb(37, 150, 190);
                ProductsTabButton.ForeColor = Color.White;
                ProductsCategoryTabButton.BackColor = Color.Transparent;
                ProductsCategoryTabButton.ForeColor = SystemColors.GrayText;
                IngrediantsTabButton.BackColor = Color.Transparent;
                IngrediantsTabButton.ForeColor = SystemColors.GrayText;
                string query = "select * from products";
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
                IngrediantsTabButton.BackColor = Color.Transparent;
                IngrediantsTabButton.ForeColor = SystemColors.GrayText;
                string query = "select * from product_category";
                LoadDataAsync(ProductsDataGrid, query, "Async");
            }
        }

        private void IngrediantsTabButton_Click(object sender, EventArgs e)
        {
            if (IngrediantsTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {

                IngrediantsTabButton.BackColor = Color.FromArgb(37, 150, 190);
                IngrediantsTabButton.ForeColor = Color.White;
                ProductsTabButton.BackColor = Color.Transparent;
                ProductsTabButton.ForeColor = SystemColors.GrayText;
                ProductsCategoryTabButton.BackColor = Color.Transparent;
                ProductsCategoryTabButton.ForeColor = SystemColors.GrayText;
                string query = "select * from product_ingredients";
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
                    string query = "select * from products";
                    LoadDataAsync(ProductsDataGrid, query, "Async");
                }

                else if (ProductsCategoryTabButton.BackColor == Color.FromArgb(37, 150, 190))
                {
                    string query = "select * from product_category";
                    LoadDataAsync(ProductsDataGrid, query, "Async");
                }
                else
                {
                    string query = "select * from product_ingredients";
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
                    if (ProductsDataGrid.Columns["product_name"] != null)
                    {
                        ProductsForm productsForm = new ProductsForm((int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        productsForm.ShowDialog();
                        LoadDataAsync(ProductsDataGrid, "select * from products", "Sync");
                    }
                    else if (ProductsDataGrid.Columns["types"] != null)
                    {
                        ProductsCategoryForm productsCatForm = new ProductsCategoryForm((int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        productsCatForm.ShowDialog();
                        LoadDataAsync(ProductsDataGrid, "select * from product_category", "Sync");
                    }
                    else
                    {
                        ProductIngredientsForm IngForm = new ProductIngredientsForm((int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        IngForm.ShowDialog();
                        LoadDataAsync(ProductsDataGrid, "select * from product_ingredients", "Sync");
                    }

                }

                else if (ProductsDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    if (ProductsDataGrid.Columns["product_name"] != null)
                    {
                        if (MessageBox.Show("Are you sure you want to delete this product?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            DeleteRowFromDatabase(Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value), "products", ProductsDataGrid, e.RowIndex);
                        }
                    }
                    else if (ProductsDataGrid.Columns["types"] != null)
                    {
                        if (MessageBox.Show("Are you sure you want to delete this product category?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            DeleteRowFromDatabase(Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value), "product_category", ProductsDataGrid, e.RowIndex);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure you want to delete this Ingredient?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            DeleteRowFromDatabase(Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value), "product_ingredients", ProductsDataGrid, e.RowIndex);
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
                ProductsForm productsForm = new ProductsForm();
                productsForm.ShowDialog();
                LoadDataAsync(ProductsDataGrid, "select * from products", "Sync");
            }

            else if (ProductsCategoryTabButton.BackColor == Color.FromArgb(37, 150, 190))
            {
                ProductsCategoryForm productCategory = new ProductsCategoryForm();
                productCategory.ShowDialog();
                string query = "select * from product_category";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
            else
            {
                ProductIngredientsForm IngForm = new ProductIngredientsForm();
                IngForm.ShowDialog();
                string query = "select * from product_ingredients";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
        }


        // Products Search TextBox
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (ProductsDataGrid.Columns["product_name"] != null)
            {
                string query = $"select * from products where product_name like '%{textBox1.Text}%' ";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
            else if (ProductsDataGrid.Columns["types"] != null)
            {
                string query = $"select * from product_category where types like '%{textBox1.Text}%'";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
            else
            {
                string query = $"select * from product_ingredients where product like '%{textBox1.Text}%'";
                LoadDataAsync(ProductsDataGrid, query, "Sync");
            }
        }


        #endregion

        #endregion


        #region All Ingredients Screen Functions


        #region Ingredients Add Button and Search TextBox Functions

        private void IngredientsAddButton_Click(object sender, EventArgs e)
        {
            IngredientsForm ingredientsForm = new IngredientsForm();
            ingredientsForm.ShowDialog();
            LoadDataAsync(Ingredients_DataGrid, "select * from ingredients", "Sync");
        }

        private void Ingredients_SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string query = $"select * from ingredients where ingredient_name like '%{Ingredients_SearchTextBox.Text}%' ";
            LoadDataAsync(Ingredients_DataGrid, query, "Sync");
        }

        #endregion

        #region Ingredient Panel VisibleChanged and Ingredient Data Grid Functions
        private void Ingredients_DataGrid_VisibleChanged(object sender, EventArgs e)
        {
            string query = "select * from ingredients";
            LoadDataAsync(Ingredients_DataGrid, query, "Async");
        }

        private void Ingredients_DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (Ingredients_DataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    IngredientsForm ingredientsForm = new IngredientsForm((int)Ingredients_DataGrid.Rows[e.RowIndex].Cells["id"].Value);
                    ingredientsForm.ShowDialog();
                    LoadDataAsync(Ingredients_DataGrid, "select * from ingredients", "Sync");
                }

                else if (Ingredients_DataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {

                    if (MessageBox.Show("Are you sure you want to delete this Ingredient?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DeleteRowFromDatabase(Convert.ToInt32(Ingredients_DataGrid.Rows[e.RowIndex].Cells["id"].Value), "ingredients", Ingredients_DataGrid, e.RowIndex);
                    }

                }

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

        #region Staff Add Button And Seach TextBox Functions
        private void AddStaffButton_Click(object sender, EventArgs e)
        {
            if (StaffTab.BackColor == Color.FromArgb(37, 150, 190))
            {
                StaffForm staffForm = new StaffForm();
                staffForm.ShowDialog();
                LoadDataAsync(StaffDataGrid, "select * from staff_details", "Sync");
            }

            else
            {
                StaffCategoryForm staffCategory = new StaffCategoryForm();
                staffCategory.ShowDialog();
                string query = "select * from staff_category";
                LoadDataAsync(StaffDataGrid, query, "Sync");
            }
        }

        private void SearchStaff_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (StaffDataGrid.Columns["staff_name"] != null)
            {
                string query = $"select * from staff_details where staff_name like '%{SearchStaff_TextBox.Text}%' ";
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
                        StaffForm staffForm = new StaffForm((int)StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        staffForm.ShowDialog();
                        LoadDataAsync(StaffDataGrid, "select * from staff_details", "Sync");
                    }
                    else
                    {
                        StaffCategoryForm staffcategory = new StaffCategoryForm((int)StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value);
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


        #region All Tables Screen Functions

        #region Tables Panel VisibleChanged and Tables Data Grid Functions
        private void TablesPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (TablesPanel.Visible == true)
            {
                LoadDataAsync(TablesDataGrid, "select * from tables", "Async");
            }
        }

        private void TablesDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (TablesDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    TablesForm tablesForm = new TablesForm((int)TablesDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                    tablesForm.ShowDialog();
                    LoadDataAsync(TablesDataGrid, "select * from tables", "Sync");
                }

                else if (TablesDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {

                    if (MessageBox.Show("Are you sure you want to delete this table name?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DeleteRowFromDatabase(Convert.ToInt32(TablesDataGrid.Rows[e.RowIndex].Cells["id"].Value), "tables", TablesDataGrid, e.RowIndex);
                    }

                }

            }


        }
        #endregion

        #region Tables Add Button and Search TextBox Functions
        private void TablesAddButton_Click(object sender, EventArgs e)
        {
            TablesForm tablesForm = new TablesForm();
            tablesForm.ShowDialog();
            LoadDataAsync(TablesDataGrid, "select * from tables", "Sync");
        }

        private void TablesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string query = $"select * from tables where table_name like '%{TablesSearchTextBox.Text}%'";
            LoadDataAsync(TablesDataGrid, query, "Sync");
        }

        #endregion

        #endregion


        #region All POS Screen Functions


        #region POS Add Category and Event Listener Functions 

        private void AddPOSCategory()
        {
            DataTable table = new DataTable();
            string qry = "select * from product_category";
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
            string qry = "select * from products";
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
                        AddPOSProducts(Convert.ToInt32(row["id"]), Convert.ToDecimal(row["product_price"]), row["category"].ToString(), row["product_name"].ToString(), ByteArraytoImage(imageByteArray));
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
        private void AddPOSProducts(int pid, decimal price, string category, string name, Image image)
        {
            var w = new ProductCard()
            {
                id = Convert.ToInt32(pid),
                product_name = name,
                product_category = category,
                product_price = price,
                product_image = image
            };
            ProductsFlowLayoutPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ProductCard)ss;
                if (SelectMode_ComboBox.SelectedItem.ToString() == "Select Products")
                {
                    if (BillID == -1)
                    {

                        foreach (DataGridViewRow item in POSProductsDataGrid.Rows)
                        {
                            if (Convert.ToInt32(item.Cells["hidden_id"].Value) == wdg.id)
                            {
                                item.Cells["quantity"].Value = int.Parse(item.Cells["quantity"].Value.ToString()) + 1;
                                item.Cells["total_amount"].Value = decimal.Parse(item.Cells["quantity"].Value.ToString()) *
                                                                   decimal.Parse(item.Cells["product_price"].Value.ToString());
                                return;
                            }

                        }

                        POSProductsDataGrid.Rows.Add(new object[] { 0, wdg.id, wdg.product_name, 1, wdg.product_price, wdg.product_price });

                    }
                    else
                    {
                        MessageBox.Show("Complete the Selected Bill Payement First", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }

                else
                {
                    POSIngredientScreen IngForm = new POSIngredientScreen(wdg.product_name, wdg.product_category, (wdg.product_price).ToString(), wdg.product_image);
                    IngForm.Show();
                }



            };

            //w.onProductDetailsClick += (ss, ee) =>
            //{
            //    var wdg = (ProductCard)ss;
            //    POSIngredientScreen IngForm = new POSIngredientScreen(wdg.product_name,wdg.product_category,(wdg.product_price).ToString(),wdg.product_image);
            //    IngForm.Show();
            //};
        }





        #endregion


        #region POSPanel Visibilty,POSProductDataGrid CellFormat & SearchBox Event Functions

        private void POSPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (POSPanel.Visible == true)
            {
                SelectMode_ComboBox.SelectedIndex = 0;
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
                    SqlCommand cmd = new SqlCommand($"select * from products where product_name='{parts[0]}'", connection);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                POSProductsDataGrid.Rows.Add(new object[] { 0, (int)reader["id"], reader["product_name"].ToString(), Convert.ToInt32(parts[1]), Convert.ToDecimal(reader["product_price"]), Convert.ToDecimal(parts[1]) * Convert.ToDecimal(reader["product_price"]) });
                                total += Convert.ToDecimal(parts[1]) * Convert.ToDecimal(reader["product_price"]);

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

            using (BillList billList = new BillList())
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


        private void KOTButton_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Kitchen_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Kitchen";
            if (KitchenPanel.Visible == false)
            {
                KitchenPanel.Visible = true;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
            }
        }


        private void DinInButton_Click(object sender, EventArgs e)
        {
            string CustomerName;
            string PhoneNumber;
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
                using (SelectTable selectTable = new SelectTable(json, total_amount))
                {
                    if (selectTable.ShowDialog() != DialogResult.OK)
                    {
                        Added = selectTable.UpdatedString;
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

        private void TakeAwayButton_Click(object sender, EventArgs e)
        {
            if (BillID != -1)
            {
                MessageBox.Show("Complete the Selected Bill Payment First", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (POSProductsDataGrid.Rows.Count > 0)
            {
                // Calculate json and total_amount from POSProductsDataGrid rows
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

                // Open the AddCustomerInfo form with json and total_amount
                AddCustomerInfo addCustomerInfoForm = new AddCustomerInfo(json, total_amount);
                DialogResult result = addCustomerInfoForm.ShowDialog();

                // Check if user confirmed to add customer details
                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Order saved with customer details.");
                }
                else if (result == DialogResult.Cancel)
                {
                    // Insert without customer details
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("INSERT INTO bill_list (items, date, type, status, total_amount, net_total_amount) VALUES (@Items, @Date, @Type, @Status, @Total, @NetTotal); SELECT SCOPE_IDENTITY();", connection);
                        command.Parameters.AddWithValue("@Items", json);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        command.Parameters.AddWithValue("@Type", "Take Away");
                        command.Parameters.AddWithValue("@Status", "Incomplete");
                        command.Parameters.AddWithValue("@Total", total_amount);
                        command.Parameters.AddWithValue("@NetTotal", total_amount);

                        // Get the generated BillID (the new record's ID)
                        int billId = Convert.ToInt32(command.ExecuteScalar());

                        if (billId > 0)
                        {
                            MessageBox.Show("Saved Successfully without customer details.");
                            POSProductsDataGrid.Rows.Clear();

                            // Log the activity
                            string actionDescription = $"Order inserted into bill list (BillID: {billId}). Total: {total_amount}";
                            LogActivity("Insert", actionDescription);
                        }
                        else
                        {
                            MessageBox.Show("There was a problem saving.");
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
                using (DeliveryForm DF = new DeliveryForm(json, total_amount))
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

        private async Task updateDashboardValues()
        {
            try
            {
                await connection.OpenAsync(); // Open connection asynchronously

                decimal sales = 0;
                decimal discount = 0;
                decimal totalCost = 0;
                decimal cancelledSales = 0;  // New variable to store cancelled sales value
                int cancelledOrdersCount = 0; // Variable to store the count of cancelled orders

                // Retrieve sales and discount information
                string querySalesDiscount = "SELECT SUM((discount/100) * total_amount) AS discount, SUM(net_total_amount) AS net_total FROM bill_list";
                using (SqlCommand command = new SqlCommand(querySalesDiscount, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sales = (decimal)reader["net_total"];
                        discount = (decimal)reader["discount"];
                    }
                }

                // Calculate cancelled sales and cancelled orders count
                string queryCancelledSales = "SELECT COUNT(*) AS cancelled_orders, SUM(net_total_amount) AS cancelled_sales FROM bill_list WHERE status = 'Cancelled'";
                using (SqlCommand cmdCancelled = new SqlCommand(queryCancelledSales, connection))
                using (SqlDataReader rdrCancelled = await cmdCancelled.ExecuteReaderAsync())
                {
                    if (await rdrCancelled.ReadAsync())
                    {
                        cancelledOrdersCount = (int)rdrCancelled["cancelled_orders"];
                        cancelledSales = (decimal)rdrCancelled["cancelled_sales"];
                    }
                }

                // Update UI with sales and discount information
                sales = sales - cancelledSales;
                //T_Sale_Amount_label.Text = sales.ToString("C"); // Display as currency format
                //T_Disc_Amount_label.Text = discount.ToString("C");

                // Update T_Pay_Amount_label with cancelled orders count and sales amount
                T_Pay_Amount_label.Text = $"({cancelledOrdersCount}) {cancelledSales.ToString("C")}";

                // Process JSON data from bill_list items
                string queryJsonData = "SELECT items FROM bill_list WHERE status = 'Paid' or status = 'Cancelled'";
                using (SqlCommand cmd = new SqlCommand(queryJsonData, connection))
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    if (await rdr.ReadAsync())
                    {
                        dt.Load(rdr);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        string json = row["items"].ToString();
                        List<string> JSONLIST = JsonConvert.DeserializeObject<List<string>>(json);

                        foreach (var item in JSONLIST)
                        {
                            try
                            {
                                SqlCommand cmd2 = new SqlCommand("ParseJsonData", connection);
                                cmd2.CommandType = CommandType.StoredProcedure;

                                // Add parameters
                                cmd2.Parameters.Add("@jsonData", SqlDbType.NVarChar, -1).Value = item;
                                cmd2.Parameters.Add("@TOTAL", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                                await cmd2.ExecuteNonQueryAsync();

                                // Retrieve the output parameter value
                                totalCost += Convert.ToDecimal(cmd2.Parameters["@TOTAL"].Value);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error processing item: " + ex.Message);
                            }
                        }
                    }
                }

                // Display total cost after processing all items
                //
                //T_Cost_Amount_label.Text = totalCost.ToString("C");                 // Display as currency format
                T_Profit_Amount_label.Text = (sales - totalCost).ToString("C");     // Display as currency format
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



        /* OLD DINEIN, DELIVERY AND TAKEWAY BUTTON
        private void DinInButton_Click(object sender, EventArgs e)
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
                using (SelectTable selectTable = new SelectTable(json, total_amount))
                {
                    if (selectTable.ShowDialog() != DialogResult.OK)
                    {
                        Added = selectTable.UpdatedString;
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






        private void TakeAwayButton_Click(object sender, EventArgs e)
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
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("insert into bill_list(items,date,type,status,total_amount,net_total_amount) values(@Items,@Date,@Type,@Status,@Total,@NetTotal)", connection);
                    command.Parameters.AddWithValue("@Items", json);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.Parameters.AddWithValue("@Type", "Take Away");
                    command.Parameters.AddWithValue("@Status", "In Complete");
                    command.Parameters.AddWithValue("@Total", total_amount);
                    command.Parameters.AddWithValue("@NetTotal", total_amount);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Saved Successfully");
                        POSProductsDataGrid.Rows.Clear();
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
                using (DeliveryForm DF = new DeliveryForm(json, total_amount))
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
        */
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
                    return;
                }
                decimal qty = Convert.ToDecimal(POSProductsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (qty > 0)
                {
                    POSProductsDataGrid.Rows[e.RowIndex].Cells["total_amount"].Value = Convert.ToDecimal(POSProductsDataGrid.Rows[e.RowIndex].Cells["product_price"].Value) * qty;
                }
                else
                {
                    POSProductsDataGrid.Rows.RemoveAt(e.RowIndex);

                }

            }

        }

        #endregion


        #region Fast Cash and Checkout Buttons Event Functions

        private void FastCashButton_Click(object sender, EventArgs e)
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
            UpdateIngredientsInventory();
            NewScreen();
        }

        private void CheckOutButton_Click(object sender, EventArgs e)
        {
            string updateStatus = "";
            using (PaymentMethodScreen pms = new PaymentMethodScreen(TotalItemsAmount, BillID))
            {
                if (pms.ShowDialog() != DialogResult.OK)
                {
                    updateStatus = pms.StatusUpdated;
                }
            }
            if (updateStatus == "Updated")
            {
                UpdateIngredientsInventory();
                NewScreen();
            }
        }

        #endregion

        #endregion


        #region All Kitchen Screen Functions

        private void KitchenPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (KitchenPanel.Visible == true)
            {
                KitchenFlowLayoutPanel.Controls.Clear();
                LoadKitchenCards();



            }

        }

        private void LoadKitchenCards()
        {

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from bill_list where status ='In Complete'", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["type"].ToString() == "Dine In")
                            {
                                var w = new KitchenCard()
                                {
                                    BillId = "Bill No: " + reader["bill_id"].ToString(),
                                    Label2 = "Table No: " + reader["table_name"].ToString(),
                                    Label3 = "Bill Timing: " + reader["date"].ToString(),
                                    Label4 = "Bill Type: " + reader["type"].ToString(),
                                    Items = JsonConvert.DeserializeObject<List<string>>(reader["items"].ToString()),
                                };

                                KitchenFlowLayoutPanel.Controls.Add(w);
                                w.onComplete += (ss, ee) =>
                                {
                                    var kitchenCard = (KitchenCard)ss;
                                    try
                                    {
                                        connection.Open();
                                        string[] parts = kitchenCard.BillId.Split(" ");
                                        SqlCommand cmd = new SqlCommand($"update bill_list set status='Complete' where bill_id={parts[2]}", connection);
                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            KitchenFlowLayoutPanel.Controls.Clear();
                                            LoadKitchenCards();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                };
                            }
                            else if (reader["type"].ToString() == "Take Away")
                            {
                                var w = new KitchenCard()
                                {
                                    BillId = "Bill No: " + reader["bill_id"].ToString(),
                                    Label2 = "Bill Timing: " + reader["date"].ToString(),
                                    Label3 = "Bill Type: " + reader["type"].ToString(),
                                    Items = JsonConvert.DeserializeObject<List<string>>(reader["items"].ToString()),
                                };

                                KitchenFlowLayoutPanel.Controls.Add(w);
                                w.onComplete += (ss, ee) =>
                                {
                                    var kitchenCard = (KitchenCard)ss;
                                    try
                                    {
                                        connection.Open();
                                        string[] parts = kitchenCard.BillId.Split(" ");
                                        SqlCommand cmd = new SqlCommand($"update bill_list set status='Complete' where bill_id={parts[2]}", connection);
                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            KitchenFlowLayoutPanel.Controls.Clear();
                                            LoadKitchenCards();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                };
                            }
                            else
                            {
                                var w = new KitchenCard()
                                {
                                    BillId = "Bill No: " + reader["bill_id"].ToString(),
                                    Label2 = "Customer: " + reader["customer"].ToString(),
                                    Label3 = "Bill Timing: " + reader["date"].ToString(),
                                    Label4 = "Bill Type: " + reader["type"].ToString(),
                                    Items = JsonConvert.DeserializeObject<List<string>>(reader["items"].ToString()),
                                };

                                KitchenFlowLayoutPanel.Controls.Add(w);
                                w.onComplete += (ss, ee) =>
                                {
                                    var kitchenCard = (KitchenCard)ss;
                                    try
                                    {
                                        connection.Open();
                                        string[] parts = kitchenCard.BillId.Split(" ");
                                        SqlCommand cmd = new SqlCommand($"update bill_list set status='Complete' where bill_id={parts[2]}", connection);
                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            KitchenFlowLayoutPanel.Controls.Clear();
                                            LoadKitchenCards();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                };
                            }

                        }
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


        #region Function for Inventory Reduction Acc. To Sales

        public void UpdateIngredientsInventory()
        {

            try
            {
                connection.Open();
                List<string> products = JsonConvert.DeserializeObject<List<string>>(JSONItems);
                foreach (var item in products)
                {
                    string[] pArray = item.Split("-");
                    string query = $"select ingredients from product_ingredients WHERE product='{pArray[0]}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string ing = reader.GetString(0);
                            reader.Close();
                            SqlCommand cmd = new SqlCommand("UpdateMyIngredients", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ingredientString", ing);
                            cmd.Parameters.AddWithValue("@productqty", Convert.ToDecimal(pArray[1]));
                            cmd.ExecuteNonQuery();

                        }

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



        #region All Room Screen Functions

        #region Rooms Tab Button Functions


        private void RoomDetailsTab_Click(object sender, EventArgs e)
        {
            if (RoomDetailsTab.BackColor != Color.FromArgb(37, 150, 190))
            {
                RoomDetailsTab.BackColor = Color.FromArgb(37, 150, 190);
                RoomDetailsTab.ForeColor = Color.White;
                RoomTypeTab.BackColor = Color.Transparent;
                RoomTypeTab.ForeColor = SystemColors.GrayText;
                RoomViewTab.BackColor = Color.Transparent;
                RoomViewTab.ForeColor = SystemColors.GrayText;
                RoomHistoryTab.BackColor = Color.Transparent;
                RoomHistoryTab.ForeColor = SystemColors.GrayText;
            }

            if (RoomDetailsPanel.Visible == false)
            {
                Rooms_FlowLayoutPanel.Visible = false;
                RoomTypePanel.Visible = false;
                RoomHIstory_Panel.Visible = false;
                RoomDetailsPanel.Visible = true;

                LoadRoomDetailsData();
            }
        }

        private void RoomViewTab_Click(object sender, EventArgs e)
        {
            if (RoomViewTab.BackColor != Color.FromArgb(37, 150, 190))
            {
                RoomViewTab.BackColor = Color.FromArgb(37, 150, 190);
                RoomViewTab.ForeColor = Color.White;
                RoomTypeTab.BackColor = Color.Transparent;
                RoomTypeTab.ForeColor = SystemColors.GrayText;
                RoomDetailsTab.BackColor = Color.Transparent;
                RoomDetailsTab.ForeColor = SystemColors.GrayText;
                RoomHistoryTab.BackColor = Color.Transparent;
                RoomHistoryTab.ForeColor = SystemColors.GrayText;
            }
            if (!Rooms_FlowLayoutPanel.Visible)
            {
                RoomHIstory_Panel.Visible = false;
                RoomTypePanel.Visible = false;
                RoomDetailsPanel.Visible = false;
                Rooms_FlowLayoutPanel.Visible = true;
                Rooms_FlowLayoutPanel.Controls.Clear();
                var roomDetails = GetRoomDetailsFromDatabase(); 

                foreach (var room in roomDetails)
                {
                    var roomCard = new RoomCard();
                    roomCard.pictureBox1.Image = room.Image; 
                    roomCard.label1.Text = room.RoomType;
                    roomCard.label3.Text = Convert.ToString(room.RentDay); 
                    roomCard.label4.Text = $"Room: {room.RoomNo}";
                    Rooms_FlowLayoutPanel.Controls.Add(roomCard);
                }
            }
        }


        private List<RoomDetails> GetRoomDetailsFromDatabase()
        {
            var roomDetailsList = new List<RoomDetails>();
            string query = "SELECT room_no, room_type, rent_day, image FROM room_details";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var roomDetails = new RoomDetails
                            {
                                RoomNo = reader["room_no"].ToString(),
                                RoomType = reader["room_type"].ToString(),
                                RentDay = Convert.ToDecimal(reader["rent_day"]),
                                Image = GetImageFromDatabaseField(reader["image"]) // Handle the image conversion
                            };

                            roomDetailsList.Add(roomDetails);
                        }
                    }
                }
            }

            return roomDetailsList;
        }

        private Image GetImageFromDatabaseField(object imageField)
        {
            if (imageField == DBNull.Value)
            {
                return null; // Handle null images
            }

            try
            {
                // Attempt to treat the field as a byte array (binary data)
                if (imageField is byte[] byteArray)
                {
                    return ConvertByteArrayToImage(byteArray);
                }

                // Attempt to treat the field as a Base64-encoded string
                if (imageField is string base64String && !string.IsNullOrEmpty(base64String))
                {
                    var byteArray2 = Convert.FromBase64String(base64String);
                    return ConvertByteArrayToImage(byteArray2);
                }
            }
            catch
            {

                return null;
            }

            throw new InvalidCastException("Unsupported image format in database.");
        }

        private Image ConvertByteArrayToImage(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }


        private void RoomHistoryTab_Click(object sender, EventArgs e)
        {
            if (RoomHistoryTab.BackColor != Color.FromArgb(37, 150, 190))
            {
                RoomHistoryTab.BackColor = Color.FromArgb(37, 150, 190);
                RoomHistoryTab.ForeColor = Color.White;
                RoomTypeTab.BackColor = Color.Transparent;
                RoomTypeTab.ForeColor = SystemColors.GrayText;
                RoomDetailsTab.BackColor = Color.Transparent;
                RoomDetailsTab.ForeColor = SystemColors.GrayText;
                RoomViewTab.BackColor = Color.Transparent;
                RoomViewTab.ForeColor = SystemColors.GrayText;
            }

            if (RoomHIstory_Panel.Visible == false)
            {
                Rooms_FlowLayoutPanel.Visible = false;
                RoomTypePanel.Visible = false;
                RoomDetailsPanel.Visible = false;
                RoomHIstory_Panel.Visible = true;
                LoadDataAsync2(RoomHistoryDataGrid, "select * from room_details", "Sync");

            }
        }

        private DataTable SampleDataTable()
        {
            DataTable dataTable = new DataTable();

            // Define columns
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Room Type", typeof(string));
            dataTable.Columns.Add("Room No.", typeof(int));
            dataTable.Columns.Add("Rent", typeof(int));
            dataTable.Columns.Add("Occupied", typeof(string));

            // Generate sample data rows
            dataTable.Rows.Add(1, "A/C Standard", 101, 125, "Y");
            dataTable.Rows.Add(2, "A/C Standard", 102, 125, "N");
            dataTable.Rows.Add(3, "A/C Standard", 103, 125, "N");
            dataTable.Rows.Add(4, "A/C Standard", 104, 125, "Y");
            dataTable.Rows.Add(5, "A/C Standard", 105, 125, "Y");


            return dataTable;
        }

        private void RoomTypeTab_Click(object sender, EventArgs e)
        {
            if (RoomTypeTab.BackColor != Color.FromArgb(37, 150, 190))
            {
                RoomTypeTab.BackColor = Color.FromArgb(37, 150, 190);
                RoomTypeTab.ForeColor = Color.White;
                RoomHistoryTab.BackColor = Color.Transparent;
                RoomHistoryTab.ForeColor = SystemColors.GrayText;
                RoomDetailsTab.BackColor = Color.Transparent;
                RoomDetailsTab.ForeColor = SystemColors.GrayText;
                RoomViewTab.BackColor = Color.Transparent;
                RoomViewTab.ForeColor = SystemColors.GrayText;
            }
            if (RoomTypePanel.Visible == false)
            {
                Rooms_FlowLayoutPanel.Visible = false;
                RoomHIstory_Panel.Visible = false;
                RoomDetailsPanel.Visible = false;
                RoomTypePanel.Visible = true;
                LoadRoomTypeData();
            }
        }
        #endregion


        #region Room Type Data Load Function 
        private async Task LoadRoomTypeData()
        {
            try
            {
                bool addEditDel = false;
                DataTable dt = new DataTable();
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                SqlConnection connection2 = new SqlConnection(connectionString);
                await connection2.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from room_types", connection2);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
                if (RoomTypeDataGrid.DataSource == null)
                {
                    addEditDel = true;
                    DataGridViewTextBoxColumn SR = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "SR#",
                        ValueType = typeof(string),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    };
                    RoomTypeDataGrid.Columns.Insert(0, SR);

                }


                RoomTypeDataGrid.DataSource = dt;
                RoomTypeDataGrid.Columns["room_type"].HeaderText = "Room Types";
                RoomTypeDataGrid.Columns["id"].Visible = false;

                if (addEditDel)
                {
                    DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                    {
                        HeaderText = "Edit",
                        Image = ResizeImage((Image)EditImage, 15, 15),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    RoomTypeDataGrid.Columns.Add(EditBtn);

                    DataGridViewImageColumn DelBtn = new DataGridViewImageColumn
                    {
                        HeaderText = "Delete",
                        Image = ResizeImage((Image)DeleteImage, 15, 15),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    RoomTypeDataGrid.Columns.Add(DelBtn);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RoomTypeDataGrid.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
                connection2.Close();
            }
            catch (Exception)
            {

                throw;
            }


        }

        #endregion


        #region  Room Type Add,Update,Reset and DataGrid Click Event Functions
        private void AddRoomType_Button_Click(object sender, EventArgs e)
        {
            if (RoomTypeTextBox.Text != "")
            {
                string query = $"insert into room_types(room_type) values('{RoomTypeTextBox.Text}')";
                DataToDataBase(query, "Added");
                LoadRoomTypeData();
                RoomTypeTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Field can't be empty");
            }
        }


        int UpdationRTID = -1;
        private void RoomTypeDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (RoomTypeDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    RoomTypeTextBox.Text = RoomTypeDataGrid.Rows[e.RowIndex].Cells["room_type"].Value.ToString();
                    UpdationRTID = (int)RoomTypeDataGrid.Rows[e.RowIndex].Cells["id"].Value;
                    UpdateRoomType_Button.Enabled = true;
                    AddRoomType_Button.Enabled = false;
                }
                else if (RoomTypeDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    if (MessageBox.Show("Are you sure you want to delete this Room Type?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string query = $"delete from room_types where id = {RoomTypeDataGrid.Rows[e.RowIndex].Cells["id"].Value}";
                        DataToDataBase(query, "Deleted");
                        LoadRoomTypeData();
                    }
                }
            }
        }

        private void UpdateRoomType_Button_Click(object sender, EventArgs e)
        {
            if (RoomTypeTextBox.Text != "")
            {
                string query = $"update room_types set room_type = '{RoomTypeTextBox.Text}' where id = {UpdationRTID}";
                DataToDataBase(query, "Updated");
                LoadRoomTypeData();
            }
            else
            {
                MessageBox.Show("Field can't be empty");
            }
        }

        private void ResetRoomType_Button_Click(object sender, EventArgs e)
        {
            AddRoomType_Button.Enabled = true;
            UpdateRoomType_Button.Enabled = false;
            RoomTypeTextBox.Text = "";
        }
        #endregion


        #region Data to Database Function for Both Room Types Screen and Room Details Screen

        private void DataToDataBase(string query, string message)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                SqlConnection connection2 = new SqlConnection(connectionString);
                connection2.Open();
                SqlCommand cmd = new SqlCommand(query, connection2);
                int rowsAff = cmd.ExecuteNonQuery();
                if (rowsAff > 0)
                {
                    MessageBox.Show($"{message} Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                connection2.Close();
            }
            catch (Exception ex)
            {
                // Show exception details in a MessageBox with an error icon
                MessageBox.Show($"An error occurred: {ex.Message}\n\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Browse Button Function and Image to ByteArray Function 

        string filepath;
        private void browseRoom_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filepath = file.FileName;
                RoomPictureBox.Image = ResizeImage(new Bitmap(filepath), 330, 231);
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Clone the image to prevent it from being locked
                using (Image clonedImage = (Image)image.Clone())
                {
                    clonedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Adjust format as needed
                }
                return ms.ToArray();
            }
        }

        #endregion

        #region Room Details Add, Update, Reset ,Load Functions and DataGrid EventListener Funcions

        private void AddRoomDetailsButton_Click(object sender, EventArgs e)
        {
            if (RoomNoTextBox.Text != "" && RentTextBox.Text != "" && RoomType_ComboBox.SelectedItem != null && RoomPictureBox.Image != null)
            {
                byte[] imageData = ImageToByteArray(RoomPictureBox.Image);
                string base64Image = Convert.ToBase64String(imageData);
                string query = $"insert into room_details(room_no,room_type,rent_day,image,occupied) values({RoomNoTextBox.Text},'{RoomType_ComboBox.SelectedItem}',{RentTextBox.Text},'{base64Image}', 'N')";
                DataToDataBase(query, "Added");
                LoadRoomDetailsData();
                RoomNoTextBox.Text = "";
                RentTextBox.Text = "";
                RoomType_ComboBox.SelectedItem = null;
                RoomPictureBox.Image = null;
            }
            else
            {
                MessageBox.Show("Field can't be empty");
            }
        }

        int UpdationRDID = -1;
        private void UpdateRoomDetailsButton_Click(object sender, EventArgs e)
        {
            if (RoomNoTextBox.Text != "" && RentTextBox.Text != "" && RoomType_ComboBox.SelectedItem != null && RoomPictureBox.Image != null)
            {
                byte[] imageData = ImageToByteArray(RoomPictureBox.Image);
                string base64Image = Convert.ToBase64String(imageData);
                string query = $"update room_details set room_no = {RoomNoTextBox.Text},room_type = '{RoomType_ComboBox.SelectedItem}',rent_day = {RentTextBox.Text},image = '{base64Image}' where id = {UpdationRDID}";
                DataToDataBase(query, "Updated");
                LoadRoomDetailsData();
            }
            else
            {
                MessageBox.Show("Field can't be empty");
            }
        }

        private void ResetRoomDetailsButton_Click(object sender, EventArgs e)
        {
            AddRoomDetailsButton.Enabled = true;
            UpdateRoomDetailsButton.Enabled = false;
            RoomNoTextBox.Text = "";
            RentTextBox.Text = "";
            RoomType_ComboBox.SelectedItem = null;
            RoomPictureBox.Image = null;
        }

        private async Task LoadRoomDetailsData()
        {
            try
            {
                bool addEditDel = false;
                DataTable dt = new DataTable();
                SqlConnection connection2 = new SqlConnection(connectionString);
                await connection2.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from room_details", connection2);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
                if (RoomDetailsDataGrid.DataSource == null)
                {
                    addEditDel = true;
                    DataGridViewTextBoxColumn SR = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "SR#",
                        ValueType = typeof(string),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    };
                    RoomDetailsDataGrid.Columns.Insert(0, SR);

                }


                RoomDetailsDataGrid.DataSource = dt;
                RoomDetailsDataGrid.Columns["room_no"].HeaderText = "Room No.";
                RoomDetailsDataGrid.Columns["room_type"].HeaderText = "Room Type";
                RoomDetailsDataGrid.Columns["rent_day"].HeaderText = "Room/Day";
                RoomDetailsDataGrid.Columns["id"].Visible = false;
                RoomDetailsDataGrid.Columns["image"].Visible = false;
                RoomDetailsDataGrid.Columns["occupied"].Visible = false;

                if (addEditDel)
                {
                    DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                    {
                        HeaderText = "Edit",
                        Image = ResizeImage((Image)EditImage, 15, 15),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    RoomDetailsDataGrid.Columns.Add(EditBtn);

                    DataGridViewImageColumn DelBtn = new DataGridViewImageColumn
                    {
                        HeaderText = "Delete",
                        Image = ResizeImage((Image)DeleteImage, 15, 15),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    RoomDetailsDataGrid.Columns.Add(DelBtn);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RoomDetailsDataGrid.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
                connection2.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RoomDetailsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (RoomDetailsDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    RoomNoTextBox.Text = RoomDetailsDataGrid.Rows[e.RowIndex].Cells["room_no"].Value.ToString();
                    RoomType_ComboBox.SelectedItem = RoomDetailsDataGrid.Rows[e.RowIndex].Cells["room_type"].Value.ToString();
                    RentTextBox.Text = RoomDetailsDataGrid.Rows[e.RowIndex].Cells["rent_day"].Value.ToString();
                    RoomPictureBox.Image = ByteArraytoImage(Convert.FromBase64String(RoomDetailsDataGrid.Rows[e.RowIndex].Cells["image"].Value.ToString()));
                    UpdationRDID = (int)RoomDetailsDataGrid.Rows[e.RowIndex].Cells["id"].Value;
                    UpdateRoomDetailsButton.Enabled = true;
                    AddRoomDetailsButton.Enabled = false;

                }
                else if (RoomDetailsDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    if (MessageBox.Show("Are you sure you want to delete this Room?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string query = $"delete from room_details where id = {RoomDetailsDataGrid.Rows[e.RowIndex].Cells["id"].Value}";
                        DataToDataBase(query, "Deleted");
                        LoadRoomDetailsData();
                    }
                }
            }
        }
        #endregion

        #region Room Type ComboBox Set Function and Room Details Panel Visibility Function 


        private async Task setRoomTypeComboBox()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                SqlConnection connection2 = new SqlConnection(connectionString);
                await connection2.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from room_types", connection2);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        RoomType_ComboBox.Items.Add(reader["room_type"].ToString());
                    }
                }
                await connection2.CloseAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RoomDetailsPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (RoomDetailsPanel.Visible == true)
            {
                LoadRoomDetailsData();
                if (RoomType_ComboBox.Items.Count == 0)
                {
                    setRoomTypeComboBox();
                }
            }
        }

        #endregion

        #endregion

        private void GuestInfoListTabButton_Click(object sender, EventArgs e)
        {
            if (GuestInfoListTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                GuestInfoListTabButton.BackColor = Color.FromArgb(37, 150, 190);
                GuestInfoListTabButton.ForeColor = Color.White;
                GuestInfoTabButton.BackColor = Color.Transparent;
                GuestInfoTabButton.ForeColor = SystemColors.GrayText;
            }
            if (GuestInfoListMiniPanel.Visible == false)
            {
                GuestInfoListMiniPanel.Visible = true;
                LoadDataAsync(GuestInfoListDataGrid, "select * from guests", "Sync");
            }
        }

        private void GuestInfoTabButton_Click(object sender, EventArgs e)
        {
            if (GuestInfoTabButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                GuestInfoTabButton.BackColor = Color.FromArgb(37, 150, 190);
                GuestInfoTabButton.ForeColor = Color.White;
                GuestInfoListTabButton.BackColor = Color.Transparent;
                GuestInfoListTabButton.ForeColor = SystemColors.GrayText;

            }
            if (GuestInfoListMiniPanel.Visible == true)
            {
                GuestInfoListMiniPanel.Visible = false;
            }
        }

        #region Guest Working
        private void InsertGuest()
        {
            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(GuestNameTB.Text) ||
                string.IsNullOrWhiteSpace(GuestAddressTB.Text) ||
                string.IsNullOrWhiteSpace(GuestContactNumberTB.Text) ||
                string.IsNullOrWhiteSpace(RoomTypeTB.Text) ||
                string.IsNullOrWhiteSpace(RentPerDayTB.Text) ||
                string.IsNullOrWhiteSpace(AdultsNumberTB.Text) ||
                string.IsNullOrWhiteSpace(ChildrenNumberTB.Text) ||
                string.IsNullOrWhiteSpace(ExtraBedsTB.Text) ||
                RoomNumberComboBox.SelectedIndex == -1)  // Ensure a room number is selected
            {
                MessageBox.Show("Please fill in all fields before submitting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Retrieve the connection string from the configuration file
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;

                // SQL query to insert guest information into the guests table
                string query = @"
                 INSERT INTO guests (GuestName, Address, Phone, RoomNumber, RoomType, RentPerDay, NumberOfAdults, NumberOfChildren, ExtraBeds)
                 VALUES (@GuestName, @GuestAddress, @GuestContact, @RoomNo, @RoomType, @RentPerDay, @Adults, @Children, @ExtraBeds)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GuestName", GuestNameTB.Text);
                        command.Parameters.AddWithValue("@GuestAddress", GuestAddressTB.Text);
                        command.Parameters.AddWithValue("@GuestContact", GuestContactNumberTB.Text);
                        command.Parameters.AddWithValue("@RoomNo", RoomNumberComboBox.SelectedItem.ToString()); // Assuming RoomNumberComboBox contains room numbers
                        command.Parameters.AddWithValue("@RoomType", RoomTypeTB.Text);
                        command.Parameters.AddWithValue("@RentPerDay", RentPerDayTB.Text);
                        command.Parameters.AddWithValue("@Adults", AdultsNumberTB.Text);
                        command.Parameters.AddWithValue("@Children", ChildrenNumberTB.Text);
                        command.Parameters.AddWithValue("@ExtraBeds", ExtraBedsTB.Text);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Guest information inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearGuestInfo();
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert guest information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting guest information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            InsertGuest();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all fields?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                ClearGuestInfo();
            }
        }

        private void ClearGuestInfo()
        {
            GuestNameTB.Text = "";
            GuestAddressTB.Text = "";
            GuestContactNumberTB.Text = "";
            RoomTypeTB.Text = "";
            RoomNumberComboBox.SelectedIndex = -1;
            RentPerDayTB.Text = "";
            AdultsNumberTB.Text = "";
            ChildrenNumberTB.Text = "";
            ExtraBedsTB.Text = "";
        }

        private void AllowOnlyNumbers()
        {
            TextBox[] numericTextBoxes = {
                 GuestContactNumberTB,
                 RentPerDayTB,
                 AdultsNumberTB,
                 ChildrenNumberTB,
                 ExtraBedsTB,
                 RoomNoTextBox,
                 RentTextBox,
                 BillAdditionalChargesTB,
                 BillCashReceivedTB,
                 BillDiscountTB,
                 BillTaxTB,
                 CheckDaysTB
            };

            foreach (TextBox textBox in numericTextBoxes)
            {
                textBox.KeyPress += NumericTextBox_KeyPress;
            }
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PreventEnterKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        // Method to add multiple TextBoxes to the event handler
        private void AddEnterKeyPreventionToTextBoxes(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.KeyPress += PreventEnterKey;  // Attach the key press event to prevent Enter key
            }
        }


        private void RoomHistoryDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RoomHistoryDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                DataGridViewRow row = RoomHistoryDataGrid.Rows[e.RowIndex];
                string roomNo = row.Cells["room_no"].Value.ToString();
                string currentStatus = row.Cells["occupied"].Value.ToString();

                string newStatus = currentStatus == "N" ? "Y" : "N";
                string confirmationMessage = currentStatus == "N"
                    ? "Do you want to change the occupied status to 'Y'?"
                    : "Do you want to change the occupied status to 'N'?";

                DialogResult result = MessageBox.Show(confirmationMessage, "Confirm Status Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "UPDATE room_details SET occupied = @newStatus WHERE room_no = @roomNo";
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@newStatus", newStatus);
                                cmd.Parameters.AddWithValue("@roomNo", roomNo);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    row.Cells["occupied"].Value = newStatus;
                                }
                                else
                                {
                                    MessageBox.Show("Failed to update status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void PopulateRoomNumberComboBox()
        {
            RoomNumberComboBox.Items.Clear();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                string query = "SELECT room_no FROM room_details";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoomNumberComboBox.Items.Add(reader["room_no"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating room numbers: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void RoomNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure a room number is selected
            if (RoomNumberComboBox.SelectedItem == null)
                return;

            string selectedRoomNumber = RoomNumberComboBox.SelectedItem.ToString();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
                string query = "SELECT room_type, rent_day FROM room_details WHERE room_no = @RoomNumber";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", selectedRoomNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                RoomTypeTB.Text = reader["room_type"].ToString();
                                RentPerDayTB.Text = reader["rent_day"].ToString();
                            }
                            else
                            {
                                RoomTypeTB.Text = string.Empty;
                                RentPerDayTB.Text = string.Empty;

                                MessageBox.Show("No data found for the selected room number.",
                                                "Data Not Found",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching room details: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #region Timesheet Panel
        private void Menu_Attendance_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Attendance_Label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;

            GuestInfoPanel.Visible = false;
            RoomsPanel.Visible = false;
            CheckPanel.Visible = false;
            BillingPanel.Visible = false;
            ContentContainer_panel.Visible = false;
            ProductPanel.Visible = false;
            IngredientsPanel.Visible = false;
            StaffPanel.Visible = false;
            POSPanel.Visible = false;
            TablesPanel.Visible = false;
            KitchenPanel.Visible = false;
            ReportsPanel.Visible = false;
            CustomerPanel.Visible = false;
            AttendancePanel.Visible = true;
            TimesheetPanel.Visible = true;
            AdminPanel.Visible = false;
            AdminOrdersDataGrid.Visible = false;
            TimesheetGridView.Visible = true;
            LoadTimesheetData();
            TimesheetSearchBox.TextChanged += TimesheetSearchBox_TextChanged;
            SetColumnHeaderText(TimesheetGridView);
            Current_ScreenName_label.Text = "Tracking";

            TimesheetButton.BackColor = Color.FromArgb(37, 150, 190);
            TimesheetButton.ForeColor = Color.White;
            ActivityLogButton.BackColor = Color.Transparent;
            ActivityLogButton.ForeColor = SystemColors.GrayText;
        }

        private void LoadTimesheetData()
        {
            string query = "SELECT name, email, clock_in_time, clock_out_time, worked_hours, monthly_total_hours FROM timesheet";
            string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;
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
                            dt.Columns.Add("SR#", typeof(int));
                            int serialNumber = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                row["SR#"] = serialNumber++;
                            }
                            dt.Columns["SR#"].SetOrdinal(0);
                            TimesheetGridView.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading timesheet data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void TimesheetSearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = TimesheetSearchBox.Text.ToLower();
            if (TimesheetGridView.DataSource is DataTable dt)
            {
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

        private void LoadActivityLogData()
        {
            string query = "SELECT time, action, description, username FROM activity_log";
            string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;

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
                            dt.Columns.Add("SR#", typeof(int));
                            int serialNumber = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                row["SR#"] = serialNumber++;
                            }
                            dt.Columns["SR#"].SetOrdinal(0);
                            dt.Columns["description"].SetOrdinal(3);
                            dt.Columns["username"].SetOrdinal(3);
                            ActivityGridView.DataSource = dt;

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

        #endregion

        #region Admin Work
        private void Menu_Admin_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Admin_Label, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;

            GuestInfoPanel.Visible = false;
            RoomsPanel.Visible = false;
            CheckPanel.Visible = false;
            BillingPanel.Visible = false;
            AttendancePanel.Visible = false;
            AdminPanel.Visible = true;
            ContentContainer_panel.Visible = false;
            ProductPanel.Visible = false;
            IngredientsPanel.Visible = false;
            StaffPanel.Visible = false;
            POSPanel.Visible = false;
            TablesPanel.Visible = false;
            KitchenPanel.Visible = false;
            ReportsPanel.Visible = false;
            CustomerPanel.Visible = false;
            AdminOrdersDataGrid.Visible = false;

            Current_ScreenName_label.Text = "Admin";
            LoadDataAsync(adminDataGrid, Unionquery, "Sync");

            AdminOrdersDataGrid.Visible = false;
            adminDataGrid.Visible = true;
            AdminUsersButton.BackColor = Color.FromArgb(37, 150, 190);
            AdminUsersButton.ForeColor = Color.White;
            AdminBillsButton.BackColor = Color.Transparent;
            AdminBillsButton.ForeColor = SystemColors.GrayText;
            button4.Visible = true;
            AdminSearchTB.Visible = true;
            CheckSaveButton.Visible = true;
            AdminSearchTB.TextChanged += AdminSearchTB_TextChanged;
        }

        private void AdminBillsButton_Click(object sender, EventArgs e)
        {
            if (AdminBillsButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                AdminBillsButton.BackColor = Color.FromArgb(37, 150, 190);
                AdminBillsButton.ForeColor = Color.White;
                AdminUsersButton.BackColor = Color.Transparent;
                AdminUsersButton.ForeColor = SystemColors.GrayText;

            }
            if (adminDataGrid.Visible == true)
            {
                adminDataGrid.Visible = false;
                button4.Visible = false;
                AdminSearchTB.Visible = false;
                CheckSaveButton.Visible = false;
                AdminOrdersDataGrid.Visible = true;
                LoadDataAsync2(AdminOrdersDataGrid, "select bill_id, table_name, customer, phone, date, type , status , total_amount , discount , net_total_amount from bill_list", "Sync");
            }
        }

        private void AdminUsersButton_Click(object sender, EventArgs e)
        {
            if (AdminUsersButton.BackColor != Color.FromArgb(37, 150, 190))
            {
                AdminUsersButton.BackColor = Color.FromArgb(37, 150, 190);
                AdminUsersButton.ForeColor = Color.White;
                AdminBillsButton.BackColor = Color.Transparent;
                AdminBillsButton.ForeColor = SystemColors.GrayText;
                AdminOrdersDataGrid.Visible = false;
            }
            if (adminDataGrid.Visible == false)
            {
                adminDataGrid.Visible = true;
                button4.Visible = true;
                AdminSearchTB.Visible = true;
                CheckSaveButton.Visible = true;
                LoadDataAsync(adminDataGrid, Unionquery, "Sync");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AccountsAddForm AAForm = new AccountsAddForm();
            AAForm.ShowDialog();
            LoadDataAsync(adminDataGrid, Unionquery, "Sync");
        }

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
                        string password = adminDataGrid.Rows[e.RowIndex].Cells["password"].Value.ToString();
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

                            foreach (string connStringName in connectionStrings)
                            {
                                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
                                {
                                    conn.Open(); // Use synchronous open
                                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);
                                        int rowsAffected = cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            adminDataGrid.Rows.RemoveAt(e.RowIndex);
                            MessageBox.Show("User deleted successfully from all databases.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (orderStatus == "Cancelled")
                    {
                        MessageBox.Show("Cancelled orders cannot be edited.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    EditOrderForm editOrderForm = new EditOrderForm(billId);
                    editOrderForm.ShowDialog();
                    LoadDataAsync2(AdminOrdersDataGrid, "select bill_id, table_name, customer, phone, date, type , status , total_amount , discount , net_total_amount from bill_list", "Sync");
                }
            }
        }

        #endregion

        #region Customer 

        private void CustomersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
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
                        CustomerUpdateForm updateForm = new CustomerUpdateForm(customerId, customerName, phoneNumber, email, address, lastPurchaseDate, credit, points, e.RowIndex);
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

                            // Use only the "myconn" connection string
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString))
                            {
                                conn.Open(); // Use synchronous open
                                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                                    int rowsAffected = cmd.ExecuteNonQuery(); // Execute the command
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

        private void LogActivity(string actionType, string description)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString))
                {
                    conn.Open();
                    string logQuery = "INSERT INTO activity_log (action, description, time, username) VALUES (@Action, @Description, @Time, @Username)";

                    using (SqlCommand logCommand = new SqlCommand(logQuery, conn))
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
            CustomerAddForm customerAddForm = new CustomerAddForm();
            customerAddForm.ShowDialog();
            LoadDataAsync(CustomersGridView, "Select * from customers", "Sync");
        }

        private void CustomerDetailsButton_Click(object sender, EventArgs e)
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

        private void PurchaseHistoryGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PurchaseHistoryGridView.Columns[e.ColumnIndex].HeaderText == "Edit" || PurchaseHistoryGridView.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                MessageBox.Show("Purchase history can neither be edited or deleted", "Purchase History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Guest Billing
        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all fields?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ClearBillFields();
            }
        }

        private void BillCashReceivedTB_TextChanged(object sender, EventArgs e)
        {
            string text = BillCashReceivedTB.Text;
            if (!string.IsNullOrEmpty(text))
            {
                decimal cashReceived = Convert.ToDecimal(text);
                decimal netAmount = Convert.ToDecimal(BillNetTotalTB.Text);
                decimal change = cashReceived - netAmount;
                BillChangeTB.Text = change.ToString();
            }
        }

        private void BillAdditionalChargesTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void BillDiscountTB_TextChanged(object sender, EventArgs e)
        {
            string text = BillDiscountTB.Text;

            if (!string.IsNullOrEmpty(text))
            {
                decimal discountPercentage;
                if (decimal.TryParse(text, out discountPercentage))
                {
                    if (discountPercentage > 100)
                    {
                        MessageBox.Show("Discount cannot be more than 100.", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BillDiscountTB.Text = "0";
                        discountPercentage = 0;
                    }
                    decimal taxPercentage = Convert.ToDecimal(BillTaxTB.Text);
                    decimal billAmount = Convert.ToDecimal(BillTotalAmountTB.Text);
                    decimal discountAmount = (discountPercentage / 100) * billAmount;
                    decimal taxAmount = (taxPercentage / 100) * billAmount;

                    decimal netAmount = (billAmount + taxAmount) - discountAmount;
                    BillNetTotalTB.Text = netAmount.ToString();
                }
                else
                {

                    BillDiscountTB.Text = "0";
                    BillNetTotalTB.Text = Convert.ToDecimal(BillTotalAmountTB.Text).ToString();
                }
            }
            else
            {

                decimal billAmount = Convert.ToDecimal(BillTotalAmountTB.Text);
                BillNetTotalTB.Text = billAmount.ToString();
            }
        }

        private void BillTaxTB_TextChanged(object sender, EventArgs e)
        {
            string text = BillTaxTB.Text;

            if (!string.IsNullOrEmpty(text))
            {
                decimal taxPercentage;
                if (decimal.TryParse(text, out taxPercentage))
                {
                    if (taxPercentage > 100)
                    {
                        MessageBox.Show("Tax cannot be more than 100.", "Invalid Tax", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BillTaxTB.Text = "0";
                        taxPercentage = 0;
                    }

                    decimal discountPercentage = Convert.ToDecimal(BillDiscountTB.Text);

                    decimal billAmount = Convert.ToDecimal(BillTotalAmountTB.Text);
                    decimal taxAmount = (taxPercentage / 100) * billAmount;
                    decimal discountAmount = (discountPercentage / 100) * billAmount;
                    decimal netAmount = (billAmount - discountAmount) + taxAmount;

                    BillNetTotalTB.Text = netAmount.ToString();
                }
                else
                {

                    BillTaxTB.Text = "0";
                    BillNetTotalTB.Text = Convert.ToDecimal(BillTotalAmountTB.Text).ToString();
                }
            }
            else
            {

                decimal billAmount = Convert.ToDecimal(BillTotalAmountTB.Text);
                BillNetTotalTB.Text = billAmount.ToString();
            }
        }

        private decimal previousAdditionalCharges = 0;

        private void BillAdditionalChargesTB_Leave(object sender, EventArgs e)
        {
            string text = BillAdditionalChargesTB.Text;
            decimal addcharges = 0;
            if (!string.IsNullOrEmpty(text) && decimal.TryParse(text, out addcharges))
            {
                if (addcharges != previousAdditionalCharges)
                {
                    decimal billAmount = Convert.ToDecimal(BillTotalAmountTB.Text);
                    decimal netAmount = billAmount - previousAdditionalCharges + addcharges;
                    BillTotalAmountTB.Text = netAmount.ToString("F2");
                    previousAdditionalCharges = addcharges;
                }
            }
            else
            {
                BillAdditionalChargesTB.Text = "0";
                decimal billAmount = Convert.ToDecimal(BillTotalAmountTB.Text);
                BillTotalAmountTB.Text = billAmount.ToString("F2");
                previousAdditionalCharges = 0;
            }
        }


        private void ClearBillFields()
        {
            BillAdditionalChargesTB.Text = "0";
            BillGuestIDTB.Text = "";
            BillGuestNameTB.Text = "";
            BillRoomNumberTB.Text = "";
            BillRentPerDayTB.Text = "0";
            BillTaxTB.Text = "0";
            BillTotalAmountTB.Text = "0";
            BillDiscountTB.Text = "0";
            BillNetTotalTB.Text = "0";
            BillCashReceivedTB.Text = "0";
            BillChangeTB.Text = "0";
            BillDaysLabel.Text = "0";
            BillCheckGridView1.DataSource = null;
            BillCheckGridView1.Rows.Clear();
            BillCheckGridView1.Columns.Clear();
            BillingPanelGridView1.Visible = true;
            connection.Close();
            LoadDataAsync2(BillingPanelGridView1, "select * from guestbill", "Sync");
            BillCheckGridView1.Visible = false;
        }

        private void BillSaveButton_Click(object sender, EventArgs e)
        {
            // 1. Check if guest name is empty
            if (string.IsNullOrEmpty(BillGuestNameTB.Text))
            {
                MessageBox.Show("Guest name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // Exit the method
            }

            if (string.IsNullOrEmpty(BillNetTotalTB.Text))
            {
                MessageBox.Show("Net total cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // Exit the method
            }

            // 2. Check if change is negative
            decimal change = Convert.ToDecimal(BillChangeTB.Text);
            if (change < 0)
            {
                MessageBox.Show("Change cannot be negative.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // Exit the method
            }

            // 3. Prepare the values for insertion into the guestbill table
            int guestId = Convert.ToInt32(BillGuestIDTB.Text);
            int days = Convert.ToInt32(BillDaysLabel.Text);
            string guestName = BillGuestNameTB.Text;
            string roomNumber = BillRoomNumberTB.Text;
            decimal rentPerDay = Convert.ToDecimal(BillRentPerDayTB.Text);
            decimal totalTax = Convert.ToDecimal(BillTaxTB.Text);
            decimal additionalCharges = Convert.ToDecimal(BillAdditionalChargesTB.Text);
            decimal totalAmount = Convert.ToDecimal(BillTotalAmountTB.Text);
            decimal discount = Convert.ToDecimal(BillDiscountTB.Text);
            decimal netTotal = Convert.ToDecimal(BillNetTotalTB.Text);
            decimal cashReceived = Convert.ToDecimal(BillCashReceivedTB.Text);
            decimal changeAmount = Convert.ToDecimal(BillChangeTB.Text);

            // 4. Construct the SQL query to insert the data into the guestbill table
            string query = @"
        INSERT INTO guestbill (guestid, guestname, roomno, rentperday, totaltax, additionalcharges, totalamount, discount, nettotal, cashreceived, change , days)
        VALUES (@guestid, @guestname, @roomno, @rentperday, @totaltax, @additionalcharges, @totalamount, @discount, @nettotal, @cashreceived, @change , @days)";

            try
            {
                // 5. Execute the query to insert data
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@guestid", guestId);
                        command.Parameters.AddWithValue("@guestname", guestName);
                        command.Parameters.AddWithValue("@roomno", roomNumber);
                        command.Parameters.AddWithValue("@rentperday", rentPerDay);
                        command.Parameters.AddWithValue("@totaltax", totalTax);
                        command.Parameters.AddWithValue("@additionalcharges", additionalCharges);
                        command.Parameters.AddWithValue("@totalamount", totalAmount);
                        command.Parameters.AddWithValue("@discount", discount);
                        command.Parameters.AddWithValue("@nettotal", netTotal);
                        command.Parameters.AddWithValue("@cashreceived", cashReceived);
                        command.Parameters.AddWithValue("@change", changeAmount);
                        command.Parameters.AddWithValue("@days", days);

                        command.ExecuteNonQuery();  // Execute the query
                    }
                }

                // 6. Inform the user that the data has been saved
                MessageBox.Show("Bill saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearBillFields();
            }
            catch (Exception ex)
            {
                // 8. Handle any errors during the save process
                MessageBox.Show($"Error saving bill: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectGuestIDBillingButton_Click(object sender, EventArgs e)
        {
            int days;
            using (var guestForm = new GuestSelectForm())
            {
                if (guestForm.ShowDialog() == DialogResult.OK)
                {
                    if (guestForm.SelectedGuestId.HasValue)
                    {
                        int guestId = guestForm.SelectedGuestId.Value;
                        BillGuestIDTB.Text = guestId.ToString();
                        PopulateGuestDetails(guestId);

                        // Query to fetch the latest entry
                        string query = $"SELECT TOP 1 * FROM guestcheck WHERE guestid = {guestId} AND checkoutdate IS NOT NULL ORDER BY checkindate DESC";

                        connection.Open();
                        using (var command = new SqlCommand(query, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Assuming the `days` column is in the result set
                                    days = reader["days"] != DBNull.Value ? Convert.ToInt32(reader["days"]) : 0;
                                    BillDaysLabel.Text = days.ToString();
                                    decimal renperday = decimal.Parse(BillRentPerDayTB.Text);
                                    decimal totalamount = renperday * days;
                                    BillTotalAmountTB.Text = totalamount.ToString("0.00");
                                    BillingPanelGridView1.Visible = false;
                                    BillCheckGridView1.Visible = true;
                                }
                                else
                                {
                                    // No entry found for the guest, show message and close form
                                    ClearBillFields();
                                    MessageBox.Show("No previous guest entries found. Please make sure both the checkin and checkout has been performed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    connection.Close();
                                    return; // Exit the method
                                }
                            }
                        }

                        LoadDataAsync2(BillCheckGridView1, query, "Sync");
                    }
                    else
                    {
                        MessageBox.Show("No Guest ID was selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Guest selection was canceled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void PopulateGuestDetails(int guestId)
        {
            string query = "SELECT GuestName, RoomNumber, RentPerDay FROM Guests WHERE GuestID = @GuestID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@GuestID", guestId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        BillGuestNameTB.Text = reader["GuestName"].ToString();
                        BillRoomNumberTB.Text = reader["RoomNumber"].ToString();
                        BillRentPerDayTB.Text = reader["RentPerDay"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No guest details found for the selected ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Guest Check
        private void CheckDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CheckDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                // Get the ID of the selected row
                selectedRecordId = Convert.ToInt32(CheckDataGrid.Rows[e.RowIndex].Cells["id"].Value);

                // Populate textboxes with data from the selected row
                CheckGuestIDTB.Text = CheckDataGrid.Rows[e.RowIndex].Cells["guestid"].Value?.ToString();
                CheckCheckinDateTB.Text = CheckDataGrid.Rows[e.RowIndex].Cells["checkindate"].Value?.ToString();
                CheckCheckintimeTB.Text = CheckDataGrid.Rows[e.RowIndex].Cells["checkintime"].Value?.ToString();
                CheckDaysTB.Text = CheckDataGrid.Rows[e.RowIndex].Cells["days"].Value?.ToString();
                CheckCheckOutDateTB.Text = CheckDataGrid.Rows[e.RowIndex].Cells["checkoutdate"].Value?.ToString();
                CheckCheckOutTimeTB.Text = CheckDataGrid.Rows[e.RowIndex].Cells["checkouttime"].Value?.ToString();

                CheckCheckOUTButton.Enabled = true;
                CheckCCheckINButton.Enabled = true;
                CheckUpdateButton.Enabled = true;
                CheckSaveButton.Enabled = false;
            }
            else if (CheckDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Get the ID of the row to delete
                    int idToDelete = Convert.ToInt32(CheckDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                    CheckClearFields();

                    // Call the delete method with the ID
                    DeleteCheckInEntry(idToDelete);
                }
            }
        }

        private void DeleteCheckInEntry(int id)
        {
            string query = "DELETE FROM guestcheck WHERE id = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Check-in entry deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataAsync(CheckDataGrid, "select * from guestcheck", "Sync");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the entry. It may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void CheckClearTB_Click(object sender, EventArgs e)
        {
            CheckClearFields();
        }

        private void CheckClearFields()
        {
            CheckGuestIDTB.Text = "";
            CheckCheckintimeTB.Text = "";
            CheckCheckinDateTB.Text = "";
            CheckDaysTB.Text = "";
            CheckCheckOutDateTB.Text = "";
            CheckCheckOutTimeTB.Text = "";
            CheckCheckOUTButton.Enabled = false;
            CheckCCheckINButton.Enabled = false;
            CheckUpdateButton.Enabled = false;
            CheckSaveButton.Enabled = true;

        }

        private void CheckCCheckINButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CheckCheckintimeTB.Text))
            {
                MessageBox.Show("Check-in has already been performed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                CheckCheckintimeTB.Text = DateTime.Now.ToString("HH:mm:ss");
                CheckCheckinDateTB.Text = DateTime.Now.ToString("yyyy-MM-dd");
                MessageBox.Show("Check-in has been successfully recorded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CheckCheckOUTButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CheckCheckinDateTB.Text) || string.IsNullOrWhiteSpace(CheckCheckintimeTB.Text))
            {
                MessageBox.Show("Check-in must be completed before checking out.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(CheckCheckOutDateTB.Text))
                {
                    MessageBox.Show("Check-out has already been performed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    CheckCheckOutDateTB.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    CheckCheckOutTimeTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    MessageBox.Show("Check-out has been successfully recorded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CheckSaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CheckGuestIDTB.Text))
            {
                MessageBox.Show("Guest ID cannot be empty. Please select a guest.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Debugging log to ensure Guest ID is being correctly read
            Console.WriteLine($"Guest ID: {CheckGuestIDTB.Text}");

            // Prepare the SQL query for insertion
            string query = @"
        INSERT INTO guestcheck (guestid, checkindate, checkintime, days, checkoutdate, checkouttime)
        VALUES (@GuestID, @CheckInDate, @CheckInTime, @Days, @CheckOutDate, @CheckOutTime)";

            // Retrieve field values, allowing nulls for empty fields
            string guestId = CheckGuestIDTB.Text.Trim();
            string checkInDate = string.IsNullOrWhiteSpace(CheckCheckinDateTB.Text) ? null : CheckCheckinDateTB.Text.Trim();
            string checkInTime = string.IsNullOrWhiteSpace(CheckCheckintimeTB.Text) ? null : CheckCheckintimeTB.Text.Trim();
            string days = string.IsNullOrWhiteSpace(CheckDaysTB.Text) ? null : CheckDaysTB.Text.Trim();
            string checkOutDate = string.IsNullOrWhiteSpace(CheckCheckOutDateTB.Text) ? null : CheckCheckOutDateTB.Text.Trim();
            string checkOutTime = string.IsNullOrWhiteSpace(CheckCheckOutTimeTB.Text) ? null : CheckCheckOutTimeTB.Text.Trim();

            // Connection string from ConfigurationManager
            string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@GuestID", guestId);
                        command.Parameters.AddWithValue("@CheckInDate", (object)checkInDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckInTime", (object)checkInTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Days", (object)days ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckOutDate", (object)checkOutDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckOutTime", (object)checkOutTime ?? DBNull.Value);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataAsync(CheckDataGrid, "select * from guestcheck", "Sync");
                            CheckClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Failed to save the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void CheckUpdateButton_Click(object sender, EventArgs e)
        {
            // Validate that an ID has been selected
            if (!selectedRecordId.HasValue)
            {
                MessageBox.Show("Please select a record to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate that Guest ID is not empty
            if (string.IsNullOrWhiteSpace(CheckGuestIDTB.Text))
            {
                MessageBox.Show("Guest ID cannot be empty. Please select a guest.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prepare the SQL query for updating the record
            string query = @"
        UPDATE guestcheck
        SET guestid = @GuestID,
            checkindate = @CheckInDate,
            checkintime = @CheckInTime,
            days = @Days,
            checkoutdate = @CheckOutDate,
            checkouttime = @CheckOutTime
        WHERE id = @ID";

            // Retrieve field values, allowing nulls for empty fields
            string guestId = CheckGuestIDTB.Text.Trim();
            string checkInDate = string.IsNullOrWhiteSpace(CheckCheckinDateTB.Text) ? null : CheckCheckinDateTB.Text.Trim();
            string checkInTime = string.IsNullOrWhiteSpace(CheckCheckintimeTB.Text) ? null : CheckCheckintimeTB.Text.Trim();
            string days = string.IsNullOrWhiteSpace(CheckDaysTB.Text) ? null : CheckDaysTB.Text.Trim();
            string checkOutDate = string.IsNullOrWhiteSpace(CheckCheckOutDateTB.Text) ? null : CheckCheckOutDateTB.Text.Trim();
            string checkOutTime = string.IsNullOrWhiteSpace(CheckCheckOutTimeTB.Text) ? null : CheckCheckOutTimeTB.Text.Trim();

            // Connection string from ConfigurationManager
            string connectionString = ConfigurationManager.ConnectionStrings["myconnHM"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@ID", selectedRecordId.Value);
                        command.Parameters.AddWithValue("@GuestID", guestId);
                        command.Parameters.AddWithValue("@CheckInDate", (object)checkInDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckInTime", (object)checkInTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Days", (object)days ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckOutDate", (object)checkOutDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckOutTime", (object)checkOutTime ?? DBNull.Value);

                        // Execute the update query
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CheckClearFields();
                            LoadDataAsync(CheckDataGrid, "select * from guestcheck", "Sync");
                        }
                        else
                        {
                            MessageBox.Show("No record found to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void selectGuestIDCheckButton_Click(object sender, EventArgs e)
        {
            using (var guestForm = new GuestSelectForm())
            {
                if (guestForm.ShowDialog() == DialogResult.OK)
                {
                    if (guestForm.SelectedGuestId.HasValue)
                    {
                        int guestId = guestForm.SelectedGuestId.Value;

                        // Set the Guest ID in the BillGuestIDTB textbox
                        CheckGuestIDTB.Text = guestId.ToString();

                        // Populate other fields based on the selected Guest ID
                        PopulateGuestDetails(guestId);
                    }
                    else
                    {
                        MessageBox.Show("No Guest ID was selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Guest selection was canceled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #region Clockout Working
        private void CheckOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Are you sure you want to clock out?",
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
        #region Logout
        private void LogOutLabel_Click(object sender, EventArgs e)
        {
            // Set the color of the logout label
            SetLabelColor(LogOutLabel, "#0077C3");
            Menu_GuestInfo_Label.BackColor = Color.Transparent;
            Menu_Rooms_Label.BackColor = Color.Transparent;
            Menu_Billing_Label.BackColor = Color.Transparent;
            Menu_Check_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;

            Menu_Attendance_Label.BackColor = Color.Transparent;

            // Show confirmation message box
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the user confirms to log out
            if (result == DialogResult.Yes)
            {
                string username = Session.Username; // Assuming you have a session variable for username

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
        #endregion
        private void ContentContainer_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (ContentContainer_panel.Visible == true)
            {
                updateDashboardValues();
            }
        }
    }
}

