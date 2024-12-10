using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;
using System.Security.Policy;

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
        private Point Menu_Dashboard_label_initialPosition;
        private Point Menu_Ingredients_label_initialPosition;
        private Point Menu_Products_label_initialPosition;
        private Point Menu_Tables_label_initialPosition;
        private Point Menu_Staff_label_initialPosition;
        private Point Menu_POS_label_initialPosition;
        private Point Menu_Kitchen_label_initialPosition;
        private Point Menu_Reports_label_initialPosition;
        private Point Menu_Settings_label_initialPosition;
        private Point Menu_Logo_targetPosition;
        private Point Menu_Dashboard_label_targetPosition;
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
        private Point Menu_Admin_Label_initialPosition; // Added for Admin label
        private int targetWidth;
        private int initialWidth;
        private int ScreenContainer_panelinitialWidth;
        private int ScreenContainer_paneltargetWidth;
        private int ScreenContainer_panelinitialLeft;
        private int ScreenContainer_paneltargetLeft;
        private const int AnimationDuration = 600;
        private const int PanelAnimationDuration = 100;
        private DateTime animationStartTime;
        private SqlConnection connection;
        private SqlCommand command;
        private DataGridView WorkingDataGridView;
        Image EditImage;
        Image DeleteImage;
        private string Unionquery = @"
        SELECT username, password, email, Role, Access FROM POS.dbo.users
        UNION ALL
        SELECT username, password, email, role, Access FROM GeneralStorePOS.dbo.users
        UNION ALL
        SELECT username, password, email, role, Access FROM HotelManagementPOS.dbo.users;";

        #endregion

        public Dashboard()
        {
            InitializeComponent();
            AdjustFormSize();
            InitializeDatabaseConnection();
            ImageEditDelLoad();
            #region Calling Image Resize and Rounded Corner, Timer & Font Functions

            Menu_Admin_Label.Location = new Point(55, 645); //ADMIN LABEL
            InitializeLabel(Menu_Dashboard_label, (Image)resources.GetObject("Menu_Dashboard_label.Image"), 25, 25);
            InitializeLabel(Menu_Admin_Label, (Image)resources.GetObject("Menu_Admin_Label.Image"), 25, 25); // Initialize Admin label
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
                Menu_Dashboard_label_targetPosition = new Point(10, 112);

                Menu_Ingredients_label_initialPosition = Menu_Ingredients_label.Location;
                Menu_Ingredients_label_targetPosition = new Point(10, 172);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(10, 232);

                Menu_Tables_label_initialPosition = Menu_Tables_label.Location;
                Menu_Tables_label_targetPosition = new Point(10, 292);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(10, 352);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(10, 412);

                Menu_Kitchen_label_initialPosition = Menu_Kitchen_label.Location;
                Menu_Kitchen_label_targetPosition = new Point(10, 472);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(10, 532);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(10, 592);

                Menu_Admin_Label_initialPosition = Menu_Admin_Label.Location;
                Menu_Admin_Label_targetPosition = new Point(10, 652); //ADMIN LABEL 

                Menu_Attendance_Label_InitialPosition = Menu_Attendance_Label.Location;
                Menu_Attendance_Label_targetPosition = new Point(10, 712);

                LogOutLabel_initialPosition = LogOutLabel.Location;
                LogOutLabel_targetPosition = new Point(10, 772);

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

                Menu_Ingredients_label_initialPosition = Menu_Ingredients_label.Location;
                Menu_Ingredients_label_targetPosition = new Point(55, 172);

                Menu_Products_label_initialPosition = Menu_Products_label.Location;
                Menu_Products_label_targetPosition = new Point(55, 232);

                Menu_Tables_label_initialPosition = Menu_Tables_label.Location;
                Menu_Tables_label_targetPosition = new Point(55, 292);

                Menu_Staff_label_initialPosition = Menu_Staff_label.Location;
                Menu_Staff_label_targetPosition = new Point(55, 352);

                Menu_POS_label_initialPosition = Menu_POS_label.Location;
                Menu_POS_label_targetPosition = new Point(55, 412);

                Menu_Kitchen_label_initialPosition = Menu_Kitchen_label.Location;
                Menu_Kitchen_label_targetPosition = new Point(55, 472);

                Menu_Reports_label_initialPosition = Menu_Reports_label.Location;
                Menu_Reports_label_targetPosition = new Point(55, 532);

                Menu_Settings_label_initialPosition = Menu_Settings_label.Location;
                Menu_Settings_label_targetPosition = new Point(55, 592);

                Menu_Admin_Label_initialPosition = Menu_Admin_Label.Location;
                Menu_Admin_Label_targetPosition = new Point(55, 652); //ADMIN LABEL 

                Menu_Attendance_Label_InitialPosition = Menu_Attendance_Label.Location;
                Menu_Attendance_Label_targetPosition = new Point(55, 712);

                LogOutLabel_initialPosition = LogOutLabel.Location;
                LogOutLabel_targetPosition = new Point(55, 772);

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


        #region Label and Panel Timer Tick Event Functions 

        private void LabelTimer_Tick(object sender, EventArgs e)
        {
            double progress = (DateTime.Now - animationStartTime).TotalMilliseconds / AnimationDuration;
            if (progress >= 1.0)
            {
                LabelTimer.Stop();
                Menu_Dashboard_label.Location = Menu_Dashboard_label_targetPosition;
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

                int Menu_Logo_newX = (int)(Menu_Logo_initialPosition.X + (Menu_Logo_targetPosition.X - Menu_Logo_initialPosition.X) * progress);
                int Menu_Logo_newY = (int)Menu_Logo_targetPosition.Y;
                Logo.Location = new Point(Menu_Logo_newX, Menu_Logo_newY);

                int Menu_Admin_Label_newX = (int)(Menu_Admin_Label_initialPosition.X + (Menu_Admin_Label_targetPosition.X - Menu_Admin_Label_initialPosition.X) * progress);
                int Menu_Admin_Label_newY = (int)Menu_Admin_Label_targetPosition.Y;
                Menu_Admin_Label.Location = new Point(Menu_Admin_Label_newX, Menu_Admin_Label_newY); //ADMIN LABEL

                int Menu_Attendance_label_newX = (int)(Menu_Attendance_Label_InitialPosition.X + (Menu_Attendance_Label_targetPosition.X - Menu_Attendance_Label_InitialPosition.X) * progress);
                int Menu_Attendance_label_newY = (int)Menu_Attendance_Label_targetPosition.Y;
                Menu_Attendance_Label.Location = new Point(Menu_Attendance_label_newX, Menu_Attendance_label_newY);

                int LogOut_label_newX = (int)(LogOutLabel_initialPosition.X + (LogOutLabel_targetPosition.X - LogOutLabel_initialPosition.X) * progress);
                int LogOut_label_newY = (int)LogOutLabel_targetPosition.Y;
                LogOutLabel.Location = new Point(LogOut_label_newX, LogOut_label_newY);


            }
        }
        #endregion

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
            SetLabelColor(Menu_Dashboard_label, "#0077C3");
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Dashboard";
            Menu_Attendance_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;

            if (ContentContainer_panel.Visible == false)
            {
                ContentContainer_panel.Visible = true;
                AttendancePanel.Visible = false;
                ProductPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
                CustomerPanel.Visible = false;
                AdminPanel.Visible = false;
            }
        }

        private void Menu_Ingredients_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Ingredients_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Ingredients";
            Menu_Attendance_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;

            if (IngredientsPanel.Visible == false)
            {
                IngredientsPanel.Visible = true;
                AttendancePanel.Visible = false;
                ContentContainer_panel.Visible = false;
                ProductPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;
            }
        }

        private void Menu_Products_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Products_label, "#0077C3");
            Menu_Attendance_Label.BackColor = Color.Transparent;
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
            Current_ScreenName_label.Text = "Products/Restaurant";
            if (ProductPanel.Visible == false)
            {
                ProductPanel.Visible = true;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                AttendancePanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;
            }
        }

        private void Menu_Tables_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Tables_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Tables";
            if (TablesPanel.Visible == false)
            {
                TablesPanel.Visible = true;
                AttendancePanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                ProductPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;
            }
        }

        private int ControlsCount = 0;
        private async void Menu_POS_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_POS_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "POS";
            if (POSPanel.Visible == false)
            {
                POSPanel.Visible = true;
                AttendancePanel.Visible = false;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                CustomerPanel.Visible = false;
                //ProductPanel.Visible = false;
                AdminPanel.Visible = false;
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
            Menu_Attendance_Label.BackColor = Color.Transparent;
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
            Current_ScreenName_label.Text = "Staff";
            if (StaffPanel.Visible == false)
            {
                StaffPanel.Visible = true;
                AttendancePanel.Visible = false;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
                CustomerPanel.Visible = false;
                AdminPanel.Visible = false;
            }
        }

        private void Menu_Kitchen_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Kitchen_label, "#0077C3");
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Kitchen";
            if (KitchenPanel.Visible == false)
            {
                KitchenPanel.Visible = true;
                AttendancePanel.Visible = false;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
                CustomerPanel.Visible = false;
                AdminPanel.Visible = false;
            }
        }

        private void Menu_Reports_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Reports_label, "#0077C3");
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Reports";
            if (ReportsPanel.Visible == false)
            {
                ReportsPanel.Visible = true;
                AttendancePanel.Visible = false;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                KitchenPanel.Visible = false;
                //ProductPanel.Visible = false;
                AdminPanel.Visible = false;
                CustomerPanel.Visible = false;
            }
        }

        private void Menu_Settings_label_Click(object sender, EventArgs e)
        { //niggers
            SetLabelColor(Menu_Settings_label, "#0077C3");
            Menu_Attendance_Label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Admin_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;
            string query = "SELECT * FROM customers";
            CustomerPanel.Visible = true;
            ReportsPanel.Visible = false;
            AttendancePanel.Visible = false;
            ProductPanel.Visible = false;
            ContentContainer_panel.Visible = false;
            IngredientsPanel.Visible = false;
            POSPanel.Visible = false;
            TablesPanel.Visible = false;
            StaffPanel.Visible = false;
            KitchenPanel.Visible = false;
            //ProductPanel.Visible = false;
            AdminPanel.Visible = false;
            Current_ScreenName_label.Text = "Customers";
            LoadDataAsync(CustomersGridView, query, "Sync");
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
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
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
            { //niggers
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
            Session.RetrieveRole();

            int CurrentUserInitialWidth = CurrentUser_label.Width;
            CurrentUser_label.Text = $"User Name:  {Session.Username}";
            CurrentUser_label.Location = new Point(CurrentUser_label.Location.X - (CurrentUser_label.Width - CurrentUserInitialWidth), CurrentUser_label.Location.Y);

            // Set positions of the cards
            Set_CardBox_Positions();
            SetLabelLocations(Menu_Dashboard_label, new Point(55, 112));
            SetLabelLocations(Menu_Ingredients_label, new Point(55, 172));
            SetLabelLocations(Menu_Products_label, new Point(55, 232));
            SetLabelLocations(Menu_Tables_label, new Point(55, 292));
            SetLabelLocations(Menu_Staff_label, new Point(55, 352));
            SetLabelLocations(Menu_POS_label, new Point(55, 412));
            SetLabelLocations(Menu_Kitchen_label, new Point(55, 472));
            SetLabelLocations(Menu_Reports_label, new Point(55, 532));
            SetLabelLocations(Menu_Settings_label, new Point(55, 592));
            SetLabelLocations(Menu_Admin_Label, new Point(55, 652));
            SetLabelLocations(Menu_Attendance_Label, new Point(55, 712));
            SetLabelLocations(LogOutLabel, new Point(57, 772));

            // Set the label colors
            SetLabelColor(Menu_Dashboard_label, "#0077C3");

            // Initialize the chart
            InitiateChart();
            await Task.Delay(10);

            // Make the content container visible
            ContentContainer_panel.Visible = true;

            if (Session.Role != "admin")
            {
                Menu_Staff_label.Enabled = false;
                Menu_Admin_Label.Enabled = false;
                MessageBox.Show("Admin and staff panel are only available for admins.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                try
                {
                    // Handle Edit action
                    if (ProductsDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                    {
                        if (ProductsDataGrid.Columns["product_name"] != null)
                        {
                            // Editing a product
                            int productId = (int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value;
                            ProductsForm productsForm = new ProductsForm(productId);
                            productsForm.ShowDialog();
                            LoadDataAsync(ProductsDataGrid, "select * from products", "Sync");

                            // Log activity for editing product
                            LogActivity("Edit Product", $"Product edited: ID {productId}");

                        }
                        else if (ProductsDataGrid.Columns["types"] != null)
                        {
                            // Editing a product category
                            int categoryId = (int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value;
                            ProductsCategoryForm productsCatForm = new ProductsCategoryForm(categoryId);
                            productsCatForm.ShowDialog();
                            LoadDataAsync(ProductsDataGrid, "select * from product_category", "Sync");

                            // Log activity for editing product category
                            LogActivity("Edit Product Category", $"Product category edited: ID {categoryId}");

                        }
                        else
                        {
                            // Editing product ingredients
                            int ingredientId = (int)ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value;
                            ProductIngredientsForm IngForm = new ProductIngredientsForm(ingredientId);
                            IngForm.ShowDialog();
                            LoadDataAsync(ProductsDataGrid, "select * from product_ingredients", "Sync");

                            // Log activity for editing product ingredient
                            LogActivity("Edit Product Ingredient", $"Product ingredient edited: ID {ingredientId}");
                        }
                    }

                    // Handle Delete action
                    else if (ProductsDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                    {
                        if (ProductsDataGrid.Columns["product_name"] != null)
                        {
                            // Deleting a product
                            int productId = Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                            if (MessageBox.Show("Are you sure you want to delete this product?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                DeleteRowFromDatabase(productId, "products", ProductsDataGrid, e.RowIndex);

                                // Log activity for deleting product
                                LogActivity("Delete Product", $"Product deleted: ID {productId}");
                            }
                        }
                        else if (ProductsDataGrid.Columns["types"] != null)
                        {
                            // Deleting a product category
                            int categoryId = Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                            if (MessageBox.Show("Are you sure you want to delete this product category?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                DeleteRowFromDatabase(categoryId, "product_category", ProductsDataGrid, e.RowIndex);

                                // Log activity for deleting product category
                                LogActivity("Delete Product Category", $"Product category deleted: ID {categoryId}");
                            }
                        }
                        else
                        {
                            // Deleting product ingredients
                            int ingredientId = Convert.ToInt32(ProductsDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                            if (MessageBox.Show("Are you sure you want to delete this Ingredient?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                DeleteRowFromDatabase(ingredientId, "product_ingredients", ProductsDataGrid, e.RowIndex);

                                // Log activity for deleting product ingredient
                                LogActivity("Delete Product Ingredient", $"Product ingredient deleted: ID {ingredientId}");
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
                try
                {
                    // Handle Edit action
                    if (Ingredients_DataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                    {
                        int itemId = (int)Ingredients_DataGrid.Rows[e.RowIndex].Cells["id"].Value;
                        string ingredientName = Ingredients_DataGrid.Rows[e.RowIndex].Cells["ingredient_name"].Value.ToString();
                        string actionDescription = $"Ingredient edited: {ingredientName}";

                        IngredientsForm ingredientsForm = new IngredientsForm(itemId);
                        ingredientsForm.ShowDialog();
                        LoadDataAsync(Ingredients_DataGrid, "select * from ingredients", "Sync");

                        // Log the activity after the edit
                        LogActivity("Edit", actionDescription);
                    }

                    // Handle Delete action
                    else if (Ingredients_DataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                    {
                        int itemId = Convert.ToInt32(Ingredients_DataGrid.Rows[e.RowIndex].Cells["id"].Value);
                        string ingredientName = Ingredients_DataGrid.Rows[e.RowIndex].Cells["ingredient_name"].Value.ToString();
                        string actionDescription = "";

                        if (MessageBox.Show("Are you sure you want to delete this Ingredient?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            DeleteRowFromDatabase(itemId, "ingredients", Ingredients_DataGrid, e.RowIndex);
                            actionDescription = $"Ingredient deleted: {ingredientName}";
                        }

                        // Log the activity after the delete
                        if (!string.IsNullOrEmpty(actionDescription))
                        {
                            LogActivity("Delete", actionDescription);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                try
                {
                    // Handle Edit action
                    if (StaffDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
                    {
                        if (StaffDataGrid.Columns["staff_name"] != null)
                        {
                            int staffId = (int)StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value;
                            string staffName = StaffDataGrid.Rows[e.RowIndex].Cells["staff_name"].Value.ToString();
                            string actionDescription = $"Staff edited: {staffName}";

                            // Open the StaffForm for editing
                            StaffForm staffForm = new StaffForm(staffId);
                            staffForm.ShowDialog();

                            // Reload data to reflect changes
                            LoadDataAsync(StaffDataGrid, "select * from staff_details", "Sync");

                            // Log the activity
                            LogActivity("Edit", actionDescription);
                        }
                        else
                        {
                            // If it's a staff category, handle category edit
                            int categoryId = (int)StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value;
                            string categoryName = StaffDataGrid.Rows[e.RowIndex].Cells["category_name"].Value.ToString();
                            string actionDescription = $"Staff category edited: {categoryName}";

                            StaffCategoryForm staffCategoryForm = new StaffCategoryForm(categoryId);
                            staffCategoryForm.ShowDialog();

                            // Reload data for category edit
                            LoadDataAsync(StaffDataGrid, "select * from staff_category", "Sync");

                            // Log the activity
                            LogActivity("Edit", actionDescription);
                        }
                    }

                    // Handle Delete action
                    else if (StaffDataGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
                    {
                        if (StaffDataGrid.Columns["staff_name"] != null)
                        {
                            if (MessageBox.Show("Are you sure you want to delete this staff detail? This will also delete the staff's user account.", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                int staffId = Convert.ToInt32(StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                                string staffName = StaffDataGrid.Rows[e.RowIndex].Cells["staff_name"].Value.ToString();
                                string actionDescription = $"Staff deleted: {staffName}";

                                // Delete staff and user records
                                DeleteStaffAndUser(staffId);

                                // Log the activity
                                LogActivity("Delete", actionDescription);

                                // Remove row from DataGridView
                                StaffDataGrid.Rows.RemoveAt(e.RowIndex);
                                MessageBox.Show("Staff and associated user account deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            // Handle delete for staff category
                            if (MessageBox.Show("Are you sure you want to delete this staff category?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                int categoryId = Convert.ToInt32(StaffDataGrid.Rows[e.RowIndex].Cells["id"].Value);
                                string categoryName = StaffDataGrid.Rows[e.RowIndex].Cells["category_name"].Value.ToString();
                                string actionDescription = $"Staff category deleted: {categoryName}";

                                // Delete the staff category
                                DeleteRowFromDatabase(categoryId, "staff_category", StaffDataGrid, e.RowIndex);

                                // Log the activity
                                LogActivity("Delete", actionDescription);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteStaffAndUser(int staffId)
        {
            try
            {
                connection.Open();

                // Retrieve the email from the staff_details record to use in deleting the user
                string emailQuery = "SELECT email FROM staff_details WHERE id = @StaffId";
                string email = "";

                using (SqlCommand getEmailCommand = new SqlCommand(emailQuery, connection))
                {
                    getEmailCommand.Parameters.AddWithValue("@StaffId", staffId);
                    var result = getEmailCommand.ExecuteScalar();
                    if (result != null)
                    {
                        email = result.ToString();
                    }
                }

                // Delete the staff record from staff_details
                string deleteStaffQuery = "DELETE FROM staff_details WHERE id = @StaffId";
                using (SqlCommand deleteStaffCommand = new SqlCommand(deleteStaffQuery, connection))
                {
                    deleteStaffCommand.Parameters.AddWithValue("@StaffId", staffId);
                    deleteStaffCommand.ExecuteNonQuery();
                }

                // Delete the user record from users where the email matches
                if (!string.IsNullOrEmpty(email))
                {
                    string deleteUserQuery = "DELETE FROM users WHERE email = @Email";
                    using (SqlCommand deleteUserCommand = new SqlCommand(deleteUserQuery, connection))
                    {
                        deleteUserCommand.Parameters.AddWithValue("@Email", email);
                        deleteUserCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
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
                                setTotalAmount();
                                return;
                            }

                        }

                        POSProductsDataGrid.Rows.Add(new object[] { 0, wdg.id, wdg.product_name, 1, wdg.product_price, wdg.product_price });
                        setTotalAmount();
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
        private void Clear_Click(object sender, EventArgs e)
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
            Menu_Admin_Label.BackColor = Color.Transparent;
            Menu_Attendance_Label.BackColor = Color.Transparent;
            LogOutLabel.BackColor = Color.Transparent;

            Current_ScreenName_label.Text = "Kitchen";
            if (KitchenPanel.Visible == false)
            {
                AttendancePanel.Visible = false;
                KitchenPanel.Visible = true;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                ContentContainer_panel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
                AdminPanel.Visible = false;
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
            try
            {
                // Display confirmation message box
                DialogResult result = MessageBox.Show("Fast cash out will not add any points to the customer if they exist in the database. Confirm?",
                                                       "Confirm Fast Cash", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
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
                            // Log the successful fast cash transaction to the activity log
                            string username = Session.Username; // Get the username of the logged-in user
                            string logQuery = "INSERT INTO activity_log (time, action, username, description) VALUES (@ActivityDate, @Action, @Username, @Description)";
                            using (SqlCommand logCommand = new SqlCommand(logQuery, connection))
                            {
                                logCommand.Parameters.AddWithValue("@ActivityDate", DateTime.Now);  // Log current date and time
                                logCommand.Parameters.AddWithValue("@Action", "Fast Cash Payment");
                                logCommand.Parameters.AddWithValue("@Username", username); // Add the username here
                                logCommand.Parameters.AddWithValue("@Description", "Fast cash payment processed for Bill ID: " + BillID); // Include Bill ID in description
                                logCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Saved Successfully. Fast Cash processed without adding points.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fast cash transaction was canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // Call other methods (like updating inventory and opening a new screen)
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
                    NewScreen();
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
                T_Sale_Amount_label.Text = sales.ToString("C"); // Display as currency format
                T_Disc_Amount_label.Text = discount.ToString("C");

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
                T_Cost_Amount_label.Text = totalCost.ToString("C");                 // Display as currency format
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



        private void ContentContainer_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (ContentContainer_panel.Visible == true)
            {
                updateDashboardValues();
            }
        }

        #region Admin Panel

        private void Menu_Admin_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Admin_Label, "#0077C3");
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
            button6.Visible = true;
            // Attach search event
            AdminSearchTB.TextChanged += AdminSearchTB_TextChanged;
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
                button6.Visible = true;
                LoadDataAsync(adminDataGrid, Unionquery, "Sync");
            }
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
                button6.Visible = false;
                AdminOrdersDataGrid.Visible = true;
                LoadDataAsync2(AdminOrdersDataGrid, "select bill_id, table_name, customer, phone, date, type , status , total_amount , discount , net_total_amount from bill_list", "Sync");
            }
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            AccountsAddForm AAForm = new AccountsAddForm();
            AAForm.ShowDialog();
            LoadDataAsync(adminDataGrid, Unionquery, "Sync");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        private void Menu_Attendance_Label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Attendance_Label, "#0077C3");
            Menu_Admin_Label.BackColor = Color.Transparent;
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

            AttendancePanel.Visible = true;
            TimesheetPanel.Visible = true;
            AdminPanel.Visible = false;
            CustomerPanel.Visible = false;
            ContentContainer_panel.Visible = false;
            ProductPanel.Visible = false;
            IngredientsPanel.Visible = false;
            StaffPanel.Visible = false;
            POSPanel.Visible = false;
            TablesPanel.Visible = false;
            KitchenPanel.Visible = false;
            ReportsPanel.Visible = false;
            LoadTimesheetData();
            TimesheetSearchBox.TextChanged += TimesheetSearchBox_TextChanged;
            SetColumnHeaderText(TimesheetGridView);
            Current_ScreenName_label.Text = "Tracking";
        }

        private void LoadTimesheetData()
        {
            string query = "SELECT name, email, clock_in_time, clock_out_time, worked_hours, monthly_total_hours FROM timesheet";
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
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
                            TimesheetGridView.Columns["SR#"].Width = 55;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur
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

        private void ScreenContainer_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TimesheetButton_Click_1(object sender, EventArgs e)
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
        #endregion

        #region Activity Log

        private void LoadActivityLogData()
        {
            string query = "SELECT time, action, description, username FROM activity_log";
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;

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
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                string username = Session.Username;
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

        #region LogOut

        private void LogOutLabel_Click(object sender, EventArgs e)
        {
            // Set the color of the logout label
            SetLabelColor(LogOutLabel, "#0077C3");

            // Reset the background color of other menu labels
            Menu_Admin_Label.BackColor = Color.Transparent;
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
                // Insert logout action into the activity log
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
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

        #region Customer 
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
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString))
                            {
                                conn.Open(); // Use synchronous open
                                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
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

        // Method to log activity
        private void LogActivity(string actionType, string description)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString))
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

        private void PurchaseHistoryButton_ForeColorChanged(object sender, EventArgs e)
        {

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
        #endregion

        private void PurchaseHistoryGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PurchaseHistoryGridView.Columns[e.ColumnIndex].HeaderText == "Edit" || PurchaseHistoryGridView.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                MessageBox.Show("Purchase history can neither be edited or deleted", "Purchase History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Class representing an item from the deserialized JSON
        public class Item
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }

        private void AdminOrdersDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid cell is clicked
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Get the clicked column header text
            string headerText = AdminOrdersDataGrid.Columns[e.ColumnIndex].HeaderText;

            // Check if the clicked column is "Edit"
            if (headerText == "Edit")
            {
                // Fetch the bill_id and status from the clicked row
                string billId = AdminOrdersDataGrid.Rows[e.RowIndex].Cells["bill_id"].Value?.ToString();
                string orderStatus = AdminOrdersDataGrid.Rows[e.RowIndex].Cells["status"].Value?.ToString();

                if (!string.IsNullOrEmpty(billId))
                {
                    // Check if the order status is "Cancelled"
                    if (orderStatus == "Cancelled")
                    {
                        MessageBox.Show("Cancelled orders cannot be edited.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Stop further processing if the order is cancelled
                    }

                    // Instantiate the EditOrderForm and pass the bill_id
                    EditOrderForm editOrderForm = new EditOrderForm(billId);
                    editOrderForm.ShowDialog();

                    // Optionally, refresh the data in AdminOrdersDataGrid after edit
                    LoadDataAsync2(AdminOrdersDataGrid, "select bill_id, table_name, customer, phone, date, type , status , total_amount , discount , net_total_amount from bill_list", "Sync");
                }
            }
        }

        private void Sidebar_panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


