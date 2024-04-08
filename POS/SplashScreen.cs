


namespace POS
{
    public partial class SplashScreen : Form
    {
        private Thread progressThread;
        public SplashScreen()
        {
            InitializeComponent();
            progressThread = new Thread(ProgressSimulation);
            progressThread.Start();


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
