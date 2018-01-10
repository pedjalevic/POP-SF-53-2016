using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF_53_2016_GUI.Model
{
    public class StavkaProdaje : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private double cena;



        private int kolicina;

        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }

        private int namestajProdajaId;

        public int NamestajProdajaId
        {
            get { return namestajProdajaId; }
            set { namestajProdajaId = value; }
        }

        private Namestaj namestajProdaja;

        public event PropertyChangedEventHandler PropertyChanged;
        [XmlIgnore]
        public Namestaj NamestajProdaja

        {
            get
            {
                return namestajProdaja;
            }
            set
            {
                namestajProdaja = value;
                OnPropertyChanged("NamestajProdaja");
            }

        }


        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public double Cena
        {
            get
            {
                if (NamestajProdaja != null)
                {
                    if (NamestajProdaja.AkcijskaCena != 0)
                        return cena = NamestajProdaja.AkcijskaCena * Kolicina;
                    else
                        return cena = NamestajProdaja.Cena * Kolicina;
                }
                else
                    return 0;
            }
            set
            {
                if (NamestajProdaja != null)
                    cena = NamestajProdaja.Cena * Kolicina;
                OnPropertyChanged("Cena");
            }
        }
        public static StavkaProdaje PronadjiStavku(int id)
        {
            foreach (var stavka in Projekat.Instance.StavkeProdaje)
            {
                if (stavka.Id == id)
                {
                    return stavka;
                }

            }
            return null;
        }
    }
}