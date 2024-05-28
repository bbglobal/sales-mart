using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POS
{
    public partial class NumberKeypad : Form
    {
        public int x = 0;
        public int y = 0;
        public event EventHandler<int> NumberButtonPressed;
        public NumberKeypad(int x, int y)
        {
            InitializeComponent();
            this.x = x;
            this.y = y;
        }

        private void OnNumberButtonPressed(int number)
        {
            NumberButtonPressed?.Invoke(this, number);
        }



        private void NumberKeypad_Load(object sender, EventArgs e)
        {
            this.Location = new Point(x, y);
        }

        private void Number1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number2_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number3_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number0_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number4_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number5_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number6_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number7_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number8_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Number9_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = Convert.ToInt32(button.Text);
            OnNumberButtonPressed(number);
        }

        private void Backspace_Click(object sender, EventArgs e)
        {
            int number = -1;
            OnNumberButtonPressed(number);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            int number = -2;
            OnNumberButtonPressed(number);
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int number = -3;
            OnNumberButtonPressed(number);
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed(-5);
            this.Hide();
        }
    }
}
