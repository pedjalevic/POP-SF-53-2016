using POP_SF_53_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF_53_2016_GUI.DAO
{
    class SalonDAO
    {
        public static Salon PrikazPodataka()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Salon", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Salon s = new Salon()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Adresa = reader.GetString(2),
                        Broj_telefona = reader.GetString(3),
                        Email = reader.GetString(4),
                        Adresa_sajta = reader.GetString(5),
                        PIB = reader.GetString(6),
                        Maticni_broj = reader.GetInt32(7),
                        Broj_ziro_racuna = reader.GetString(8)


                    };
                    return s;
                }
                return null;

            }
        }
        public static bool IzmenaSalona(Salon s)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE Salon SET Naziv=@naziv,Adresa=@adresa,Broj_telefona=@brojT
                ,Email=@email,Adresa_sajta=@adresaS,PIB=@pib,Maticni_broj=@maticni,Broj_ziro_racuna=@ziroRacun", conn);
                    cmd.Parameters.Add(new SqlParameter("@naziv", s.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@adresa", s.Adresa));
                    cmd.Parameters.Add(new SqlParameter("@brojT", s.Broj_telefona));
                    cmd.Parameters.Add(new SqlParameter("@email", s.Email));
                    cmd.Parameters.Add(new SqlParameter("@adresaS", s.Adresa_sajta));
                    cmd.Parameters.Add(new SqlParameter("@pib", s.PIB));
                    cmd.Parameters.Add(new SqlParameter("@maticni", s.Maticni_broj));
                    cmd.Parameters.Add(new SqlParameter("@ziroRacun", s.Broj_ziro_racuna));
                    cmd.ExecuteNonQuery();

                    var item = Projekat.Instance.Salon;
                    item.Id = s.Id;
                    item.Naziv = s.Naziv;
                    item.Adresa = s.Adresa;
                    item.Broj_telefona = s.Broj_telefona;
                    item.Email = s.Email;
                    item.Adresa_sajta = s.Adresa_sajta;
                    item.PIB = s.PIB;
                    item.Maticni_broj = s.Maticni_broj;
                    item.Broj_ziro_racuna = s.Broj_ziro_racuna;
                    return true;


                }
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }



        }
    }
}