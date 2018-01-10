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
using System.Windows;

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
                        Popust = reader.GetInt32(3),
                        Obrisan = false
                    };
                    if (DateTime.Today > a.KrajAkcije)
                    {
                        a.Obrisan = true;
                        BrisanjeAkcije(a);
                        continue;
                    }

                    akcije.Add(a);
                }
                reader.Close();
                foreach (var akcija in akcije)
                {
                    ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
                    cmd = new SqlCommand(@"SELECT NamestajId FROM NaAkciji WHERE AkcijaId=@id", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", akcija.Id));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        akcija.NamestajPopustId.Add(reader.GetInt32(0));
                    }
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
                if (a.NamestajPopust.Count > 0)
                for (int i = 0; i < a.NamestajPopust.Count; i++)
                {
                    a.NamestajPopust[i].AkcijskaCena = 0;
                    NamestajDAO.IzmenaNamestaja(a.NamestajPopust[i]);
                    foreach (var n in Projekat.Instance.Namestaj)
                    {
                        if (n.Id == a.NamestajPopust[i].Id)
                        {
                            n.AkcijskaCena = 0;
                        }
                    }
                }

                return IzmenaAkcije(a);
            }
        }
        public static Akcija DodavanjeAkcije(Akcija a)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();

                    {
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

                            SqlCommand cm = new SqlCommand(@"INSERT INTO NaAkciji(NamestajId,AkcijaId) VALUES(@namestajId,@akcijaId) ", conn);
                            cm.Parameters.Add(new SqlParameter("@namestajId", a.NamestajPopust[i].Id));
                            cm.Parameters.Add(new SqlParameter("@akcijaId", a.Id));
                            cm.ExecuteNonQuery();

                            foreach (var namestaj in Projekat.Instance.Namestaj)
                            {
                                if (namestaj.Id == a.NamestajPopust[i].Id)
                                {
                                    namestaj.AkcijskaCena = namestaj.Cena - ((namestaj.Cena * a.Popust) / 100);
                                    NamestajDAO.IzmenaNamestaja(namestaj);
                                }
                            }
                        }

                    }
                }
                Projekat.Instance.Akcije.Add(a);
                return a;
            }
            catch (Exception)
            {
                MessageBox.Show("Upis u bazu nije uspeo,molimo da pokusate ponovo", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;

            }



        }
        public static bool IzmenaAkcije(Akcija a)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
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
                    for (int i = 0; i < a.NamestajPopust.Count; i++)
                    {

                        foreach (var namestaj in Projekat.Instance.Namestaj)
                        {
                            if (namestaj.Id == a.NamestajPopust[i].Id)
                            {
                                namestaj.AkcijskaCena = namestaj.Cena - ((namestaj.Cena * a.Popust) / 100);
                                NamestajDAO.IzmenaNamestaja(namestaj);
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception) { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }


        }
        public static bool DodavanjeNaAkciju(Akcija a, ObservableCollection<Namestaj> dodat)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {

                    conn.Open();
                    for (int i = 0; i < dodat.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@" INSERT INTO NaAkciji(NamestajId,AkcijaId) VALUES (@namestajId,@akcijaId)", conn);
                        cm.Parameters.Add(new SqlParameter("@namestajId", dodat[i].Id));
                        cm.Parameters.Add(new SqlParameter("@akcijaId", a.Id));
                        cm.ExecuteNonQuery();
                    }
                    foreach (var namestaj in dodat)
                    {
                        namestaj.AkcijskaCena = namestaj.Cena - ((namestaj.Cena * a.Popust) / 100);
                        NamestajDAO.IzmenaNamestaja(namestaj);
                        foreach (var n in Projekat.Instance.Namestaj)
                        {
                            if (n.Id == namestaj.Id)
                            {
                                n.AkcijskaCena = n.Cena - ((n.Cena * a.Popust) / 100);
                            }
                        }
                    }
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


        }
        public static bool BrisanjeSaAkcije(Akcija a, ObservableCollection<Namestaj> obrisan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    for (int i = 0; i < obrisan.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@" DELETE FROM NaAkciji WHERE NamestajId=@namestajId AND AkcijaId=@akcijaId", conn);
                        cm.Parameters.Add(new SqlParameter("@namestajId", obrisan[i].Id));
                        cm.Parameters.Add(new SqlParameter("@akcijaId", a.Id));
                        cm.ExecuteNonQuery();
                    }
                    foreach (var namestaj in obrisan)
                    {
                        namestaj.AkcijskaCena = 0;
                        NamestajDAO.IzmenaNamestaja(namestaj);
                        foreach (var n in Projekat.Instance.Namestaj)
                        {
                            if (n.Id == namestaj.Id)
                            {
                                n.AkcijskaCena = 0;
                            }
                        }
                    }
                    return true;
                }
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }

        }
        public static ObservableCollection<Akcija> PretraziAkcije(string tekst)
        {

            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Akcija  WHERE Obrisan=@obrisan AND
        (Datum_Pocetka LIKE @tekst OR Datum_Kraja LIKE @tekst OR Popust LIKE @tekst)", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%" + tekst + "%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Akcija a = new Akcija()
                    {
                        Id = reader.GetInt32(0),
                        PocetakAkcije = (DateTime)reader.GetDateTime(1),
                        KrajAkcije = (DateTime)reader.GetDateTime(2),
                        Popust = reader.GetInt32(3),
                        Obrisan = false
                    };

                    akcije.Add(a);
                }
                reader.Close();
                foreach (var akcija in akcije)
                {
                    ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
                    cmd = new SqlCommand(@"SELECT NamestajId FROM NaAkciji WHERE AkcijaId=@id ", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", akcija.Id));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        akcija.NamestajPopustId.Add(reader.GetInt32(0));
                    }
                    reader.Close();
                }
            }
            foreach (var akcija in akcije)
            {
                foreach (var a in akcija.NamestajPopustId)
                    akcija.NamestajPopust.Add(Namestaj.PronadjiNamestaj(a));
            }

            return akcije;
        }


    }
}