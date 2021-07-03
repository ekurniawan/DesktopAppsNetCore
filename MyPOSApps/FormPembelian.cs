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

        public TextBox TxtKodeSupplier {
            get {
                return txtKodeSupplier;
            }
            set {
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

        #endregion

        private PembelianDAL pembelianDAL;
        public FormPembelian()
        {
            InitializeComponent();
            pembelianDAL = new PembelianDAL();
        }

        private void txtKodeSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Enter)
            {
                
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frmSupplier = new FormSupplier();
            FormSupplier.Instance().Show();
        }

        private void FormPembelian_Load(object sender, EventArgs e)
        {
            txtNoNotaBeli.Text = pembelianDAL.GenerateNota(DateTime.Now, 1);
        }
    }
}
