using Microsoft.Reporting.WinForms;
using MyPOSApps.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPOSApps
{
    public partial class FormReportBarang : Form
    {
        private void IsiReport()
        {

        }

        private BarangDAL barangDAL = new BarangDAL();
        private readonly ReportViewer reportViewer;

        public FormReportBarang()
        {
            InitializeComponent();
            this.Text = "Report Barang";
            this.WindowState = FormWindowState.Maximized;
            reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            this.Controls.Add(reportViewer);
        }

        protected override void OnLoad(EventArgs e)
        {

            using var fs = new FileStream("ReportBarang.rdlc",FileMode.Open);

            reportViewer.LocalReport.LoadReportDefinition(fs);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("BarangDataSet", barangDAL.GetAll()));
            reportViewer.RefreshReport();
            base.OnLoad(e);
        }
    }
}
