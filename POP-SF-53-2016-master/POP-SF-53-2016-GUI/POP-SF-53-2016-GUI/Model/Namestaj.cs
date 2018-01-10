using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POP_SF_53_2016_GUI.Model;
using System.ComponentModel;
using System.Xml.Serialization;

namespace POP_SF_53_2016_GUI.Model
{
    public class Namestaj : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private string naziv;
        private double cena;
        private bool obrisan;
        private string sifra;
        private int kolicina;
        private TipNamestaja tipNamestaja;
        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public string Sifra
        {
            get { return sifra; }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set
            {
                kolicina = value;
                OnPropertyChanged("Kolicina");
            }
        }

        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }


        private int tipNamestajaId;

        public int TipNamestajaId
        {
            get { return tipNamestajaId; }
            set { tipNamestajaId = value; }
        }


        public TipNamestaja TipNamestaja
        {
            get
            {
                if (tipNamestaja == null)
                    tipNamestaja = TipNamestaja.PronadjiTip(tipNamestajaId);
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                if (TipNamestaja != null)
                    TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
            }
        }








        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }






        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");

            }
        }

        private double akcijskaCena;

        public double AkcijskaCena
        {
            get { return akcijskaCena; }
            set
            {
                akcijskaCena = value;
                OnPropertyChanged("AkcijskaCena");
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            if (!Obrisan)
            {
                string ispis = "";
                return ispis += $"{Naziv}";
            }
            return null;


        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static Namestaj PronadjiNamestaj(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }

            }
            return null;
        }

        public object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Kolicina = Kolicina;
            kopija.Sifra = Sifra;
            kopija.Cena = Cena;
            kopija.TipNamestaja = TipNamestaja;
            kopija.AkcijskaCena = AkcijskaCena;
            return kopija;
        }
    }
}
