using Microsoft.Reporting.WinForms;
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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }






        private void Reports_Load(object sender, EventArgs e)
        {
           
            //table.Columns.Add("id", typeof(int));
            //table.Columns.Add("name", typeof(string));
            //table.Columns.Add("age", typeof(int));
            //table.Columns.Add("ge", typeof(int));

            //table.Rows.Add(1, "John Doe", 30);
            //table.Rows.Add(2, "Jane Smith", 25);
            //table.Rows.Add(3, "Mike Brown", 42);
            //rt.Name = "DataSet1";
            //rt.Value = table;
            ////List<ReportParameter> parameters = new List<ReportParameter>();
            ////parameters.Add(new ReportParameter("BillId", "648"));
            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(rt);
            //reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Report1.rdlc";
            ////reportViewer1.LocalReport.SetParameters(parameters);
            //reportViewer1.RefreshReport();


           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            POSDataSetTableAdapters.bill_listTableAdapter adapter = new POSDataSetTableAdapters.bill_listTableAdapter();
            POSDataSet.bill_listDataTable dt = new POSDataSet.bill_listDataTable();
            adapter.FillByCategory(dt, fromDate.Value, toDate.Value);
            ReportDataSource rt = new ReportDataSource("DataSet1", (DataTable)dt);
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("fromDate", fromDate.Value.ToString("d MMM yyyy")));
            parameters.Add(new ReportParameter("toDate", toDate.Value.ToString("d MMM yyyy")));
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rt);
            reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Reports.SRByCategory.rdlc";
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }
    }

}
