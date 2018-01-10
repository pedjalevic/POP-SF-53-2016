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
    class ProdajaDAO
    {
        public static ObservableCollection<ProdajaNamestaja> SveProdaje()
        {
            ObservableCollection<ProdajaNamestaja> prodaje = new ObservableCollection<ProdajaNamestaja>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT Id,Kupac,Broj_Racuna,Datum_Prodaje,Ukupan_Iznos FROM Prodaja WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ProdajaNamestaja p = new ProdajaNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Kupac = reader.GetString(1),
                        BrojRacuna = reader.GetInt32(2),
                        DatumProdaje = (DateTime)reader.GetDateTime(3),
                        UkupanIznos = (double)reader.GetDecimal(4),
                        Obrisan = false


                    };

                    prodaje.Add(p);
                }
                reader.Close();
                foreach (var prodaja in prodaje)
                {
                    ObservableCollection<StavkaProdaje> stavke = new ObservableCollection<StavkaProdaje>();
                    cmd = new SqlCommand(@"SELECT Id, Kolicina,Cena,NamestajId FROM Stavka WHERE ProdajaId=@id AND Obrisan=@obrisan", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", prodaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StavkaProdaje s = new StavkaProdaje()
                        {
                            Id = reader.GetInt32(0),
                            Kolicina = reader.GetInt32(1),
                            Cena = (double)reader.GetDecimal(2),
                            NamestajProdaja = NamestajDAO.NametajPoId(reader.GetInt32(3)),
                            Obrisan = false
                        };
                        stavke.Add(s);
                    }
                    prodaja.StavkeProdaje = stavke;
                    reader.Close();
                }
                reader.Close();
                foreach (var prodaja in prodaje)
                {
                    ObservableCollection<DodatneUsluge> usluge = new ObservableCollection<DodatneUsluge>();
                    cmd = new SqlCommand(@"SELECT UslugeId FROM ProdateUsluge WHERE ProdajaId=@id AND Obrisan=@obrisan", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", prodaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        if (UslugeDAO.UslugaPoId(reader.GetInt32(0)) != null)
                            usluge.Add(UslugeDAO.UslugaPoId(reader.GetInt32(0)));
                    }
                    prodaja.DodatneUsluge = usluge;
                    reader.Close();
                }
            }

            return prodaje;
        }
        public static ProdajaNamestaja DodajProdaju(ProdajaNamestaja p)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Prodaja(Kupac,Broj_Racuna,Datum_Prodaje,Ukupan_Iznos,Obrisan) VALUES(@kupac,@brojR,@datumProdaje,@ukupanIznos,@obrisan) ", conn);
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@kupac", p.Kupac));
                cmd.Parameters.Add(new SqlParameter("@brojR", p.BrojRacuna));
                cmd.Parameters.Add(new SqlParameter("@datumProdaje", p.DatumProdaje));
                cmd.Parameters.Add(new SqlParameter("@ukupanIznos", p.UkupanIznos));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                p.Id = newId;

                for (int i = 0; i < p.StavkeProdaje.Count; i++)
                {
                    SqlCommand cm = new SqlCommand(@"INSERT INTO Stavke(Kolicina,Cena,NamestajId,ProdajaId,Obrisn) VALUES(@kolicina,@cena,@namestajId,@prodajaId,@obrisan) ", conn);
                    cm.Parameters.Add(new SqlParameter("@kolicina", p.StavkeProdaje[i].Kolicina));
                    cm.Parameters.Add(new SqlParameter("@cena", p.StavkeProdaje[i].Cena));
                    cm.Parameters.Add(new SqlParameter("@namestajId", p.StavkeProdaje[i].NamestajProdaja.Id));
                    cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                    cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    cm.ExecuteNonQuery();
                }

                for (int i = 0; i < p.DodatneUsluge.Count; i++)
                {
                    SqlCommand cm = new SqlCommand(@"INSERT INTO ProdateUsluge(UslugeId,ProdajaId,Obrisn) VALUES(@usluge,@prodajaId,@obrisan) ", conn);
                    cm.Parameters.Add(new SqlParameter("@namestajId", p.DodatneUsluge[i].Id));
                    cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                    cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    cm.ExecuteNonQuery();
                }
            }
            Projekat.Instance.Prodaja.Add(p);
            return p;
        }
        public static bool IzmenaProdaje(ProdajaNamestaja p)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Prodaja SET Kupac=@kupac,Datum_Prodaje=@datum WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@Kupac", p.Kupac));
                cmd.Parameters.Add(new SqlParameter("@datum", p.DatumProdaje));
                cmd.Parameters.Add(new SqlParameter("@id", p.Id));
                cmd.ExecuteNonQuery();

                foreach (var item in Projekat.Instance.Prodaja)
                {
                    if (item.Id == p.Id)
                    {
                        item.Kupac = p.Kupac;
                        item.DatumProdaje = p.DatumProdaje;
                        item.StavkeProdaje = p.StavkeProdaje;
                        item.DodatneUsluge = p.DodatneUsluge;
                        item.UkupanIznos = p.UkupanIznos;
                    }
                }
                return true;
            }
        }

        public static ProdajaNamestaja ObrisiStavku(ProdajaNamestaja p, StavkaProdaje stavka)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cm = new SqlCommand(@" UPDATE  Stavka SET Obrisan=@obrisan WHERE Id=@id AND ProdajaId=@prodajaId", conn);
                cm.Parameters.Add(new SqlParameter("@id", stavka.Id));
                cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                cm.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cm.ExecuteNonQuery();

            }
            p.StavkeProdaje.Remove(stavka);
            return p;
        }
        public static ProdajaNamestaja DodajStavku(ProdajaNamestaja p, StavkaProdaje stavka)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cm = new SqlCommand(@" INSERT INTO Stavka(Kolicina,Cena,NamestajId,ProdajaId,Obrisan) VALUES (@kolicina,@cena,@namestajId,@prodajaId,@obrisan)", conn);
                cm.Parameters.Add(new SqlParameter("@namestajId", stavka.NamestajProdaja.Id));
                cm.Parameters.Add(new SqlParameter("@kolicina", stavka.Kolicina));
                cm.Parameters.Add(new SqlParameter("@cena", stavka.Cena));
                cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cm.ExecuteNonQuery();

            }
            p.StavkeProdaje.Add(stavka);
            return p;
        }
        public static ProdajaNamestaja ObrisiUslugu(ProdajaNamestaja p, DodatneUsluge usluga)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cm = new SqlCommand(@" UPDATE  ProdateUsluge SET Obrisan=@obrisan WHERE UslugeId=@id AND ProdajaId=@prodajaId", conn);
                cm.Parameters.Add(new SqlParameter("@id", usluga.Id));
                cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                cm.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cm.ExecuteNonQuery();

            }
            p.DodatneUsluge.Remove(usluga);
            return p;
        }
        public static ProdajaNamestaja DodajUslugu(ProdajaNamestaja p, DodatneUsluge usluga)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cm = new SqlCommand(@" INSERT INTO ProdateUsluge(UslugaId,ProdajaId,Obrisan) VALUES (@uslugeId,@prodajaId,@obrisan)", conn);
                cm.Parameters.Add(new SqlParameter("@uslugeId", usluga.Id));
                cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cm.ExecuteNonQuery();

            }
            p.DodatneUsluge.Add(usluga);
            return p;
        }
    }
}
