


namespace POS
{
    public partial class SplashScreen : Form
    {
        private Thread progressThread;
        public SplashScreen()
        {
            InitializeComponent();
            AdjustFormSize();
            progressThread = new Thread(ProgressSimulation);
            progressThread.Start();


        }

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

        private void ProgressSimulation()
        {
            Thread.Sleep(1000);
            for (int i = 0; i <= 100; i++)
            {
                if (label1.IsHandleCreated)
                {
                    label1.Invoke((MethodInvoker)delegate { label1.Text = i.ToString() + "%"; });
                }
                Thread.Sleep(40);

                if (i == 100)
                {
                    OpenOtherForm();
                    break;
                }
            }
        }

        private void OpenOtherForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(OpenOtherForm));
            }
            else
            {
                LoginForm otherForm = new LoginForm();
                otherForm.Show();

                this.Hide();

            }

            if (progressThread != null && progressThread.IsAlive)
            {

                progressThread = null;
            }
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;

            
        }
    }
}
