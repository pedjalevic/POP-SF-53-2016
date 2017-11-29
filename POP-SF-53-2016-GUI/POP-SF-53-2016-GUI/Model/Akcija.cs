using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
     public class Akcija : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
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

        private DateTime pocetakAkcije;

        public DateTime PocetakAkcije
        {
            get { return pocetakAkcije; }
            set
            {
                pocetakAkcije = value;
                OnPropertyChanged("PocetakAkcije");
            }
        }

        private DateTime krajAkcije;

        public DateTime KrajAkcije
        {
            get { return krajAkcije; }
            set
            {
                krajAkcije = value;
                OnPropertyChanged("KrajAkcije");
            }
        }

        private double popust;

        public double Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }

        private List<int> namestajPopustId;



        public List<int> NamestajPopustId
        {
            get { return namestajPopustId; }
            set
            {
                namestajPopustId = value;
                OnPropertyChanged("NamestajPopustId");
            }
        }
        private ObservableCollection<Namestaj> namestajPopust;
        [XmlIgnore]
        public ObservableCollection<Namestaj> NamestajPopust
        {
            get
            {
                if (namestajPopust == null)
                {
                    for (int i = 0; i < namestajPopustId.Count; id++)
                        namestajPopust.Add(Namestaj.PronadjiNamestaj(namestajPopustId[i]));
                }
                return namestajPopust;
            }
            set
            {
                namestajPopust = value;
                for (int i = 0; i < namestajPopust.Count; i++)
                    namestajPopustId.Add(namestajPopust[i].Id);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            if (!Obrisan)
            {


                string ispis = $"{Id}. {PocetakAkcije} {KrajAkcije} {Popust} ";

                for (int i = 0; i < namestajPopust.Count; i++)
                {
                    ispis += namestajPopust[i].Naziv + " ,";

                }
                return ispis;
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
        public static Akcija PronadjiAkciju(int id)
        {
            foreach (var akcija in Projekat.Instance.Akcije)
            {
                if (akcija.Id == id)
                {
                    return akcija;
                }

            }
            return null;
        }
    }
}
