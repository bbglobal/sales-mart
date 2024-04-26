using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class BillList : Form
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffCategoryForm));
        SqlConnection connection;
        SqlCommand command;
        DataGridView WorkingDataGridView;
        Image EditImage;
        Image DeleteImage;
        public BillList()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLabel(label2, (Image)resources.GetObject("label1.Image"), 45, 60);
            ImageEditDelLoad();
            LoadDataAsync(BillListDataGrid, "select * from bill_list", "Async");
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }


        private void InitializeLabel(Label label, Image image, int newWidth, int newHeight)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void RadioButtonSelect(Label label, Label label2, Label label3, Label label4)
        {



            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "POS.Resources.radio_checked.png";

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    label.Image = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }

            string resourceName2 = "POS.Resources.radio_unchecked.png";

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName2))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    label2.Image = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }

            string resourceName3 = "POS.Resources.radio_unchecked.png";

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName3))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    label3.Image = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }

            string resourceName4 = "POS.Resources.radio_unchecked.png";

            using (Stream imageStream = assembly.GetManifestResourceStream(resourceName4))
            {
                if (imageStream != null)
                {
                    Image image = Image.FromStream(imageStream);
                    label4.Image = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }





        }



        private void All_label_Click(object sender, EventArgs e)
        {
            RadioButtonSelect(All_label, DineIn_label, TakeAway_label, Delivery_label);
            LoadDataAsync(BillListDataGrid,"select * from bill_list","Sync");

        }

        private void DineIn_label_Click(object sender, EventArgs e)
        {
            RadioButtonSelect(DineIn_label, All_label, TakeAway_label, Delivery_label);
            LoadDataAsync(BillListDataGrid, "select * from bill_list where type='Dine In'", "Sync");
        }

        private void TakeAway_label_Click(object sender, EventArgs e)
        {
            RadioButtonSelect(TakeAway_label, DineIn_label, All_label, Delivery_label);
            LoadDataAsync(BillListDataGrid, "select * from bill_list where type='Take Away'", "Sync");
        }

        private void Delivery_label_Click(object sender, EventArgs e)
        {
            RadioButtonSelect(Delivery_label, DineIn_label, TakeAway_label, All_label);
            LoadDataAsync(BillListDataGrid, "select * from bill_list where type='Delivery'", "Sync");
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
                                HeaderText = "SR#",
                                ValueType = typeof(string),

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

        private void SetColumnHeaderText(DataGridView dataGridView)
        {
            if (dataGridView == BillListDataGrid)
            {
                dataGridView.Columns["bill_id"].Visible = false;

                dataGridView.Columns["table_name"].HeaderText = "Table";
                dataGridView.Columns["customer"].HeaderText = "Customer";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["address"].HeaderText = "Address";
                dataGridView.Columns["date"].HeaderText = "Date";
                dataGridView.Columns["type"].HeaderText = "Type";
                dataGridView.Columns["status"].HeaderText = "Status";


            }
        }

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

        private void BillList_Load(object sender, EventArgs e)
        {
           
        }
    }
}
