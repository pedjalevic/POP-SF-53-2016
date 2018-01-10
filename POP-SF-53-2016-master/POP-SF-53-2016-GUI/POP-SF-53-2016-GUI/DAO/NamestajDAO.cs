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
    public class NamestajDAO
    {
        public static ObservableCollection<Namestaj> SavNamestaj()
        {
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj  WHERE Obrisan=@obrisan", conn);
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
                        TipNamestajaId = reader.GetInt32(4),
                        Cena = (double)reader.GetDecimal(5),
                        AkcijskaCena = (double)reader.GetDecimal(6)
                    };
                    namestaj.Add(n);
                }
            }
            return namestaj;
        }
        public static Namestaj DodavanjeNamestaja(Namestaj n)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" INSERT INTO Namestaj(Naziv,Kolicina,Sifra,Tip_Namestaja,Cena,AkcijskaCena,Obrisan) VALUES (@naziv,@kolicina,@sifra,@tip,@cena,@akcijskaCena,@Obrisan)", conn);
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.Add(new SqlParameter("@naziv", n.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@kolicina", n.Kolicina));
                    cmd.Parameters.Add(new SqlParameter("@sifra", n.Sifra));
                    cmd.Parameters.Add(new SqlParameter("@tip", n.TipNamestaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@cena", n.Cena));
                    cmd.Parameters.Add(new SqlParameter("@akcijskaCena", n.AkcijskaCena));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                    int newId = int.Parse(cmd.ExecuteScalar().ToString());
                    n.Id = newId;
                }
                Projekat.Instance.Namestaj.Add(n);

                return n;
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return null; }



        }
        public static bool BrisanjeNamestaja(Namestaj n)
        {
            n.Obrisan = true;
            return IzmenaNamestaja(n);
        }

        public static bool IzmenaNamestaja(Namestaj n)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" UPDATE Namestaj SET Naziv=@naziv,Kolicina=@kolicina,Sifra=@sifra,Tip_Namestaja=@tip,Cena=@cena,AkcijskaCena=@akcijskaCena,Obrisan=@obrisan WHERE Id=@id", conn);
                    cmd.Parameters.Add(new SqlParameter("@naziv", n.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@kolicina", n.Kolicina));
                    cmd.Parameters.Add(new SqlParameter("@sifra", n.Sifra));
                    if (n.TipNamestaja == null)
                        cmd.Parameters.Add(new SqlParameter("@tip", '0'));
                    else
                    cmd.Parameters.Add(new SqlParameter("@tip", n.TipNamestaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@cena", n.Cena));
                    cmd.Parameters.Add(new SqlParameter("@akcijskaCena", n.AkcijskaCena));
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
                        item.Cena = n.Cena;
                        item.AkcijskaCena = n.AkcijskaCena;
                        item.Obrisan = n.Obrisan;
                    }
                }
                return true;
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }

        }


        public static ObservableCollection<Namestaj> PretraziNamestaj(string tekst)
        {
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj JOIN TipNamestaja ON Namestaj.Tip_Namestaja=TipNamestaja.Id WHERE Namestaj.Obrisan=@obrisan AND(Namestaj.Naziv LIKE @tekst OR Kolicina LIKE @tekst
                 OR Sifra LIKE @tekst OR Cena LIKE @tekst OR TipNamestaja.Naziv LIKE @tekst)", conn);
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
                        TipNamestajaId = reader.GetInt32(4),
                        Cena = (double)reader.GetDecimal(5),
                        AkcijskaCena = (double)reader.GetDecimal(6)
                    };
                    namestaj.Add(n);
                }
            }
            foreach (var tip in Projekat.Instance.TipNamestaja)
            {
                foreach (var nam in namestaj)
                {
                    if (nam.TipNamestajaId == tip.Id)
                        nam.TipNamestaja = tip;
                }
            }

            return namestaj;
        }
    }
}