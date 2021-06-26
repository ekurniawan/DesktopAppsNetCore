using MyPOSApps.DAL;
using MyPOSApps.Models;
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
    public partial class FormSupplier : Form
    {
        #region Singleton

        private static FormSupplier _instance;
        public static FormSupplier Instance()
        {
            if (_instance == null)
                _instance = new FormSupplier();
            return _instance;
        }

        #endregion


        private BindingSource bs;
        private SupplierDAL supplierDAL;
        public FormSupplier()
        {
            InitializeComponent();
            supplierDAL = new SupplierDAL();
            bs = new BindingSource();
        }

        private void FillBarang()
        {
            try
            {
                bs.DataSource = supplierDAL.GetAll();
                dgvSupplier.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Kesalahan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchData(string namaSup)
        {
            try
            {
                bs.Clear();
                //var results = barangDAL.GetByNama(namaBarang);

                bs.DataSource = supplierDAL.GetByNama(namaSup);
                dgvSupplier.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Kesalahan",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormSupplier_Load(object sender, EventArgs e)
        {
            FillBarang();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length >= 3)
            {
                SearchData(txtSearch.Text);
            }

            if (txtSearch.Text.Length == 0)
                FillBarang();
        }

        private void dgvSupplier_DoubleClick(object sender, EventArgs e)
        {
            Supplier currSupplier = (Supplier)bs.Current;

            //MessageBox.Show($"{currSupplier.Nama}");
            FormPembelian.Instance().TxtKodeSupplier.Text = currSupplier.KodeSupplier;
            FormPembelian.Instance().TxtNamaSupplier.Text = currSupplier.Nama;
            FormPembelian.Instance().TxtAlamatSupplier.Text = currSupplier.Alamat;
            FormPembelian.Instance().TxtEmailSupplier.Text = currSupplier.Email;
            FormPembelian.Instance().TxtTelponSupplier.Text = currSupplier.Telp;

            this.Hide();
        }
    }
}
