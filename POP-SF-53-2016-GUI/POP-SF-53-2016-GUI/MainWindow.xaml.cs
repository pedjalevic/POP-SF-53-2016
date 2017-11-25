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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF_53_2016_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Prijava(object sender, RoutedEventArgs e)
        {
            {
                var korisnici = Projekat.Instance.Korisnici;
                foreach (var korisnik in korisnici)
                {
                    var userName = tbKorisnickoIme.Text.Trim();
                    var password = pbSifra.Password.Trim();
                    if (userName == "" || password == "")
                    {
                        MessageBox.Show("Morate uneti sve podatke!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else if (userName == korisnik.KorisnickoIme && password == korisnik.Lozinka)
                    {
                        MessageBox.Show("Uneti podaci su tacni", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                }
                MessageBox.Show("Uneti podaci nisu tacni", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
