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
using POP_SF_53_2016_GUI.Utils;
using POP_SF_53_2016_GUI.Data;
using POP_SF_53_2016_GUI.DAO;

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
            dgPrikaz.SelectedIndex = 0;
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;
        }

        private void btnProdaja_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Prodaja";
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Prodaja);
            view.Filter = ProdajaFlter;
            dgPrikaz.ItemsSource = view;
            btnIzmeni.Content = "Storniraj";
            btnObrisi.Visibility = Visibility.Hidden;
            btnIzlistajStavke.Visibility = Visibility.Visible;
            btnIzlistajStavke.Content = "Izlistaj Stavke";
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Akcije";
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcije);
            view.Filter = AkcijaFilter;
            dgPrikaz.ItemsSource = view;
            cbSortiraj.SelectedItem = null;
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Visible;
            btnIzlistajStavke.Content = "Izlistaj Namestaj";
            btnObrisi.Visibility = Visibility.Visible;
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "DodatneUsluge";
            cbSortiraj.SelectedItem = null;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
            view.Filter = UslugeFilter;
            dgPrikaz.ItemsSource = view;
            var ponudjeno = new List<string>() { "Naziv", "Cena" };
            cbSortiraj.ItemsSource = ponudjeno;
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;

        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Namestaj";
            cbSortiraj.SelectedItem = null;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajFilter;
            dgPrikaz.ItemsSource = view;
            var ponudjeno = new List<string>() { "Naziv", "Sifra", "Cena", "Kolicina", "Tip Namestaja" };
            cbSortiraj.ItemsSource = ponudjeno;
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;

        }

        private void btnTipoviNamestaja_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "TipoviNamestaja";
            cbSortiraj.SelectedItem = null;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.TipNamestaja);
            view.Filter = TipNamestajaFilter;
            dgPrikaz.ItemsSource = view;
            var ponudjeno = new List<string>() { "Naziv" };
            cbSortiraj.ItemsSource = ponudjeno;
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Korisnici";
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnici);
            view.Filter = KorisnikFilter;
            dgPrikaz.ItemsSource = view;
            cbSortiraj.SelectedItem = null;
            var ponudjeno = new List<string>() { "Ime", "Prezime", "Tip Korisnika", "Korisnicko Ime", "Lozinka" };
            cbSortiraj.ItemsSource = ponudjeno;
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;

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

        private bool NamestajFilter(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;
        }
        private bool TipNamestajaFilter(object obj)
        {
            return ((TipNamestaja)obj).Obrisan == false;
        }
        private bool KorisnikFilter(object obj)
        {
            return ((Korisnik)obj).Obrisan == false;
        }

        private bool UslugeFilter(object obj)
        {
            return ((DodatneUsluge)obj).Obrisan == false;
        }
        public bool ProdajaFlter(object obj)
        {
            return ((ProdajaNamestaja)obj).Obrisan == false;
        }
        public bool AkcijaFilter(object obj)
        {
            return ((Akcija)obj).Obrisan == false;
        }
        private void dgPrikaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "NamestajProdajaId" || (string)e.Column.Header == "DodatneUslugaId"
                || (string)e.Column.Header == "StavkaProdajeId" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "NamestajPopustId"
                || (string)e.Column.Header == "StavkeProdaje" || (string)e.Column.Header == "NamestajPopust" || (string)e.Column.Header == "DodatnaUslugaId" || (string)e.Column.Header == "DodatneUsluge")

            {
                e.Cancel = true;
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    Namestaj noviNamestaj = new Namestaj();
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(noviNamestaj, NamestajDodavanjeIzmena.Operacija.DODAVANJE);
                    ndi.ShowDialog();
                    break;
                case "TipoviNamestaja":
                    TipNamestaja noviTip = new TipNamestaja();
                    TipNamestajaDodavanjeIzmena tdi = new TipNamestajaDodavanjeIzmena(noviTip, TipNamestajaDodavanjeIzmena.Operacija.DODAVANJE);
                    tdi.ShowDialog();
                    break;
                case "DodatneUsluge":
                    DodatneUsluge usluga = new DodatneUsluge();
                    DodatneUslugeDodavanjeIzmene ddi = new DodatneUslugeDodavanjeIzmene(usluga, DodatneUslugeDodavanjeIzmene.Operacija.DODAVANJE);
                    ddi.ShowDialog();
                    break;
                case "Korisnici":
                    Korisnik korisnik = new Korisnik();
                    KorisniciDodavanjeIzmena dik = new KorisniciDodavanjeIzmena(korisnik, KorisniciDodavanjeIzmena.Operacija.DODAVANJE);
                    dik.ShowDialog();
                    break;
                case "Akcije":
                    Akcija akcija = new Akcija();
                    AkcijaDodavanjeIzmena dia = new AkcijaDodavanjeIzmena(akcija, AkcijaDodavanjeIzmena.Operacija.DODAVANJE);
                    dia.ShowDialog();
                    break;
                case "Prodaja":
                    ProdajaNamestaja prodaja = new ProdajaNamestaja();
                    ProdajaProzor pwd = new ProdajaProzor(prodaja, ProdajaProzor.Operacija.DODAVANJE);
                    pwd.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    Namestaj namestajIzmena = dgPrikaz.SelectedItem as Namestaj;
                    Namestaj namestajKopija = (Namestaj)namestajIzmena.Clone();
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(namestajKopija, NamestajDodavanjeIzmena.Operacija.IZMENA);
                    ndi.ShowDialog();
                    view.Refresh();
                    break;
                case "TipoviNamestaja":
                    TipNamestaja tipIzmena = dgPrikaz.SelectedItem as TipNamestaja;
                    TipNamestaja kopija = (TipNamestaja)tipIzmena.Clone();
                    TipNamestajaDodavanjeIzmena tdi = new TipNamestajaDodavanjeIzmena(kopija, TipNamestajaDodavanjeIzmena.Operacija.IZMENA);
                    tdi.ShowDialog();
                    view.Refresh();
                    break;
                case "DodatneUsluge":
                    DodatneUsluge usluga = dgPrikaz.SelectedItem as DodatneUsluge;
                    DodatneUsluge kopijaUsluge = (DodatneUsluge)usluga.Clone();
                    DodatneUslugeDodavanjeIzmene ddi = new DodatneUslugeDodavanjeIzmene(kopijaUsluge, DodatneUslugeDodavanjeIzmene.Operacija.IZMENA);
                    ddi.ShowDialog();
                    view.Refresh();
                    break;
                case "Korisnici":
                    Korisnik korisnik = dgPrikaz.SelectedItem as Korisnik;
                    Korisnik kopijaKorisnika = (Korisnik)korisnik.Clone();
                    KorisniciDodavanjeIzmena dik = new KorisniciDodavanjeIzmena(kopijaKorisnika, KorisniciDodavanjeIzmena.Operacija.IZMENA);
                    dik.ShowDialog();
                    view.Refresh();
                    break;
                case "Akcije":
                    Akcija akcija = dgPrikaz.SelectedItem as Akcija;
                    Akcija kopijaAkcije = (Akcija)akcija.Clone();
                    AkcijaDodavanjeIzmena dia = new AkcijaDodavanjeIzmena(kopijaAkcije, AkcijaDodavanjeIzmena.Operacija.IZMENA);
                    dia.ShowDialog();
                    view.Refresh();
                    break;
                case "Prodaja":
                    ProdajaNamestaja prodaja = dgPrikaz.SelectedItem as ProdajaNamestaja;
                    ProdajaNamestaja kopijaProdaje = (ProdajaNamestaja)prodaja.Clone();
                    ProdajaProzor pw = new ProdajaProzor(prodaja, ProdajaProzor.Operacija.IZMENA);
                    if (pw.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.Prodaja.IndexOf(prodaja);
                        Projekat.Instance.Prodaja[index] = kopijaProdaje;
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    var list = Projekat.Instance.Namestaj;
                    Namestaj namestajBrisanje = dgPrikaz.SelectedItem as Namestaj;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        namestajBrisanje.Obrisan = true;
                        NamestajDAO.BrisanjeNamestaja(namestajBrisanje);
                    }
                    view.Refresh();
                    break;
                case "TipoviNamestaja":
                    var lista = Projekat.Instance.TipNamestaja;
                    TipNamestaja tip = dgPrikaz.SelectedItem as TipNamestaja;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        tip.Obrisan = true;
                        TipNamestajaDAO.BrisanjeTipa(tip);
                    }
                    view.Refresh();
                    break;
                case "DodatneUsluge":
                    DodatneUsluge uslugaBrisanje = dgPrikaz.SelectedItem as DodatneUsluge;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        uslugaBrisanje.Obrisan = true;
                        UslugeDAO.BrisanjeUsluge(uslugaBrisanje);
                    }
                    view.Refresh();
                    break;
                case "Korisnici":
                    var listaKorisnika = Projekat.Instance.Korisnici;
                    var korisnikBrisanje = dgPrikaz.SelectedItem as Korisnik;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        korisnikBrisanje.Obrisan = true;
                        KorisnikDAO.BrisanjeKorisnika(korisnikBrisanje);
                    }
                    view.Refresh();
                    break;
                case "Akcije":
                    var listaAkcija = Projekat.Instance.Akcije;
                    Akcija akcijaBrisanje = dgPrikaz.SelectedItem as Akcija;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        akcijaBrisanje.Obrisan = true;
                        AkcijaDAO.BrisanjeAkcije(akcijaBrisanje);
                    }
                    view.Refresh();
                    break;
                case "Prodaja":
                    var listaProdaja = Projekat.Instance.Prodaja;
                    ProdajaNamestaja prodajaBrisanje = dgPrikaz.SelectedItem as ProdajaNamestaja;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        prodajaBrisanje.Obrisan = true;
                    }
                    view.Refresh();
                    break;
                default:
                    break;
            }

        }

        private void Izlistaj(object sender, RoutedEventArgs e)
        {
            ProdajaNamestaja pn = dgPrikaz.SelectedItem as ProdajaNamestaja;
            Akcija a = dgPrikaz.SelectedItem as Akcija;
            if (a == null)
            {
                IzlistajStavke izs = new IzlistajStavke(pn, null);
                izs.ShowDialog();
            }
            else
            {
                IzlistajStavke izs = new IzlistajStavke(null, a);
                izs.ShowDialog();
            }
        }

        private void cbSortiraj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    string izabranoN = cbSortiraj.SelectedItem as string;
                    izabranoN = izabranoN.Replace(" ", "_");
                    if (izabranoN == "Tip_Namestaja")
                        izabranoN = "TipNamestaja.Naziv";
                    if (izabranoN == "Naziv")
                        izabranoN = "Namestaj.Naziv";
                    view = CollectionViewSource.GetDefaultView(NamestajDAO.SortirajNamestaj(izabranoN));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "TipoviNamestaja":
                    string izabranoT = cbSortiraj.SelectedItem as string;
                    view = CollectionViewSource.GetDefaultView(TipNamestajaDAO.SortirajTipove(izabranoT));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "DodatneUsluge":
                    string izabranoU = cbSortiraj.SelectedItem as string;
                    view = CollectionViewSource.GetDefaultView(UslugeDAO.SortirajUsluge(izabranoU));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "Korisnici":
                    string izabranoK = cbSortiraj.SelectedItem as string;
                    if (izabranoK != null)
                    {
                        izabranoK = izabranoK.Replace(" ", "_");
                        view = CollectionViewSource.GetDefaultView(KorisnikDAO.SortirajKorisnika(izabranoK));
                        dgPrikaz.ItemsSource = view;
                    }
                    break;
                case "Akcije":
                    break;
                case "Prodaja":
                    break;
                default:
                    break;
            }
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    string unetoN = tbPretraga.Text.Trim();
                    view = CollectionViewSource.GetDefaultView(NamestajDAO.PretraziNamestaj(unetoN));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "TipoviNamestaja":
                    string unetoT = tbPretraga.Text.Trim();
                    view = CollectionViewSource.GetDefaultView(TipNamestajaDAO.PretraziTipove(unetoT));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "DodatneUsluge":
                    string unetoU = tbPretraga.Text.Trim();
                    view = CollectionViewSource.GetDefaultView(UslugeDAO.PretraziUsluge(unetoU));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "Korisnici":
                    string unetoK = tbPretraga.Text.Trim();
                    view = CollectionViewSource.GetDefaultView(KorisnikDAO.PretragaKorisnika(unetoK));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "Akcije":
                    string unetoA = tbPretraga.Text.Trim();
                    view = CollectionViewSource.GetDefaultView(AkcijaDAO.PretraziAkcije(unetoA));
                    dgPrikaz.ItemsSource = view;
                    break;
                case "Prodaja":
                    break;
                default:
                    break;
            }
        }
    }
}
