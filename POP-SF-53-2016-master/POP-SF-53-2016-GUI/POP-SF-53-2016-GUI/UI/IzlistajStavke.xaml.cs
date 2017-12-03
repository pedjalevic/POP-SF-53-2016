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
        ProdajaNamestaja prodaja;
        public IzlistajStavke(ProdajaNamestaja prodaja)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            dgIspisStavki.ItemsSource = prodaja.StavkeProdaje;
            dgIspisStavki.SelectedIndex = 0;
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgIspisStavki_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "NamestajProdajaId" ||
                (string)e.Column.Header == "DodatnaUslugaId" || (string)e.Column.Header == "Obrisan")
                e.Cancel = true;
        }
    }
}
