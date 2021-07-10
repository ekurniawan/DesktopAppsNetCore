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
    public partial class FormPembelian : Form
    {
        #region Singleton

        private static FormPembelian _instance;
        public static FormPembelian Instance()
        {
            if (_instance == null)
                _instance = new FormPembelian();
            return _instance;
        }

        #endregion

        #region public property yg nanti akan diakses

        public TextBox TxtKodeSupplier
        {
            get
            {
                return txtKodeSupplier;
            }
            set
            {
                txtKodeSupplier = value;
            }
        }

        public TextBox TxtNamaSupplier
        {
            get
            {
                return txtNamaSupplier;
            }
            set
            {
                txtNamaSupplier = value;
            }
        }

        public TextBox TxtAlamatSupplier
        {
            get
            {
                return txtAlamatSupplier;
            }
            set
            {
                txtAlamatSupplier = value;
            }
        }

        public TextBox TxtEmailSupplier
        {
            get
            {
                return txtEmailSupplier;
            }
            set
            {
                txtEmailSupplier = value;
            }
        }

        public TextBox TxtTelponSupplier
        {
            get
            {
                return txtTelponSupplier;
            }
            set
            {
                txtTelponSupplier = value;
            }
        }

        public TextBox TxtKodeBarang
        {
            get
            {
                return txtKodeBarang;
            }
            set
            {
                txtKodeBarang = value;
            }
        }

        public TextBox TxtNamaBarang
        {
            get
            {
                return txtNamaBarang;
            }
            set
            {
                txtNamaBarang = value;
            }
        }

        public TextBox TxtHargaBeli
        {
            get
            {
                return txtHargaBeli;
            }
            set
            {
                txtHargaBeli = value;
            }
        }

        public TextBox TxtNoNotaBeli
        {
            get
            {
                return txtNoNotaBeli;
            }
            set
            {
                TxtNoNotaBeli = value;
            }
        }

        #endregion

        private ItemBeliDAL itemBeliDAL;
        private PembelianDAL pembelianDAL;
        private BindingSource bs;
        public FormPembelian()
        {
            InitializeComponent();
            pembelianDAL = new PembelianDAL();
            itemBeliDAL = new ItemBeliDAL();
            bs = new BindingSource();
        }

        private void TambahBinding()
        {
            txtKodeBarang.DataBindings.Add("Text", bs, "KodeBarang");
            txtNamaBarang.DataBindings.Add("Text", bs, "NamaBarang");
            txtHargaBeli.DataBindings.Add("Text", bs, "HargaBeli");
            txtQty.DataBindings.Add("Text", bs, "JumlahBeli");
        }

        private void HapusBinding()
        {
            txtKodeBarang.DataBindings.Clear();
            txtNamaBarang.DataBindings.Clear();
            txtHargaBeli.DataBindings.Clear();
            txtQty.DataBindings.Clear();
        }

        private void FillBarang()
        {
            try
            {
                bs.DataSource = itemBeliDAL.GetAll(txtNoNotaBeli.Text);
                dgvNotaBeli.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Kesalahan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtKodeSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormSupplier.Instance().Show();
            }
        }

        private void FormPembelian_Load(object sender, EventArgs e)
        {
            txtNoNotaBeli.Text = pembelianDAL.GenerateNota(DateTime.Now, 1);

            HapusBinding();
            FillBarang();
            TambahBinding();
        }

        private void txtKodeBarang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormBarang.Instance().Show();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSubtotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtHargaBeli.Text)).ToString();
                txtSubtotal.Focus();
            }
        }

        private void txtSubtotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HapusBinding();
                var newItemBeli = new ItemBeli
                {
                    No = txtNoNotaBeli.Text,
                    KodeBarang = txtKodeBarang.Text,
                    HargaBeli = Convert.ToDecimal(txtHargaBeli.Text),
                    Jumlah = Convert.ToInt32(txtQty.Text)
                };

                try
                {
                    itemBeliDAL.TambahItemBeli(newItemBeli);
                    MessageBox.Show("Data berhasil ditambah", "Keterangan");
                    FillBarang();
                    TambahBinding();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error");
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pembelianDAL.UpdateDataSupplier(txtKodeSupplier.Text, TxtNoNotaBeli.Text,dtTanggalBeli.Value);
            MessageBox.Show("Update data supplier");
        }
    }
}
