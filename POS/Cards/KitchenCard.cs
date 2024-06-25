using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class KitchenCard : UserControl
    {

        public KitchenCard()
        {
            InitializeComponent();

        }

        public event EventHandler onPrint = null;
        public event EventHandler onComplete = null;
        public string BillId
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public string Label2
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }

        public string Label3
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        public string Label4
        {
            get { return label4.Text; }
            set { label4.Text = value; }
        }



        private List<string> _items;

        public List<string> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                SetItems();
            }
        }


        private void SetItems()
        {
            if (Items != null && Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    string[] parts = Items[i].Split('-');

                    Label label = new Label();
                    label.Font = new Font("Segoe UI Semibold", 9.25F, FontStyle.Bold);
                    label.ForeColor = Color.FromArgb(65, 66, 68);
                    label.Location = new Point(3, 5);
                    label.Margin = new Padding(3, 5, 3, 0);
                    label.Size = new Size(183, 20);
                    label.Text = $"{i + 1}. " + parts[0] + " -- " + parts[1];
                    flowLayoutPanel2.Controls.Add(label);
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            onPrint?.Invoke(this, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            onComplete?.Invoke(this, e);
        }
    }
}