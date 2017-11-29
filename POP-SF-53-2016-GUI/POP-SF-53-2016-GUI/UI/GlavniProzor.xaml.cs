using System;
using POP_SF_53_2016_GUI.Model;
using POP_SF_53_2016_GUI.UI;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window
    {
        ICollectionView view;
        public static string TrenutnoAktivno;
        public GlavniProzor()
        {
            InitializeComponent();
            ProveraprijavljenogKorisnika();
            dgPrikaz.IsSynchronizedWithCurrentItem = true;
        }

        private void btnProdaja_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Prodaja";
            dgPrikaz.ItemsSource = Projekat.Instance.Prodaja;
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Akcije";
            dgPrikaz.ItemsSource = Projekat.Instance.Akcije;
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "DodatneUsluge";
            dgPrikaz.ItemsSource = Projekat.Instance.DodatneUsluge;
        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Namestaj";
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajIspis;
            dgPrikaz.ItemsSource = view;

        }

        private void btnTipoviNamestaja_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "TipoviNamestaja";
            dgPrikaz.ItemsSource = Projekat.Instance.TipNamestaja;
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Korisnici";
            dgPrikaz.ItemsSource = Projekat.Instance.Korisnici;
        }
        private void ProveraprijavljenogKorisnika()
        {
            var korisnik = Korisnik.PronadjiKorisnika(MainWindow.loggedUser);
            if (korisnik.TipKorisnika != TipKorisnika.Administrator)
            {
                btnAkcije.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipoviNamestaja.Visibility = Visibility.Hidden;
                btnAkcije.Visibility = Visibility.Hidden;
            }


        }
        public bool NamestajIspis(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;
        }
        private void dgPrikaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "NamestajProdajaId" || (string)e.Column.Header == "DodatneUslugaId"
                || (string)e.Column.Header == "StavkaProdajeId" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "NamestajPopustId")

            {
                e.Cancel = true;
            }
        }
    }
}
