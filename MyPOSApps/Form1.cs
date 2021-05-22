using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//library ADO.NET khusus untuk koneksi ke MySQL
using MySql.Data.MySqlClient;

namespace MyPOSApps
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKoneksi_Click(object sender, EventArgs e)
        {
            string strConn = "server=localhost;user=erick;password=KosongkanSaja;database=posdb";
            MySqlConnection conn = new MySqlConnection(strConn);
            try
            {
                conn.Open();
                MessageBox.Show("Koneksi dengan MySQL berhasil dibuat");
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show($"Error: {sqlEx.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
