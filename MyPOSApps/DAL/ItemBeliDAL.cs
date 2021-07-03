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

        public void TambahItemBeli(ItemBeli itemBeli)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"insert into itembeli(No,KodeBarang,JumlahBeli,HargaBeli) 
                values(@No,@KodeBarang,@JumlahBeli,@HargaBeli)";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@No", itemBeli.No);
                cmd.Parameters.AddWithValue("@KodeBarang", itemBeli.KodeBarang);
                cmd.Parameters.AddWithValue("@JumlahBeli", itemBeli.Jumlah);
                cmd.Parameters.AddWithValue("@HargaBeli", itemBeli.HargaBeli);

                try
                {
                    conn.Open();
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
