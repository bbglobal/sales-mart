    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace POS
{
    public partial class RoomCard : UserControl
    {

        public RoomCard()
        {
            InitializeComponent();
        }

        public event EventHandler onSelect = null;

        public class RoomDetails
        {
            public string RoomNo { get; set; }
            public string RoomType { get; set; }
            public decimal RentDay { get; set; }
            public Image Image { get; set; }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
