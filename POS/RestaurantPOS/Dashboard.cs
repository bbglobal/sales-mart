using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

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
        private Point Menu_Ingredients_label_initialPosition;
        private Point Menu_Products_label_initialPosition;
        private Point Menu_Tables_label_initialPosition;
        private Point Menu_Staff_label_initialPosition;
        private Point Menu_POS_label_initialPosition;
        private Point Menu_Kitchen_label_initialPosition;
        private Point Menu_Reports_label_initialPosition;
        private Point Menu_Settings_label_initialPosition;
        private Point Menu_Dashboard_label_targetPosition;
        private Point Menu_Ingredients_label_targetPosition;
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
        private SqlConnection connection;
        private SqlCommand command;
        private DataGridView WorkingDataGridView;
        Image EditImage;
        Image DeleteImage;

        #endregion
        public Dashboard()
        {
            InitializeComponent();
            AdjustFormSize();
            InitializeDatabaseConnection();
            ImageEditDelLoad();

            #region Calling Image Resize and Rounded Corner,Timer & Font Functions 
            InitializeLabel(Menu_Dashboard_label, (Image)resources.GetObject("Menu_Dashboard_label.Image"), 25, 25);
            InitializeLabel(Menu_Ingredients_label, (Image)resources.GetObject("Menu_Ingredients_label.Image"), 25, 25);
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
            RoundCorners(Menu_Ingredients_label, 20);
            RoundCorners(Menu_Products_label, 20);
            RoundCorners(Menu_Tables_label, 20);
            RoundCorners(Menu_Staff_label, 20);
            RoundCorners(Menu_POS_label, 20);
            RoundCorners(Menu_Kitchen_label, 20);
            RoundCorners(Menu_Reports_label, 20);
            RoundCorners(Menu_Settings_label, 20);
            InitializeTimer();
            //LoadCustomFont("POS.MyriadProSemibold.ttf");
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
                Menu_Ingredients_label.Location = Menu_Ingredients_label_targetPosition;
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
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Dashboard";
            if (ContentContainer_panel.Visible == false)
            {
                ContentContainer_panel.Visible = true;
                ProductPanel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
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
            Current_ScreenName_label.Text = "Ingredients";
            if (IngredientsPanel.Visible == false)
            {
                IngredientsPanel.Visible = true;
                ContentContainer_panel.Visible = false;
                ProductPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Products_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Products_label, "#0077C3");
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Products/Restaurant";
            if (ProductPanel.Visible == false)
            {
                ProductPanel.Visible = true;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
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
            Current_ScreenName_label.Text = "Tables";
            if (TablesPanel.Visible == false)
            {
                TablesPanel.Visible = true;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                POSPanel.Visible = false;
                ProductPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
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
            Menu_Settings_label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "POS";
            if (POSPanel.Visible == false)
            {
                POSPanel.Visible = true;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                StaffPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
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
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Staff";
            if (StaffPanel.Visible == false)
            {
                StaffPanel.Visible = true;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                KitchenPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Kitchen_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Kitchen_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
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
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                ReportsPanel.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Reports_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Reports_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
            Menu_Settings_label.BackColor = Color.Transparent;
            Current_ScreenName_label.Text = "Reports";
            if (ReportsPanel.Visible == false)
            {
                ReportsPanel.Visible = true;
                ProductPanel.Visible = false;
                ContentContainer_panel.Visible = false;
                IngredientsPanel.Visible = false;
                POSPanel.Visible = false;
                TablesPanel.Visible = false;
                StaffPanel.Visible = false;
                KitchenPanel.Visible = false;
                //ProductPanel.Visible = false;
            }
        }

        private void Menu_Settings_label_Click(object sender, EventArgs e)
        {
            SetLabelColor(Menu_Settings_label, "#0077C3");
            Menu_Products_label.BackColor = Color.Transparent;
            Menu_Ingredients_label.BackColor = Color.Transparent;
            Menu_Tables_label.BackColor = Color.Transparent;
            Menu_Dashboard_label.BackColor = Color.Transparent;
            Menu_POS_label.BackColor = Color.Transparent;
            Menu_Kitchen_label.BackColor = Color.Transparent;
            Menu_Reports_label.BackColor = Color.Transparent;
            Menu_Staff_label.BackColor = Color.Transparent;
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
            SetLabelLocations(Menu_Dashboard_label, new Point(55, 112));
            SetLabelLocations(Menu_Ingredients_label, new Point(55, 172));
            SetLabelLocations(Menu_Products_label, new Point(55, 232));
            SetLabelLocations(Menu_Tables_label, new Point(55, 292));
            SetLabelLocations(Menu_Staff_label, new Point(55, 352));
            SetLabelLocations(Menu_POS_label, new Point(55, 412));
            SetLabelLocations(Menu_Kitchen_label, new Point(55, 472));
            SetLabelLocations(Menu_Reports_label, new Point(55, 532));
            SetLabelLocations(Menu_Settings_label, new Point(55, 592));
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

        
        private async Task updateDashboardValues()
        {
            try
            {
                await connection.OpenAsync(); // Open connection asynchronously

                decimal sales = 0;
                decimal discount = 0;
                decimal totalCost = 0;

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

                // Update UI with sales and discount information
                T_Sale_Amount_label.Text = sales.ToString("C"); // Display as currency format
                T_Disc_Amount_label.Text = discount.ToString("C");

                // Process JSON data from bill_list items
                string queryJsonData = "SELECT items FROM bill_list WHERE status = 'Paid'";
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
                T_Cost_Amount_label.Text = totalCost.ToString("C"); // Display as currency format
                T_Profit_Amount_label.Text = (sales - totalCost).ToString("C"); // Display as currency format
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
    }
}

