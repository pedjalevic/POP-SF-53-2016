using POP_SF_53_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF_53_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for IzlistajStavke.xaml
    /// </summary>
    public partial class IzlistajStavke : Window
    {
        public ProdajaNamestaja prodaja;
        public Akcija akcija;
        public IzlistajStavke(ProdajaNamestaja prodaja = null, Akcija akcija = null)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            this.akcija = akcija;
            if (prodaja != null)
            {
                dgAkcijePrikaz.Visibility = Visibility.Hidden;
                tbIspis.Text = IspisRacuna();
            }

            else if (akcija != null)
            {
                tbIspis.Visibility = Visibility.Hidden;
                ScrollBar.Visibility = Visibility.Hidden;
                dgAkcijePrikaz.ItemsSource = akcija.NamestajPopust;
            }

        }

        public string IspisRacuna()
        {
            string ispis = "";
            var radnik = Korisnik.PronadjiKorisnika(MainWindow.loggedUser);
            string crtice2 = new String('=', 40);
            string crtice = new String('-', 68);
            Salon s = Projekat.Instance.Salon;
            ispis += "" + crtice2 + "\n" +
                "\t" + s.Naziv + "\n" +
                "" + s.Adresa + "\n" +
                "PIB:" + s.PIB + "\n" +
                "Broj racuna: " + prodaja.BrojRacuna + "\n" +
                "" + crtice2 + "\n";
            if (prodaja.StavkeProdaje != null && prodaja.StavkeProdaje.Count > 0)
                foreach (var stavka in prodaja.StavkeProdaje)
                {
                    if (stavka.NamestajProdaja != null)
                    {
                        ispis += "" + stavka.NamestajProdaja.Naziv + "\n" + "\t\t" + stavka.Kolicina + "x";
                        if (stavka.NamestajProdaja.AkcijskaCena > 0)
                            ispis += " " + stavka.NamestajProdaja.AkcijskaCena + "\t\t" + stavka.Cena + "\n";
                        else
                            ispis += " " + stavka.NamestajProdaja.Cena + "\t\t" + stavka.Cena + "\n";
                    }
                }
            if (prodaja.DodatneUsluge.Count == 0)
                ispis += crtice + "-----";
            else
                ispis += crtice;
            foreach (var usluga in prodaja.DodatneUsluge)
            {
                ispis += "" + usluga.Naziv + "\t\t\t\t\t" + usluga.Cena + "\n";

            }
            ispis += crtice;
            ispis += "PDV: " + 20 + "%" + "\n";
            ispis += "Ukupan iznos: " + prodaja.UkupanIznos + "\n";
            ispis += "Iznos sa PDV-om: " + (prodaja.UkupanIznos * 1.2) + "\n";
            ispis += "Datum: " + prodaja.DatumProdaje + "\n";
            ispis += "Prodavac: " + radnik.Ime + " " + radnik.Prezime + "\n";
            ispis += crtice2;
            return ispis;
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgAkcijePrikaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "TipNamestajaId")
                e.Cancel = true;

        }
    }
}
