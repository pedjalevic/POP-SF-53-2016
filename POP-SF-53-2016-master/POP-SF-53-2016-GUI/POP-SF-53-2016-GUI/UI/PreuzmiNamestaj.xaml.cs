using POP_SF_53_2016_GUI.DAO;
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
    /// Interaction logic for PreuzmiNamestaj.xaml
    /// </summary>
    public partial class PreuzmiNamestaj : Window
    {
        public Namestaj Namestaj { get; set; }
        public PreuzmiNamestaj()
        {
            InitializeComponent();
            dgNamestajPreuzimanje.ItemsSource = Projekat.Instance.Namestaj.Where(a => a.AkcijskaCena == 0);
            dgNamestajPreuzimanje.SelectedIndex = 0;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            var izabrana = dgNamestajPreuzimanje.SelectedItem as Namestaj;
            Namestaj = izabrana;
            this.DialogResult = true;
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void dgNamestajPreuzimanje_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "TipNamestajaId")
                e.Cancel = true;
        }
    }
}
