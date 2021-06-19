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
    public partial class FormBarang : Form
    {
        private BindingSource bs;
        private BarangDAL barangDAL;
        private bool isNew = false;

        public FormBarang()
        {
            InitializeComponent();
            barangDAL = new BarangDAL();
            bs = new BindingSource();
        }

        #region Inisialisasi dan Binding

        private void TambahBinding()
        {
            txtKodeBarang.DataBindings.Add("Text", bs, "KodeBarang");
            txtNamaBarang.DataBindings.Add("Text", bs, "NamaBarang");
            txtHargaBeli.DataBindings.Add("Text", bs, "HargaBeli");
            txtHargaJual.DataBindings.Add("Text", bs, "HargaJual");
            txtStok.DataBindings.Add("Text", bs, "Stok");
            dtpTanggalBeli.DataBindings.Add("Value", bs, "TanggalBeli");
        }

        private void HapusBinding()
        {
            txtKodeBarang.DataBindings.Clear();
            txtNamaBarang.DataBindings.Clear();
            txtHargaBeli.DataBindings.Clear();
            txtHargaJual.DataBindings.Clear();
            txtStok.DataBindings.Clear();
            dtpTanggalBeli.DataBindings.Clear();
        }

        private void InisialisasiAwal()
        {
            txtKodeBarang.Enabled = false;
            txtNamaBarang.Enabled = false;
            txtHargaBeli.Enabled = false;
            txtHargaJual.Enabled = false;
            txtHargaBeli.Enabled = false;
            txtStok.Enabled = false;
            dtpTanggalBeli.Enabled = false;

            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = true;

            FillBarang();
            TambahBinding();
            isNew = false;
        }

        private void InisialisasiNew()
        {
            //reset binding
            HapusBinding();

            foreach (var ctr in this.Controls)
            {
                if (ctr is TextBox)
                {
                    var myTextBox = ctr as TextBox;
                    myTextBox.Enabled = true;
                    myTextBox.Text = string.Empty;
                }
            }
            dtpTanggalBeli.Enabled = true;
            txtKodeBarang.Focus();

            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;

            isNew = true;
        }


        private void InisialisasiEdit()
        {
            HapusBinding();

            foreach (var ctr in this.Controls)
            {
                if (ctr is TextBox)
                {
                    var myTextBox = ctr as TextBox;
                    myTextBox.Enabled = true;
                }
            }

            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;

            dtpTanggalBeli.Enabled = true;
            txtKodeBarang.Enabled = false;
            txtNamaBarang.Focus();
            isNew = false;
        }

        #endregion

        #region CRUD

        private void FillBarang()
        {
            try
            {
                bs.DataSource = barangDAL.GetAll();
                dgvBarang.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Kesalahan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertBarang()
        {
            try
            {
                Barang newBarang = new Barang
                {
                    KodeBarang = txtKodeBarang.Text,
                    NamaBarang = txtNamaBarang.Text,
                    HargaBeli = Convert.ToDecimal(txtHargaBeli.Text),
                    HargaJual = Convert.ToDecimal(txtHargaJual.Text),
                    Stok = Convert.ToInt32(txtStok.Text),
                    TanggalBeli = dtpTanggalBeli.Value
                };
                int result = barangDAL.Insert(newBarang);
                if (result == 1)
                {
                    MessageBox.Show("Data Barang berhasil ditambahkan", "Info");
                    InisialisasiAwal();
                }
                else
                {
                    MessageBox.Show("Gagal menambahkan data..", "Kesalahan", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }

        private void UpdateBarang()
        {
            try
            {
                var editBarang = new Barang
                {
                    NamaBarang = txtNamaBarang.Text,
                    HargaBeli = Convert.ToDecimal(txtHargaBeli.Text),
                    HargaJual = Convert.ToDecimal(txtHargaJual.Text),
                    Stok = Convert.ToInt32(txtStok.Text),
                    TanggalBeli = Convert.ToDateTime(dtpTanggalBeli.Value),
                    KodeBarang = txtKodeBarang.Text
                };
                int result = barangDAL.Update(editBarang);
                if (result == 1)
                {
                    MessageBox.Show("Data Berhasil Diupdate");
                    InisialisasiAwal();
                }
                else
                {
                    MessageBox.Show("Data gagal diupdate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Pencarian Data

        private void SearchData(string namaBarang)
        {
            try
            {
                HapusBinding();
                bs.Clear();
                //var results = barangDAL.GetByNama(namaBarang);
                
                bs.DataSource = barangDAL.GetByNama(namaBarang);
                dgvBarang.DataSource = bs;
                TambahBinding();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Kesalahan",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void FormBarang_Load(object sender, EventArgs e)
        {
            InisialisasiAwal();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isNew)
            {
                InsertBarang();
            }
            else
            {
                UpdateBarang();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var output = MessageBox.Show("Apakah anda yakin akan mendelete data ?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (output == DialogResult.Yes)
            {
                try
                {
                    HapusBinding();
                    int result = barangDAL.Delete(txtKodeBarang.Text);
                    if (result == 1)
                    {
                        MessageBox.Show("Data barang berhasil di delete", "Konfirmasi", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        InisialisasiAwal();
                    }
                    else
                    {
                        MessageBox.Show("Data gagal di delete");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kesalahan: {ex.Message}", "Kesalahan", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            InisialisasiNew();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            InisialisasiEdit();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length >= 2)
            {
                SearchData(txtSearch.Text);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FormReportDataBarang frmReportDataBarang = new FormReportDataBarang();
            frmReportDataBarang.Show();
        }
    }
}
