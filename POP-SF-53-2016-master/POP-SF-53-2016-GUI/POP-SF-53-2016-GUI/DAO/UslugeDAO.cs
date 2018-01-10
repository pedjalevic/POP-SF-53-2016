using POP_SF_53_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.DAO
{
   public class UslugeDAO
    {
        public static ObservableCollection<DodatneUsluge> SveUsluge()
        {
            ObservableCollection<DodatneUsluge> usluge = new ObservableCollection<DodatneUsluge>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM DodatneUsluge WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DodatneUsluge du = new DodatneUsluge()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Cena = (double)reader.GetDecimal(2)
                    };
                    usluge.Add(du);

                }
            }
            return usluge;
        }
        public static DodatneUsluge DodavanjeUsluge(DodatneUsluge du)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO DodatneUsluge(Naziv,Cena,Obrisan) VALUES (@naziv,@cena,@Obrisan)", conn);
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@naziv", du.Naziv));
                cmd.Parameters.Add(new SqlParameter("@cena", du.Cena));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                du.Id = newId;
            }
            Projekat.Instance.DodatneUsluge.Add(du);
            return du;
        }
        public static bool BrisanjeUsluge(DodatneUsluge du)
        {
            du.Obrisan = true;
            return IzmenaUsluge(du);
        }
        public static bool IzmenaUsluge(DodatneUsluge du)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE DodatneUsluge SET Naziv=@naziv,Cena=@cena,Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", du.Naziv));
                cmd.Parameters.Add(new SqlParameter("@cena", du.Cena));
                cmd.Parameters.Add(new SqlParameter("@Id", du.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", du.Obrisan));
                cmd.ExecuteNonQuery();

            }
            foreach (var item in Projekat.Instance.DodatneUsluge)
            {
                if (item.Id == du.Id)
                {
                    item.Id = du.Id;
                    item.Naziv = du.Naziv;
                    item.Cena = du.Cena;
                    item.Obrisan = du.Obrisan;
                }
            }
            return true;
        }
        public static ObservableCollection<DodatneUsluge> PretraziUsluge(string tekst)
        {
            ObservableCollection<DodatneUsluge> usluge = new ObservableCollection<DodatneUsluge>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM DodatneUsluge WHERE Obrisan=@obrisan AND (Naziv like @tekst OR Cena like @tekst)", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%" + tekst + "%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DodatneUsluge du = new DodatneUsluge()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Cena = (double)reader.GetDecimal(2)
                    };
                    usluge.Add(du);

                }
            }
            return usluge;
        }
        public static ObservableCollection<DodatneUsluge> SortirajUsluge(string tekst)
        {
            ObservableCollection<DodatneUsluge> usluge = new ObservableCollection<DodatneUsluge>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM DodatneUsluge WHERE Obrisan=@obrisan ORDER BY " + tekst, conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DodatneUsluge du = new DodatneUsluge()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Cena = (double)reader.GetDecimal(2)
                    };
                    usluge.Add(du);

                }
            }
            return usluge;
        }
        public static DodatneUsluge UslugaPoId(int id)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM DodatneUsluge WHERE Obrisan=@obrisan AND Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DodatneUsluge du = new DodatneUsluge()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Cena = (double)reader.GetDecimal(2)
                    };
                    return du;
                }

            }
            return null;
        }
    }
}
