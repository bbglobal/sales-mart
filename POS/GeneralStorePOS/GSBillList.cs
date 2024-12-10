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
    public partial class GSBillList : Form
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(StaffCategoryForm));
        SqlConnection connection;
        SqlCommand command;
        DataGridView WorkingDataGridView;
        Image EditImage;
        Image PrintImage;
        int billID;
        string jsonData;
        string status = "";
        string billStatus = "";
        public GSBillList()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLabel(label2, (Image)resources.GetObject("label1.Image"), 45, 60);
            ImageEditDelLoad();
            LoadDataAsync(BillListDataGrid, "select * from bill_list where status='In Complete' or status = 'Incomplete' order by bill_id DESC", "Async"); //tabdeli
            StatusComboBox.SelectedIndex = 0;
            StatusComboBox.SelectedIndexChanged += StatusComboBox_SelectedIndexChanged;
        }

        public int BillID
        {
            get { return billID; }
        }

        public string JSONData
        {
            get { return jsonData; }
        }
        public string Status
        {
            get { return status; }
        }

        public string BillStatus
        {
            get { return billStatus; }
        }


        private void InitializeDatabaseConnection()
        {
            if (Session.BranchCode == "PK728")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnGS"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            else if (Session.BranchCode == "BR001")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myconnGSBR001"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
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


        #region Radio Button Select Function

        private string RadioButtonSelected = "";

        private void RadioButtonSelect(Label label, Label label3, Label label4)
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

        #endregion


        #region Labels Click Event Handler Functions

        private void All_label_Click(object sender, EventArgs e)
        {
            RadioButtonSelect(All_label, TakeAway_label, Delivery_label);
            RadioButtonSelected = "";
            string SelectedItem = StatusComboBox.SelectedItem.ToString();
            //LoadDataAsync(BillListDataGrid, $"select * from bill_list where status='{SelectedItem}' order by bill_id DESC", "Sync");
            LoadDataAsync(BillListDataGrid, $"select * from bill_list where type IN ('Take Away', 'Delivery')  and status= '{SelectedItem}' order by bill_id DESC", "Sync");


        }

        private void TakeAway_label_Click(object sender, EventArgs e)
        {
            RadioButtonSelect(TakeAway_label, All_label, Delivery_label);
            RadioButtonSelected = "Take Away";
            string SelectedItem = StatusComboBox.SelectedItem.ToString();
            LoadDataAsync(BillListDataGrid, $"select * from bill_list where type='Take Away' and status='{SelectedItem}' order by bill_id DESC", "Sync");
        }

        private void Delivery_label_Click(object sender, EventArgs e)
        {
            RadioButtonSelect(Delivery_label, TakeAway_label, All_label);
            RadioButtonSelected = "Delivery";
            string SelectedItem = StatusComboBox.SelectedItem.ToString();
            LoadDataAsync(BillListDataGrid, $"select * from bill_list where type='Delivery' and status='{SelectedItem}' order by bill_id DESC", "Sync");
        }

        #endregion


        #region All Database Related Functions
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
                            //DataGridViewTextBoxColumn SR = new DataGridViewTextBoxColumn
                            //{
                            //    HeaderText = "SR#",
                            //    ValueType = typeof(string),
                            //    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,

                            //};
                            //WorkingDataGridView.Columns.Insert(0, SR);

                            WorkingDataGridView.DataSource = dataTable;
                            SetColumnHeaderText(WorkingDataGridView);
                            DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                            {
                                HeaderText = "Edit",
                                Image = ResizeImage((Image)EditImage, 15, 15),
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            };
                            WorkingDataGridView.Columns.Add(EditBtn);

                            DataGridViewImageColumn PrintBtn = new DataGridViewImageColumn
                            {
                                HeaderText = "Print",
                                Image = ResizeImage((Image)PrintImage, 15, 15),
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            };
                            WorkingDataGridView.Columns.Add(PrintBtn);
                            //for (int i = 0; i < dataTable.Rows.Count; i++)
                            //{
                            //    WorkingDataGridView.Rows[i].Cells[0].Value = (i + 1).ToString();
                            //}
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
                        //DataGridViewTextBoxColumn SR = new DataGridViewTextBoxColumn
                        //{
                        //    HeaderText = "SR#",
                        //    ValueType = typeof(string),
                        //    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                        //};
                        //WorkingDataGridView.Columns.Insert(0, SR);
                        WorkingDataGridView.DataSource = dataTable;
                        SetColumnHeaderText(WorkingDataGridView);
                        DataGridViewImageColumn EditBtn = new DataGridViewImageColumn
                        {
                            HeaderText = "Edit",
                            Image = ResizeImage((Image)EditImage, 15, 15),
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        };
                        WorkingDataGridView.Columns.Add(EditBtn);

                        DataGridViewImageColumn PrintBtn = new DataGridViewImageColumn
                        {
                            HeaderText = "Print",
                            Image = ResizeImage((Image)PrintImage, 15, 15),
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        };
                        WorkingDataGridView.Columns.Add(PrintBtn);
                        //for (int i = 0; i < dataTable.Rows.Count; i++)
                        //{
                        //    WorkingDataGridView.Rows[i].Cells[0].Value = (i + 1).ToString();
                        //}
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


        #region Set Column Headers Function

        private void SetColumnHeaderText(DataGridView dataGridView)
        {
            if (dataGridView == BillListDataGrid)
            {

                //dataGridView.Columns["bill_id"].Visible = false;
                dataGridView.Columns["items"].Visible = false;
                dataGridView.Columns["total_amount"].Visible = false;
                dataGridView.Columns["discount"].Visible = false;
                dataGridView.Columns["net_total_amount"].Visible = false;
                dataGridView.Columns["cash_received"].Visible = false;
                dataGridView.Columns["change"].Visible = false;

                dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                dataGridView.Columns["customer"].HeaderText = "Customer";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["address"].HeaderText = "Address";
                dataGridView.Columns["date"].HeaderText = "Date";
                dataGridView.Columns["type"].HeaderText = "Type";
                dataGridView.Columns["status"].HeaderText = "Status";


                //if (RadioButtonSelected == "")
                //{
                //    //dataGridView.Columns["bill_id"].Visible = false;
                //    dataGridView.Columns["items"].Visible = false;
                //    dataGridView.Columns["total_amount"].Visible = false;
                //    dataGridView.Columns["discount"].Visible = false;
                //    dataGridView.Columns["net_total_amount"].Visible = false;
                //    dataGridView.Columns["cash_received"].Visible = false;
                //    dataGridView.Columns["change"].Visible = false;

                //    dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                //    dataGridView.Columns["table_name"].HeaderText = "Table";
                //    dataGridView.Columns["customer"].HeaderText = "Customer";
                //    dataGridView.Columns["phone"].HeaderText = "Phone";
                //    dataGridView.Columns["address"].HeaderText = "Address";
                //    dataGridView.Columns["date"].HeaderText = "Date";
                //    dataGridView.Columns["type"].HeaderText = "Type";
                //    dataGridView.Columns["status"].HeaderText = "Status";
                //}
                //else if (RadioButtonSelected == "Dine In")
                //{
                //    //dataGridView.Columns["bill_id"].Visible = false;
                //    dataGridView.Columns["items"].Visible = false;
                //    dataGridView.Columns["total_amount"].Visible = false;
                //    dataGridView.Columns["discount"].Visible = false;
                //    dataGridView.Columns["net_total_amount"].Visible = false;
                //    dataGridView.Columns["cash_received"].Visible = false;
                //    dataGridView.Columns["change"].Visible = false;
                //    dataGridView.Columns["customer"].Visible = false;
                //    dataGridView.Columns["phone"].Visible = false;
                //    dataGridView.Columns["address"].Visible = false;

                //    dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                //    dataGridView.Columns["table_name"].HeaderText = "Table";
                //    //dataGridView.Columns["customer"].HeaderText = "Customer";
                //    //dataGridView.Columns["phone"].HeaderText = "Phone";
                //    //dataGridView.Columns["address"].HeaderText = "Address";
                //    dataGridView.Columns["date"].HeaderText = "Date";
                //    dataGridView.Columns["type"].HeaderText = "Type";
                //    dataGridView.Columns["status"].HeaderText = "Status";
                //}
                //else if (RadioButtonSelected == "Take Away")
                //{
                //    //dataGridView.Columns["bill_id"].Visible = false;
                //    dataGridView.Columns["items"].Visible = false;
                //    dataGridView.Columns["total_amount"].Visible = false;
                //    dataGridView.Columns["discount"].Visible = false;
                //    dataGridView.Columns["net_total_amount"].Visible = false;
                //    dataGridView.Columns["cash_received"].Visible = false;
                //    dataGridView.Columns["change"].Visible = false;
                //    dataGridView.Columns["table_name"].Visible = false;
                //    dataGridView.Columns["customer"].Visible = false;
                //    dataGridView.Columns["phone"].Visible = false;
                //    dataGridView.Columns["address"].Visible = false;

                //    dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                //    //dataGridView.Columns["table_name"].HeaderText = "Table";
                //    //dataGridView.Columns["customer"].HeaderText = "Customer";
                //    //dataGridView.Columns["phone"].HeaderText = "Phone";
                //    //dataGridView.Columns["address"].HeaderText = "Address";
                //    dataGridView.Columns["date"].HeaderText = "Date";
                //    dataGridView.Columns["type"].HeaderText = "Type";
                //    dataGridView.Columns["status"].HeaderText = "Status";
                //}
                //else if (RadioButtonSelected == "Delivery")
                //{
                //    //dataGridView.Columns["bill_id"].Visible = false;
                //    dataGridView.Columns["items"].Visible = false;
                //    dataGridView.Columns["total_amount"].Visible = false;
                //    dataGridView.Columns["discount"].Visible = false;
                //    dataGridView.Columns["net_total_amount"].Visible = false;
                //    dataGridView.Columns["cash_received"].Visible = false;
                //    dataGridView.Columns["change"].Visible = false;
                //    dataGridView.Columns["table_name"].Visible = false;

                //    dataGridView.Columns["bill_id"].HeaderText = "Bill ID";
                //    //dataGridView.Columns["table_name"].HeaderText = "Table";
                //    dataGridView.Columns["customer"].HeaderText = "Customer";
                //    dataGridView.Columns["phone"].HeaderText = "Phone";
                //    dataGridView.Columns["address"].HeaderText = "Address";
                //    dataGridView.Columns["date"].HeaderText = "Date";
                //    dataGridView.Columns["type"].HeaderText = "Type";
                //    dataGridView.Columns["status"].HeaderText = "Status";
                //}

            }
        }

        #endregion

        #endregion



        private void ImageEditDelLoad()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "POS.Resources.edit.png";
            string resourceName1 = "POS.Resources.printIcon.png";

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
                    PrintImage = image;
                }
                else
                {
                    MessageBox.Show("Error: Could not load Edit image resource.");
                }
            }
        }

        private void BillListDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BillListDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                billID = Convert.ToInt32(BillListDataGrid.Rows[e.RowIndex].Cells["bill_id"].Value);
                jsonData = BillListDataGrid.Rows[e.RowIndex].Cells["items"].Value.ToString();
                billStatus = BillListDataGrid.Rows[e.RowIndex].Cells["status"].Value.ToString();
                status = "Edit";
                this.Close();
            }
            else if (BillListDataGrid.Columns[e.ColumnIndex].HeaderText == "Print")
            {

            }

        }

        private void StatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonSelected == "")
            {
                string SelectedItem = StatusComboBox.SelectedItem.ToString();
                LoadDataAsync(BillListDataGrid, $"select * from bill_list where status='{SelectedItem}' order by bill_id DESC", "Sync");
            }
            else
            {
                string SelectedItem = StatusComboBox.SelectedItem.ToString();
                LoadDataAsync(BillListDataGrid, $"select * from bill_list where type='{RadioButtonSelected}' and status='{SelectedItem}' order by bill_id DESC", "Sync");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StatusComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
