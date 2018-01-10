using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using POP_SF_53_2016_GUI.Model;

namespace POP_SF_53_2016_GUI.DAO
{
    public class KorisnikDAO {

        public static ObservableCollection<Korisnik> SviKorisnici()
        {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Korisnik WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Korisnik k = new Korisnik()
                    {
                        Id = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        KorisnickoIme = reader.GetString(3),
                        Lozinka = reader.GetString(4),
                        TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), reader.GetString(5)),

                    };
                    korisnici.Add(k);
                }
            }
            return korisnici;
        }
        public static Korisnik DodavanjeKorisnika(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO Korisnik(Ime,Prezime,Korisnicko_Ime,Lozinka,Tip_Korisnika,Obrisan) VALUES (@ime,@prezime,@kIme,@lozinka,@tip,@Obrisan)", conn);
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@kIme", k.KorisnickoIme));
                cmd.Parameters.Add(new SqlParameter("@lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@tip", k.TipKorisnika.ToString()));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                k.Id = newId;
            }
            Projekat.Instance.Korisnici.Add(k);
            return k;
        }
        public static bool BrisanjeKorisnika(Korisnik k)
        {
            k.Obrisan = true;
            return IzmenaKorisnika(k);
        }
        public static bool IzmenaKorisnika(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Korisnik SET Ime=@ime, Prezime=@prezime, Korisnicko_Ime=@kIme, Lozinka=@lozinka, Tip_Korisnika=@tip, Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@kIme", k.KorisnickoIme));
                cmd.Parameters.Add(new SqlParameter("@lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@tip", k.TipKorisnika.ToString()));
                cmd.Parameters.Add(new SqlParameter("@obrisan", k.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@id", k.Id));


                cmd.ExecuteNonQuery();
            }
            foreach (var item in Projekat.Instance.Korisnici)
            {
                if (item.Id == k.Id)
                {
                    item.Id = k.Id;
                    item.Ime = k.Ime;
                    item.Prezime = k.Prezime;
                    item.KorisnickoIme = k.KorisnickoIme;
                    item.Lozinka = k.Lozinka;
                    item.TipKorisnika = k.TipKorisnika;
                    item.Obrisan = k.Obrisan;
                }
            }
            return true;
        }
        public static ObservableCollection<Korisnik> PretragaKorisnika(string tekst)
        {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            int id = 1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Korisnik WHERE Obrisan=@obrisan AND (Ime LIKE (@tekst) OR Prezime LIKE (@tekst)" +
                    "OR Korisnicko_Ime LIKE (@tekst) OR Lozinka LIKE (@tekst) OR Tip_Korisnika LIKE (@tekst))", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%" + tekst + "%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Korisnik k = new Korisnik()
                    {
                        Id = id++,
                        Ime = reader.GetString(0),
                        Prezime = reader.GetString(1),
                        KorisnickoIme = reader.GetString(2),
                        Lozinka = reader.GetString(3),
                        TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), reader.GetString(4)),
                        Obrisan = false

                    };
                    korisnici.Add(k);
                }
            }
            return korisnici;
        }
        public static ObservableCollection<Korisnik> SortirajKorisnika(string tekst)
        {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            int id = 1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Korisnik WHERE Obrisan=@obrisan ORDER BY " + tekst, conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", tekst));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Korisnik k = new Korisnik()
                    {
                        Id = id++,
                        Ime = reader.GetString(0),
                        Prezime = reader.GetString(1),
                        KorisnickoIme = reader.GetString(2),
                        Lozinka = reader.GetString(3),
                        TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), reader.GetString(4)),
                        Obrisan = false

                    };
                    korisnici.Add(k);
                }
            }
            return korisnici;
        }
    }
}
