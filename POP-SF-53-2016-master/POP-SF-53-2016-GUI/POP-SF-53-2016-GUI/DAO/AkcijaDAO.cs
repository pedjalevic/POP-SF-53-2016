using POP_SF_53_2016_GUI;
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
    public class AkcijaDAO
    {
        public static ObservableCollection<Akcija> SveAkcije()
        {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT Id,Datum_Pocetka,Datum_Kraja,Popust FROM Akcija WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Akcija a = new Akcija()
                    {
                        Id = reader.GetInt32(0),
                        PocetakAkcije = (DateTime)reader.GetDateTime(1),
                        KrajAkcije = (DateTime)reader.GetDateTime(2),
                        Popust = (double)reader.GetDecimal(3),
                        Obrisan = false
                    };

                    akcije.Add(a);
                }
                reader.Close();
                foreach (var akcija in akcije)
                {
                    ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
                    cmd = new SqlCommand(@"SELECT NamestajId FROM NaAkciji WHERE AkcijaId=@id AND Obrisan=@obrisan", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", akcija.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (NamestajDAO.NametajPoId(reader.GetInt32(0)) != null)
                            namestaj.Add(NamestajDAO.NametajPoId(reader.GetInt32(0)));
                    }
                    akcija.NamestajPopust = namestaj;
                    reader.Close();
                }
            }

            return akcije;
        }
        public static bool BrisanjeAkcije(Akcija a)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                a.Obrisan = true;
                return IzmenaAkcije(a);
            }
        }
        public static Akcija DodavanjeAkcije(Akcija a)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Akcija(Datum_Pocetka,Datum_Kraja,Popust,Obrisan) VALUES(@datP,@datK,@popust,@obrisan) ", conn);
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@datP", a.PocetakAkcije));
                cmd.Parameters.Add(new SqlParameter("@datK", a.KrajAkcije));
                cmd.Parameters.Add(new SqlParameter("@popust", a.Popust));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                a.Id = newId;
                for (int i = 0; i < a.NamestajPopust.Count; i++)
                {
                    SqlCommand cm = new SqlCommand(@"INSERT INTO NaAkciji(NamestajId,AkcijaId,Obrisn) VALUES(@namestajId,@akcijaId) ", conn);
                    cm.Parameters.Add(new SqlParameter("@namestajId", a.NamestajPopust[i].Id));
                    cm.Parameters.Add(new SqlParameter("@akcijaId", a.Id));
                    cm.ExecuteNonQuery();
                }
            }
            Projekat.Instance.Akcije.Add(a);
            return a;
        }
        public static bool IzmenaAkcije(Akcija a)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                for (int i = 0; i < a.NamestajPopust.Count; i++)
                {
                    SqlCommand cm = new SqlCommand(@" UPDATE NaAkciji SET NamestajId=@namestajId WHERE AkcijaId=@akcijaId", conn);
                    cm.Parameters.Add(new SqlParameter("@namestajId", a.NamestajPopust[i].Id));
                    cm.Parameters.Add(new SqlParameter("@akcijaId", a.Id));
                    cm.ExecuteNonQuery();
                }
                SqlCommand cmd = new SqlCommand(@" UPDATE Akcija SET Datum_Pocetka=@datP,Datum_Kraja=@datK,Popust=@popust,Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@datp", a.PocetakAkcije));
                cmd.Parameters.Add(new SqlParameter("@datK", a.KrajAkcije));
                cmd.Parameters.Add(new SqlParameter("@popust", a.Popust));
                cmd.Parameters.Add(new SqlParameter("@id", a.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", a.Obrisan));
                cmd.ExecuteNonQuery();

                foreach (var item in Projekat.Instance.Akcije)
                {
                    if (item.Id == a.Id)
                    {
                        item.Id = a.Id;
                        item.PocetakAkcije = a.PocetakAkcije;
                        item.KrajAkcije = a.KrajAkcije;
                        item.Popust = a.Popust;
                        item.NamestajPopust = a.NamestajPopust;
                        item.Obrisan = a.Obrisan;
                    }
                }
                return true;
            }
        }
        public static Akcija DodavanjeNaAkciju(Akcija a, Namestaj namestaj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cm = new SqlCommand(@" INSERT INTO NaAkciji(NamestajId,AkcijaId,Obrisan) VALUES (@namestajId,@akcijaId,@obrisan)", conn);
                cm.Parameters.Add(new SqlParameter("@namestajId", namestaj.Id));
                cm.Parameters.Add(new SqlParameter("@akcijaId", a.Id));
                cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cm.ExecuteNonQuery();

            }
            a.NamestajPopust.Add(namestaj);
            return a;

        }
        public static Akcija BrisanjeSaAkcije(Akcija a, Namestaj namestaj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cm = new SqlCommand(@" UPDATE NaAkciji SET Obrisan=@obrisan WHERE NamestajId=@namestajId AND AkcijaId=@akcijaId", conn);
                cm.Parameters.Add(new SqlParameter("@namestajId", namestaj.Id));
                cm.Parameters.Add(new SqlParameter("@akcijaId", a.Id));
                cm.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cm.ExecuteNonQuery();

            }
            a.NamestajPopust.Remove(namestaj);
            return a;

        }
        public static ObservableCollection<Akcija> PretraziAkcije(string tekst)
        {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            ObservableCollection<Namestaj> namestajAkcija = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Akcija join Namestaj on Akcija.Namestaj=Namestaj.Id WHERE Akcija.Obrisan=@obrisan AND
        (Datum_Pocetka like @tekst OR Datum_Kraja like @tekst OR Popust like @tekst)", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%" + tekst + "%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Namestaj n = new Namestaj()
                    {
                        Id = reader.GetInt32(6),
                        Naziv = reader.GetString(7),
                        Kolicina = reader.GetInt32(8),
                        Sifra = reader.GetString(9),
                        TipNamestaja = TipNamestajaDAO.TipPoId(reader.GetInt32(10)),
                        Cena = (double)reader.GetDecimal(11)
                    };
                    namestaj.Add(n);
                    Akcija a = new Akcija()
                    {
                        Id = reader.GetInt32(0),
                        PocetakAkcije = (DateTime)reader.GetDateTime(1),
                        KrajAkcije = (DateTime)reader.GetDateTime(2),
                        NamestajPopust = namestaj,
                        Popust = (double)reader.GetDecimal(4)
                    };
                    if (akcije.Count == 0)
                        akcije.Add(a);
                    else if (ProveriAkciju(a, akcije) == true)
                    {
                        continue;
                    }

                }
            }
            return akcije;
        }
        public static bool ProveriAkciju(Akcija a, ObservableCollection<Akcija> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (a.PocetakAkcije == lista[i].PocetakAkcije && a.KrajAkcije == lista[i].KrajAkcije && a.Popust == lista[i].Popust)
                    return false;
            }
            return true;
        }
    }
}