﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
    public class DodatneUsluge : INotifyPropertyChanged, ICloneable
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

        private double cena;

        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            if (!Obrisan)
            {
                return $"{Naziv}";
            }
            return null;
        }
        public static DodatneUsluge PronadjiUslugu(int id)
        {
            foreach (var usluga in Projekat.Instance.DodatneUsluge)
            {
                if (usluga.Id == id)
                {
                    return usluga;
                }

            }
            return null;
        }
        public static ObservableCollection<DodatneUsluge> PronadjiUsluge(List<int> id)
        {
            ObservableCollection<DodatneUsluge> lista = new ObservableCollection<DodatneUsluge>();
            if (id != null)
            {
                for (int i = 0; i < id.Count; i++)
                {

                    lista.Add(PronadjiUslugu(id[i]));
                }
                return lista;
            }
            return null;
        }
        public static List<int> PronadjiIdove(ObservableCollection<DodatneUsluge> stavke)
        {
            var lista = new List<int>();
            if (stavke != null)
            {
                for (int i = 0; i < stavke.Count; i++)
                {
                    lista.Add(stavke[i].Id);
                }
                return lista;
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

        public object Clone()
        {
            DodatneUsluge clone = new DodatneUsluge();
            clone.Id = Id;
            clone.Naziv = Naziv;
            clone.Cena = Cena;
            return clone;
        }
    }
}
