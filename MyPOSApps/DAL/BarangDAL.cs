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

        public int Insert(Barang barang)
        {
            int hasil = 0;
            using(MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"insert into barang(KodeBarang,NamaBarang,HargaBeli,HargaJual,Stok,TanggalBeli) 
                values(@KodeBarang,@NamaBarang,@HargaBeli,@HargaJual,@Stok,@TanggalBeli)";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@KodeBarang", barang.KodeBarang);
                cmd.Parameters.AddWithValue("@NamaBarang", barang.NamaBarang);
                cmd.Parameters.AddWithValue("@HargaBeli", barang.HargaBeli);
                cmd.Parameters.AddWithValue("@HargaJual", barang.HargaJual);
                cmd.Parameters.AddWithValue("@Stok", barang.Stok);
                cmd.Parameters.AddWithValue("@TanggalBeli", barang.TanggalBeli);

                try
                {
                    conn.Open();
                    hasil = cmd.ExecuteNonQuery();
                    if (hasil != 1)
                        throw new Exception("Gagal menambahkan data");

                    return hasil;
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

        public int Update(Barang barang)
        {
            int hasil = 0;
            using(MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"update barang set NamaBarang=@NamaBarang,HargaBeli=@HargaBeli,HargaJual=@HargaJual,
                                Stok=@Stok,TanggalBeli=@TanggalBeli where KodeBarang=@KodeBarang";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@NamaBarang", barang.NamaBarang);
                cmd.Parameters.AddWithValue("@HargaBeli", barang.HargaBeli);
                cmd.Parameters.AddWithValue("@HargaJual", barang.HargaJual);
                cmd.Parameters.AddWithValue("@Stok", barang.Stok);
                cmd.Parameters.AddWithValue("@TanggalBeli", barang.TanggalBeli);
                cmd.Parameters.AddWithValue("@KodeBarang", barang.KodeBarang);
                try
                {
                    conn.Open();
                    hasil = cmd.ExecuteNonQuery();
                    if (hasil != 1)
                        throw new Exception("Gagal untuk mengupdate data");
                    return hasil;
                }
                catch (MySqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx.Message}");
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
