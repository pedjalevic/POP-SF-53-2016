using POP_SF_53_2016_GUI.DAO;
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
            dgUsluge.ItemsSource = prodaja.DodatneUsluge;
            tbKupac.DataContext = prodaja;
            dpDatum.DataContext = prodaja;
        }

        private void DodajStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje stavka = new StavkaProdaje();
            StavkeProzor st = new StavkeProzor(stavka, StavkeProzor.Operacija.DODAVANJE);
            if (st.ShowDialog() == true)
            {
                prodaja = ProdajaDAO.DodajStavku(prodaja, st.Stavka);
            }
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Random rn = new Random();
            this.DialogResult = true;
            prodaja.UkupanIznos = prodaja.StavkeProdaje.Sum(item => item.Cena) + prodaja.DodatneUsluge.Sum(item => item.Cena);
            if (operacija == Operacija.DODAVANJE)
            {
                prodaja.BrojRacuna = rn.Next(100, 10000);
                ProdajaDAO.DodajProdaju(prodaja);
            }
            ProdajaDAO.IzmenaProdaje(prodaja);

            this.Close();
        }

        private void dgStavke_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "NamestajProdajaId" || (string)e.Column.Header == "Id"
                           || (string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void UkloniStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje izabrana = dgStavke.SelectedItem as StavkaProdaje;
            prodaja = ProdajaDAO.ObrisiStavku(prodaja, izabrana);

        }

        private void btnDodajU_Click(object sender, RoutedEventArgs e)
        {
            PreuzmiUslugu pu = new PreuzmiUslugu();
            if (pu.ShowDialog() == true)
            {

                prodaja = ProdajaDAO.DodajUslugu(prodaja, pu.Usluge);

            }
        }

        private void btnObisiU_Click(object sender, RoutedEventArgs e)
        {
            var izabrana = dgUsluge.SelectedItem as DodatneUsluge;
            prodaja.DodatneUsluge.Remove(izabrana);
            prodaja = ProdajaDAO.ObrisiUslugu(prodaja, izabrana);
        }

        private void dgUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan")
                e.Cancel = true;
        }
    }
}
