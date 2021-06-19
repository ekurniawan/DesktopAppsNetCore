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
    public partial class FormReportDataBarang : Form
    {
        private readonly ReportViewer reportViewer;
        private BarangDAL barangDAL;
        public FormReportDataBarang()
        {
            InitializeComponent();
            barangDAL = new BarangDAL();
            this.Text = "Laporan Data Barang";
            this.WindowState = FormWindowState.Maximized;
            reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            this.Controls.Add(reportViewer);
        }

        protected override void OnLoad(EventArgs e)
        {
            var dataBarang = barangDAL.GetAll();
            ReportDataSource rds = new ReportDataSource("dsDataBarang", dataBarang);
            using var fs = new FileStream("ReportDataBarang.rdlc", FileMode.Open);
            reportViewer.LocalReport.LoadReportDefinition(fs);
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

            base.OnLoad(e);
        }
    }
}
