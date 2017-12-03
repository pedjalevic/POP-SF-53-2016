using POP_SF_53_2016_GUI.Model;
using POP_SF_53_2016_GUI.Utils;
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
    /// Interaction logic for StavkeProzor.xaml
    /// </summary>
    public partial class StavkeProzor : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public StavkaProdaje Stavka { set; get; }
        private Operacija operacija;
        public StavkeProzor(StavkaProdaje stavka, Operacija operacija)
        {
            InitializeComponent();
            Stavka = stavka;
            this.operacija = operacija;
            dgNamestaj.ItemsSource = NamestajPrikaz();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.SelectedIndex = 0;
            cbUsluga.ItemsSource = UslugePrikaz();
            cbUsluga.DataContext = Stavka;
            tbKolicina.DataContext = Stavka;
        }
        public List<DodatneUsluge> UslugePrikaz()
        {
            var usluge = Projekat.Instance.DodatneUsluge;
            List<DodatneUsluge> zaPrikaz = new List<DodatneUsluge>();
            foreach (var usluga in usluge)
            {
                if (usluga.Obrisan == false)
                    zaPrikaz.Add(usluga);


            }
            return zaPrikaz;
        }
        public List<Namestaj> NamestajPrikaz()
        {
            var namestaj = Projekat.Instance.Namestaj;
            List<Namestaj> zaPrikaz = new List<Namestaj>();
            foreach (var trenutni in namestaj)
            {
                if (trenutni.Obrisan == false)
                    zaPrikaz.Add(trenutni);


            }
            return zaPrikaz;
        }
        private void PotvrdiUslugu(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var lista = Projekat.Instance.StavkeProdaje;
            if (operacija == Operacija.DODAVANJE)
            {
                Stavka.Id = lista.Count + 1;
                Stavka.NamestajProdaja = dgNamestaj.SelectedItem as Namestaj;
                Stavka.Cena = Stavka.DodatneUsluge.Cena + Stavka.NamestajProdaja.Cena * Stavka.Kolicina;
                lista.Add(Stavka);

            }
            GenericSerializer.Serialize("stavka_prodaje.xml", lista);
            this.Close();
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "TipNamestajaId" ||
            (string)e.Column.Header == "DodatneUslugaId" || (string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
