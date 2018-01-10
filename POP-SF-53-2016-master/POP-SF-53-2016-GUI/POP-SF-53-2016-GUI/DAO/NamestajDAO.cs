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
    public class NamestajDAO
    {
        public static ObservableCollection<Namestaj> SavNamestaj()
        {
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj n join TipNamestaja t on n.Tip_Namestaja=t.Id WHERE n.Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipNamestaja t = new TipNamestaja()
                    {
                        Id = reader.GetInt32(7),
                        Naziv = reader.GetString(8),
                        Obrisan = false,
                    };

                    Namestaj n = new Namestaj()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Kolicina = reader.GetInt32(2),
                        Sifra = reader.GetString(3),
                        TipNamestaja = t,
                        Cena = (double)reader.GetDecimal(5)
                    };
                    namestaj.Add(n);
                }
            }
            return namestaj;
        }
        public static Namestaj DodavanjeNamestaja(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO Namestaj(Naziv,Kolicina,Sifra,Tip_Namestaja,Cena,Obrisan) VALUES (@naziv,@kolicina,@sifra,@tip,@cena,@Obrisan)", conn);
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@naziv", n.Naziv));
                cmd.Parameters.Add(new SqlParameter("@kolicina", n.Kolicina));
                cmd.Parameters.Add(new SqlParameter("@sifra", n.Sifra));
                cmd.Parameters.Add(new SqlParameter("@tip", n.TipNamestaja.Id));
                cmd.Parameters.Add(new SqlParameter("@cena", n.Cena));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                n.Id = newId;
            }

            Projekat.Instance.Namestaj.Add(n);

            return n;
        }
        public static bool BrisanjeNamestaja(Namestaj n)
        {
            n.Obrisan = true;
            return IzmenaNamestaja(n);
        }

        public static bool IzmenaNamestaja(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Namestaj SET Naziv=@naziv,Kolicina=@kolicina,Sifra=@sifra,Tip_Namestaja=@tip,Cena=@cena,Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", n.Naziv));
                cmd.Parameters.Add(new SqlParameter("@kolicina", n.Kolicina));
                cmd.Parameters.Add(new SqlParameter("@sifra", n.Sifra));
                cmd.Parameters.Add(new SqlParameter("@tip", n.TipNamestaja.Id));
                cmd.Parameters.Add(new SqlParameter("@cena", n.Cena));
                cmd.Parameters.Add(new SqlParameter("@id", n.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", n.Obrisan));
                cmd.ExecuteNonQuery();
            }

            foreach (var item in Projekat.Instance.Namestaj)
            {
                if (item.Id == n.Id)
                {
                    item.Id = n.Id;
                    item.Kolicina = n.Kolicina;
                    item.Naziv = n.Naziv;
                    item.Sifra = n.Sifra;
                    item.TipNamestaja = n.TipNamestaja;
                    item.Obrisan = n.Obrisan;
                }
            }
            return true;
        }
        public static Namestaj NametajPoId(int id)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj WHERE Obrisan=@obrisan AND Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Namestaj n = new Namestaj()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Kolicina = reader.GetInt32(2),
                        Sifra = reader.GetString(3),
                        TipNamestaja = (TipNamestaja)TipNamestajaDAO.TipPoId(reader.GetInt32(4)),
                        Cena = (double)reader.GetDecimal(5)
                    };
                    return n;
                }
            }
            return null;
        }

        public static ObservableCollection<Namestaj> SortirajNamestaj(string tekst)
        {
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj join TipNamestaja on Namestaj.Tip_Namestaja=TipNamestaja.Id WHERE Namestaj.Obrisan=@obrisan ORDER BY " + tekst, conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Namestaj n = new Namestaj()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Kolicina = reader.GetInt32(2),
                        Sifra = reader.GetString(3),
                        TipNamestaja = (TipNamestaja)TipNamestajaDAO.TipPoId(reader.GetInt32(4)),
                        Cena = (double)reader.GetDecimal(5)
                    };
                    namestaj.Add(n);
                }
            }
            return namestaj;
        }
        public static ObservableCollection<Namestaj> PretraziNamestaj(string tekst)
        {
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj join TipNamestaja on Namestaj.Tip_Namestaja=TipNamestaja.Id WHERE Namestaj.Obrisan=@obrisan AND(Namestaj.Naziv like @tekst OR Kolicina like @tekst
                 OR Sifra like @tekst OR Cena like @tekst OR TipNamestaja.Naziv like @tekst)", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%" + tekst + "%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Namestaj n = new Namestaj()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Kolicina = reader.GetInt32(2),
                        Sifra = reader.GetString(3),
                        TipNamestaja = (TipNamestaja)TipNamestajaDAO.TipPoId(reader.GetInt32(4)),
                        Cena = (double)reader.GetDecimal(5)
                    };
                    namestaj.Add(n);
                }
            }
            return namestaj;
        }
    }
}