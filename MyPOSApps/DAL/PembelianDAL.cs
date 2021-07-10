using MyPOSApps.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOSApps.DAL
{
    public class PembelianDAL
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }


        //
        public string GenerateNota(DateTime tanggaBeli,int kodeSupplier)
        {
            //cek nomor nota beli terakhir
            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"SELECT No FROM posdb.transaksibeli order by No desc";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                conn.Open();
                var result = cmd.ExecuteScalar().ToString();
                int nomor = Convert.ToInt32(result.Substring(2,4));
                nomor++;

                var nomorbaru = "NB" + nomor.ToString().PadLeft(4, '0');
                string strSql2 = "insert into transaksibeli values(@No,@TanggalBeli,@KodeSupplier)";

                cmd = new MySqlCommand(strSql2, conn);
                cmd.Parameters.AddWithValue("@No", nomorbaru);
                cmd.Parameters.AddWithValue("@TanggalBeli", tanggaBeli);
                cmd.Parameters.AddWithValue("@KodeSupplier", kodeSupplier);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();

                return nomorbaru;

            }
        }

    }
}
