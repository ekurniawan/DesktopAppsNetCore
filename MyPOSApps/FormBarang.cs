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

        private void btnSave_Click(object sender, EventArgs e)
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
                }
                else
                {
                    MessageBox.Show("Gagal menambahkan data..","Kesalahan",MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
    }
}
