using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
    public class Salon : INotifyPropertyChanged, ICloneable
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            Salon s = new Salon();
            s.Id = Id;
            s.Naziv = Naziv;
            s.Adresa = Adresa;
            s.Broj_telefona = Broj_telefona;
            s.Adresa_sajta = Adresa_sajta;
            s.PIB = PIB;
            s.Maticni_broj = Maticni_broj;
            s.Broj_ziro_racuna = Broj_ziro_racuna;
            s.Email = Email;

            return s;
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }



        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        private string adresa;

        public string Adresa
        {
            get { return adresa; }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }

        private string broj_telefona;

        public string Broj_telefona
        {
            get { return broj_telefona; }
            set
            {
                broj_telefona = value;
                OnPropertyChanged("Broj_telefona");
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        private string adresa_sajta;

        public string Adresa_sajta
        {
            get { return adresa_sajta; }
            set
            {
                adresa_sajta = value;
                OnPropertyChanged("Adresa_sajta");
            }
        }

        private string pib;
        public string PIB
        {
            get { return pib; }
            set
            {
                pib = value;
                OnPropertyChanged("PIB");
            }
        }

        private int maticni_broj;

        public int Maticni_broj
        {
            get { return maticni_broj; }
            set
            {
                maticni_broj = value;
                OnPropertyChanged("Maticni_broj");
            }
        }

        private string broj_ziro_racuna;

        public string Broj_ziro_racuna
        {
            get { return broj_ziro_racuna; }
            set
            {
                broj_ziro_racuna = value;
                OnPropertyChanged("Broj_ziro_racuna");
            }
        }


    }
}
