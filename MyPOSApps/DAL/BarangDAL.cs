using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using MyPOSApps.Models;

namespace MyPOSApps.DAL
{
    public class BarangDAL
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public List<Barang> GetAll()
        {
            List<Barang> lstBarang = new List<Barang>();
            using(MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"select * from barang order by NamaBarang asc";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                try
                {
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstBarang.Add(new Barang
                            {
                                KodeBarang = dr["KodeBarang"].ToString(),
                                NamaBarang = dr["NamaBarang"].ToString(),
                                HargaBeli = Convert.ToDecimal(dr["HargaBeli"]),
                                HargaJual = Convert.ToDecimal(dr["HargaJual"]),
                                Stok = Convert.ToInt32(dr["Stok"]),
                                TanggalBeli = Convert.ToDateTime(dr["TanggalBeli"])
                            });
                        }
                    }
                    dr.Close();
                    return lstBarang;
                }
                catch (MySqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx.InnerException.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
