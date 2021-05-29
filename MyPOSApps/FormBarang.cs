using MyPOSApps.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPOSApps
{
    public partial class FormBarang : Form
    {
        private BarangDAL barangDAL;

        public FormBarang()
        {
            InitializeComponent();
            barangDAL = new BarangDAL();
        }

        private void FillBarang()
        {
            try
            {
                dgvBarang.DataSource = barangDAL.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Kesalahan", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void FormBarang_Load(object sender, EventArgs e)
        {
            FillBarang();
        }
    }
}
