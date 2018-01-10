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
    /// Interaction logic for PreuzmiUslugu.xaml
    /// </summary>
    public partial class PreuzmiUslugu : Window
    {
        public DodatneUsluge Usluge { get; set; }
        public PreuzmiUslugu()
        {
            InitializeComponent();
            dgUsluga.ItemsSource = Projekat.Instance.DodatneUsluge;
            dgUsluga.SelectedIndex = 0;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            var izabrana = dgUsluga.SelectedItem as DodatneUsluge;
            Usluge = izabrana;
            this.DialogResult = true;
            this.Close();
        }

        private void dgUsluga_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan")
                e.Cancel = true;
        }
    }
}
