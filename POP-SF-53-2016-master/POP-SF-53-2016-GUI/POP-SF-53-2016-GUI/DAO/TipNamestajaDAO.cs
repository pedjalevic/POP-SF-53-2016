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
   public class TipNamestajaDAO
    {
        public static ObservableCollection<TipNamestaja> SviTipovi()
        {
            ObservableCollection<TipNamestaja> tipovi = new ObservableCollection<TipNamestaja>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM TipNamestaja WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipNamestaja tip = new TipNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1)
                    };
                    tipovi.Add(tip);
                }
            }
            return tipovi;
        }
        public static TipNamestaja DodavanjeTipa(TipNamestaja t)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" INSERT INTO TipNamestaja(Naziv,Obrisan) VALUES (@naziv,@Obrisan)", conn);
                    cmd.Parameters.Add(new SqlParameter("@naziv", t.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    int newId = int.Parse(cmd.ExecuteScalar().ToString());
                    t.Id = newId;
                }
                Projekat.Instance.TipNamestaja.Add(t);
                return t;
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return null; }

        }
        public static bool BrisanjeTipa(TipNamestaja t)
        {
            t.Obrisan = true;
            foreach (var n in Projekat.Instance.Namestaj)
            {
                if (n.TipNamestaja.Id == t.Id)
                {
                    n.Obrisan = true;
                    NamestajDAO.BrisanjeNamestaja(n);
                }
            }
            return IzmenaTipa(t);
        }
        public static bool IzmenaTipa(TipNamestaja t)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" UPDATE TipNamestaja SET Naziv=@naziv, Obrisan=@obrisan WHERE Id=@id", conn);
                    cmd.Parameters.Add(new SqlParameter("@naziv", t.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@Id", t.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", t.Obrisan));
                    cmd.ExecuteNonQuery();

                }
                foreach (var item in Projekat.Instance.TipNamestaja)
                {
                    if (item.Id == t.Id)
                    {
                        item.Id = t.Id;
                        item.Naziv = t.Naziv;
                        item.Obrisan = t.Obrisan;
                    }
                }
                return true;
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }

        }
        public static ObservableCollection<TipNamestaja> PretraziTipove(string tekst)
        {
            ObservableCollection<TipNamestaja> tipovi = new ObservableCollection<TipNamestaja>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM TipNamestaja WHERE Obrisan=@obrisan AND Naziv LIKE @tekst", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%" + tekst + "%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipNamestaja tip = new TipNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1)
                    };
                    tipovi.Add(tip);
                }
            }
            return tipovi;
        }

    }
}
