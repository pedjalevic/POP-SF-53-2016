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
    /// Interaction logic for ProdajaProzor.xaml
    /// </summary>
    public partial class ProdajaProzor : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private ProdajaNamestaja prodaja;
        private Operacija operacija;
        public ProdajaProzor(ProdajaNamestaja prodaja, Operacija operacija)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            this.operacija = operacija;
            dgStavke.ItemsSource = prodaja.StavkeProdaje;
            tbKupac.DataContext = prodaja;
            dpDatum.DataContext = prodaja;
        }

        private void DodajStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje stavka = new StavkaProdaje();
            StavkeProzor st = new StavkeProzor(stavka, StavkeProzor.Operacija.DODAVANJE);
            if (st.ShowDialog() == true)
                prodaja.StavkeProdaje.Add(st.Stavka);
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Random rn = new Random();
            this.DialogResult = true;
            var lista = Projekat.Instance.Prodaja;
            if (operacija == Operacija.DODAVANJE)
            {
                prodaja.Id = lista.Count + 1;
                prodaja.BrojRacuna = rn.Next(100, 10000);
                prodaja.UkupanIznos = prodaja.StavkeProdaje.Sum(item => item.Cena);
                lista.Add(prodaja);
            }
            GenericSerializer.Serialize("ProdajaNamestaja.xml", lista);
            this.Close();
        }

        private void dgStavke_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "DodatnaUslugaId" || (string)e.Column.Header == "NamestajProdajaId" || (string)e.Column.Header == "Id"
                || (string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void UkloniStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje izabrana = dgStavke.SelectedItem as StavkaProdaje;
            prodaja.StavkeProdaje.Remove(izabrana);

        }
    }
}
