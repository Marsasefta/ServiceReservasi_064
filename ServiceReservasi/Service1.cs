using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=DESKTOP-MJR06SE;Initial Catalog=WCFTest;Persist Security Info=True;User ID=sa;Password=marsa010300";
        SqlConnection connection;
        SqlCommand com;

        public string deletepemesanan(string IDpemesanan)
        {
            throw new NotImplementedException();
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>(); 
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi"; 
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open(); 
                SqlDataReader reader = com.ExecuteReader(); 
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0); 
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data); 
                }
                connection.Close(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string EditPemesanan(string IDpemesanan, string NamaCustomer)
        {
            throw new NotImplementedException();
        }

        public string pemesanan(string IDPemesanan, string Namacustomer, string NoTelepon, int Jumlahpemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + Namacustomer + "', '" + NoTelepon + "', " + Jumlahpemesanan + ", '" + IDLokasi + "')";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<Pemesanan> Pemesanan()
        {
            throw new NotImplementedException();
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }
    }
}
