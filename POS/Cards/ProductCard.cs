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
    public partial class ProductCard : UserControl
    {

        public ProductCard()
        {
            InitializeComponent();
            //ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            //ToolStripMenuItem productDetailsItem = new ToolStripMenuItem("Product Details");
            //productDetailsItem.Click += (sender, e) =>
            //{
            //    onProductDetailsClick?.Invoke(this, e);
            //};
            //contextMenuStrip.Items.Add(productDetailsItem);
            //pictureBox1.ContextMenuStrip = contextMenuStrip;
        }

        public event EventHandler onSelect = null;
        //public event EventHandler onProductDetailsClick = null;

        public int id { get; set; }
        public decimal product_price { get; set; }
        public string unit { get; set; }
        public string product_category { get; set; }
        public string product_name
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Image product_image
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //onSelect?.Invoke(this, e);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                onSelect?.Invoke(this, e);
            }
            else if (e.Button == MouseButtons.Right)
            {

                //onProductDetailsClick?.Invoke(this, e);
            }
        }
    }
}
