using System;
using System.Collections;
using System.Collections.ObjectModel;
using AlgoHero.MusicEntities.Enums;
using System.Collections.Generic;
using System.Windows.Input;

namespace AlgoHero.Player
{
    public class Tecla
    {
        private List<Tono> tonos;

        public Tecla(Key key)
        {
            this.tonos = new List<Tono>();
            this.Key = key;
        }

        public void AgregarTonoAsociado(Tono tono)
        {
            if (this.tonos.Contains(tono))
            {
                throw new InvalidOperationException();
            }
            this.tonos.Add(tono);
        }

        public int CantidadTonos
        {
            get
            {
                return this.tonos.Count;
            }
        }

        public Key Key
        {
            get; private set;
        }

        public Tono ObtenerTono(int index)
        {
            return this.tonos[index];
        }

        public ReadOnlyCollection<Tono> ObtenerTonosAsociados()
        {
            return this.tonos.AsReadOnly();
        }
    }
}
