using Microsoft.Reporting.WinForms;
using POS.DataSet;
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
    public partial class ReportsDSR : Form
    {
        private DateTime fromDate ;
        private DateTime toDate ;
        string category;
         public ReportsDSR()
        {
            InitializeComponent();
            //this.fromDate = fromDate;
            //this.toDate = toDate;
            //this.category = category;
            //TableAdapter adapter = new TableAdapter();
            //adapter.Fill(dt, fromDate, toDate, category);
        }






        private void Reports_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //adapter = new ();
            //adapter.Fill(dt);
            ReportDataSource rt = new ReportDataSource("DataSet1", (DataTable)dt);
            //List<ReportParameter> parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("fromDate", fromDate.ToString("dd MMMM yyyy")));
            //parameters.Add(new ReportParameter("toDate", toDate.ToString("dd MMMM yyyy")));
            //parameters.Add(new ReportParameter("category", category));
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rt);
            reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Reports.DailySalesReport.rdlc";
            //reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
            this.Cursor = Cursors.Default;


            //table.Columns.Add("id", typeof(int));
            //table.Columns.Add("name", typeof(string));
            //table.Columns.Add("age", typeof(int));
            //table.Columns.Add("ge", typeof(int));

            //table.Rows.Add(1, "John Doe", 30);
            //table.Rows.Add(2, "Jane Smith", 25);
            //table.Rows.Add(3, "Mike Brown", 42);
            //rt.Name = "DataSet1";
            //rt.Value = table;
            //List<ReportParameter> parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("BillId", "648"));
            ////reportViewer1.LocalReport.DataSources.Clear();
            ////reportViewer1.LocalReport.DataSources.Add(rt);
            //reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Reports.Receipt.rdlc";
            //reportViewer1.LocalReport.SetParameters(parameters);
            //reportViewer1.RefreshReport();




        }

        DSR.GetTodaySalesDataTable dt = new DSR.GetTodaySalesDataTable();
       
    }

}
