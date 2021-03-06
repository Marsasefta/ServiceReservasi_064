﻿using System;
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

        public List<DataRegister> DataRegist()
        {
            List<DataRegister> list = new List<DataRegister>();
            try
            {
                string sql = "select ID_Login,Username,Password,Kategori from Login";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                   
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return list;
        }

        public string deletepemesanan(string IDpemesanan)
        {
            {
                string a = "gagal";
                try
                {
                    string sql = "delete from dbo.Pemesanan where ID_pemesanan = '" + IDpemesanan + "'";
                    connection = new SqlConnection(constring); //fungsi konek ke db
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

        }

        public string DeleteRegister(string username)
        {
            try
            {
                int id = 0;
                string sql = "select ID_Login from dbo.Login where Username='" + username + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while(reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                connection.Close();
                string sql2 = "delete drom Login where ID_Login" + id + "";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "Sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
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

        public string Login(string username, string password)
        {
            string kategori = "";

            string sql = "select Kategori from Login where Username='"+username+"' and Password='"+password +"'";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();

            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                kategori = reader.GetString(0);
            }
            return kategori;
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

                string sql2 = "update dbo.Lokasi set Kuota = Kuota - " + Jumlahpemesanan + " where ID_lokasi = '" + IDLokasi + "' ";
                connection = new SqlConnection(constring); //fungsi koneksi ke db
                com = new SqlCommand(sql2, connection);
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
            List<Pemesanan> pemesanan = new List<Pemesanan>(); //proses untuk mendeklarasikan nama list yang telah dibuat
            try
            {
                string sql = "select ID_pemesanan, Nama_customer, No_telpon, Jumlah_pemesanan, Nama_lokasi from dbo.Pemesanan p join dbo.Lokasi l on p.ID_lokasi = l.ID_lokasi";
                connection = new SqlConnection(constring); //fungsi koneksi ke db
                com = new SqlCommand(sql, connection); //proses execute query
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader(); //menampilkan data query
                while (reader.Read())
                {
                    /*new class*/
                    Pemesanan data = new Pemesanan(); //deklarasi data, mengambil 1 per 1 dari db
                    //bentuk array
                    data.IDPemesanan = reader.GetString(0); //0 itu index, ada di kolom ke berapa di string sql di atas
                    data.NamaCustomer = reader.GetString(1);
                    data.Notelepon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.IDLokasi = reader.GetString(4);
                    pemesanan.Add(data); //mengumpulkan data yang awalnya dari array
                }
                connection.Close(); //untuk menutup akses ke db
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return pemesanan;
        }

        public string Register(string username, string password, string kategori)
        {
            try
            {
                string sql = "insert into Login values('"+username+"', '"+password+"', '"+kategori+"')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "Sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public string UpdateRegister(string username, string password, string kategori, int id)
        {
            try
            {
                string sql2 = "update Login set Username='" + username + "', Password='" + password + "',Kategori='" + kategori + "'" +
                    "where ID_Login="+id+"";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "Sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
