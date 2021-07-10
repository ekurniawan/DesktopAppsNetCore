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
    public class ItemBeliDAL
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public IEnumerable<ViewItemBeli> GetAll(string noNotaBeli)
        {
            List<ViewItemBeli> listViewItemBeli = new List<ViewItemBeli>();
            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"select KodeBarang,NamaBarang,JumlahBeli,HargaBeli from viewitembeli where No=@No order by NoItemBeli asc";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@No", noNotaBeli);
                try
                {
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            listViewItemBeli.Add(new ViewItemBeli
                            {
                                KodeBarang = dr["KodeBarang"].ToString(),
                                NamaBarang = dr["NamaBarang"].ToString(),
                                HargaBeli = Convert.ToDecimal(dr["HargaBeli"]),
                                JumlahBeli = Convert.ToInt32(dr["JumlahBeli"])
                            });
                        }
                    }
                    dr.Close();
                    return listViewItemBeli;
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

        public void TambahItemBeli(ItemBeli itemBeli)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                //cek apakah item tersebut sudah ada di nota
                string strSqlCek = @"select JumlahBeli from itembeli where No=@No and KodeBarang=@KodeBarang";
                MySqlCommand cmd = new MySqlCommand(strSqlCek, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@No", itemBeli.No);
                cmd.Parameters.AddWithValue("@KodeBarang", itemBeli.KodeBarang);
                conn.Open();

                int jumlahBeli = Convert.ToInt32(cmd.ExecuteScalar());
                string strSql = string.Empty;
                if (jumlahBeli > 0)
                {
                    strSql = @"update itembeli set JumlahBeli=@JumlahBeli where No=@No and KodeBarang=@KodeBarang";
                    cmd = new MySqlCommand(strSql, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@No", itemBeli.No);
                    cmd.Parameters.AddWithValue("@KodeBarang", itemBeli.KodeBarang);
                    cmd.Parameters.AddWithValue("@JumlahBeli", jumlahBeli + itemBeli.Jumlah );
                }
                else
                {
                    strSql = @"insert into itembeli(No,KodeBarang,JumlahBeli,HargaBeli) 
                values(@No,@KodeBarang,@JumlahBeli,@HargaBeli)";
                    cmd = new MySqlCommand(strSql, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@No", itemBeli.No);
                    cmd.Parameters.AddWithValue("@KodeBarang", itemBeli.KodeBarang);
                    cmd.Parameters.AddWithValue("@JumlahBeli", itemBeli.Jumlah);
                    cmd.Parameters.AddWithValue("@HargaBeli", itemBeli.HargaBeli);
                }


                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception($"Kesalahan: {ex.Message}");
                }
            }
        }
    }
}
