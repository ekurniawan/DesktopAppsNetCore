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
    public class SupplierDAL
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public List<Supplier> GetAll()
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"select * from supplier order by Nama asc";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                try
                {
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstSupplier.Add(new Supplier
                            {
                               KodeSupplier = dr["KodeSupplier"].ToString(),
                               Nama = dr["Nama"].ToString(),
                               Alamat = dr["Alamat"].ToString(),
                               Email = dr["Email"].ToString(),
                               Telp = dr["Telp"].ToString()
                            });
                        }
                    }
                    dr.Close();
                    return lstSupplier;
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

        public List<Supplier> GetByNama(string namaSup)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                string strSql = @"select * from supplier where Nama like @Nama";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", "%" + namaSup + "%");
                try
                {
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstSupplier.Add(new Supplier
                            {
                                KodeSupplier = dr["KodeSupplier"].ToString(),
                                Nama = dr["Nama"].ToString(),
                                Alamat = dr["Alamat"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telp = dr["Telp"].ToString()
                            });
                        }
                    }
                    dr.Close();
                    return lstSupplier;
                }
                catch (MySqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
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
